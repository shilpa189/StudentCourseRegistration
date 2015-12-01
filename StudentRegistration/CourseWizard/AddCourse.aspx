<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AddCourse.aspx.cs" Inherits="StudentRegistration.CourseWizard.AddCourse" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>

    <asp:PlaceHolder runat="server" ID="ErrorMessageCourseList" Visible="false">
        <div class="jumbotron">
            <p class="text-danger">
                <asp:Literal runat="server" ID="ErrorTextCourseList" />
            </p>
        </div>
    </asp:PlaceHolder>


    <div class="row">
        <asp:PlaceHolder runat="server" ID="SiteContentDisplayCourseList" Visible="false">
            <div class="jumbotron">
                <asp:Label ID="SelectSemesterLb" runat="server">Select a particular semester:</asp:Label>
                <asp:RadioButtonList ID="SemesterRadioButton" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SemesterRadioButton_SelectedIndexChanged" Visible="false">
                    <asp:ListItem Text="Fall 2015" Value="Fall 2015"></asp:ListItem>
                    <asp:ListItem Text="Spring 2016" Value="Spring 2016"></asp:ListItem>
                </asp:RadioButtonList>
                <div class="form-group">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:Table ID="CourseListTable" CssClass="table table-bordered" runat="server" Visible="false">
                                <asp:TableHeaderRow ID="HeaderRowCourseListTable" runat="server">
                                    <asp:TableHeaderCell ID="CourseID" runat="server" Text="Course ID" />
                                    <asp:TableHeaderCell ID="CourseName" runat="server" Text="Course Name" />
                                    <asp:TableHeaderCell ID="Professor" runat="server" Text="Course Name" />
                                    <asp:TableHeaderCell ID="Timings" runat="server" Text="Course Name" />
                                    <asp:TableHeaderCell ID="PrerequisiteCourseID" runat="server" Text="Course Name" />

                                </asp:TableHeaderRow>
                            </asp:Table>


                                <asp:Label runat="server" ID="lblCourseSelection" Text="Please enter the courseid you want to enroll for: " Visible="false" />
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtCourseSelection" Visible="false"></asp:TextBox>
                                <br />
                                <asp:Button runat="server" ID="btnAddCourse" OnClick="btnAddCourse_Click" Text="Add Course" CssClass="btn btn-default" Visible="false" />
                       
                            <br />

                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="SemesterRadioButton" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                  
                </div>
            </div>
        </asp:PlaceHolder>
    </div>


</asp:Content>
