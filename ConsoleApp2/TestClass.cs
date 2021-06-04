using System.Collections.Generic;

namespace ConsoleApp2
{
    public class TestClass
    {
        public IEnumerable<string> Run(string word, HashSet<string> bigHash, HashSet<string> littleHash,
            Dictionary<string, Dictionary<int, List<string>>> dictionary)
        {
            return bigHash.TryGetValue(word, out var targetWord)
                ? targetWord.ToOneEnum()
                : LevClass.GetPrediction(word,
                    word != null && word.Length < 3 ? littleHash : Trigramm.GetWords(word, dictionary));
        }
    }
}