<%@ Page language="c#" Codebehind="WebForm1.aspx.cs" AutoEventWireup="false" Inherits="WebApplicationDataBase3.WebForm1" %>
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
				<asp:DataGrid id=DataGrid1 style="Z-INDEX: 101; LEFT: 7px; POSITION: absolute; TOP: 7px" runat="server" Height="69px" BorderColor="Blue" BorderStyle="None" BorderWidth="1px" BackColor="#DEBA84" CellPadding="1" DataSource="<%# dataSet11 %>" DataKeyField="username" DataMember="user" Width="356px">
					<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
					<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
					<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#A55129"></HeaderStyle>
					<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
					<Columns>
						<asp:EditCommandColumn ButtonType="PushButton" UpdateText="更新" CancelText="取消" EditText="编辑"></asp:EditCommandColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
				</asp:DataGrid></FONT>
		</form>
	</body>
</HTML>
