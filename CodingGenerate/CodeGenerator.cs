using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;

namespace CodeAnalyzer
{
    public static class CodeGenerator
    {
        private static ArgumentListSyntax CreateSingleArgument(string value)
        {
            return SyntaxFactory.ArgumentList(
                arguments:
                    SyntaxFactory.SingletonSeparatedList(
                        node:
                            SyntaxFactory.Argument(
                                expression:
                                    SyntaxFactory.LiteralExpression(
                                        kind:
                                            SyntaxKind.StringLiteralExpression,
                                        token:
                                            SyntaxFactory.Literal(value)))));
        }
        public static SyntaxTree CreateTree()
        {
            MemberAccessExpressionSyntax? ConsoleWriteLine = SyntaxFactory.MemberAccessExpression(
                kind:
                    SyntaxKind.SimpleMemberAccessExpression,
                expression:
                    SyntaxFactory.IdentifierName(
                        name: 
                            nameof(Console).ToString()),
                name:    
                    SyntaxFactory.IdentifierName(
                        name:
                            nameof(Console.WriteLine)));

            ExpressionStatementSyntax? statement = SyntaxFactory.ExpressionStatement(
                expression:
                    SyntaxFactory.InvocationExpression(ConsoleWriteLine)
                    .WithArgumentList(
                        argumentList:
                            CreateSingleArgument("Hello World")));

            MethodDeclarationSyntax? main = SyntaxFactory.MethodDeclaration(
               returnType:
                    SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
               identifier:
                    nameof(Program.Main))
                    .AddModifiers(
                        items:
                            SyntaxFactory.Token(
                                kind:
                                    SyntaxKind.StaticKeyword))
                    .AddBodyStatements(
                                items:
                                    statement);

            ClassDeclarationSyntax? program = SyntaxFactory.ClassDeclaration(
                identifier:
                    nameof(Program))
                .AddMembers(main);

            NamespaceDeclarationSyntax? rootNameSpace = SyntaxFactory.NamespaceDeclaration(
                name:
                    SyntaxFactory.IdentifierName(
                        name:
                            nameof(CodeAnalyzer)));

            rootNameSpace = rootNameSpace.AddMembers(
                items:
                    program);

            UsingDirectiveSyntax? usingDirective = SyntaxFactory.UsingDirective(
                name:
                    SyntaxFactory.ParseName(
                        text:
                            nameof(System)));

            SyntaxTrivia comment = SyntaxFactory.Comment("// Generating Code - Do not Edit");

            CompilationUnitSyntax? unit = SyntaxFactory.CompilationUnit()
                                    .AddUsings(usingDirective)
                                    .AddMembers(rootNameSpace)
                                    .WithLeadingTrivia(comment);

            return unit.SyntaxTree;

        }

    }
}
