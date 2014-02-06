<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Version3.aspx.cs" Inherits="Basil.Sample.Version3" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Basil Tests</title>

    <link href="/Content/bootstrap3/bootstrap.min.css" rel="stylesheet" />
    <link href="/Content/Site.css" rel="stylesheet" />

    <script src="http://code.jquery.com/jquery-1.10.2.min.js"></script>
    <script src="/Scripts/bootstrap3/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <div class="container">
            <div class="row">
                <h3>Basil Test Form (Bootstrap 3)</h3>
            </div>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="pnlSuccess" CssClass="alert alert-success" Visible="false">
                        Yay, you have a valid form!
                    </asp:Panel>

                    <asp:Panel runat="server" ID="pnlErrors" CssClass="alert alert-danger" Visible="false">
                        <asp:Literal runat="server" ID="litErrors" />
                    </asp:Panel>

                    <asp:Panel runat="server" ID="pnlForm" CssClass="row">
                        <div class="col-md-12">
                            <basil:BasilTextBox runat="server" ID="btbFirstName" Label="First Name" Required="true" ErrorMessage="First name is required" HasFeedback="true" BootstrapVersion="V3" />

                            <basil:BasilTextBox runat="server" ID="btbLastName" Label="Last Name" Required="true" BootstrapVersion="V3" />

                            <basil:BasilTextBox runat="server" ID="btbEmail" Label="Email Address" Required="true" RequiredType="Email" BootstrapVersion="V3" />

                            <basil:BasilCheckBox runat="server" ID="bcbIsSomething" Label="Required Checkbox" Required="true" BootstrapVersion="V3" />

                            <basil:BasilDropDownList runat="server" ID="bddlItems" Label="DropDownList Items" Required="true" BootstrapVersion="V3">
                                <asp:ListItem Text="- Select -" Value="" />
                                <asp:ListItem Text="Item 1" Value="1" />
                                <asp:ListItem Text="Item 2" Value="2" />
                                <asp:ListItem Text="Item 3" Value="3" />
                            </basil:BasilDropDownList>

                            <basil:BasilRadioButtonList runat="server" ID="brblItems" Label="RadioButtonList Items" Required="true" BootstrapVersion="V3">
                                <asp:ListItem Text="Item 1" Value="1" />
                                <asp:ListItem Text="Item 2" Value="2" />
                                <asp:ListItem Text="Item 3" Value="3" />
                            </basil:BasilRadioButtonList>

                            <basil:BasilCheckBoxList runat="server" ID="bcblItems" Label="CheckBoxList Items" Required="true" BootstrapVersion="V3">
                                <asp:ListItem Text="Item 1" Value="1" />
                                <asp:ListItem Text="Item 2" Value="2" />
                                <asp:ListItem Text="Item 3" Value="3" />
                            </basil:BasilCheckBoxList>
                        </div>
                    </asp:Panel>

                    <br />

                    <div class="row">
                        <div class="col-md-12">
                            <div class="well well-sm">
                                <asp:Button runat="server" ID="btnSubmit" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>