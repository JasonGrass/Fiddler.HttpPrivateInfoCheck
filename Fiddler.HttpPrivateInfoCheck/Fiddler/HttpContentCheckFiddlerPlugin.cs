using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiddler;
using Fiddler.HttpPrivateInfoCheck.View;
using Fiddler.HttpPrivateInfoCheck.ViewModel;
using Jgrass.FiddlerPlugin;

[assembly: RequiredVersion("2.1.8.1")]

namespace Fiddler.HttpPrivateInfoCheck.Fiddler
{
    public class HttpContentCheckFiddlerPlugin: FiddlerPluginApplication
    {
        private Func<MainView> MainView { get; set; }

        public override IFiddlerViewProvider GetFiddlerViewProvider()
        {
            var provider = new FiddlerViewProvider();
            MainView = () => provider.MainView;
            return provider;
        }

        public override void AutoTamperRequestAfter(Session oSession)
        {
            if (!oSession.url.Contains("seewo.com"))
            {
                return;
            }

            var headers = oSession.RequestHeaders;
            var requestBody = "";
            try
            {
                requestBody = Encoding.UTF8.GetString(oSession.RequestBody);
            }
            catch (Exception e)
            {
                requestBody = $"[Cannot Get RequestBody] {e.Message}";
            }

            AddInfo(new HttpCheckInfo()
            {
                RequestUrl = $"[Request] {oSession.fullUrl}",
                Message = "测试信息",
                Detail = requestBody
            });
        }

        public override void AutoTamperResponseAfter(Session oSession)
        {
            base.AutoTamperResponseAfter(oSession);
        }

        private void AddInfo(HttpCheckInfo info)
        {
            var view = MainView.Invoke();
            if (view == null)
            {
                return;
            }

            view.Dispatcher.Invoke(() =>
            {
                view.AddInfo(info);
            });
        }

    }
}
