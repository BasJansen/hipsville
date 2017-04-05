#tool nuget:?package=Wyam
#addin nuget:?package=Cake.Wyam
#addin nuget:?package=Cake.Git
#addin nuget:?package=Cake.FileHelpers

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var output = DirectoryPath.FromString("./docs");
var input = DirectoryPath.FromString("./input");
//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Build")
    .Does(() =>
    {
        var cname = File(input+"/CNAME");
        FileWriteText(cname, "www.hipsville.com");

        Wyam(new WyamSettings
        {            
            UpdatePackages = true,
            OutputPath = output
        });        
    });
    
Task("Preview")
    .Does(() =>
    {
        var cname = File(input+"/CNAME");
        FileWriteText(cname, "www.hipsville.com");

        Wyam(new WyamSettings
        {            
            UpdatePackages = false,
            Preview = true,
            Watch = true,
            OutputPath = output
        });
    });

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Preview");    

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);