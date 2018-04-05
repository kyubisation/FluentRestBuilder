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
    using System.Threading.Tasks;
    using Models;
    using Templating;

    public class OperatorDocumentationGenerator
    {
        private readonly OperatorResolver operatorResolver;
        private readonly DocumentationRenderer renderer;
        private string outputDirectory;

        public OperatorDocumentationGenerator(
            OperatorResolver operatorResolver,
            DocumentationRenderer renderer)
        {
            this.operatorResolver = operatorResolver;
            this.renderer = renderer;
        }

        public async Task Generate(string targetDirectory)
        {
            this.outputDirectory = targetDirectory;
            this.AssertTargetDirectory();
            var files = this.operatorResolver.Resolve();
            Console.WriteLine("\nGenerating Operators");
            Console.WriteLine(new string('-', 10));
            await this.GenerateDocumentationFiles(files);
            await this.GenerateIndexFile(files.Keys);
            Console.WriteLine(new string('-', 10));
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private void AssertTargetDirectory()
        {
            if (!Directory.Exists(this.outputDirectory))
            {
                throw new InvalidOperationException(
                    $"Missing target directory: '{this.outputDirectory}'");
            }
        }

        private async Task GenerateDocumentationFiles(
            IReadOnlyDictionary<string, OperatorDocumentation> files)
        {
            foreach (var file in files)
            {
                var filename = Path.Combine(this.outputDirectory, this.ToFilename(file.Key));
                if (File.Exists(filename))
                {
                    Console.WriteLine($"Skipped {file.Key}");
                }
                else
                {
                    var content = await this.renderer.RenderDocumentation(file.Value);
                    File.WriteAllText(filename, content, Encoding.UTF8);
                    Console.WriteLine($"Created {file.Key}");
                }
            }
        }

        private async Task GenerateIndexFile(IEnumerable<string> files)
        {
            var content = await this.renderer.RenderIndex(files.Select(this.ToFilename));
            var indexFile = Path.Combine(this.outputDirectory, this.ToFilename("index"));
            File.WriteAllText(indexFile, content, Encoding.UTF8);
            Console.WriteLine("Created index");
        }

        private string ToFilename(string name) => $"{name}.rst";
    }
}
