using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace SRClasses
{
    public class DB
    {
        static string connectionstring = "Server=tcp:icnncvnhw4.database.windows.net,1433;Database=umassboston;User ID=shilpa189@icnncvnhw4;Password=Sbhambhw@189;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

        //this method verifies the user credentials from the databse 
        public static string validateuserdb(string email)
        {
            string queryString = "SELECT password from dbo.useraccounts WHERE email=@email";
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@email", email);
            string pass = "";
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    pass = reader[0].ToString();
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return pass;
        }

        //this method fetches the details of a course based on the courseID
        public static string[] getCourseDetailsdb(string courseID)
        {
            string[] courseDetail = new string[10];
            string queryString = "SELECT name,descp,semester, professor,schedule, classroom,type, prereq,prereqcourseid from dbo.course WHERE courseid=@courseid";
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand command = new SqlCommand(queryString, connection);
           try
            { 
                connection.Open();
                command.Parameters.AddWithValue("@courseid", courseID);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    courseDetail[0] = courseID;
                    courseDetail[1] = reader["name"].ToString();
                    courseDetail[2] = reader["descp"].ToString();
                    courseDetail[3] = reader["semester"].ToString();
                    courseDetail[4] = reader["professor"].ToString();
                    courseDetail[5] = reader["schedule"].ToString();
                    courseDetail[6] = reader["classroom"].ToString();
                    courseDetail[7] = reader["type"].ToString();
                    courseDetail[8] = reader["prereq"].ToString();
                    courseDetail[9] = reader["prereqcourseid"].ToString();

                }

                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return courseDetail;
        }



        //this method makes a call to the database to fetch a list of all the couses available based on the semester
        public static System.Collections.Generic.List<Course> getCourseListdb(string sem)
        {
            System.Collections.Generic.List<Course> courselist = new List<Course>();
            System.Data.SqlClient.SqlCommand cmd;
            SqlDataReader rdr;
            SqlConnection cn = new SqlConnection(connectionstring);
            try
            {
                
                cmd = new SqlCommand("spgetCourseList", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@semester",sem);
                cn.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (rdr["courseid"] != DBNull.Value)
                    {
                        Course clitem = new Course();
                        clitem.courseID= rdr["courseid"].ToString();
                        clitem.courseName = rdr["name"].ToString();
                        courselist.Add(clitem);
                    }
                }
                cn.Close();
            }
            catch (Exception e) { string msg = e.Message; }
            return courselist;
        }


        //this method fetches the details for a particular student based on his/her userid
        public static string[] getStudentInfodb(string userid)
        {
            string[] studentinfo = new string[10];
            string queryString = "SELECT userid,studentid,firstname,lastname,enrollementtype,studenttype,personalemail,phone,primaryaddress,internationalstudent from dbo.userinfo WHERE userid=@userid";
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand command = new SqlCommand(queryString, connection);
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@userid", userid);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    studentinfo[0] = userid;
                    studentinfo[1] = reader["studentid"].ToString();
                    studentinfo[2] = reader["firstname"].ToString();
                    studentinfo[3] = reader["lastname"].ToString();
                    studentinfo[4] = reader["enrollementtype"].ToString();
                    studentinfo[5] = reader["studenttype"].ToString();
                    studentinfo[6] = reader["personalemail"].ToString();
                    studentinfo[7] =reader["phone"].ToString();
                    studentinfo[8] =reader["primaryaddress"].ToString();
                    studentinfo[9] = reader["internationalstudent"].ToString();
                }

                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return studentinfo;
        }

        //this method gets the userid of a student based on the email 
        public static string getuseriddb(string email)
        {
            string queryString = "SELECT userid from dbo.useraccounts WHERE email=@email";
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@email", email);
            string userid = "";
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    userid = reader[0].ToString();
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return userid;
        }




        //this method gives the count of total no. of courses a student is enrolled in
        public static int getcountofcourses(string studentid)
        {
            string queryString = "SELECT count(*) from dbo.studentenrollement WHERE studentid=@studentid and status=@status";
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@studentid", studentid);
            command.Parameters.AddWithValue("@status", "Enrolled");
            int count = 0;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    count = Convert.ToInt32(reader[0].ToString());
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return count;

        }

        //this method allows a student to enroll/add a course
        public static void addcourse(string courseid, string semester, string studentid)
        {
            System.Data.SqlClient.SqlCommand cmd;
            SqlConnection cn = new SqlConnection(connectionstring);
            string enrollmentid = studentid + "_" + courseid;

            try
            {
                cn.Open();
                cmd = new SqlCommand("spaddCourse", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@enrollmentid", enrollmentid.ToString());
                cmd.Parameters.AddWithValue("@studentid", studentid.ToString());
                cmd.Parameters.AddWithValue("@courseid", courseid.ToString());
                cmd.Parameters.AddWithValue("@semester", semester.ToString());
                cmd.Parameters.AddWithValue("@status", "Enrolled");
                cmd.ExecuteNonQuery();
                cn.Close();
                
            }
            catch (Exception e) 
            {
                string msg = e.Message;
            }
          
        }

        //this method diaplays a list of enrolled courses for a particular student
        public static System.Collections.Generic.List<Course> getEnrolledCourseList(string sem,string studentid)
        {
            System.Collections.Generic.List<Course> courselist = new List<Course>();
            System.Data.SqlClient.SqlCommand cmd;
            SqlDataReader rdr;
            SqlConnection cn = new SqlConnection(connectionstring);
            try
            {
                cn.Open();
                cmd = new SqlCommand("spgetEnrolledCourseList", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@semester", sem);
                cmd.Parameters.AddWithValue("@studentid", studentid);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (rdr["courseid"] != DBNull.Value)
                    {
                        Course clitem = new Course();
                        clitem.courseID = rdr["courseid"].ToString();
                        clitem.courseName = rdr["name"].ToString();
                        clitem.prof = rdr["professor"].ToString();
                        clitem.classroom = rdr["classroom"].ToString();
                        clitem.schedule = rdr["schedule"].ToString();
                        courselist.Add(clitem);
                    }
                }
                cn.Close();
            }
            catch (Exception e) { string msg = e.Message; }
            return courselist;
        }


        //this method diaplays a list of all the courses (Enrolled and Dropped) for a particular student
        public static System.Collections.Generic.List<Course> getCourseHistorydb(string sem, string studentid)
        {
            System.Collections.Generic.List<Course> courselist = new List<Course>();
            System.Data.SqlClient.SqlCommand cmd;
            SqlDataReader rdr;
            SqlConnection cn = new SqlConnection(connectionstring);
            try
            {
                cn.Open();
                cmd = new SqlCommand("spgetCourseHistory", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@semester", sem);
                cmd.Parameters.AddWithValue("@studentid", studentid);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (rdr["courseid"] != DBNull.Value)
                    {
                        Course clitem = new Course();
                        clitem.courseID = rdr["courseid"].ToString();
                        clitem.courseName = rdr["name"].ToString();
                        clitem.prof = rdr["professor"].ToString();
                        clitem.classroom = rdr["classroom"].ToString();
                        clitem.schedule = rdr["schedule"].ToString();
                        clitem.status = rdr["status"].ToString();
                        courselist.Add(clitem);
                    }
                }
                cn.Close();
            }
            catch (Exception e) { string msg = e.Message; }
            return courselist;
        }




        //this method allows a student to drop a course
        public static void dropCoursedb(string courseid, string semester, string studentid)
        {
            System.Data.SqlClient.SqlCommand cmd;
            SqlConnection cn = new SqlConnection(connectionstring);
          
            try
            {
                cn.Open();
                cmd = new SqlCommand("spdropCourse", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@studentid", studentid.ToString());
                cmd.Parameters.AddWithValue("@courseid", courseid.ToString());
                cmd.Parameters.AddWithValue("@semester", semester.ToString());
                cmd.ExecuteNonQuery();
                cn.Close();

            }
            catch (Exception e)
            {
                string msg = e.Message;
            }

        }








    }
}