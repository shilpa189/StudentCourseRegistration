using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using StudentRegistration.Models;

namespace StudentRegistration.Account
{
    public partial class Logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["email"] != null)
            {
                MessageLogout.Visible = true;
                TextLogout.Visible = true;
                TextLogout.Text = "You have successfully logged out. Thank you for using our system.";
                Session["email"] = null;
            }

           else
            {
                Response.Redirect("~/Default");
            }


        }
    }
}