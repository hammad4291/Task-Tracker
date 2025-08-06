using System;
using System.Data;
using System.Web.UI;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            Response.Redirect("~/Dashboard.aspx");
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text.Trim();
        string password = txtPassword.Text;

        // Validate inputs
        if (string.IsNullOrEmpty(username))
        {
            ShowError("Please enter username");
            return;
        }

        if (string.IsNullOrEmpty(password))
        {
            ShowError("Please enter password");
            return;
        }

        try
        {
            DataAccessLayer dal = new DataAccessLayer();
            DataRow user = dal.AuthenticateUser(username, password);

            if (user != null)
            {
                // Login successful
                Session["UserId"] = user["UserId"];
                Session["Username"] = user["Username"];
                Response.Redirect("~/Dashboard.aspx");
            }
            else
            {
                ShowError("Invalid username or password");
            }
        }
        catch (Exception ex)
        {
            ShowError("Login error: " + ex.Message);
        }
    }

    private void ShowError(string message)
    {
        lblError.Text = message;
        lblError.Visible = true;
    }
}