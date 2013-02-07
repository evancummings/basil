# basil

Batched server-side form validation for ASP.NET WebForms with two lines of code and a whole lot of form decoration.

##What is basil?

Basil is an herb.

It is also a server-side validation implementation that utilizes attributes from HTML markup to drive validating large forms painlessly - because who wants 40 required field validators?

Fields are validated individually and highlighted visually with optional support for Bootstrap tooltips.

## How it works

Basil is designed to validate large amounts of form fields en mass with no dependence on any .NET validator controls. This is achieved by heavily utilizing data-* attributes that are used to decorate .NET input components. This approach is quite popular among Javascript based validation libraries. Basil brings this behavior to the server-side.

## A simple example

The functionality of basil relies on two conditions on the front-end ASPX markup:

1. The fields to be validated are wrapped in a Panel control
2. The individual input elements are decorated with a data-required="true|false" tag and/or a data-type tag (more on this to come).

Let's consider this markup

```html
    <asp:Panel ID="pnlForm" runat="server">
        <div class="row">
            <div class="span12">
                <asp:TextBox ID="txtName" runat="server" placeholder="Name" rel="tooltip" data-required="true"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="span12">
                <asp:TextBox ID="txtEmail" runat="server" placeholder="Email" rel="tooltip" data-required="true" data-type="email"></asp:TextBox>
            </div>
        </div>        
        <div class="row">
            <div class="span12">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" rel="tooltip" CssClass="btn" OnClick="btnSubmit_Click" />
            </div>
        </div> 
    </asp:Panel>
```

Points to note:

1. The fields to be validated are wrapped in a Panel named myForm
2. The Textbox txtName has a data-required="true" to force user input
3. The Textbox txtEmail has both a data-required="true" and a data-type="email", forcing user input as well as enforcing proper email formatting

That's all there is to the front-end, let's now look at how this works when btnSubmit is clicked.

The server-side click event is as follows:

```csharp
protected void btnSubmit_Click(object sender, EventArgs e)
{
    Basil.BasilValidator validator = new Basil.BasilValidator();
    validator.Validate(pnlForm);            
}
```

Two lines to make the magic happen:

1. We instantiate a new instance of the BasilValidator class, and
2. We call the Validate method, providing the Panel containing the input elements to validate

A boolean value is produced from this call, which you can then incorporate into your own logic flow within your applicaion.




