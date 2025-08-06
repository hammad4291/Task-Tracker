<%@ Page Title="Register" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .register-container {
            max-width: 400px;
            margin: 50px auto;
            padding: 30px;
            border: 1px solid #e0e0e0;
            border-radius: 5px;
            background-color: #ffffff;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
        }
        .register-title {
            text-align: center;
            margin-bottom: 25px;
            color: #333;
            font-size: 24px;
        }
        .form-group {
            margin-bottom: 20px;
        }
        .form-group label {
            display: block;
            margin-bottom: 8px;
            font-weight: 600;
            color: #555;
        }
        .form-control {
            width: 100%;
            padding: 10px;
            border: 1px solid #ddd;
            border-radius: 4px;
            font-size: 14px;
            box-sizing: border-box;
        }
        .btn-register {
            width: 100%;
            padding: 12px;
            background-color: #4CAF50;
            color: white;
            border: none;
            border-radius: 4px;
            font-size: 16px;
            cursor: pointer;
            transition: background-color 0.3s;
        }
        .btn-register:hover {
            background-color: #45a049;
        }
        .error-message {
            color: #d32f2f;
            margin-top: 15px;
            text-align: center;
            font-size: 14px;
        }
        .login-link {
            text-align: center;
            margin-top: 20px;
            font-size: 14px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="register-container">
        <h2 class="register-title">Create Account</h2>
        
        <div class="form-group">
            <asp:Label ID="lblUsername" runat="server" Text="Username" AssociatedControlID="txtUsername"></asp:Label>
            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Choose a username"></asp:TextBox>
        </div>
        
        <div class="form-group">
            <asp:Label ID="lblPassword" runat="server" Text="Password" AssociatedControlID="txtPassword"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" 
                placeholder="Create password"></asp:TextBox>
        </div>
        
        <div class="form-group">
            <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password" AssociatedControlID="txtConfirmPassword"></asp:Label>
            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" 
                placeholder="Retype password"></asp:TextBox>
        </div>
        
        <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn-register" OnClick="btnRegister_Click" />
        
        <asp:Label ID="lblError" runat="server" CssClass="error-message" Visible="false"></asp:Label>
        
        <div class="login-link">
            Already have an account? <a href="Login.aspx">Login here</a>
        </div>
    </div>
</asp:Content>