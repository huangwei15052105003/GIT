<%@ Page language="c#" Codebehind="WebForm1.aspx.cs" AutoEventWireup="false" Inherits="WebApplicationWebXML.WebForm1" %>
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
				<asp:CheckBox id="CheckBox1" style="Z-INDEX: 101; LEFT: 7px; POSITION: absolute; TOP: -1px" runat="server" Width="155px" Height="21px" Text="只显示标题" Checked="True" AutoPostBack="True"></asp:CheckBox>
				<asp:Xml id="Xml1" runat="server" DocumentSource="Emails.xml" TransformSource="EmailHeader.xslt"></asp:Xml></FONT>
		</form>
	</body>
</HTML>
