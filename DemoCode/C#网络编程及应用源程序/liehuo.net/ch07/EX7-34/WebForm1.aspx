<%@ Page language="c#" Codebehind="WebForm1.aspx.cs" AutoEventWireup="false" Inherits="EX7_31.WebForm1" %>
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
			<asp:DataList id="DataList1" style="Z-INDEX: 101; LEFT: 96px; POSITION: absolute; TOP: 24px" runat="server"
				RepeatColumns="2">
				<HeaderTemplate>
					<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="300" border="1">
						<TR>
							<TD colspan="2" align="center">这是页眉部分</TD>
						</TR>
				</HeaderTemplate>
				<FooterTemplate>
					<TR>
						<TD colspan="2" align="center">这是页脚部分</TD>
					</TR>
					</TABLE>
				</FooterTemplate>
				<ItemTemplate>
					<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="300" border="0">
						<TR>
							<TD>
								<asp:TextBox id=TextBox1 runat="server" ForeColor="Red" ReadOnly="True" Text='<%# DataBinder.Eval(Container, "DataItem.stor_id") %>'>
								</asp:TextBox></TD>
						</TR>
						<TR>
							<TD>
								<asp:TextBox id=TextBox2 runat="server" ReadOnly="True" Text='<%# DataBinder.Eval(Container, "DataItem.stor_name") %>'>
								</asp:TextBox></TD>
						</TR>
						<TR>
							<TD>
								<asp:TextBox id=TextBox3 runat="server" ReadOnly="True" Text='<%# DataBinder.Eval(Container, "DataItem.stor_address") %>'>
								</asp:TextBox></TD>
						</TR>
					</TABLE>
				</ItemTemplate>
			</asp:DataList>
		</form>
	</body>
</HTML>
