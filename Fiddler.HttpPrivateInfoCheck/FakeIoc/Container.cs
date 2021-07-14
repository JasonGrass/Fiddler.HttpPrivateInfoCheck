using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiddler.HttpPrivateInfoCheck.FakeIoc
{
    static class Container
    {
        public static ICheckInformationView View { get; set; }
    }
}
