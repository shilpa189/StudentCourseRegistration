using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SRClasses;

namespace StudentRegistrationWebServices
{

    [WebService(Namespace = "http://microsoft.com/webservices/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]

    public class WebService1 : System.Web.Services.WebService
    {
        /// <summary>
        /// This web method checks whether the user credentials are valid.
        /// </summary>
        /// <param name="emailIn"></param>
        /// <returns>password from the db</returns>
        [WebMethod]
        public string validateUserAccountWS(string emailIn)
        {
            string pass = SRClasses.DB.validateuserdb(emailIn);
            if (pass != null)
            {
                return pass;
            }
            else return "Invalid User Credentials. Please try again!";
        }

        /// <summary>
        /// This web method gets a list of all courses available for a particular semester
        /// </summary>
        /// <param name="semIn"></param>
        /// <returns></returns>
        [WebMethod]
        public List<Course> getCourseList(string semIn)
        {
            List<Course> cl = DB.getCourseListdb(semIn);
            return cl;
        }

        /// <summary>
        /// This web method gets the detailed information about a particular course based on couseid
        /// </summary>
        /// <param name="courseID"></param>
        /// <returns></returns>
        [WebMethod]
        public string[] getCourseDetails(string courseID)
        {
            string[] courseDetail = DB.getCourseDetailsdb(courseID);
            return courseDetail;
        }

        /// <summary>
        /// This web method get detailed information about a particular student
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [WebMethod]
        public string[] getStudentInfo(string userid)
        {
            string[] studentinfo = DB.getStudentInfodb(userid);
            return studentinfo;
        }

        /// <summary>
        /// This web method returns the userid of the student, given the emailid
        /// </summary>
        /// <param name="emailIn"></param>
        /// <returns></returns>
        [WebMethod]
        public string getuserid(string emailIn)
        {
            string userid = SRClasses.DB.getuseriddb(emailIn);
            if (userid != null)
            {
                return userid;
            }
            else return "Invalid User Credentials. Please try again!";
        }


        
        /// <summary>
        /// This web method enrolls the student for a particular couse based on certain business rules.
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="courseid"></param>
        /// <param name="semester"></param>
        /// <returns></returns>
        [WebMethod]
        public string addcourse(string userid, string courseid, string semester)
        {
            string returnmessage = "";
            string[] studentinfo = DB.getStudentInfodb(userid);
            string studentid = studentinfo[1].ToString();
            List<Course> enrolledCourseList = DB.getEnrolledCourseList(semester, studentid);

            bool duplicateflag = duplicatecourse(userid, courseid, semester);
            bool conflictimingflag = conflictintimings(userid, courseid, semester);

            if (duplicateflag && (enrolledCourseList.Count>0) )
            {
                returnmessage = "Sorry! This course already exists in your enrollment list. ";
            }
            else if(conflictimingflag && (enrolledCourseList.Count>0))
            {
                returnmessage = "Sorry! This timings of this course conflict with the timings of an already enrolled course. Please choose some other course. ";

            }

            else
            {

                string studenttype = "";
                int mincourses = 0;
                int maxcourses = 0;
                string prereqcourseid = "";
                bool meetscriteria = false;
                //this variable stores the information if the student is a full-timne or a part-time student
                string enrollmenttype = studentinfo[4].ToString();
                bool internationstudent = Convert.ToBoolean(studentinfo[9]);
                string[] courseDetail = DB.getCourseDetailsdb(courseid);
                //To fetch if registrating this course requires a prerequisite
                bool isPreReq = Convert.ToBoolean(courseDetail[8]);
                //if it does, then fetching the pre-requisite courseid
                if (isPreReq)
                {
                    prereqcourseid = courseDetail[9];
                }

                //All  student are assigned a flag=1 if they are international students
                if (internationstudent == true)
                {
                    studenttype = "International";
                }
                //All the domestic students are assigned a flag=0
                else
                {
                    studenttype = "Domestic";
                }

                //Some important Business Rules:
                //The student is allowed to enroll for a course only if he/she has previously enrolled in the pre-requsite course
                //International Full time students: All such students need to participate in atleast 3 courses each semester upto a maximum of  6 courses
                //International Part time students: All such students need to participate in atleast 3 courses each semester upto a maximum of  4 courses
                //Domestic Full time-All such students need to participate in a maximum of  6 courses per semester
                //Domestic Part time-All such students need to participate in a maximum of  4 courses per semester

                //seting the min and max courses a student is allowed to participate in based on enrollment type and student type
                //if loop for all international students
                if (studenttype.Equals("International", StringComparison.InvariantCultureIgnoreCase))
                {
                    //for all Full time International Students
                    if (enrollmenttype.Equals("Full-time", StringComparison.InvariantCultureIgnoreCase))
                    {
                        mincourses = 6;
                        maxcourses = 12;
                    }

                    //for all Part time International students
                    if (enrollmenttype.Equals("Part-time", StringComparison.InvariantCultureIgnoreCase))
                    {
                        mincourses = 6;
                        maxcourses = 8;
                    }

                }

                //if loop for all domestic students
                if (studenttype.Equals("Domestic", StringComparison.InvariantCultureIgnoreCase))
                {
                    //for all Full time Domestic Students
                    if (enrollmenttype.Equals("Full-time", StringComparison.InvariantCultureIgnoreCase))
                    {
                        maxcourses = 12;
                    }

                    //for all Part time Domestic students
                    if (enrollmenttype.Equals("Part-time", StringComparison.InvariantCultureIgnoreCase))
                    {
                        maxcourses = 8;
                    }

                }

                //this method call gives the total no. of courses the student is enrolled in
                int totalCountOfCourses = DB.getcountofcourses(studentid);
             
                if (totalCountOfCourses < maxcourses)
                {

                    //When there is a required prerequisite, here we need to verify if the student is already enrolled in the prerequired coursework
                    if (isPreReq && prereqcourseid != null)
                    {
                        if (enrolledCourseList.Count > 0)
                        {
                            foreach (Course c in enrolledCourseList)
                            {
                                if (prereqcourseid.Equals(c.courseID, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    meetscriteria = true;
                                    break;
                                }
                                else
                                    meetscriteria = false;
                            }

                            if (meetscriteria)
                            {
                                DB.addcourse(courseid, semester, studentid);
                                returnmessage = "Congratulations! You have successfully been enrolled for " + courseid;

                            }
                            else
                            {
                                returnmessage = "Sorry !! You do not meet the pre-requisite eligibility criteria. You cannot be registered for " + courseid + ". Please consult your program advisor.";
                            }

                        }
                        else
                        {
                            returnmessage = "Sorry !! You do not meet the pre-requisite eligibility criteria. You cannot be registered for " + courseid + ". Please consult your program advisor.";

                        }
                    }

                    //When the student is trying to enroll in a course which doesnt have a required course work associated with it
                    if (isPreReq == false)
                    {
                        DB.addcourse(courseid, semester, studentid);
                        returnmessage = "Congratulations! You have successfully been enrolled for " + courseid;

                    }

                }
                else
                {
                    returnmessage = "Sorry !! You do not meet the overall course registration eligibility criteria. You cannot be registered for " + courseid + ". Please consult your program advisor.";
                }

            }
            return returnmessage;

        }

        /// <summary>
        /// This method displays the list of all the courses enrolled by the student for a particular semester
        /// </summary>
        /// <param name="semIn"></param>
        /// <param name="studentIn"></param>
        /// <returns></returns>
        [WebMethod]
        public List<Course> getEnrolledCourses(string semIn,string studentidIn)
        {
            List<Course> cl = DB.getEnrolledCourseList(semIn,studentidIn);
            return cl;
        }

        /// <summary>
        /// This method drops a course from the students enrollment list
        /// </summary>
        /// <param name="courseid"></param>
        /// <param name="semester"></param>
        /// <param name="studentid"></param>
        /// <returns></returns>
        [WebMethod]
        public string dropCourse(string courseid,string semester,string studentid)
        {
            DB.dropCoursedb(courseid, semester, studentid);
            string message = courseid + " has been successfully dropped.";
            return message;
        }

        /// <summary>
        /// This method fetches the course history (Enrolled as well as Dropped courses) for a student 
        /// </summary>
        /// <param name="semIn"></param>
        /// <param name="studentidIn"></param>
        /// <returns></returns>
        [WebMethod]
        public List<Course> getCourseHistory(string semIn, string studentidIn)
        {
            List<Course> cl = DB.getCourseHistorydb(semIn, studentidIn);
            return cl;
        }

        //This method is called in the beginning of the addcourse Webmethod.
        //This method verifies if the user is not trying to enter a duplicate entry in his enrollement courselist
        public bool duplicatecourse(string userid, string courseid, string semester)
        {
            string[] studentinfo = DB.getStudentInfodb(userid);
            string studentid = studentinfo[1].ToString();
            List<Course> enrolledCourseList = DB.getEnrolledCourseList(semester, studentid);
            bool flag = true;
            if (enrolledCourseList.Count > 0)
            {
                while (flag)
                {
                    foreach (Course c in enrolledCourseList)
                    {
                        if (!courseid.Equals(c.courseID, StringComparison.InvariantCultureIgnoreCase))
                        {
                            flag = false;

                        }
                        else
                        {
                            flag = true;
                            break;
                        }
                    }

                    break;
                }
            }

            return flag;
        }


        //This method is called in the beginning of the addcourse Webmethod, immediately after duplicatecourse() method call.
        //This method verifies if the user is not trying to enroll in a course whose timing conflicts with the timing of an already enrolled course.
        public bool conflictintimings(string userid, string courseid, string semester)
        {
            string[] studentinfo = DB.getStudentInfodb(userid);
            string studentid = studentinfo[1].ToString();
            string[] courseDetail = DB.getCourseDetailsdb(courseid);
            string schedule = courseDetail[5].ToString();
            List<Course> enrolledCourseList = DB.getEnrolledCourseList(semester, studentid);
            bool flag = true;
            if (enrolledCourseList.Count > 0)
            {
                while (flag)
                {
                    foreach (Course c in enrolledCourseList)
                    {
                        if (c.schedule != "Online")
                        {
                            if (!schedule.Equals(c.schedule, StringComparison.InvariantCultureIgnoreCase))
                            {
                                flag = false;

                            }
                            else
                            {
                                flag = true;
                                break;
                            }
                        }
                    }

                    break;
                }
            }

            return flag;
        }

    }
}
