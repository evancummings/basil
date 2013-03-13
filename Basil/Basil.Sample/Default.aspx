<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Basil.Sample.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Basil Tests</title>

    <link href="/Content/bootstrap.min.css" rel="stylesheet" />
    <link href="/Content/Site.css" rel="stylesheet" />

    <script src="/Scripts/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <div class="container">
            <h3>Basil Test Form</h3>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="pnlSuccess" CssClass="alert alert-success" Visible="false">
                        Yay, you have a valid form!
                    </asp:Panel>

                    <asp:Panel runat="server" ID="pnlErrors" CssClass="alert alert-error" Visible="false">
                        <asp:Literal runat="server" ID="litErrors" />
                    </asp:Panel>

                    <asp:Panel runat="server" ID="pnlForm" CssClass="row-fluid">
                        <basil:BasilTextBox runat="server" ID="btbFirstName" Label="First Name" Required="true" ErrorMessage="First name is required" />

                        <basil:BasilTextBox runat="server" ID="btbLastName" Label="Last Name" Required="true" />

                        <basil:BasilTextBox runat="server" ID="btbEmail" Label="Email Address" Required="true" RequiredType="Email" />

                        <div class="control-group">
                            <asp:Label runat="server" ID="Label1" AssociatedControlID="btbNoControlGroup" Text="No Control Group" />

                            <div class="controls">
                                <basil:BasilTextBox runat="server" ID="btbNoControlGroup" Required="true" RenderControlGroupMarkup="false" />
                            </div>
                        </div>

                        <basil:BasilCheckBox runat="server" ID="bcbIsSomething" Label="Required Checkbox" Required="true" />

                        <basil:BasilDropDownList runat="server" ID="bddlItems" Label="DropDownList Items" Required="true">
                            <asp:ListItem Text="- Select -" Value="" />
                            <asp:ListItem Text="Item 1" Value="1" />
                            <asp:ListItem Text="Item 2" Value="2" />
                            <asp:ListItem Text="Item 3" Value="3" />
                        </basil:BasilDropDownList>

                        <basil:BasilRadioButtonList runat="server" ID="brblItems" Label="RadioButtonList Items" Required="true" CssClass="unstyled" RepeatLayout="UnorderedList">
                            <asp:ListItem Text="Item 1" Value="1" />
                            <asp:ListItem Text="Item 2" Value="2" />
                            <asp:ListItem Text="Item 3" Value="3" />
                        </basil:BasilRadioButtonList>

                        <basil:BasilCheckBoxList runat="server" ID="bcblItems" Label="CheckBoxList Items" Required="true" CssClass="unstyled" RepeatLayout="UnorderedList">
                            <asp:ListItem Text="Item 1" Value="1" />
                            <asp:ListItem Text="Item 2" Value="2" />
                            <asp:ListItem Text="Item 3" Value="3" />
                        </basil:BasilCheckBoxList>
                    </asp:Panel>

                    <asp:Panel runat="server" ID="pnlFormTwo" CssClass="row-fluid">
                        <basil:BasilTextBox runat="server" ID="btbGroupTwo" Label="Business Name" Required="true" />
                    </asp:Panel>

                    <div class="form-actions">
                        <asp:Button runat="server" ID="btnSubmit" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>