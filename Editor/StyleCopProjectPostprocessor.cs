// Copyright (c) AIR Pty Ltd. All rights reserved.

using UnityEditor;

namespace AIR.StyleCopAnalyzer.Editor {
    [InitializeOnLoad]
    public class StyleCopProjectPostprocessor : AssetPostprocessor {
        public static string OnGeneratedCSProject(string path, string contents) {
            var xmlGenerator = new StyleCopProjectXmlGenerator(contents);
            xmlGenerator.ReferenceStyleCopDlls();
            xmlGenerator.ReferenceStyleCopJsonRules();
            xmlGenerator.ReferenceStyleCopRuleSet();
            return xmlGenerator.ToString();
        }
    }
}