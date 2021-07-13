using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiddler.HttpPrivateInfoCheck.Configurations
{
    /// <summary>
    /// 全局配置
    /// </summary>
    public class GlobalConfiguration
    {
        /// <summary>
        /// 需要进行检查的匹配域名
        /// </summary>
        public IList<string> MatchHosts { get; set; }

        /// <summary>
        /// 匹配检查的规则
        /// </summary>
        public IList<CheckRule> CheckRules { get; set; }

    }
}
