using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{
    public class Trigramm
    {
        public string TrigrammLine { get; set; }
        public int TrigrammPos { get; set; }

        public static IEnumerable<Trigramm> GetTrigramms(string word)
        {
            if (word.Length < 3) throw new Exception("Word for trigramm should be more than 3");
            for (var i = 0; i < word.Length-2; i++)
            {
                yield return new Trigramm
                {
                    TrigrammLine = word[i..(i + 3)],
                    TrigrammPos = i
                };
            }
        }

        public static IEnumerable<string> GetWords(string word, Dictionary<string, Dictionary<int, List<string>>> dictionary)
        {
            var listOfWords = new List<string>();
            var enumerable = GetTrigramms(word);
            foreach (var trigramm in enumerable)
            {
                if (!dictionary.TryGetValue(trigramm.TrigrammLine, out var posDict)) continue;
                if(posDict.TryGetValue(trigramm.TrigrammPos, out var wordCollect))
                    listOfWords.AddRange(wordCollect);
            }

            return new List<string>(listOfWords.Distinct());
        }
    }
}