using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StudentRegistration.CourseWizard
{
    public partial class ClassSchedule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scm = ScriptManager.GetCurrent(this.Page);
            scm.RegisterAsyncPostBackControl(SemesterRadioButton);

            if (Session["email"] != null)
            {
                SemesterRadioButton.Visible = true;
                SiteContentClassSchedule.Visible = true;
            }
            else
            {
                ErrorMessageClassSchedule.Visible = true;
                ErrorMessageClassSchedule.Visible = true;
                ErrorTextClassSchedule.Text = "You are not a logged in user. Please log in.";
                SiteContentClassSchedule.Visible = false;

            }


        }

        protected void SemesterRadioButton_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["email"] != null)
            {
                ClassScheduleTable.Visible = true;
                localhost.WebService1 ws = new localhost.WebService1();

                string semester = SemesterRadioButton.SelectedItem.Value;
                string email = Session["email"].ToString();
                string userid= ws.getuserid(email);
                string[] studentinfo = ws.getStudentInfo(userid);
                string studentid = studentinfo[1].ToString();
                //localhopstList<Course> cl = (ws.getCourseList(semester));
                localhost.Course[] cllist = ws.getEnrolledCourses(semester,studentid);

                if (cllist.Length != 0)
                {

                    foreach (localhost.Course c in cllist)
                    {
                        TableRow tr = new TableRow();
                        ClassScheduleTable.Rows.Add(tr);

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
                    NotRegisteredlbl.Visible = true;
                    NotRegisteredlbl.Text = "You do not have any classes registered.";
                    ClassScheduleTable.Visible = false;
                }
            }

            else
            {
                ErrorMessageClassSchedule.Visible = true;
                ErrorTextClassSchedule.Visible = true;
                ErrorTextClassSchedule.Text = "You are not a logged in user. Please log in.";
            }

        }
    }
}