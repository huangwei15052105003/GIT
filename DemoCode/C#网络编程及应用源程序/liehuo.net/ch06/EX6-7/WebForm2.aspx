<%@ Page language="c#" Codebehind="WebForm2.aspx.cs" AutoEventWireup="false" Inherits="book6_7.WebForm2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm2</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="WebForm2" method="post" runat="server">
			<FONT face="宋体">
				<asp:CompareValidator id="CompareValidator1" style="Z-INDEX: 101; LEFT: 98px; POSITION: absolute; TOP: 156px" runat="server" ErrorMessage="必须大于0" Width="86px" Height="40px" ControlToValidate="TextBox1" Operator="GreaterThan" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
				<asp:Label id="Label1" style="Z-INDEX: 102; LEFT: 115px; POSITION: absolute; TOP: 42px" runat="server" Width="101px" Height="24px">最小年龄</asp:Label>
				<asp:TextBox id="TextBox1" style="Z-INDEX: 103; LEFT: 89px; POSITION: absolute; TOP: 85px" runat="server" Width="161px" Height="30px"></asp:TextBox>
				<asp:Label id="Label2" style="Z-INDEX: 104; LEFT: 301px; POSITION: absolute; TOP: 43px" runat="server" Width="85px" Height="22px">最大年龄</asp:Label>
				<asp:TextBox id="TextBox2" style="Z-INDEX: 105; LEFT: 273px; POSITION: absolute; TOP: 85px" runat="server" Width="163px" Height="28px"></asp:TextBox>
				<asp:Button id="Button1" style="Z-INDEX: 106; LEFT: 207px; POSITION: absolute; TOP: 217px" runat="server" Width="94px" Height="31px" Text="确定"></asp:Button>
				<asp:RequiredFieldValidator id="RequiredFieldValidator1" style="Z-INDEX: 107; LEFT: 98px; POSITION: absolute; TOP: 126px" runat="server" ErrorMessage="不允许空值" Width="139px" Height="20px" ControlToValidate="TextBox1"></asp:RequiredFieldValidator>
				<asp:RequiredFieldValidator id="RequiredFieldValidator2" style="Z-INDEX: 108; LEFT: 274px; POSITION: absolute; TOP: 124px" runat="server" ErrorMessage="不允许空值" Width="169px" Height="33px" ControlToValidate="TextBox2" Display="Dynamic"></asp:RequiredFieldValidator>
				<asp:CompareValidator id="CompareValidator2" style="Z-INDEX: 109; LEFT: 278px; POSITION: absolute; TOP: 158px" runat="server" ErrorMessage="必须大于最小年龄" Width="161px" Height="42px" ControlToValidate="TextBox2" Display="Dynamic" ControlToCompare="TextBox1" Operator="GreaterThan" Type="Integer"></asp:CompareValidator></FONT>
		</form>
	</body>
</HTML>
