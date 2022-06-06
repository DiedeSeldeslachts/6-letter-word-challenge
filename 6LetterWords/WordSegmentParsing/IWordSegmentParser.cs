using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6LetterWords.WordSegmentParsing
{
    public interface IWordSegmentParser
    {
        public int MaxWordSize { get; }
        public int MinSegmentSize { get; }
        public IWordSegmentResult Parse(string fileName);
    }
}
