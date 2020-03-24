using UnityEditor;

namespace AIR.StyleCopAnalyzer.Editor {
    [InitializeOnLoad]
    public class CsProjectPostProcessor : AssetPostprocessor {
        static CsProjectPostProcessor() { }

        private static string OnGeneratedCSProject(string path, string contents) {
            var xmlGenerator = new StyleCopProjectXmlGenerator(contents); 
            xmlGenerator.ReferenceStyleCopDlls(); 
            return xmlGenerator.ToString();
        }
    }
}

