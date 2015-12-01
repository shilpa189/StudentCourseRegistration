using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StudentRegistration.CourseWizard
{
    public partial class CourseEnrollmentMessage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["returnmessage"].ToString() != null)
            {
                Message.Visible = true;
                lblEnrollmentText.Text = Session["returnmessage"].ToString();
            }
        }
    }
}