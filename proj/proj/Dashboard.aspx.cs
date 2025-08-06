using System;
using System.Data;
using System.Web.UI;

public partial class Dashboard : System.Web.UI.Page
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

            // Display username
            litUsername.Text = Session["Username"].ToString();

            // Load dashboard statistics
            LoadDashboardStatistics();
        }
    }

    private void LoadDashboardStatistics()
    {
        int userId = (int)Session["UserId"];
        DataAccessLayer dal = new DataAccessLayer();

        // Get task statistics
        DataTable stats = dal.GetTaskStatistics(userId);
        if (stats.Rows.Count > 0)
        {
            litTotalTasks.Text = stats.Rows[0]["TotalTasks"].ToString();
            litPendingTasks.Text = stats.Rows[0]["PendingTasks"].ToString();
            litInProgressTasks.Text = stats.Rows[0]["InProgressTasks"].ToString();
            litCompletedTasks.Text = stats.Rows[0]["CompletedTasks"].ToString();
        }
    }
}