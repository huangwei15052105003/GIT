<%@ Page language="c#" Codebehind="WebForm4.aspx.cs" AutoEventWireup="false" Inherits="book6_7.WebForm4" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm4</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="WebForm4" method="post" runat="server">
			<FONT face="宋体">
				<asp:TextBox id="TextBox1" style="Z-INDEX: 101; LEFT: 120px; POSITION: absolute; TOP: 66px" runat="server" Width="170px" Height="54px"></asp:TextBox>
				<asp:CustomValidator id="CustomValidator1" style="Z-INDEX: 102; LEFT: 310px; POSITION: absolute; TOP: 96px" runat="server" Width="129px" Height="20px" ErrorMessage="必须数输入偶数" ControlToValidate="TextBox1"></asp:CustomValidator>
				<asp:Label id="Label1" style="Z-INDEX: 103; LEFT: 142px; POSITION: absolute; TOP: 35px" runat="server" Width="137px" Height="30px">请输入偶数</asp:Label>
				<asp:Button id="Button1" style="Z-INDEX: 104; LEFT: 162px; POSITION: absolute; TOP: 142px" runat="server" Width="85px" Height="23px" Text="确定"></asp:Button>
				<asp:CompareValidator id="CompareValidator1" style="Z-INDEX: 105; LEFT: 311px; POSITION: absolute; TOP: 68px" runat="server" Width="115px" Height="21px" ErrorMessage="该值必须大于0" ControlToValidate="TextBox1" ValueToCompare="0" Operator="GreaterThan" Type="Integer"></asp:CompareValidator></FONT>
		</form>
	</body>
</HTML>
