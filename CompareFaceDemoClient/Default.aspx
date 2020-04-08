<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Label3" runat="server" Text="Face Compare - "></asp:Label>
        <br />
        <div>
            <asp:FileUpload ID="FileUpload1" runat="server" />
        </div>
        <asp:CheckBox ID="chkCompareBase" runat="server" Text="Este prima imagine (baza cu care comparam)?" />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Upload" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="Download" OnClick="Button2_Click" Visible="False" />

        <hr />
        <asp:Label ID="Label1" runat="server" Text="Compara imaginea cu imaginea de baza"></asp:Label>
        <hr />
        <asp:Image ID="Image1" runat="server" Height="100" Width="100" />
        <hr />
        <asp:Label ID="Label2" runat="server" Text="Imagine de baza"></asp:Label>
        <hr />
        <asp:Image ID="Image2" runat="server" Height="100" Width="100" />
    </form>

</body>
</html>
