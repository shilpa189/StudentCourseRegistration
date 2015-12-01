<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CourseDetails.aspx.cs" Inherits="StudentRegistration.CourseWizard.CourseDetails" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <div class="form-group">
        <br />
        <asp:label ID="lblCourseID" runat="server"  Text="Course ID: " CssClass="form-control"/>
       <br />
           <asp:Label ID="lblCourseName" runat="server" Text="Course Name: " CssClass="form-control" />
       <br />
           <asp:Label ID="lblCourseSemester" runat="server" Text="Offered in Semester: " CssClass="form-control" />
       <br />
           <asp:Label ID="lblProfessor" runat="server" Text="Professor: " CssClass="form-control" />
       <br />
               <asp:Label ID="lblCourseType" runat="server"  Text="Course Type: " CssClass="form-control"/>
      <br />
           <asp:Label ID="lblTiming" runat="server" Text="Timings: " CssClass="form-control" Visible="false" />
       <br />
           <asp:Label ID="lblClassroom" runat="server" Text="Classroom Details: " CssClass="form-control" Visible="false"/>
       <br />
           <asp:Label ID="lblPrereqcourseID" runat="server" Visible="false" Text="Prerequisite Course ID: " CssClass="form-control" />
     
          <br />
        </div>
</asp:Content>