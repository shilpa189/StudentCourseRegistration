<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CourseEnrollmentMessage.aspx.cs" Inherits="StudentRegistration.CourseWizard.CourseEnrollmentMessage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    
    <div class="jumbotron">
        <asp:PlaceHolder runat="server" ID="Message" Visible="true">
            <p class="text-success">
                 <asp:Literal runat="server" ID="lblEnrollmentText" /><br />
                
                
            </p>
        </asp:PlaceHolder>
    </div>

    
</asp:Content>

