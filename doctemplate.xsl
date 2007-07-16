<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" omit-xml-declaration="yes" />
  <xsl:template match="Page">
    <html>
      <head>
        <title>
          <xsl:value-of select="Title" />
        </title>
        <style>
					a { text-decoration: none }
				
					.CollectionTitle { font-weight: bold }
					.PageTitle { font-size: 150%; font-weight: bold }

					.Summary { }
					.Signature { }					
					.Remarks { }
					.Members { }
					.Copyright { }
					
					.Section { font-size: 125%; font-weight: bold }
					.SectionBox { margin-left: 2em }
					.NamespaceName { font-size: 105%; font-weight: bold }
					.NamespaceSumary { }
					.MemberName { font-size: 115%; font-weight: bold; margin-top: 1em }
					.MemberSignature { font-family: monospace; margin-top: 1em; }
					.MemberBox { }
					.Subsection { font-size: 105%; font-weight: bold }
					.SubsectionBox { margin-left: 2em; margin-bottom: 1em }

					.SignatureTable { background-color: #c0c0c0; }
					.EnumerationsTable th { background-color: #f2f2f2; }
					.CodeExampleTable { background-color: #f5f5dd; border: thin solid black; padding: .25em; }
					
					.MembersListing td { margin: 0px; border: 1px solid black; padding: .25em }
					
					.TypesListing td { margin: 0px;  padding: .25em }
					.InnerSignatureTable tr { background-color: #f2f2f2; }
					.TypePermissionsTable tr { background-color: #f2f2f2; }
					
				</style>
      </head>
      <body>
        <!-- HEADER -->
        <div class="CollectionTitle">
          <xsl:apply-templates select="CollectionTitle/node()" />
        </div>
        <h1 class="PageTitle">
          <xsl:apply-templates select="PageTitle/node()" />
        </h1>
        <p class="Summary">
          <xsl:apply-templates select="Summary/node()" />
        </p>
        <div class="Signature">
          <xsl:apply-templates select="Signature/node()" />
        </div>
        <div class="Remarks">
          <xsl:apply-templates select="Remarks/node()" />
        </div>
        <div class="Members">
          <xsl:apply-templates select="Members/node()" />
        </div>
        <hr size="1" />
        <div class="Copyright">
          <xsl:apply-templates select="Copyright/node()" />
        </div>
      </body>
    </html>
  </xsl:template>
  <!-- IDENTITY TRANSFORMATION -->
  <xsl:template match="@*|node()">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()" />
    </xsl:copy>
  </xsl:template>
</xsl:stylesheet>