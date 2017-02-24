using Microsoft.Build.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NugetFixup
{
    public class NugetIssue4532Fix : Microsoft.Build.Utilities.Task
    {
        [Required]
        public string IntermediatePath { get; set; }

        public override bool Execute()
        {
            string assetFilePath = Path.Combine(IntermediatePath, "project.assets.json");
            var o = JObject.Parse(File.ReadAllText(assetFilePath));

            var q = from compileNode in o.SelectTokens($"..compile").Concat(o.SelectTokens($"..runtime"))
                    from child in compileNode.Children()
                    where (child as JProperty)?.Name?.Contains("bin/placeholder") ?? false
                    select compileNode.Parent;

            q.Distinct().ToList().ForEach(c => c.Remove());

            File.WriteAllText(assetFilePath, o.ToString());

            return true;
        }
    }
}
