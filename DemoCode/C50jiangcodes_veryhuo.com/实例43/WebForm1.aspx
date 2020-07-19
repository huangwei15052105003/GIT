<%@ Page language="c#" Codebehind="WebForm1.aspx.cs" AutoEventWireup="false" Inherits="ConfirmInput.WebForm1" %>
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
				<asp:TextBox id="txtName" style="Z-INDEX: 101; LEFT: 144px; POSITION: absolute; TOP: 17px" runat="server" Width="150px" Height="30px"></asp:TextBox>
				<asp:TextBox id="txtBirthdate" style="Z-INDEX: 102; LEFT: 144px; POSITION: absolute; TOP: 61px" runat="server" Width="150px" Height="30px"></asp:TextBox>
				<asp:TextBox id="txtPassword" style="Z-INDEX: 103; LEFT: 144px; POSITION: absolute; TOP: 105px" runat="server" Width="150px" Height="30px"></asp:TextBox>
				<asp:TextBox id="txtPasswordAgain" style="Z-INDEX: 104; LEFT: 144px; POSITION: absolute; TOP: 149px" runat="server" Width="150" Height="30"></asp:TextBox>
				<asp:Label id="Label1" style="Z-INDEX: 105; LEFT: 24px; POSITION: absolute; TOP: 22px" runat="server" Width="80px" Height="23">用户名</asp:Label>
				<asp:Label id="Label2" style="Z-INDEX: 106; LEFT: 24px; POSITION: absolute; TOP: 66px" runat="server" Width="86px" Height="23px">出生日期</asp:Label>
				<asp:Label id="Label3" style="Z-INDEX: 107; LEFT: 24px; POSITION: absolute; TOP: 110px" runat="server" Width="93px" Height="23px">密码</asp:Label>
				<asp:Label id="Label4" style="Z-INDEX: 108; LEFT: 24px; POSITION: absolute; TOP: 154px" runat="server" Width="95px" Height="23px">重复密码</asp:Label>
				<asp:Button id="btnRegister" style="Z-INDEX: 109; LEFT: 26px; POSITION: absolute; TOP: 197px" runat="server" Width="109px" Height="31px" Text="注册"></asp:Button>
				<asp:RequiredFieldValidator id="RequiredFieldValidator1" style="Z-INDEX: 110; LEFT: 310px; POSITION: absolute; TOP: 22px" runat="server" ErrorMessage="Email name is required." ControlToValidate="txtName">*</asp:RequiredFieldValidator>
				<asp:RequiredFieldValidator id="RequiredFieldValidator2" style="Z-INDEX: 111; LEFT: 309px; POSITION: absolute; TOP: 67px" runat="server" Width="10px" Height="27px" ErrorMessage="You must enter a birth date." ControlToValidate="txtBirthdate">*</asp:RequiredFieldValidator>
				<asp:RequiredFieldValidator id="RequiredFieldValidator3" style="Z-INDEX: 112; LEFT: 308px; POSITION: absolute; TOP: 112px" runat="server" Width="10px" Height="24px" ErrorMessage="You must enter a password." ControlToValidate="txtPassword">*</asp:RequiredFieldValidator>
				<asp:RequiredFieldValidator id="RequiredFieldValidator4" style="Z-INDEX: 113; LEFT: 309px; POSITION: absolute; TOP: 153px" runat="server" Width="6px" Height="4px" ErrorMessage="Re-enter the password to confirm it." ControlToValidate="txtPasswordAgain">*</asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator id="RegularExpressionValidator1" style="Z-INDEX: 114; LEFT: 329px; POSITION: absolute; TOP: 22px" runat="server" Width="12px" Height="4px" ErrorMessage="Name must be in the format name@domain.xxx." ControlToValidate="txtName" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
				<asp:CompareValidator id="CompareValidator1" style="Z-INDEX: 115; LEFT: 331px; POSITION: absolute; TOP: 67px" runat="server" Width="10px" Height="6px" ErrorMessage="Birth date is not a valid date." ControlToValidate="txtBirthdate" ValueToCompare="1900/1/1" Type="Date" Display="Dynamic" Operator="GreaterThan">*</asp:CompareValidator>
				<asp:CompareValidator id="CompareValidator2" style="Z-INDEX: 116; LEFT: 332px; POSITION: absolute; TOP: 154px" runat="server" Width="11px" Height="4px" ErrorMessage="The passwords do not match." ControlToValidate="txtPasswordAgain" Display="Dynamic" ControlToCompare="txtPassword">*</asp:CompareValidator>
				<asp:ValidationSummary id="ValidationSummary1" style="Z-INDEX: 117; LEFT: 28px; POSITION: absolute; TOP: 247px" runat="server" Width="318px" Height="71px"></asp:ValidationSummary></FONT>
		</form>
	</body>
</HTML>
