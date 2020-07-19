<%@ Page language="c#" Codebehind="WebForm1.aspx.cs" AutoEventWireup="false" Inherits="book6_7.WebForm1" %>
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
				<asp:RequiredFieldValidator id="RequiredFieldValidator1" style="Z-INDEX: 101; LEFT: 187px; POSITION: absolute; TOP: 76px"
					runat="server" ErrorMessage="电话号码不能为空" Width="141px" Height="32px" ControlToValidate="TextBox1"></asp:RequiredFieldValidator>
				<asp:Label id="Label1" style="Z-INDEX: 102; LEFT: 53px; POSITION: absolute; TOP: 32px" runat="server"
					Width="100px" Height="25px">输入电话号码</asp:Label>
				<asp:TextBox id="TextBox1" style="Z-INDEX: 103; LEFT: 173px; POSITION: absolute; TOP: 27px" runat="server"
					Width="151px" Height="35px"></asp:TextBox>
				<asp:Button id="Button1" style="Z-INDEX: 104; LEFT: 131px; POSITION: absolute; TOP: 120px" runat="server"
					Width="108px" Height="33px" Text="确定"></asp:Button></FONT>
		</form>
	</body>
</HTML>
