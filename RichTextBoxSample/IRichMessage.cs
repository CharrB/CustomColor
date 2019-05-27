using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichTextBoxSample
{
    public interface IRichMessage
    {
        // not very nice because it sauys nothing what do I mean by Tuple
        // <stirng, sting>... anyway let's keep it for the sake of 
        // simplicity of this example
        IEnumerable<Tuple<string,string>> GetProperties();
        string Content { get; }
        int Properties { get; }
    }
}
