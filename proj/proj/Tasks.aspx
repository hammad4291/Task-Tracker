<%@ Page Title="Tasks" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="Tasks.aspx.cs" Inherits="Tasks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .tasks-container {
            max-width: 1200px;
            margin: 30px auto;
            padding: 20px;
        }
        .header-section {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 20px;
        }
        .btn-create {
            padding: 10px 20px;
            background-color: #4CAF50;
            color: white;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }
        .task-grid {
            width: 100%;
            border-collapse: collapse;
        }
        .task-grid th, .task-grid td {
            padding: 12px;
            border: 1px solid #ddd;
            text-align: left;
        }
        .task-grid th {
            background-color: #f2f2f2;
        }
        .action-button {
            padding: 5px 10px;
            margin-right: 5px;
            border: none;
            border-radius: 3px;
            cursor: pointer;
        }
        .btn-edit {
            background-color: #2196F3;
            color: white;
        }
        .btn-delete {
            background-color: #f44336;
            color: white;
        }
        .btn-details {
            background-color: #ff9800;
            color: white;
        }
        .sort-container {
            margin-bottom: 15px;
            display: flex;
            align-items: center;
        }
        .sort-container label {
            margin-right: 10px;
            font-weight: bold;
        }
        .sort-dropdown {
            padding: 8px;
            border-radius: 4px;
            border: 1px solid #ddd;
        }
        .modal {
            display: none;
            position: fixed;
            z-index: 1;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            overflow: auto;
            background-color: rgba(0,0,0,0.4);
        }
        .modal-content {
            background-color: #fefefe;
            margin: 10% auto;
            padding: 20px;
            border: 1px solid #888;
            width: 50%;
            border-radius: 5px;
        }
        .close {
            color: #aaa;
            float: right;
            font-size: 28px;
            font-weight: bold;
            cursor: pointer;
        }
        .form-group {
            margin-bottom: 15px;
        }
        .form-group label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
        }
        .form-control {
            width: 100%;
            padding: 8px;
            border: 1px solid #ddd;
            border-radius: 4px;
            box-sizing: border-box;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="tasks-container">
                <div class="header-section">
                    <h2>Your Tasks</h2>
                    <asp:Button ID="btnOpenCreate" runat="server" Text="Create New Task" CssClass="btn-create" OnClientClick="openCreateModal(); return false;" />
                </div>
                
                <div class="sort-container">
                    <label for="ddlSort">Sort by:</label>
                    <asp:DropDownList ID="ddlSort" runat="server" CssClass="sort-dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlSort_SelectedIndexChanged">
                        <asp:ListItem Text="Most Recent First" Value="RecentFirst" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Oldest First" Value="OldestFirst"></asp:ListItem>
                        <asp:ListItem Text="By Status (Pending First)" Value="StatusPendingFirst"></asp:ListItem>
                        <asp:ListItem Text="By Status (Completed First)" Value="StatusCompletedFirst"></asp:ListItem>
                        <asp:ListItem Text="By Title (A-Z)" Value="TitleAsc"></asp:ListItem>
                        <asp:ListItem Text="By Title (Z-A)" Value="TitleDesc"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                
                <asp:GridView ID="gvTasks" runat="server" CssClass="task-grid" AutoGenerateColumns="false"
                    OnRowCommand="gvTasks_RowCommand" DataKeyNames="TaskId">
                    <Columns>
                        <asp:BoundField DataField="Title" HeaderText="Title" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:MM/dd/yyyy hh:mm tt}" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="action-button btn-edit" 
                                    CommandName="EditTask" CommandArgument='<%# Eval("TaskId") %>' />
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="action-button btn-delete" 
                                    CommandName="DeleteTask" CommandArgument='<%# Eval("TaskId") %>'
                                    OnClientClick="return confirm('Are you sure you want to delete this task?');" />
                                <asp:Button ID="btnDetails" runat="server" Text="Details" CssClass="action-button btn-details" 
                                    CommandName="ViewDetails" CommandArgument='<%# Eval("TaskId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        No tasks found. Click "Create New Task" to get started.
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>

            <!-- Create/Edit Task Modal -->
            <div id="taskModal" class="modal">
                <div class="modal-content">
                    <span class="close" onclick="closeModal()">&times;</span>
                    <h3 id="modalTitle">Create New Task</h3>
                    <asp:HiddenField ID="hdnTaskId" runat="server" Value="0" />
                    <div class="form-group">
                        <label for="txtTaskTitle">Title</label>
                        <asp:TextBox ID="txtTaskTitle" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="ddlStatus">Status</label>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Pending" Value="Pending" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="In Progress" Value="In Progress"></asp:ListItem>
                            <asp:ListItem Text="Completed" Value="Completed"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="txtTaskDescription">Description</label>
                        <asp:TextBox ID="txtTaskDescription" runat="server" TextMode="MultiLine" 
                            Rows="4" CssClass="form-control"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnSaveTask" runat="server" Text="Save Task" CssClass="btn-create" 
                        OnClick="btnSaveTask_Click" OnClientClick="return validateForm();" />
                </div>
            </div>

            <!-- Task Details Modal -->
            <div id="detailsModal" class="modal">
                <div class="modal-content">
                    <span class="close" onclick="closeModal()">&times;</span>
                    <h3>Task Details</h3>
                    <div class="form-group">
                        <label>Title:</label>
                        <p id="detailTitle"></p>
                    </div>
                    <div class="form-group">
                        <label>Status:</label>
                        <p id="detailStatus"></p>
                    </div>
                    <div class="form-group">
                        <label>Created Date:</label>
                        <p id="detailCreatedDate"></p>
                    </div>
                    <div class="form-group">
                        <label>Description:</label>
                        <p id="detailDescription"></p>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function openCreateModal() {
            document.getElementById('<%= hdnTaskId.ClientID %>').value = '0';
            document.getElementById('<%= txtTaskTitle.ClientID %>').value = '';
            document.getElementById('<%= txtTaskDescription.ClientID %>').value = '';
            document.getElementById('<%= ddlStatus.ClientID %>').value = 'Pending';
            document.getElementById('modalTitle').innerText = 'Create New Task';
            document.getElementById('taskModal').style.display = 'block';
        }

        function openEditModal(taskId, title, description, status) {
            document.getElementById('<%= hdnTaskId.ClientID %>').value = taskId;
            document.getElementById('<%= txtTaskTitle.ClientID %>').value = title;
            document.getElementById('<%= txtTaskDescription.ClientID %>').value = description;
            document.getElementById('<%= ddlStatus.ClientID %>').value = status;
            document.getElementById('modalTitle').innerText = 'Edit Task';
            document.getElementById('taskModal').style.display = 'block';
        }

        function openDetailsModal(title, status, createdDate, description) {
            document.getElementById('detailTitle').innerText = title;
            document.getElementById('detailStatus').innerText = status;
            document.getElementById('detailCreatedDate').innerText = createdDate;
            document.getElementById('detailDescription').innerText = description;
            document.getElementById('detailsModal').style.display = 'block';
        }

        function closeModal() {
            document.getElementById('taskModal').style.display = 'none';
            document.getElementById('detailsModal').style.display = 'none';
        }

        window.onclick = function(event) {
            if (event.target == document.getElementById('taskModal')) {
                document.getElementById('taskModal').style.display = 'none';
            }
            if (event.target == document.getElementById('detailsModal')) {
                document.getElementById('detailsModal').style.display = 'none';
            }
        }

        function validateForm() {
            var title = document.getElementById('<%= txtTaskTitle.ClientID %>').value.trim();
            if (title === '') {
                alert('Please enter a title for the task.');
                return false;
            }
            return true;
        }
    </script>
</asp:Content>