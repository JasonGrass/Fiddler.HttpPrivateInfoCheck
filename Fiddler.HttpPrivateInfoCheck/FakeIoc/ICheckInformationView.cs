using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiddler.HttpPrivateInfoCheck.ViewModel;

namespace Fiddler.HttpPrivateInfoCheck.FakeIoc
{
    public interface ICheckInformationView
    {
        void AddInfo(HttpCheckInfo info);
    }

}
