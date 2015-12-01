using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SRClasses;

namespace StudentRegistration.CourseWizard
{
    public partial class DisplayCourseList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scm = ScriptManager.GetCurrent(this.Page);
            scm.RegisterAsyncPostBackControl(SemesterRadioButton);

            if(Session["email"]!=null)
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
                localhost.WebService1 ws = new localhost.WebService1();
                
                string semester = SemesterRadioButton.SelectedItem.Value;
                //localhopstList<Course> cl = (ws.getCourseList(semester));
                localhost.Course[] cllist = ws.getCourseList(semester);
                foreach (localhost.Course c in cllist)
                {
                    TableRow tr = new TableRow();
                    CourseListTable.Rows.Add(tr);

                    //To fetch the courseid
                    TableCell tc1 = new TableCell();
                    //HyperLink hl = new HyperLink();
                    //hl.ID = c.courseID.ToString();
                    //hl.NavigateUrl = "~/CourseWizard/CourseDetails";
                    //hl.Text = c.courseID.ToString();
                    //Session["coursedetail"] = hl.ID.ToString();
                    LinkButton lb = new LinkButton();
                    lb.Text = c.courseID.ToString();
                    lb.ID = c.courseID.ToString();
                    lb.Click += new System.EventHandler(lb_Onclick);
                    tc1.Controls.Add(lb);
                    //tc1.Text = c.courseID.ToString();
                    tr.Cells.Add(tc1);


                    //To fetch the course name
                    TableCell tc2 = new TableCell();
                    tc2.Text = c.courseName.ToString();
                    tr.Cells.Add(tc2);

                }
            }

            else
            {
                ErrorMessageCourseList.Visible = true;
                ErrorTextCourseList.Visible = true;
                ErrorTextCourseList.Text = "You are not a logged in user. Please log in.";
            }

        }

        protected void lb_Onclick(object sender, EventArgs e)
        {
            LinkButton lbrcvd=sender as LinkButton;
            Session["coursedetail"] = lbrcvd.Text;
            Response.Redirect("~/CourseWizard/CourseDetails");
        }
    }
}