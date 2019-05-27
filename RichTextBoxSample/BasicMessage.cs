using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichTextBoxSample
{
    public class BasicMessage : IRichMessage
    {
        string content;

        protected List<Tuple<string, string>> Controls =
            new List<Tuple<string, string>>();

        public BasicMessage(string _content)
        {
            this.content = _content;
        }

        public string Content => content;

        public int Properties => Controls.Count;

        public IEnumerable<Tuple<string, string>> GetProperties()
        {
            return Controls;
        }
    }
}
