using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;
using System.Reflection;

namespace CodeAnalyzer
{
    public class Program
    {
        
        //static void BuildAssembly(SyntaxTree syntaxTree, string assemblyName)
        //{
        //    List<MetadataReference> references = new List<MetadataReference>() {
        //        MetadataReference.CreateFromFile(path: Assembly.Load("System.Private.Corelib").Location),
        //        MetadataReference.CreateFromFile(path: Assembly.Load("System.Runtime").Location),
        //        MetadataReference.CreateFromFile(path: Assembly.Load("System.Console").Location),
        //    };
        //    CSharpCompilation compilation = CSharpCompilation.Create(
        //        assemblyName: assemblyName,
        //        syntaxTrees: new[] { syntaxTree },
        //        references: references,
        //        options: new CSharpCompilationOptions(OutputKind.ConsoleApplication)
        //        );

        //    Directory.CreateDirectory("Output");

        //    using FileStream fileStream = new FileStream(Path.Combine("Output" , assemblyName),FileMode.Create);

        //    EmitResult result = compilation.Emit(fileStream);


        //    foreach (var dignostic in result.Diagnostics)
        //    {
        //        Console.WriteLine($"{dignostic.Id} : {dignostic.GetMessage()}");
        //    }
        //}


        public static void Main()
        {
            SyntaxTree tree = CodeGenerator.CreateTree();

            Console.WriteLine(tree.GetRoot().NormalizeWhitespace().ToFullString());


            //BuildAssembly(tree, "MyHelloWorld.dll");
        }
    }
}
