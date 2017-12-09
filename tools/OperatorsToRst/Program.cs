// <copyright file="Program.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace OperatorsToRst
{
    using System.IO;

    public class Program
    {
        public static void Main(string[] args)
        {
            var target = GetTargetDirectory();
            new OperatorDocumentationGenerator(target).GenerateOperatorDocumentation();
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
