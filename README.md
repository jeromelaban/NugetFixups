# Nuget Fixups
This repo is the home of an msbuild task which fixes the [NuGet 4.0 issue 4532](https://github.com/NuGet/Home/issues/4532), where having two new-generation
csproj files referencing each other fail for a missing project.json file that 
does not exist.

# Compiling and using this task
* Open the solution VS2017 RC4 or later
* Use the `Pack` command on the solution's project
* Install the resulting package inside all the projects of your failing solution. 
you may may need to create a nuget local source as part of your nuget.config to include the package.

*Note: If you make changes to this task, make use to kill all your msbuild.exe instances for your changes
to be taken into account.*