// <copyright file="Program.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace OperatorsToRst
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using FluentRestBuilder;
    using Microsoft.Extensions.DependencyInjection;
    using Templating;
    using Xml;

    public class Program
    {
        private readonly ServiceProvider services;

        public Program()
        {
            this.services = new ServiceCollection()
                .AddTransient<OperatorDocumentationGenerator>()
                .AddTransient<OperatorResolver>()
                .AddTransient<DocumentationRenderer>()
                .AddTransient<XmlDocContainer>()
                .AddSingleton<OperatorMethods>()
                .AddSingleton<IEnumerable<Type>>(s => new[]
                    {
                        typeof(DoAsyncOperator),
                        typeof(CacheInDistributedCacheAliases),
                        typeof(DeleteEntityOperator),
                        typeof(MapToRestCollectionOperator),
                    }
                    .SelectMany(t => t.Assembly.ExportedTypes)
                    .ToList())
                .BuildServiceProvider();
        }

        public static void Main(string[] args)
        {
            var program = new Program();
            program.Execute();
        }

        public void Execute()
        {
            var generator = this.services.GetRequiredService<OperatorDocumentationGenerator>();
            var target = GetTargetDirectory();
            generator.Generate(target).Wait();
        }

        private static string GetTargetDirectory()
        {
            var dir = Directory.GetCurrentDirectory();
            do
            {
                var targetDir = Path.Combine(dir, "docs", "operators");
                if (Directory.Exists(targetDir))
                {
                    return targetDir;
                }

                dir = Directory.GetParent(dir).FullName;
            }
            while (true);
        }
    }
}
