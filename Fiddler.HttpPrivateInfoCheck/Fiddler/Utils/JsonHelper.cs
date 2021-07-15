using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Fiddler.HttpPrivateInfoCheck.Fiddler.Utils
{
    public static class JsonHelper
    {
        /// <summary>
        /// 格式化json字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FormatJsonString(string str)
        {
            try
            {
                object obj = JsonConvert.DeserializeObject(str);
                return JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
            }
            catch (Exception e)
            {
                return str;
            }
        }
    }
}
