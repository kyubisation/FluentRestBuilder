// <copyright file="OperatorDocumentationGenerator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace OperatorsToRst
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Templating;
    using Xml;

    public class OperatorDocumentationGenerator
    {
        private readonly string targetDirectory;

        public OperatorDocumentationGenerator(string targetDirectory)
        {
            if (!Directory.Exists(targetDirectory))
            {
                throw new InvalidOperationException(
                    $"Missing target directory: '{targetDirectory}'");
            }

            this.targetDirectory = targetDirectory;
        }

        public void GenerateOperatorDocumentation()
        {
            new Generator(this.targetDirectory).Generate();
        }

        private sealed class Generator
        {
            private static readonly RstTemplate Template = new RstTemplate("operators");

            private readonly string targetDirectory;
            private readonly Dictionary<string, OperatorDocumentationFile> files;

            public Generator(string targetDirectory)
            {
                this.targetDirectory = targetDirectory;
                var operatorMethods = new OperatorMethods();
                var xmlContainer = new XmlDocContainer(operatorMethods);
                this.files = operatorMethods
                    .ToDictionary(
                        m => Path.Combine(targetDirectory, $"{m.Key}.rst"),
                        m => new OperatorDocumentationFile(m, xmlContainer));
            }

            public void Generate()
            {
                Console.WriteLine("\nGenerating Operators");
                Console.WriteLine(new string('-', 10));
                this.GenerateDocumentationFiles();
                this.GenerateIndexFile();
                Console.WriteLine(new string('-', 10));
                Console.WriteLine("Done");
                Console.ReadLine();
            }

            private void GenerateDocumentationFiles()
            {
                foreach (var file in this.files)
                {
                    if (File.Exists(file.Key))
                    {
                        Console.WriteLine($"Skipped {file.Key}");
                    }
                    else
                    {
                        File.WriteAllText(file.Key, file.Value.Content(), Encoding.UTF8);
                        Console.WriteLine($"Created {file.Key}");
                    }
                }
            }

            private void GenerateIndexFile()
            {
                var indexFile = Path.Combine(this.targetDirectory, "index.rst");
                var operators = Template.Render(
                    new { files = this.files.Select(f => Path.GetFileName(f.Key)) });
                File.WriteAllText(indexFile, operators, Encoding.UTF8);
                Console.WriteLine($"Created {indexFile}");
            }
        }
    }
}
