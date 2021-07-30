using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiddler.HttpPrivateInfoCheck.View.CheckInformation
{
    /// <summary>
    /// 检查结果信息
    /// </summary>
    class CheckInfoEntry
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public string DetailTitle { get; set; }

        public string DetailContent { get; set; }
    }
}
