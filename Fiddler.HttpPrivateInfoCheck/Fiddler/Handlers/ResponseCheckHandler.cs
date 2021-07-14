using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiddler.HttpPrivateInfoCheck.Fiddler.Handlers
{
    class ResponseCheckHandler: BaseCheckHandler
    {
        protected override string Prefix { get; } = "Response";

        public ResponseCheckHandler(string url, IList<HTTPHeaderItem> headers, string body) : base(url, headers, body)
        {
        }

    }
}
