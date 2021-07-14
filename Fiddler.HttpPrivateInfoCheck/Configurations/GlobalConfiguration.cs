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
        /// 是否开启检查
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 需要进行检查的匹配域名
        /// </summary>
        public IList<string> MatchHosts { get; set; } = new List<string>(0);

        /// <summary>
        /// 排除的 Host 域名
        /// </summary>
        public IList<string> ExcludeHosts { get; set; } = new List<string>(0);

        /// <summary>
        /// 匹配检查的规则
        /// </summary>
        public IList<CheckRule> CheckRules { get; set; } = new List<CheckRule>(0);

    }
}
