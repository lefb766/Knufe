using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Versioning;
using Xunit;

namespace Knufe.Test
{
    public class FrameworkDirectoryTest
    {
        [Fact]
        public void InitializeTest()
        {
            var path = Path.Combine(new[] { "hoge", "lib", "net40" });
            var frameworkDir = new FrameworkDirectory(path);

            Assert.Equal(path, frameworkDir.Path);
            Assert.Equal(".NETFramework", frameworkDir.FrameworkName.Identifier);
            Assert.Equal(new Version(4, 0), frameworkDir.FrameworkName.Version);

            var net20 = new FrameworkName(".NETFramework", new Version(2, 0));
            var net40 = new FrameworkName(".NETFramework", new Version(4, 0));
            var net451 = new FrameworkName(".NETFramework", new Version(4, 5, 1));

            Assert.False(frameworkDir.IsAvailableOn(net20));
            Assert.True(frameworkDir.IsAvailableOn(net40));
            Assert.True(frameworkDir.IsAvailableOn(net451));
        }
    }
}
