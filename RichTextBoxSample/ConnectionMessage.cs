using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichTextBoxSample
{
    public class ConnectionMessage : BasicMessage
    {
      public ConnectionMessage(string cntnt) :
            base(cntnt)
        {
            Controls.Add(new Tuple<string, string>("Foreground", "Yellow"));
        }

    }
}
