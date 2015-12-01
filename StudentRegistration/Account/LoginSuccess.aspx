<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginSuccess.aspx.cs" Inherits="StudentRegistration.Home.HomePage" MasterPageFile="~/Site.Master" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    
    <div class="jumbotron">
        <asp:PlaceHolder runat="server" ID="SuccessMessage" Visible="true">
            <p class="text-success">
                <asp:Label runat="server" ID="lblStudentName" /><br />
               
                <asp:Label runat="server" ID="lblStudentID" /><br />
                 <asp:Literal runat="server" ID="SuccessText" /><br />
                
                
            </p>
        </asp:PlaceHolder>
    </div>

    <div class="jumbotron">
        <p class="lead">UMass Student Portal is where you manage your classes and information about yourself.</p>
        <p class="lead">Using this portal you can add a course, drop a course, get detailed information about a course and see the list of available courses.</p>
    </div>
</asp:Content>
