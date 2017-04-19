# dotnet does not support net4*, so we remove it as a target
find . -type f -name '*.csproj' -exec sed -i.bak 's/net452;//' {} +

# The generated *.AssemblyInfo.cs causes StyleCop Analyzers to complain
# https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/2314
sed -e 's/Id="SA1633" Action="Error"/Id="SA1633" Action="Hidden"/' -i.bak FluentRestBuilder.ruleset
sed -e 's/Id="SA1412" Action="Warning"/Id="SA1412" Action="Hidden"/' -i.bak FluentRestBuilder.ruleset
