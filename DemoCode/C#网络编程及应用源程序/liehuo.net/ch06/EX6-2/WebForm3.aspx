<%@ Page language="c#" Codebehind="WebForm3.aspx.cs" AutoEventWireup="false" Inherits="book6_2.WebForm3" %>
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
				<asp:Panel id="Panel1" style="Z-INDEX: 101; LEFT: 72px; POSITION: absolute; TOP: 32px" runat="server"
					Width="358px" Height="193px" BackColor="Gainsboro">
					<P><BR>
						<asp:Label id="Label1" runat="server">你的业余爱好是：</asp:Label><BR>
						<BR>
						<asp:CheckBox id="CheckBox1" runat="server" Text="体育活动"></asp:CheckBox>&nbsp;&nbsp;
						<asp:CheckBox id="CheckBox2" runat="server" Text="逛商场"></asp:CheckBox>&nbsp;&nbsp;
						<asp:CheckBox id="CheckBox3" runat="server" Text="聊天"></asp:CheckBox><BR>
						<BR>
						<BR>
						&nbsp;<BR>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
						&nbsp;
						<asp:Button id="Button1" runat="server" Height="32px" Width="68px" Text="确定"></asp:Button></P>
					<P>
						<asp:TextBox id="TextBox1" runat="server" BackColor="SkyBlue" Width="358px"></asp:TextBox></P>
				</asp:Panel></FONT>
		</form>
		<P><FONT face="宋体"></FONT></P>
	</body>
</HTML>
