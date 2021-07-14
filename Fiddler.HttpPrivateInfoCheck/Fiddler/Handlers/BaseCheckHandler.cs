using Fiddler.HttpPrivateInfoCheck.Configurations;
using Fiddler.HttpPrivateInfoCheck.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fiddler.HttpPrivateInfoCheck.Fiddler.Handlers
{
    abstract class BaseCheckHandler
    {

        protected abstract string Prefix { get; }

        public IList<HTTPHeaderItem> Headers { get; }

        public string Body { get; }

        public string Url { get; }

        protected BaseCheckHandler(string url, IList<HTTPHeaderItem> headers, string body)
        {
            Url = url;
            Headers = headers;
            Body = body;
        }

        public HttpCheckInfo Check()
        {
            var checkHeaders = CheckHeaders();
            var checkBody = CheckBody();

            // 提示信息
            var messageBuilder = new StringBuilder();
            if (checkHeaders.Count > 0)
            {
                messageBuilder.Append("[Header] ");
                messageBuilder.Append(string.Join(";", checkHeaders));
            }
            if (checkBody.Count > 0)
            {
                messageBuilder.Append("[Body] ");
                messageBuilder.Append(string.Join(";", checkBody));
            }

            // 详情（重复一遍提示信息）
            var detailBuilder = new StringBuilder();
            if (checkHeaders.Count > 0)
            {
                detailBuilder.AppendLine("[Header] ");
                detailBuilder.AppendLine(string.Join(";", checkHeaders));
            }
            if (checkBody.Count > 0)
            {
                detailBuilder.AppendLine("[Body] ");
                detailBuilder.AppendLine(string.Join("\r\n", checkBody));
            }

            // 详情（Header 和 Body 的内容）
            detailBuilder.AppendLine("---------------------------");
            if (checkHeaders.Count > 0)
            {
                detailBuilder.AppendLine("[Header]");
                detailBuilder.AppendLine(string.Join("\r\n", Headers.Select(h => $"{h.Name}: {h.Value}")));
            }
            if (checkBody.Count > 0)
            {
                detailBuilder.AppendLine("[Body]");
                detailBuilder.AppendLine(Body);
            }

            if (checkHeaders.Count < 1 && checkBody.Count < 1)
            {
                return null;
            }

            var info = new HttpCheckInfo
            {
                RequestUrl = $"[{Prefix}] {Url}",
                Message = messageBuilder.ToString(),
                Detail = detailBuilder.ToString()
            };

            return info;
        }

        private List<string> CheckHeaders()
        {
            List<string> messages = new List<string>(0);
            foreach (var header in Headers)
            {
                var check = Check($"{header.Name} {header.Value}");
                messages.AddRange(check);
            }
            return messages;
        }

        private List<string> CheckBody()
        {
            return Check(Body);
        }

        private List<string> Check(string content)
        {
            List<string> messages = new List<string>(0);
            var rules = ConfigurationsManager.Instance.Configurations.CheckRules;
            foreach (var checkRule in rules)
            {
                var tuple = IsMatchCheckRule(checkRule, content);
                if (tuple.Item1)
                {
                    messages.Add(tuple.Item2);
                }
            }
            return messages;
        }

        private (bool, string) IsMatchCheckRule(CheckRule rule, string content)
        {
            if (rule.MatchType == RuleMatchType.String)
            {
                var contains = content.ToLower().Contains(rule.Value.ToLower());
                return (contains, rule.Message);
            }
            else
            {
                try
                {
                    var isMatch = Regex.IsMatch(content, rule.Value);
                    return (isMatch, rule.Message);
                }
                catch (Exception ex)
                {
                    return (true, $"正则表达式匹配出现异常，({ex.GetType().Name}){ex.Message}");
                }
            }
        }
    }
}
