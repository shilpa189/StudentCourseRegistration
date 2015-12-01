using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StudentRegistration.Home
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            localhost.WebService1 ws = new localhost.WebService1();
            string email = Session["email"].ToString();
            string userid=ws.getuserid(email);
            string[] student = ws.getStudentInfo(userid);
            string name = student[2] + " " + student[3];
            string studentid = student[1].ToString();
            if (Session["email"] != null)
            {
                SuccessMessage.Visible = true;
                SuccessText.Visible = true;
                SuccessText.Text = "You have successfully logged in.";
                lblStudentName.Text="Welcome " + name;
                lblStudentID.Text = "Student ID: " + studentid;
            }
            
        }
    }
}