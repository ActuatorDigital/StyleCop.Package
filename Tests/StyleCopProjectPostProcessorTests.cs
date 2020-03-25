// Copyright (c) AIR Pty Ltd. All rights reserved.

using AIR.StyleCopAnalyzer.Editor;
using NUnit.Framework;

[TestFixture]
public class StyleCopProjectPostProcessorTests {
    private string _styleCopDestFile;

    const string PROJECT_FILE_XML = "<Project></Project>";

    [Test]
    [Sequential]
    public void OnGeneratedCSProject_EmptyProject_AddsStyleCopReferences(
        [Values("<AdditionalFiles Include=", "<Analyzer Include=", "<Analyzer Include=")] string referenceElement,
        [Values("stylecop.json", "/StyleCop.Analyzers.dll", "/StyleCop.Analyzers.CodeFixes.dll")] string referenceFile
    ) {
        // Act
        var referencedProject = StyleCopProjectPostProcessor
            .OnGeneratedCSProject(string.Empty, PROJECT_FILE_XML);

        // Assert
        StringAssert.Contains(referenceElement, referencedProject, $"Missing reference element: {referenceElement}.");
        StringAssert.Contains(referenceFile, referencedProject, $"Missing reference file: {referenceFile}.");
    }
}