<?xml version='1.0'?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
<xsl:template match="/">
<HTML>
<BODY>
<BR></BR>
<FONT face="Verdana" size="2">
<TABLE cellspacing="1" cellpadding="0">
   <xsl:for-each select="MESSAGES/MESSAGE">
   <TR bgcolor="#CCCCCC">
   <TD class="info">
         Subject: <xsl:value-of select="SUBJECT"/><BR></BR>
         <xsl:value-of select="BODY"/>
   </TD>
   </TR>
   </xsl:for-each>
</TABLE>
</FONT>
</BODY>
</HTML>
</xsl:template>
</xsl:stylesheet>
  