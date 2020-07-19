<%@ Page language="c#" Codebehind="WebForm1.aspx.cs" AutoEventWireup="false" Inherits="EX7_30.WebForm1" %>
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
			<asp:DataGrid id="DataGrid1" style="Z-INDEX: 101; LEFT: 64px; POSITION: absolute; TOP: 64px" runat="server"
				AutoGenerateColumns="False" Width="432px" Height="88px" PageSize="1" AllowPaging="True">
				<Columns>
					<asp:TemplateColumn>
						<HeaderTemplate>
							<FONT face="ËÎÌו"></FONT>
						</HeaderTemplate>
						<ItemTemplate>
							<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="300" border="1">
								<TR>
									<TD>
										<asp:TextBox id=TextBox1 runat="server" Width="256px" Text='<%# DataBinder.Eval(Container, "DataItem.stor_id") %>' ReadOnly="True">
										</asp:TextBox></TD>
								</TR>
								<TR>
									<TD>
										<asp:TextBox id=TextBox2 runat="server" Width="256px" Text='<%# DataBinder.Eval(Container, "DataItem.stor_name") %>' ReadOnly="True">
										</asp:TextBox></TD>
								</TR>
								<TR>
									<TD>
										<asp:TextBox id=TextBox3 runat="server" Width="256px" Text='<%# DataBinder.Eval(Container, "DataItem.stor_address") %>' ReadOnly="True">
										</asp:TextBox></TD>
								</TR>
							</TABLE>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
		</form>
	</body>
</HTML>
