<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="StudentRegistration.Account.Logout" MasterPageFile="~/Site.Master" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    
    <div class="jumbotron">
        <asp:PlaceHolder runat="server" ID="MessageLogout" Visible="false">
            <p class="text-success">
                <asp:Literal runat="server" ID="TextLogout" />
            </p>
        </asp:PlaceHolder>
    </div>

    <div class="jumbotron">
        <p class="lead">UMass Student Portal is where you manage your classes and information about yourself.</p>
        <p class="lead">Using this portal you can add a course, drop a course, get detailed information about a course and see the list of available courses.</p>
    </div>
</asp:Content>
