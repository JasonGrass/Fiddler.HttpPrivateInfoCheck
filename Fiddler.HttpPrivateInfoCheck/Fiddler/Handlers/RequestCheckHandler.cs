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
    class RequestCheckHandler : BaseCheckHandler
    {
        protected override string Prefix { get; } = "Request";

        public RequestCheckHandler(string url, IList<HTTPHeaderItem> headers, string body) : base(url, headers, body)
        {

        }

    }
}
