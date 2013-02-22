<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ValidatorSample._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
                <h2>Modify this template to jump-start your ASP.NET application.</h2>
            </hgroup>
            <p>
                To learn more about ASP.NET, visit <a href="http://asp.net" title="ASP.NET Website">http://asp.net</a>.
                The page features <mark>videos, tutorials, and samples</mark> to help you get the most from ASP.NET.
                If you have any questions about ASP.NET visit
                <a href="http://forums.asp.net/18.aspx" title="ASP.NET Forum">our forums</a>.
            </p>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>We suggest the following:</h3>

    <asp:Panel ID="pnlForm" runat="server">
        <div class="row">
            <div class="span12">
                <asp:TextBox ID="txtName" runat="server" placeholder="Name" data-required="true"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="span12">
                <asp:TextBox ID="txtAddress" runat="server" placeholder="Address" data-required="true"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="span12">
                <asp:TextBox ID="txtCity" runat="server" placeholder="City" data-required="true"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="span12">
                <asp:TextBox ID="txtEmail" runat="server" placeholder="Email" data-required="true" data-type="email"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="span12">
                <asp:TextBox ID="txtPhone" runat="server" placeholder="Phone" data-required="true" data-type="phone"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="span12">
                <asp:TextBox ID="txtZIP" runat="server" placeholder="ZIP" data-required="true" data-type="zip"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="span12">
                <asp:TextBox ID="txtDate" runat="server" placeholder="Date" data-required="true" data-type="date"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="span12">
                <asp:CheckBox runat="server" ID="cbRequired" data-required="true" Text="Required Checkbox" />
            </div>
        </div>
        <div class="row">
            <div class="span12">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>