using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichTextBoxSample
{
    public class ServerMessage : BasicMessage
    {
        public ServerMessage(string _content) : base(_content)
        {
            Controls.Add(new Tuple<string, string>("Foreground", "Blue"));
        }
    }
}
