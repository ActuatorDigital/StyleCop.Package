// <copyright file="/media/Work/AIR-Hex/Packages/com.air.stylecop/Tests/StyleCopProjectPostProcessorTests.cs" company="AIR Pty Ltd">
// Copyright (c) AIR Pty Ltd. All rights reserved.
// </copyright>

using AIR.StyleCopAnalyzer.Editor;
using NUnit.Framework;

[TestFixture]
public class StyleCopProjectPostProcessorTests {
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

    // [Test]
    // public void OnGeneratedCSProject_EmptyProject_AddsStyleCopReferences() {
    //     // Act
    //     var referencedProject = StyleCopProjectPostProcessor
    //         .OnGeneratedCSProject(string.Empty, PROJECT_FILE_XML);
    //
    //     // Assert
    //     StringAssert.Contains(referencedProject, "<AdditionalFiles Include=", "Additional files reference missing.");
    //     StringAssert.Contains(referencedProject, "/StyleCop.Analyzers.dll", "/StyleCop.Analyzers.dll not mentioned in output.");
    // }
}