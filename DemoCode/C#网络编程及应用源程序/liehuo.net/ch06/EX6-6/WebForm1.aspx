<%@ Page language="c#" Codebehind="WebForm1.aspx.cs" AutoEventWireup="false" Inherits="book6_6.WebForm1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<asp:RegularExpressionValidator id="RegularExpressionValidator1" style="Z-INDEX: 101; LEFT: 228px; POSITION: absolute; TOP: 90px"
					runat="server" ErrorMessage="输入形式1234-1234567" Width="185px" Height="19px" ControlToValidate="TextBox1" ValidationExpression='[0-9]{4}-{1}[0-9]{7}'></asp:RegularExpressionValidator>
				<asp:Label id="Label1" style="Z-INDEX: 102; LEFT: 82px; POSITION: absolute; TOP: 49px" runat="server"
					Width="131px" Height="20px">输入电话号码</asp:Label>
				<asp:TextBox id="TextBox1" style="Z-INDEX: 103; LEFT: 230px; POSITION: absolute; TOP: 45px" runat="server"
					Width="168px" Height="24px"></asp:TextBox>
				<asp:Button id="Button1" style="Z-INDEX: 104; LEFT: 177px; POSITION: absolute; TOP: 150px" runat="server"
					Width="103px" Height="36px" Text="Button"></asp:Button></FONT>
		</form>
	</body>
</HTML>
