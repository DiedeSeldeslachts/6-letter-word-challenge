using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6LetterWords.WordSegmentParsing
{
    public class IWordSegmentResult
    {
        public IReadOnlyDictionary<int, List<string>> CategorizedWords { get; protected set;  }

        public IReadOnlyDictionary<string, IReadOnlyDictionary<int, string>> SegmentedMaxLengthWords { get; protected set; }

    }
}
