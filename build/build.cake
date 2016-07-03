var target = Argument("target", "Finalize");
var configuration = Argument("configuration", "Release");
var sourceDir = Directory("../src/DynamicDescriptors");
var workingDir = Directory("./temp");
var outputDir = Directory("./output");
var solutionFile = File("../DynamicDescriptors.sln");

Task("Clean")
    .Does(() =>
    {
        CleanDirectory(workingDir);
        CleanDirectory(outputDir);
    });

Task("PackageRestore")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        NuGetRestore(solutionFile);
    });

Task("Build")
    .IsDependentOn("PackageRestore")
    .Does(() =>
    {
        MSBuild(solutionFile, settings =>
        {
            settings.SetConfiguration(configuration);
            settings.WithProperty("OutputPath", MakeAbsolute(workingDir).FullPath);
        });
    });

Task("NuGetPack")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var assemblyInfoFile = sourceDir + File("./Properties/AssemblyInfo.cs");
        var assemblyInfo = ParseAssemblyInfo(assemblyInfoFile);

        var nuspec = File("./DynamicDescriptors.nuspec");

        var settings = new NuGetPackSettings();
        settings.OutputDirectory = MakeAbsolute(outputDir).FullPath;
        settings.Version = assemblyInfo.AssemblyInformationalVersion;
        settings.Files = new[]
        {
            new NuSpecContent() { Source = workingDir + File("DynamicDescriptors.dll"), Target = "lib/net40" },
            new NuSpecContent() { Source = workingDir + File("DynamicDescriptors.xml"), Target = "lib/net40" },
        };

        NuGetPack(nuspec, settings);
    });

Task("Finalize")
    .IsDependentOn("NuGetPack")
    .Does(() =>
    {
        DeleteDirectory(workingDir, true);
    });

RunTarget(target);
