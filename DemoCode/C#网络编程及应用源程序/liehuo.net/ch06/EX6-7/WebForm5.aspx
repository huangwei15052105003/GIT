<%@ Page language="c#" Codebehind="WebForm5.aspx.cs" AutoEventWireup="false" Inherits="book6_7.WebForm5" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm5</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="WebForm5" method="post" runat="server">
			<FONT face="宋体">
				<asp:ValidationSummary id="ValidationSummary1" style="Z-INDEX: 101; LEFT: 148px; POSITION: absolute; TOP: 214px" runat="server" Width="170px" Height="27px" HeaderText="提示信息"></asp:ValidationSummary>
				<asp:Label id="Label1" style="Z-INDEX: 102; LEFT: 57px; POSITION: absolute; TOP: 63px" runat="server" Width="103px" Height="26px">姓名</asp:Label>
				<asp:Label id="Label2" style="Z-INDEX: 103; LEFT: 60px; POSITION: absolute; TOP: 114px" runat="server" Width="94px" Height="31px">成绩</asp:Label>
				<asp:TextBox id="TextBox1" style="Z-INDEX: 104; LEFT: 175px; POSITION: absolute; TOP: 56px" runat="server" Width="114px" Height="33px"></asp:TextBox>
				<asp:TextBox id="TextBox2" style="Z-INDEX: 105; LEFT: 177px; POSITION: absolute; TOP: 113px" runat="server" Width="116px" Height="31px"></asp:TextBox>
				<asp:RequiredFieldValidator id="RequiredFieldValidator1" style="Z-INDEX: 106; LEFT: 316px; POSITION: absolute; TOP: 63px" runat="server" Width="134px" Height="32px" ErrorMessage="姓名字段不能为空" ControlToValidate="TextBox1">*</asp:RequiredFieldValidator>
				<asp:RequiredFieldValidator id="RequiredFieldValidator2" style="Z-INDEX: 107; LEFT: 316px; POSITION: absolute; TOP: 113px" runat="server" Width="138px" Height="37px" ErrorMessage="成绩字段不能为空" ControlToValidate="TextBox2">*</asp:RequiredFieldValidator>
				<asp:Button id="Button1" style="Z-INDEX: 108; LEFT: 192px; POSITION: absolute; TOP: 170px" runat="server" Width="82px" Text="确定"></asp:Button></FONT>
		</form>
	</body>
</HTML>
