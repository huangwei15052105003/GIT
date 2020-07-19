<?xml version='1.0'?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
<xsl:template match="/">
<HTML>
<BODY>
<BR></BR>
<TABLE cellspacing="1" cellpadding="0">
   <TR bgcolor="#AAAAAA">
      <TD class="heading"><B>Date</B></TD>
      <TD class="heading"><B>From</B></TD>
      <TD class="heading"><B>To</B></TD>
      <TD class="heading"><B>Subject</B></TD>
   </TR>
   <xsl:for-each select="MESSAGES/MESSAGE">
   <TR bgcolor="#DDDDDD">
       <TD width="20%" valign="top">
         <xsl:value-of select="DATE"/>
       </TD>
      <TD width="15%" valign="top">
         <xsl:value-of select="FROM"/>
      </TD>
      <TD width="15%" valign="top">
         <xsl:value-of select="TO"/>
      </TD>
       <TD width="50%" valign="top">
         <B><xsl:value-of select="SUBJECT"/></B>
      </TD>
   </TR>
   </xsl:for-each>
</TABLE>
</BODY>
</HTML>
</xsl:template>
</xsl:stylesheet>

  