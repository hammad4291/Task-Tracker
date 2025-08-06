using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UpdateNavigation();
        }
    }

    protected void UpdateNavigation()
    {
        bool isAuthenticated = (Session["UserId"] != null);

        // Show/hide navigation panels
        pnlAuthenticatedLinks.Visible = isAuthenticated;
        pnlUserInfo.Visible = isAuthenticated;
        pnlGuestLinks.Visible = !isAuthenticated;

        // Display username if logged in
        if (isAuthenticated)
        {
            litUsername.Text = Session["Username"]?.ToString();
        }
    }

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        // Clear session
        Session.Clear();
        Session.Abandon();

        // Redirect to login
        Response.Redirect("~/Login.aspx");
    }
}