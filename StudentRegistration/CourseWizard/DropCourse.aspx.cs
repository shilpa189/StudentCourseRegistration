using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StudentRegistration.CourseWizard
{
    public partial class CourseAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scm = ScriptManager.GetCurrent(this.Page);
            scm.RegisterAsyncPostBackControl(SemesterRadioButton);

            if (Session["email"] != null)
            {
                SemesterRadioButton.Visible = true;
                SiteContentDisplayCourseList.Visible = true;
            }
            else
            {
                ErrorMessageCourseList.Visible = true;
                ErrorTextCourseList.Visible = true;
                ErrorTextCourseList.Text = "You are not a logged in user. Please log in.";
                SiteContentDisplayCourseList.Visible = false;

            }


        }

        protected void SemesterRadioButton_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["email"] != null)
            {
                CourseListTable.Visible = true;
                btnAddCourse.Visible = true;
                txtCourseSelection.Visible = true;
                lblCourseSelection.Visible = true;
                localhost.WebService1 ws = new localhost.WebService1();

                string semester = SemesterRadioButton.SelectedItem.Value;
                string email = Session["email"].ToString();
                string userid = ws.getuserid(email);
                string[] studentinfo = ws.getStudentInfo(userid);
                string studentid = studentinfo[1].ToString();

                localhost.Course[] cllist = ws.getEnrolledCourses(semester, studentid);

                if (cllist.Length != 0)
                {

                    foreach (localhost.Course c in cllist)
                    {
                        TableRow tr = new TableRow();
                        CourseListTable.Rows.Add(tr);

                        //To fetch the courseid
                        TableCell tc1 = new TableCell();
                        tc1.Text = c.courseID.ToString();
                        tr.Cells.Add(tc1);

                        //To fetch the course name
                        TableCell tc2 = new TableCell();
                        tc2.Text = c.courseName.ToString();
                        tr.Cells.Add(tc2);

                        //To fetch Professor
                        TableCell tc3 = new TableCell();
                        tc3.Text = c.prof.ToString();
                        tr.Cells.Add(tc3);

                        //To fetch Classroom Details
                        TableCell tc4 = new TableCell();
                        tc4.Text = c.classroom.ToString();
                        tr.Cells.Add(tc4);

                        //To fetch Timings
                        TableCell tc5 = new TableCell();
                        tc5.Text = c.schedule.ToString();
                        tr.Cells.Add(tc5);

                    }
                }


                else
                {
                    ErrorMessageCourseList.Visible = true;
                    ErrorTextCourseList.Visible = true;
                    ErrorTextCourseList.Text = "You are not a logged in user. Please log in.";
                }

            }
        }

        protected void btnAddCourse_Click(object sender, EventArgs e)
        {
            string semester = SemesterRadioButton.SelectedItem.Value;
            if (semester == "Spring 2016")
            {
                localhost.WebService1 ws = new localhost.WebService1();
                string userid = ws.getuserid(Session["email"].ToString());
                string email = Session["email"].ToString();
                string[] studentinfo = ws.getStudentInfo(userid);
                string studentid = studentinfo[1].ToString();

                bool flag = false;
                if (txtCourseSelection.Text != null)
                {
                    localhost.Course[] cllist = ws.getCourseList(semester);

                    foreach (localhost.Course c in cllist)
                    {
                        if (c.courseID == txtCourseSelection.Text)
                        {
                            flag = true;
                            break;
                        }
                    }

                    if (flag)
                    {
                        Session["courseid"] = txtCourseSelection.Text;
                        Session["returnmessage"] = ws.dropCourse(txtCourseSelection.Text, semester, studentid );

                    }
                    else
                    {
                        Session["returnmessage"] = "You have entered an incorrect courseid. Please try again.";
                    }
                }
            }
            else
            {
                Session["returnmessage"] = "You are not allowed to drop courses for Fall 2015 at this time.";
            }
            Response.Redirect("~/CourseWizard/CourseEnrollmentMessage");

        }
    }
}
   
 