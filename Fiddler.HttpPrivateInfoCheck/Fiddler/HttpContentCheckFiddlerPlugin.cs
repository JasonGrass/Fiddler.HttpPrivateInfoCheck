using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiddler;
using Jgrass.FiddlerPlugin;

[assembly: RequiredVersion("2.1.8.1")]

namespace Fiddler.HttpPrivateInfoCheck.Fiddler
{
    public class HttpContentCheckFiddlerPlugin: FiddlerPluginApplication
    {
        public override IFiddlerViewProvider GetFiddlerViewProvider()
        {
            return new FiddlerViewProvider();
        }
    }
}
