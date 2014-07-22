using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Versioning;
using NuGet;

namespace Knufe
{
    public class FrameworkDirectory
    {
        public FrameworkDirectory(string path)
        {
            Path = path;
            path = System.IO.Path.GetFileName(path);
            FrameworkName = VersionUtility.ParseFrameworkName(path);
        }

        public string Path { get; private set; }
        public FrameworkName FrameworkName { get; private set; }

        static FrameworkName currentFramework =
            new FrameworkName(".NETFramework", Environment.Version);

        public bool IsAvailableOnCurrentFramework()
        {
            return IsAvailableOn(currentFramework);
        }

        public bool IsAvailableOn(FrameworkName framework)
        {
            bool sameFramework = framework.Identifier == FrameworkName.Identifier;
            bool sameOrNewerVersion = framework.Version >= FrameworkName.Version;
            return sameFramework && sameOrNewerVersion;
        }
    }
}
