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
        public MainView MainView { get; private set; }

        public IList<FiddlerTabPage> BuildFiddlerTabPages()
        {
            return BuildFiddlerViews();
        }

        public IList<FiddlerTabPage> BuildFiddlerViews()
        {
            MainView = new MainView();
            var page = new FiddlerTabPage("HTTP 敏感信息监测", MainView)
            {
                TabIcon = SessionIcons.Information
            };

            return new List<FiddlerTabPage>() {page};
        }
    }
}
