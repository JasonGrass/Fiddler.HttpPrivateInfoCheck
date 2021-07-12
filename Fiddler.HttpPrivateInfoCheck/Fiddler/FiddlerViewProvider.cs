using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiddler.HttpPrivateInfoCheck.View;
using Jgrass.FiddlerPlugin;

namespace Fiddler.HttpPrivateInfoCheck.Fiddler
{
    class FiddlerViewProvider: IFiddlerViewProvider
    {
        public IList<FiddlerTabPage> BuildFiddlerTabPages()
        {
            return BuildFiddlerViews();
        }

        public IList<FiddlerTabPage> BuildFiddlerViews()
        {
            var page = new FiddlerTabPage("HTTP 敏感信息监测", new MainView())
            {
                TabIcon = SessionIcons.Information
            };

            return new List<FiddlerTabPage>() {page};
        }
    }
}
