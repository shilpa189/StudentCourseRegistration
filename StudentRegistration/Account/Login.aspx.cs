using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using StudentRegistration.Models;

namespace StudentRegistration.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            Email.Focus();
            
        }

        protected void LogIn(object sender, EventArgs e)
        {
            localhost.WebService1 ws = new localhost.WebService1();
            if (IsValid)
            {
                // Validate the user password
                string email = Email.Text;
                string pass = Password.Text;
                string returnPass = ws.validateUserAccountWS(email);
                if (pass.Equals(returnPass))
                {
                    Session["email"] = Email.Text;
                    Response.Redirect("~/Account/LoginSuccess");
                }
                else
                {
                    ErrorMessage.Visible = true;
                    FailureText.Visible = true;
                    FailureText.Text = "Invalid Credentials. Please try again!";
                }

            }
        }
    }
}