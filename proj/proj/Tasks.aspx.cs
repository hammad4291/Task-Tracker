using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tasks : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
            BindTasks();
        }
    }

    private void BindTasks()
    {
        int userId = (int)Session["UserId"];
        string sortOption = ddlSort.SelectedValue;
        DataAccessLayer dal = new DataAccessLayer();
        DataTable tasks = dal.GetUserTasks(userId, sortOption);
        gvTasks.DataSource = tasks;
        gvTasks.DataBind();
    }

    protected void ddlSort_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindTasks();
    }

    protected void gvTasks_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditTask")
        {
            int taskId = Convert.ToInt32(e.CommandArgument);
            DataAccessLayer dal = new DataAccessLayer();
            DataRow task = dal.GetTaskById(taskId);

            if (task != null)
            {
                string script = string.Format(
                    "openEditModal('{0}', '{1}', '{2}', '{3}');",
                    task["TaskId"],
                    task["Title"].ToString().Replace("'", "\\'"),
                    task["Description"].ToString().Replace("'", "\\'"),
                    task["Status"]
                );
                ScriptManager.RegisterStartupScript(this, this.GetType(), "EditModal", script, true);
            }
        }
        else if (e.CommandName == "DeleteTask")
        {
            int taskId = Convert.ToInt32(e.CommandArgument);
            DataAccessLayer dal = new DataAccessLayer();
            bool success = dal.DeleteTask(taskId);

            if (success)
            {
                BindTasks();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Task deleted successfully.');", true);
            }
        }
        else if (e.CommandName == "ViewDetails")
        {
            int taskId = Convert.ToInt32(e.CommandArgument);
            DataAccessLayer dal = new DataAccessLayer();
            DataRow task = dal.GetTaskById(taskId);

            if (task != null)
            {
                string script = string.Format(
                    "openDetailsModal('{0}', '{1}', '{2}', '{3}');",
                    task["Title"].ToString().Replace("'", "\\'"),
                    task["Status"],
                    Convert.ToDateTime(task["CreatedDate"]).ToString("MM/dd/yyyy hh:mm tt"),
                    task["Description"].ToString().Replace("'", "\\'").Replace("\r\n", "<br />")
                );
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DetailsModal", script, true);
            }
        }
    }

    protected void btnSaveTask_Click(object sender, EventArgs e)
    {
        int taskId = Convert.ToInt32(hdnTaskId.Value);
        string title = txtTaskTitle.Text.Trim();
        string description = txtTaskDescription.Text.Trim();
        string status = ddlStatus.SelectedValue;
        int userId = (int)Session["UserId"];

        DataAccessLayer dal = new DataAccessLayer();
        bool success;

        if (taskId == 0) // New task
        {
            success = dal.CreateTask(userId, title, description, status);
        }
        else // Existing task
        {
            success = dal.UpdateTask(taskId, title, description, status);
        }

        if (success)
        {
            BindTasks();
            string script = @"
                alert('Task saved successfully.');
                document.getElementById('taskModal').style.display='none';
            ";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveSuccess", script, true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error saving task.');", true);
        }
    }
}