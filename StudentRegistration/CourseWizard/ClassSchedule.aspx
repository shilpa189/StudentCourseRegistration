<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="ClassSchedule.aspx.cs" Inherits="StudentRegistration.CourseWizard.ClassSchedule" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
        
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
   
        <asp:PlaceHolder runat="server" ID="ErrorMessageClassSchedule" Visible="false">
             <div class="jumbotron">
             <p class="text-danger">
                <asp:Literal runat="server" ID="ErrorTextClassSchedule" />
            </p>
                  </div>
        </asp:PlaceHolder>
   

    <div class="row">
        <asp:PlaceHolder runat="server" ID="SiteContentClassSchedule" Visible="false">
        <div class="jumbotron">
             <asp:label ID="AcademicYear" runat="server">Current Academic Year: 2015-2016</asp:label>
           <br />
            <asp:label ID="SelectSemesterLb" runat="server">Select a particular semester:</asp:label>
            <asp:RadioButtonList ID="SemesterRadioButton" runat="server" AutoPostBack="True"  Visible="false" OnSelectedIndexChanged="SemesterRadioButton_SelectedIndexChanged">
                <asp:ListItem Text="Fall 2015" Value="Fall 2015"></asp:ListItem>
                <asp:ListItem Text="Spring 2016" Value="Spring 2016"></asp:ListItem>
            </asp:RadioButtonList>
            <div class="form-group">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            
                            <br />
                            <asp:Label ID="NotRegisteredlbl" runat="server" Visible="false" />
                            <asp:Table ID="ClassScheduleTable" CssClass="table table-bordered" runat="server" Visible="false">
                                <asp:TableHeaderRow ID="HeaderRowClassScheduleTable" runat="server">
                                    <asp:TableHeaderCell ID="CourseID" runat="server" Text="Course ID" />
                                    <asp:TableHeaderCell ID="CourseName" runat="server" Text="Course Name" />
                               <asp:TableHeaderCell ID="Professor" runat="server" Text="Professor" />
                               <asp:TableHeaderCell ID="Location" runat="server" Text="Location" />
                               <asp:TableHeaderCell ID="Timings" runat="server" Text="Timings" />
                               
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

