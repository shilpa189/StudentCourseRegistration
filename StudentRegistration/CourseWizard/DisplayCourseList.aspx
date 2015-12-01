<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="DisplayCourseList.aspx.cs" Inherits="StudentRegistration.CourseWizard.DisplayCourseList" %>

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
            <asp:label ID="SelectSemesterLb" runat="server">Select a particular semester:</asp:label>
            <asp:RadioButtonList ID="SemesterRadioButton" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SemesterRadioButton_SelectedIndexChanged" Visible="false">
                <asp:ListItem Text="Fall 2015" Value="Fall 2015"></asp:ListItem>
                <asp:ListItem Text="Spring 2016" Value="Spring 2016"></asp:ListItem>
            </asp:RadioButtonList>
            <div class="form-group">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            
                            <br />
                            <asp:Table ID="CourseListTable" CssClass="table table-bordered" runat="server" Visible="false">
                                <asp:TableHeaderRow ID="HeaderRowCourseListTable" runat="server">
                                    <asp:TableHeaderCell ID="CourseID" runat="server" Text="Course ID" />
                                    <asp:TableHeaderCell ID="CourseName" runat="server" Text="Course Name" />
                                </asp:TableHeaderRow>
                            </asp:Table>
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
