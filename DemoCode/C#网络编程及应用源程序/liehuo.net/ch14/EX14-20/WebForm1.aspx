<%@ Page language="c#" Codebehind="WebForm1.aspx.cs" AutoEventWireup="false" Inherits="EX14_20.WebForm1" %>
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
			<iframe id="IFRAME1" style="Z-INDEX: 101; LEFT: 80px; WIDTH: 296px; POSITION: absolute; TOP: 40px; HEIGHT: 240px"
				runat="server"></iframe>
			<asp:Button id="Button3" style="Z-INDEX: 105; LEFT: 320px; POSITION: absolute; TOP: 296px" runat="server"
				Text="下一个"></asp:Button>
			<asp:Button id="ButtonPosition" style="Z-INDEX: 104; LEFT: 176px; POSITION: absolute; TOP: 296px"
				runat="server" Width="112px" Text="ButtonPosition"></asp:Button>
			<asp:Button id="Button1" style="Z-INDEX: 103; LEFT: 80px; POSITION: absolute; TOP: 296px" runat="server"
				Text="上一个"></asp:Button>
			<asp:Label id="Label1" style="Z-INDEX: 102; LEFT: 184px; POSITION: absolute; TOP: 16px" runat="server"
				Width="96px">图片浏览器</asp:Label>
		</form>
	</body>
</HTML>
