using System.IO;
using AIR.StyleCopAnalyzer.Editor;
using NUnit.Framework;

[TestFixture]
public class StyleCopProjectXmlGeneratorTests {
    [Test]
    public void ToString_ForValidXml_ReturnsInputString() {
        // Arrange 
        const string XML_MARKUP =
            "<Project " +
            "ToolsVersion=\"4.0\" " +
            "DefaultTargets=\"Build\" " +
            "xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\" />";
        var projectGenerator = new StyleCopProjectXmlGenerator(XML_MARKUP);

        // Act
        var xmlToStringOutput = projectGenerator.ToString();

        Assert.AreEqual(XML_MARKUP, xmlToStringOutput);
    }

    [Test]
    public void ReferenceStyleCopDll_ProjectWithItemGroup_AddsNewConfigItemGroup() {
        // Arrange
        const string XML_MARKUP = "<Project> <ItemGroup> </ItemGroup> </Project>";
        var projectGenerator = new StyleCopProjectXmlGenerator(XML_MARKUP);

        var packagePath = "./Packages/com.air.stylecop";
        var packageDir = new DirectoryInfo(packagePath);
        const string ANALYZER_STR = "<Analyzer Include=\"{0}/{1}\" />";
        const string XML_DLL_REFERENCE_OPEN = "<ItemGroup>";
        var analyzerDllPath = string.Format(ANALYZER_STR, packageDir.FullName, "StyleCop.Analyzers.dll");
        var codeFixDllPath = string.Format(ANALYZER_STR, packageDir.FullName, "StyleCop.Analyzers.dll");
        const string XML_DLL_REFERENCE_CLOSE = "</ItemGroup>";
        
        // Act
        projectGenerator.ReferenceStyleCopDlls();

        // Assert
        var projectStr = projectGenerator.ToString();
        StringAssert.Contains(XML_DLL_REFERENCE_OPEN, projectStr, "Reference tag missing.");
        StringAssert.Contains(analyzerDllPath, projectStr,"Analyzer include missing or invalid.");
        StringAssert.Contains(codeFixDllPath, projectStr,"Code Fix include missing or invalid.");
        StringAssert.Contains(XML_DLL_REFERENCE_CLOSE,projectStr, "Reference tag close missing.");
    }
}