$root = Resolve-Path (Join-Path $PSScriptRoot "..")
$output = "$root/artifacts"
$projects = @(
    "$root/src/DynamicDescriptors/DynamicDescriptors.csproj"
)

$timestamp = git log -1 --format=%ct

foreach ($project in $projects) {
    dotnet pack $project --configuration Release --output $output --version-suffix "alpha.$timestamp" -p:ContinuousIntegrationBuild=true
}
