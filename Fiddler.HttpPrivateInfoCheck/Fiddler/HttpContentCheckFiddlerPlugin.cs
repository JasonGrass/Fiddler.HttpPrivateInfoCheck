using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiddler;
using Fiddler.HttpPrivateInfoCheck.Configurations;
using Fiddler.HttpPrivateInfoCheck.Fiddler.Handlers;
using Fiddler.HttpPrivateInfoCheck.View;
using Fiddler.HttpPrivateInfoCheck.ViewModel;
using Jgrass.FiddlerPlugin;

[assembly: RequiredVersion("2.1.8.1")]

namespace Fiddler.HttpPrivateInfoCheck.Fiddler
{
    public class HttpContentCheckFiddlerPlugin : FiddlerPluginApplication
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
            if (!ConfigurationsManager.Instance.Configurations.IsEnable)
            {
                return;
            }

            if (!Utils.ConfigurationHelper.IsMatch(oSession.url))
            {
                return;
            }

            var headers = oSession.RequestHeaders;
            var body = "";
            try
            {
                body = Encoding.UTF8.GetString(oSession.RequestBody);
            }
            catch (Exception e)
            {
                body = $"[Cannot Get RequestBody] {e.Message}";
            }

            var handler = new RequestCheckHandler(oSession.fullUrl, headers.ToList(), body);
            var info = handler.Check();
            AddInfo(info);
        }

        public override void AutoTamperResponseAfter(Session oSession)
        {
            if (!ConfigurationsManager.Instance.Configurations.IsEnable)
            {
                return;
            }

            if (!Utils.ConfigurationHelper.IsMatch(oSession.url))
            {
                return;
            }

            var headers = oSession.ResponseHeaders;
            var body = "";
            try
            {
                body = Encoding.UTF8.GetString(oSession.ResponseBody);
            }
            catch (Exception e)
            {
                body = $"[Cannot Get ResponseBody] {e.Message}";
            }

            var handler = new ResponseCheckHandler(oSession.fullUrl, headers.ToList(), body);
            var info = handler.Check();
            AddInfo(info);
        }

        private void AddInfo(HttpCheckInfo info)
        {
            if (info == null)
            {
                return;
            }

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
