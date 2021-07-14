using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiddler.HttpPrivateInfoCheck.Configurations;

namespace Fiddler.HttpPrivateInfoCheck.Fiddler.Utils
{
    class ConfigurationHelper
    {
        /// <summary>
        /// 是否是关心的 url 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsMatch(string url)
        {
            var hosts = ConfigurationsManager.Instance.Configurations.MatchHosts;
            foreach (var host in hosts)
            {
                if (url.ToLower().Contains(host.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
