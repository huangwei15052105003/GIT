<%@ Page language="c#" Codebehind="WebForm3.aspx.cs" AutoEventWireup="false" Inherits="book6_7.WebForm3" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm3</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="WebForm3" method="post" runat="server">
			<FONT face="宋体">
				<asp:RangeValidator id="RangeValidator1" style="Z-INDEX: 101; LEFT: 304px; POSITION: absolute; TOP: 112px" runat="server" ErrorMessage="请输入0-100之间的数" Width="119px" Height="27px" ControlToValidate="TextBox1" MinimumValue="0" MaximumValue="100" Type="Integer"></asp:RangeValidator>
				<asp:TextBox id="TextBox1" style="Z-INDEX: 102; LEFT: 152px; POSITION: absolute; TOP: 107px" runat="server" Width="138px" Height="33px"></asp:TextBox>
				<asp:Button id="Button1" style="Z-INDEX: 103; LEFT: 153px; POSITION: absolute; TOP: 164px" runat="server" Width="122px" Height="30px" Text="确定"></asp:Button>
				<asp:Label id="Label1" style="Z-INDEX: 104; LEFT: 159px; POSITION: absolute; TOP: 63px" runat="server" Width="118px" Height="31px">输入一个整数</asp:Label>
				<asp:RequiredFieldValidator id="RequiredFieldValidator1" style="Z-INDEX: 105; LEFT: 309px; POSITION: absolute; TOP: 70px" runat="server" ErrorMessage="不允许控制" Width="102px" Height="37px" ControlToValidate="TextBox1"></asp:RequiredFieldValidator></FONT>
		</form>
	</body>
</HTML>
