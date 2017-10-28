//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");


//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////
// Define directories.
var sourceDir = Directory("./Source/");
var artifactDir = Directory("./Artifacts/");
var testsResultsDir = MakeAbsolute(artifactDir + Directory("TestResults"));

// Define variable.
var solutionFileName = "WordBrainSolver.sln";
var solutionFilePath = sourceDir + File(solutionFileName);


//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////
Task("Clean")
    .Does(() =>
	{
		Information($"Cleaning: {artifactDir}");
	    CleanDirectory(artifactDir);

	    var allProjectBinFolders = GetDirectories($"{sourceDir}/**/bin");
	    foreach(var projectBinFolder in allProjectBinFolders)
        {
			Information($"Cleaning: {projectBinFolder}");
        	CleanDirectory(projectBinFolder);
        }
	});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
	{
	    NuGetRestore(solutionFilePath);
	});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
	{
	    if(IsRunningOnWindows())
	    {
	    	MSBuild(solutionFilePath, settings => settings.SetConfiguration(configuration));
	    }
	    else
	    {
	    	XBuild(solutionFilePath, settings => settings.SetConfiguration(configuration));
	    }
	});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
	{
		var projects = GetFiles($"{sourceDir}/*.Tests/*.csproj");
        foreach(var project in projects)
        {
            DotNetCoreTest(project.FullPath, 
			new DotNetCoreTestSettings() {
				Configuration = configuration,
				NoBuild = true,
				ArgumentCustomization  = args => args.Append($"-r {testsResultsDir} -l trx")
				});
        }
	});


//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////
Task("Default")
    .IsDependentOn("Run-Unit-Tests");


//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////
RunTarget(target);