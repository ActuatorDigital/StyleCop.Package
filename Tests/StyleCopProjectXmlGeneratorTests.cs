// Copyright (c) AIR Pty Ltd. All rights reserved.

using System.IO;
using AIR.StyleCopAnalyzer.Editor;
using NUnit.Framework;

[TestFixture]
public class StyleCopProjectXmlGeneratorTests {
    private const string XML_ITEMGROUP_OPEN = "<ItemGroup>";
    private const string XML_ITEMGROUP_CLOSE = "</ItemGroup>";
    private const string XML_PROJECT =
        "<Project> " +
            XML_ITEMGROUP_OPEN + " " + XML_ITEMGROUP_CLOSE + " " +
        "</Project>";

    private DirectoryInfo _packageDir;

    [SetUp]
    public void SetUp() {
        var packagePath = "./Packages/com.air.stylecop";
        _packageDir = new DirectoryInfo(packagePath);
    }

    [Test]
    public void ToString_ForValidXml_ReturnsInputString() {
        // Arrange
        const string XML_PROJECT_MARKUP =
            "<Project " +
                "ToolsVersion=\"4.0\" " +
                "DefaultTargets=\"Build\" " +
                "xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\" />";
        var projectGenerator = new StyleCopProjectXmlGenerator(XML_PROJECT_MARKUP);

        // Act
        var xmlToStringOutput = projectGenerator.ToString();

        Assert.AreEqual(XML_PROJECT_MARKUP, xmlToStringOutput);
    }

    [Test]
    public void ReferenceStyleCopDll_ProjectWithItemGroup_AddsNewConfigItemGroup() {
        // Arrange
        const string XML_MARKUP = "<Project> <ItemGroup> </ItemGroup> </Project>";
        var projectXmlGenerator = new StyleCopProjectXmlGenerator(XML_MARKUP);
        const string ANALYZER_STR = "<Analyzer Include=\"{0}/{1}\" />";
        var analyzerDllPath = string.Format(ANALYZER_STR, _packageDir.FullName, "StyleCop.Analyzers.dll");
        var codeFixDllPath = string.Format(ANALYZER_STR, _packageDir.FullName, "StyleCop.Analyzers.dll");

        // Act
        projectXmlGenerator.ReferenceStyleCopDlls();

        // Assert
        var projectStr = projectXmlGenerator.ToString();
        StringAssert.Contains(XML_ITEMGROUP_OPEN, projectStr, "ItemGroup tag missing.");
        StringAssert.Contains(analyzerDllPath, projectStr, "Analyzer include missing or invalid.");
        StringAssert.Contains(codeFixDllPath, projectStr, "Code Fix include missing or invalid.");
        StringAssert.Contains(XML_ITEMGROUP_CLOSE, projectStr, "ItemGroup tag close missing.");
    }

    [Test]
    public void ReferenceStyleCopJson_ProjectWithItemGroup_AddsNewRulesetReference() {
        // Arrange
        var projectXmlGenerator = new StyleCopProjectXmlGenerator(XML_PROJECT);
        const string STYLE_FILE_REFERENCE = "<AdditionalFiles Include=\"{0}/{1}\" />";
        var codeFixDllPath = string.Format(STYLE_FILE_REFERENCE, _packageDir.FullName, "stylecop.json");

        // Act
        projectXmlGenerator.ReferenceStyleCopJsonRules();

        var projectStr = projectXmlGenerator.ToString();
        StringAssert.Contains(XML_ITEMGROUP_OPEN, projectStr, "ItemGroup tag missing.");
        StringAssert.Contains(codeFixDllPath, projectStr, "Style rules reference missing.");
        StringAssert.Contains(XML_ITEMGROUP_CLOSE, projectStr, "ItemGroup tag close missing.");
    }
}