using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StudentRegistration.CourseWizard
{
    public partial class CourseDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            localhost.WebService1 ws = new localhost.WebService1();
            string courseID=Session["coursedetail"].ToString();
            string[] courseDetailArray=ws.getCourseDetails(courseID);
            lblCourseID.Text =  lblCourseID.Text+""+ courseID;
            lblCourseName.Text = lblCourseName.Text+""+ courseDetailArray[1].ToString();
            lblCourseSemester.Text = lblCourseSemester.Text + "" + courseDetailArray[3].ToString();
            lblProfessor.Text = lblProfessor.Text + "" + courseDetailArray[4].ToString();

            if(courseDetailArray[7].ToString()=="Online")
            {
                lblTiming.Visible = false;
                lblClassroom.Visible = false;
            }
            else
            {
                lblTiming.Text = lblTiming.Text + "" + courseDetailArray[5].ToString();
                lblClassroom.Text = lblClassroom.Text + "" + courseDetailArray[6].ToString();

            }
            lblCourseType.Text= lblCourseType.Text + "" + courseDetailArray[7].ToString();

            if (Convert.ToBoolean(courseDetailArray[8].ToString()))
            {
                lblPrereqcourseID.Visible = true;
                lblPrereqcourseID.Text = lblPrereqcourseID.Text + "" + courseDetailArray[9].ToString();
            }
            else
            {
                lblPrereqcourseID.Visible = false;
            }
            }
    }
}