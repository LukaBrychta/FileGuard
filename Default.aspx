<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FileGuard.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FileGuard</title>
</head>
<body>
    <div style="margin: 10px">
        <form id="form1" runat="server">

            <div style="padding: 5px">
                <asp:Label runat="server" Text="Cesta k adresáři: " ID="LabelPathInput"></asp:Label>
                <asp:TextBox ID="TextBoxInput" runat="server" Columns="70" OnTextChanged="TextBoxInput_TextChanged" TextMode="Url"></asp:TextBox><asp:RequiredFieldValidator runat="server" ErrorMessage="Zadejte cestu k adresáři." ControlToValidate="TextBoxInput" ID="TextBoxInputValidator" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div style="padding: 5px">
                <asp:Button ID="ButtonForAnalysis" runat="server" Text="Analýza" OnClick="ButtonForAnalysis_Click" />
            </div>
            <div style="padding: 5px">
                <asp:TextBox runat="server" ID="TextBoxOutput" OnTextChanged="TextBoxOutput_TextChanged" Columns="100" TextMode="MultiLine" Rows="10" ReadOnly="True"></asp:TextBox>
            </div>
            <div style="padding: 5px">
                <asp:Label runat="server" ID="LabelPathOutput"></asp:Label>
            </div>

        </form>

        <p>[A] = added (nový soubor)</p>
        <p>[M] = modified (změněný soubor)</p>
        <p>[D] = deleted (odstraněný soubor)</p>
    </div>
</body>
</html>
