<%@ Page language="c#" Codebehind="WebForm1.aspx.cs" AutoEventWireup="false" Inherits="WebTestWebService.WebForm1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:Label id="Label1" style="Z-INDEX: 101; LEFT: 144px; POSITION: absolute; TOP: 64px" runat="server">输入</asp:Label>
			<asp:CheckBox id="CheckBox1" style="Z-INDEX: 107; LEFT: 240px; POSITION: absolute; TOP: 112px"
				runat="server" Text="大写显示"></asp:CheckBox>
			<asp:Button id="Button1" style="Z-INDEX: 106; LEFT: 216px; POSITION: absolute; TOP: 240px" runat="server"
				Text="转换" Width="112px"></asp:Button>
			<asp:TextBox id="TextBox2" style="Z-INDEX: 104; LEFT: 232px; POSITION: absolute; TOP: 160px"
				runat="server"></asp:TextBox>
			<asp:TextBox id="TextBox1" style="Z-INDEX: 103; LEFT: 224px; POSITION: absolute; TOP: 64px" runat="server"></asp:TextBox>
			<asp:Label id="Label2" style="Z-INDEX: 102; LEFT: 136px; POSITION: absolute; TOP: 160px" runat="server">输出</asp:Label>
		</form>
	</body>
</HTML>
