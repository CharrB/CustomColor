using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichTextBoxSample
{
    public class ErrorMessage : BasicMessage
    {
        public ErrorMessage(string _content) : base(_content)
        {
            Controls.Add(new Tuple<string, string>("Foreground", "Red"));
        }
    }
}
