<%@ Page language="c#" Codebehind="WebForm5.aspx.cs" AutoEventWireup="false" Inherits="book6_2.WebForm5" %>
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
				<asp:Panel id="Panel1" style="Z-INDEX: 101; LEFT: 80px; POSITION: absolute; TOP: 16px" runat="server"
					Width="318px" Height="237px" BackColor="Gainsboro">
					<P>
					</P>
					<P>&nbsp;
						<asp:Label id="Label1" runat="server" Height="26px" Width="124px">请您进行选择：</asp:Label></P>
					<P>&nbsp;
						<asp:RadioButton id="RadioButton1" runat="server" GroupName="group1" Text="鼠标"></asp:RadioButton></P>
					<P>&nbsp;
						<asp:RadioButton id="RadioButton2" runat="server" GroupName="group1" Text="键盘"></asp:RadioButton></P>
					<P>&nbsp;
						<asp:RadioButton id="RadioButton3" runat="server" GroupName="group1" Text="麦克风"></asp:RadioButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:Button id="Button1" runat="server" Text="确定"></asp:Button></P>
				</asp:Panel></FONT>
		</form>
	</body>
</HTML>
