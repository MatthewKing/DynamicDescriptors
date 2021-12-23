$root = Resolve-Path (Join-Path $PSScriptRoot "..")
$output = "$root/artifacts"
$projects = @(
    "$root/src/DynamicDescriptors/DynamicDescriptors.csproj"
)

foreach ($project in $projects) {
    dotnet pack $project --configuration Release --output $output -p:ContinuousIntegrationBuild=true
}
