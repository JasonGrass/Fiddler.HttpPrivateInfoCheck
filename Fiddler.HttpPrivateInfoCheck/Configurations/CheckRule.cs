#pragma warning disable MA0048 // File name must match type name


namespace Fiddler.HttpPrivateInfoCheck.Configurations
{
    /// <summary>
    /// 匹配的规则
    /// </summary>
    class CheckRule
    {
        /// <summary>
        /// 用于配置的值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 用于匹配的模式
        /// </summary>
        public string MatchType { get; set; }

        /// <summary>
        /// 是否匹配请求
        /// </summary>
        public bool IsMatchRequest { get; set; } = true;

        /// <summary>
        /// 是否匹配响应
        /// </summary>
        public bool IsMatchResponse { get; set; } = true;

        /// <summary>
        /// 出现匹配时给用户的提示
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// 匹配计算的模式
    /// </summary>
    enum RuleMatchType
    {
        /// <summary>
        /// 字符串比较
        /// </summary>
        String,

        /// <summary>
        /// 正则匹配
        /// </summary>
        Regex,
    }
}
