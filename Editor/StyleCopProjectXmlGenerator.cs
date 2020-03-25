using System.IO;
using System.Xml.Linq;

namespace AIR.StyleCopAnalyzer.Editor {
    public class StyleCopProjectXmlGenerator {
        const string INCLUDE_ATTRIBUTE_NAME = "Include";
        private readonly XDocument _projectXmlDocument;
        private readonly DirectoryInfo _packageDirectory;
        private readonly XNamespace _xNamespace;
        private readonly XElement _xDocumentRoot;

        public StyleCopProjectXmlGenerator(string projectXmlString) {
            _projectXmlDocument = XDocument.Parse(projectXmlString);

            var packagePath = "./Packages/com.air.stylecop";
            _packageDirectory = new DirectoryInfo(packagePath);

            _xDocumentRoot = _projectXmlDocument.Root;
            if (_xDocumentRoot == null) return;
            _xNamespace = _xDocumentRoot.Name.NamespaceName;
        }

        public override string ToString() {
            return _projectXmlDocument.ToString();
        }

        public void ReferenceStyleCopDlls() {
            if (!_packageDirectory.Exists)
                throw new DirectoryNotFoundException(_packageDirectory.FullName);

            var itemGroup = new XElement(_xNamespace + "ItemGroup");

            var analyzerDllPath = _packageDirectory.FullName + "/StyleCop.Analyzers.dll";
            var analyzerDllFile = new FileInfo(analyzerDllPath);
            if (!analyzerDllFile.Exists)
                throw new FileNotFoundException(analyzerDllFile.FullName);
            AddAssemblyToAnalyzerGroup(itemGroup, analyzerDllFile);

            var codeFixesDllPath = _packageDirectory.FullName + "/StyleCop.Analyzers.CodeFixes.dll";
            var codeFixesDllFile = new FileInfo(codeFixesDllPath);
            if (!codeFixesDllFile.Exists)
                throw new FileNotFoundException(codeFixesDllFile.FullName);
            AddAssemblyToAnalyzerGroup(itemGroup, codeFixesDllFile);

            _xDocumentRoot.Add(itemGroup);
        }

        public void ReferenceStyleCopJsonRules() {
            if (!_packageDirectory.Exists)
                throw new DirectoryNotFoundException(_packageDirectory.FullName);

            var jsonRulesPath = _packageDirectory.FullName + "/stylecop.json";
            var jsonRulesFile = new FileInfo(jsonRulesPath);
            if (!jsonRulesFile.Exists)
                throw new FileNotFoundException(jsonRulesFile.FullName);

            var itemGroup = new XElement(_xNamespace + "ItemGroup");
            var jsonRulesReference = new XElement(_xNamespace + "AdditionalFiles");
            jsonRulesReference.Add(new XAttribute(INCLUDE_ATTRIBUTE_NAME, jsonRulesFile.FullName));
            itemGroup.Add(jsonRulesReference);

            _xDocumentRoot.Add(itemGroup);
        }

        private void AddAssemblyToAnalyzerGroup(
            XElement itemGroup,
            FileInfo dllFile
        ) {
            if (!dllFile.Exists) return;
            var analyzerReference = new XElement(_xNamespace + "Analyzer");
            analyzerReference.Add(new XAttribute(INCLUDE_ATTRIBUTE_NAME, dllFile.FullName));
            itemGroup.Add(analyzerReference);
        }
    }
}