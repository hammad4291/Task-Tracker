using System;
using System.Data;
using System.Web.UI;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            Response.Redirect("~/Dashboard.aspx");
        }
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text.Trim();
        string password = txtPassword.Text;
        string confirmPassword = txtConfirmPassword.Text;

        // Validate inputs
        if (string.IsNullOrEmpty(username))
        {
            ShowError("Username is required");
            return;
        }

        if (string.IsNullOrEmpty(password))
        {
            ShowError("Password is required");
            return;
        }

        if (password != confirmPassword)
        {
            ShowError("Passwords do not match");
            return;
        }

        try
        {
            DataAccessLayer dal = new DataAccessLayer();
            bool registrationResult = dal.RegisterUser(username, password);

            if (registrationResult)
            {
                // Registration successful - redirect to login
                Response.Redirect("~/Login.aspx?registration=success");
            }
            else
            {
                ShowError("Username already exists");
            }
        }
        catch (Exception ex)
        {
            ShowError("Registration error: " + ex.Message);
        }
    }

    private void ShowError(string message)
    {
        lblError.Text = message;
        lblError.Visible = true;
    }
}