#tool nuget:?package=Wyam
#addin nuget:?package=Cake.Wyam
#addin nuget:?package=Cake.Git
#addin nuget:?package=Cake.FileHelpers

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var output = DirectoryPath.FromString("./docs");
//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Build")
    .Does(() =>
    {
        Wyam(new WyamSettings
        {            
            UpdatePackages = true,
            OutputPath = output
        });        
        
        var cname = File(output+"/CNAME");
        FileWriteText(cname, "www.hipsville.com");
    });
    
Task("Preview")
    .Does(() =>
    {
        Wyam(new WyamSettings
        {            
            UpdatePackages = false,
            Preview = true,
            Watch = true,
            OutputPath = output
        });

        var cname = File(output+"/CNAME");
        FileWriteText(cname, "www.hipsville.com");
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