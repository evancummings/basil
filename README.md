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

1. The fields to be validated are wrapped in a `Panel` control
2. The individual input elements are decorated with a `data-required="true|false"` tag and/or a `data-type` tag (more on this to come).

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

1. The fields to be validated are wrapped in a `Panel` named `myForm`
2. The `Textbox` `txtName` has a `data-required="true"` to force user input
3. The `Textbox` `txtEmail` has both a `data-required="true"` and a `data-type="email"`, forcing user input as well as enforcing proper email formatting

That's all there is to the front-end, let's now look at how this works when `btnSubmit` is clicked.

The server-side click event is as follows:

```csharp
protected void btnSubmit_Click(object sender, EventArgs e)
{
    Basil.BasilValidator validator = new Basil.BasilValidator();
    validator.Validate(pnlForm);            
}
```

Two lines to make the magic happen:

1. We instantiate a new instance of the `BasilValidator` class, and
2. We call the `Validate` method, providing the `Panel` containing the input elements to validate

A boolean value is produced from this call, which you can then incorporate into your own logic flow within your applicaion.

## Bootstrap tooltip integration

Validation error messages are stored in the title attribute of the HTML element. Several popular tooltip libraries, including Twitter Bootstrap, utilize the title attribute as the textual source of hover effect tooltips. This allows for drop-in support for any tooltip library following this method. For example, and demonstrated above, adding the `rel="tooltip"` will tell Bootstrap to automatically interpet the tagged element as a tooltip.

## Customization

Basil supports various configuration options for the validator output. All configuration options are housed within the `Settings` property within the `BasilValidator` object. All values come with a 'I thought this best' at the time default, which can be overriden as needed.

### Color
Basil operates under two states - a control is either valid or invalid.

The following controls may be modified individually:

* `BasilValidator.Settings.Textbox`
* `BasilValidator.Settings.DropDownList`
* `BasilValidator.Settings.RadioButtonList`
* `BasilValidator.Settings.Checkbox`

Each of the preceeding controls may have the following attributes modified:

| Property | Description |
| --- | --- |
| `ForeColor` | Corresponds to the Text Color of the input control |
| `BackColor` | Corresponds to the Back Color of the input control |
| `BorderColor` | Corresponds to the Border Color of the input control |

Fore, Back, and Border colors are not necessarily implemented on each .NET control - basil will apply styling that is appropriate to the control in context.

The color is an HTML style hex color, and should be preceeded with the pound symbol.

Below is an example of configuring a Textbox's styling:

``` csharp
Basil.BasilValidator validator = new Basil.BasilValidator();

validator.Settings.Textbox.InvalidColors.BackColor = "#000000";
validator.Settings.Textbox.InvalidColors.ForeColor = "#FFFFFF";
validator.Settings.Textbox.InvalidColors.BorderColor = "#333333";

validator.Settings.Textbox.ValidColors.BackColor = "#FFFFFF";
validator.Settings.Textbox.ValidColors.ForeColor = "#000000";
validator.Settings.Textbox.ValidColors.BorderColor = "#0F0F0F";
```

### Error Messages

The error message that is applied to the element via the `title` tag is also fully configurable.

The following customizations are available:

| Setting | Description | Example |
| --- | --- | --- |
| PhoneValidation.Message | Phone number fails validation | `BasilValidator.Settings.PhoneValidation.Message = "My message here";` |
| EmailValidation.Message | Email fails validation | `BasilValidator.Settings.EmailValidation.Message = "My message here";` |
| NumericEntryValidation.Message | Input is not numeric | `BasilValidator.Settings.NumericEntryValidation.Message = "My message here";` |
|RequiredFieldValidation.Message | Input field is empty | `BasilValidator.Settings.RequiredFieldValidation.Message = "My message here";` |
| SocialSecurityNumberValidation.Message | Input field is not a properly formatted SSN | `BasilValidator.Settings.SocialSecurityNumberValidation.Message = "My message here";` |
|ZipValidation.Message | ZIP code is improperly formatted | `BasilValidator.Settings.ZipValidation.Message = "My message here";` |

Code example:

``` csharp
Basil.BasilValidator validator = new Basil.BasilValidator();

validator.Settings.PhoneValidation.Message = "My message here";
validator.Settings.EmailValidation.Message = "My message here";
validator.Settings.NumericEntryValidation.Message = "My message here";
validator.Settings.RequiredFieldValidation.Message = "My message here";
validator.Settings.SocialSecurityNumberValidation.Message = "My message here";
validator.Settings.ZipValidation.Message = "My message here";
```

###  Regular Expressions

Finally, and perhaps most powerful, the validation expressions can be modified for any data type that utilizes a regular expression. This allows for fine-tuning of validation logic for your own needs.

Regular Expressions are not supported in the `RequiredFieldValidation` or `NumericEntryValidation` types.

The following regular expression types may be customized:

| Setting | Default Regex | Example |
| --- | --- | --- |
| EmailValidation.Regex | `[a-zA-Z0-9_\\-\\.]+@[a-zA-Z0-9_\\-\\.]+\\.[a-zA-Z]{2,5}` | `BaseValidator.Settings.EmailValidation.Regex = "My regex here";` |
| PhoneValidation.Regex | `\\(\\d{3}\\)\\s\\d{3}-\\d{4}$` | `BaseValidator.Settings.PhoneValidation.Regex = "My regex here";` |
| SocialSecurityNumberValidation.Regex | `^\\d{3}\\-?\\d{2}\\-?\\d{4}$` | `BaseValidator.Settings.SocialSecurityNumberValidation.Regex = "My regex here";` |
|ZipValidation.Regex | Input field is empty | `BaseValidator.Settings.ZipValidation.Regex = "My regex here";` |

Example code:

``` csharp
Basil.BasilValidator validator = new Basil.BasilValidator();

validator.Settings.PhoneValidation.Regex = "My regex here";
validator.Settings.EmailValidation.Regex = "My regex here";            
validator.Settings.SocialSecurityNumberValidation.Regex = "My regex here";
validator.Settings.ZipValidation.Regex = "My regex here";
```
## Supported .NET controls

Basil supports the following .NET components:

* `Textbox`
* `Checkbox`
* `DropDownList`
* `RadioButtonList`

## Supported data-* attributes

Basil supports the following data-* attributes:

* `data-required (true|false)`
    *  Instructs the validator whether to enforce non-empty user input
*  `data-type (phone|zip|email|ssn|number)`
    * Insructs the validator to apply additional logic based on the data type of the field
    * Custom regular expressions may be supplied for phone, zup, email, and SSN types, see 'Cusomization' for more details





