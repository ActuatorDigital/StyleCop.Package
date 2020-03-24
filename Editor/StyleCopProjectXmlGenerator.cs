using System.IO;
using System.Xml.Linq;
using UnityEngine;

namespace AIR.StyleCopAnalyzer.Editor {
    public class StyleCopProjectXmlGenerator {
        
        private readonly XDocument _projectXmlDocument;

        public StyleCopProjectXmlGenerator(string projectXmlString) {
            _projectXmlDocument = XDocument.Parse(projectXmlString);
        }

        public override string ToString() {
            return _projectXmlDocument.ToString();
        }

        public void ReferenceStyleCopDlls() {
            var packagePath = "./Packages/com.air.stylecop";
            var packageDirectory = new DirectoryInfo(packagePath);
            if (!packageDirectory.Exists) return;

            XElement xDocumentRoot = _projectXmlDocument.Root;
            if (xDocumentRoot == null) return;

            XNamespace xNamespace = xDocumentRoot.Name.NamespaceName;
            XElement itemGroup = new XElement(xNamespace + "ItemGroup");

            var analyzerDllPath = packageDirectory.FullName + "/StyleCop.Analyzers.dll";
            var analyzerDllFile = new FileInfo(analyzerDllPath);
            AddAssemblyToAnalyzerGroup(xNamespace, itemGroup, analyzerDllFile);

            var codeFixesDllPath = packageDirectory.FullName + "/StyleCop.Analyzers.CodeFixes.dll";
            var codeFixesDllFile = new FileInfo(codeFixesDllPath);
            AddAssemblyToAnalyzerGroup(xNamespace, itemGroup, codeFixesDllFile);

            xDocumentRoot.Add(itemGroup);
        }

        private static void AddAssemblyToAnalyzerGroup(
            XNamespace xDocumentRoot,
            XElement itemGroup,
            FileInfo dllFile
        ) {
            if (dllFile.Exists) {
                var analyzerReference = new XElement(xDocumentRoot + "Analyzer");
                const string INCLUDE_ATTRIBUTE_NAME = "Include";
                analyzerReference.Add(new XAttribute(INCLUDE_ATTRIBUTE_NAME, dllFile.FullName));
                itemGroup.Add(analyzerReference);
            }
        }
    }
}
