<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .dashboard-container {
            max-width: 800px;
            margin: 50px auto;
            padding: 30px;
            text-align: center;
        }
        .welcome-message {
            font-size: 28px;
            margin-bottom: 40px;
            color: #333;
        }
        .stats-container {
            display: flex;
            justify-content: center;
            flex-wrap: wrap;
            gap: 20px;
            margin-bottom: 40px;
        }
        .stat-card {
            width: 200px;
            padding: 25px;
            background: #f8f9fa;
            border-radius: 8px;
            text-align: center;
            box-shadow: 0 3px 10px rgba(0,0,0,0.1);
        }
        .stat-card h3 {
            margin: 0 0 10px 0;
            color: #555;
            font-size: 18px;
        }
        .stat-value {
            font-size: 2.5em;
            font-weight: bold;
            color: #4285f4;
            margin: 0;
        }
        .cta-section {
            margin-top: 40px;
        }
        .btn-cta {
            padding: 12px 30px;
            background-color: #4285f4;
            color: white;
            border: none;
            border-radius: 4px;
            font-size: 16px;
            cursor: pointer;
            text-decoration: none;
            display: inline-block;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dashboard-container">
        <h2 class="welcome-message">Welcome back, <asp:Literal ID="litUsername" runat="server"></asp:Literal>!</h2>
        
        <div class="stats-container">
            <div class="stat-card">
                <h3>Total Tasks</h3>
                <div class="stat-value"><asp:Literal ID="litTotalTasks" runat="server" Text="0"></asp:Literal></div>
            </div>
            <div class="stat-card">
                <h3>Pending</h3>
                <div class="stat-value"><asp:Literal ID="litPendingTasks" runat="server" Text="0"></asp:Literal></div>
            </div>
            <div class="stat-card">
                <h3>In Progress</h3>
                <div class="stat-value"><asp:Literal ID="litInProgressTasks" runat="server" Text="0"></asp:Literal></div>
            </div>
            <div class="stat-card">
                <h3>Completed</h3>
                <div class="stat-value"><asp:Literal ID="litCompletedTasks" runat="server" Text="0"></asp:Literal></div>
            </div>
        </div>
        
        <div class="cta-section">
            <a href="Tasks.aspx" class="btn-cta">Manage Your Tasks</a>
        </div>
    </div>
</asp:Content>