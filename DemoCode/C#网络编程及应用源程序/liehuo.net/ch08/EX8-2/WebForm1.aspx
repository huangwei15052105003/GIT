<%@ Page language="c#" Codebehind="WebForm1.aspx.cs" AutoEventWireup="false" Inherits="EX8_2.WebForm1" %>
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
			<asp:Label id="Label1" style="Z-INDEX: 101; LEFT: 160px; POSITION: absolute; TOP: 72px" runat="server">姓名</asp:Label>
			<asp:Label id="Label2" style="Z-INDEX: 102; LEFT: 160px; POSITION: absolute; TOP: 120px" runat="server">年龄</asp:Label>
			<asp:TextBox id="TextBoxName" style="Z-INDEX: 103; LEFT: 232px; POSITION: absolute; TOP: 72px"
				runat="server"></asp:TextBox>
			<asp:TextBox id="TextBoxAge" style="Z-INDEX: 104; LEFT: 232px; POSITION: absolute; TOP: 112px"
				runat="server"></asp:TextBox>
			<asp:Button id="Button1" style="Z-INDEX: 105; LEFT: 240px; POSITION: absolute; TOP: 176px" runat="server"
				Text="开始传递"></asp:Button>
		</form>
	</body>
</HTML>
