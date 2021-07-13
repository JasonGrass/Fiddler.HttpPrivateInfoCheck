using Fiddler.HttpPrivateInfoCheck.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiddler.HttpPrivateInfoCheck.View.ConfigurationSettings
{
    class MatchTypeInfoHelper
    {
        public RuleMatchType MatchType { get; }

        public string MatchTypeDescription { get; }

        public MatchTypeInfoHelper(RuleMatchType matchType, string matchTypeDescription)
        {
            MatchType = matchType;
            MatchTypeDescription = matchTypeDescription;
        }

        public override string ToString()
        {
            return MatchTypeDescription;
        }
    }
}
