// The following environment variables need to be set for Publish target:
// WYAM_GITHUB_TOKEN

//#tool "nuget:https://api.nuget.org/v3/index.json?package=Wyam&version=1.6.0"
//#addin "nuget:https://api.nuget.org/v3/index.json?package=Cake.Wyam&version=1.6.0"
#tool nuget:?package=Wyam&version=2.2.9
#addin nuget:?package=Cake.Wyam&version=2.2.12
#addin nuget:?package=Cake.FileHelpers&version=4.0.1

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var output = DirectoryPath.FromString("./docs");
var input = DirectoryPath.FromString("./input");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("CreateCname")    
    .Does(() =>
    {
        var cname = File(input+"/CNAME");
        FileWriteText(cname, "www.hipsville.com");  
    });


Task("Build")    
    .IsDependentOn("CreateCname")
    .Does(() =>
    {        
        Wyam(new WyamSettings
        {   
            UpdatePackages = true,
            OutputPath = output
        });        
    });
    
Task("Preview")    
    .IsDependentOn("CreateCname")
    .Does(() =>
    {
        Wyam(new WyamSettings
        {            
            UpdatePackages = true,
            Preview = true,
            Watch = true,            
            OutputPath = output
        });
    });

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Build");    

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);