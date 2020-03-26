// Copyright (c) AIR Pty Ltd. All rights reserved.

using AIR.StyleCopAnalyzer.Editor;
using NUnit.Framework;

[TestFixture]
public class StyleCopProjectPostprocessorTests {

    private const string PROJECT_FILE_XML = "<Project></Project>";
    private string _styleCopDestFile;

    [Test]
    [Sequential]
    public void OnGeneratedCSProject_EmptyProject_AddsStyleCopMarkup(
        [Values("<AdditionalFiles Include=", "<Analyzer Include=", "<Analyzer Include=", "<CodeAnalysisRuleSet>")]
        string referenceElement,
        [Values("stylecop.json", "/StyleCop.Analyzers.dll", "/StyleCop.Analyzers.CodeFixes.dll", "stylecop.ruleset")]
        string referenceFile
    ) {
        // Act
        var referencedProject = StyleCopProjectPostprocessor
            .OnGeneratedCSProject(string.Empty, PROJECT_FILE_XML);

        // Assert
        StringAssert.Contains(referenceElement, referencedProject, $"Missing reference element: {referenceElement}.");
        StringAssert.Contains(referenceFile, referencedProject, $"Missing reference file: {referenceFile}.");
    }
}