var target = Argument("target", "Finalize");
var configuration = Argument("configuration", "Release");
var workingDir = Directory("./temp").Path.MakeAbsolute(Context.Environment);
var outputDir = Directory("./output").Path.MakeAbsolute(Context.Environment);
var solutionFile = GetFiles("../*.sln").Single();

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
            settings.WithProperty("OutputPath", workingDir.FullPath);
        });
    });

Task("NuGetPack")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var assemblyInfoFile = File("../src/DynamicDescriptors/Properties/AssemblyInfo.cs");
        var assemblyInfo = ParseAssemblyInfo(assemblyInfoFile);

        var nuspec = File("./DynamicDescriptors.nuspec");

        var settings = new NuGetPackSettings();
        settings.OutputDirectory = outputDir.FullPath;
        settings.Version = assemblyInfo.AssemblyInformationalVersion;
        settings.Files = new[]
        {
            new NuSpecContent() { Source = workingDir.CombineWithFilePath("DynamicDescriptors.dll").FullPath, Target = "lib/net40" },
            new NuSpecContent() { Source = workingDir.CombineWithFilePath("DynamicDescriptors.xml").FullPath, Target = "lib/net40" },
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
