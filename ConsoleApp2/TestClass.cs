using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;
using Microsoft.Diagnostics.Runtime.DacInterface;

namespace ConsoleApp2
{
    public class TestClass
    {
        private static HashSet<string> _hashSet;
        private static HashSet<string> _littleHash = new HashSet<string>();

        private static Dictionary<string, Dictionary<int, List<string>>> _dictionary =
            new Dictionary<string, Dictionary<int, List<string>>>();

        [GlobalSetup]
        public void SetUp()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _hashSet = new HashSet<string>(File.ReadAllLines("Rus.txt", Encoding.GetEncoding(1251)));
            ComputateBaseTrigrams();

            var testClass = new TestClass();

            //если
            //ладно
            //привет
            // конечно
            // господин
            /*var enumerable = testClass.Run("добычч", _hashSet, _littleHash, _dictionary);
            foreach (var s in enumerable)
            {
                Console.WriteLine(s);
            }*/
        }

        private static void ComputateBaseTrigrams()
        {
            foreach (var word in _hashSet)
            {
                if (word.Length < 3)
                {
                    _littleHash.Add(word);
                }
                else
                {
                    foreach (var trigramm in Trigramm.GetTrigramms(word))
                    {
                        if (_dictionary.ContainsKey(trigramm.TrigrammLine))
                        {
                            var trigrammLocalObject = _dictionary[trigramm.TrigrammLine];
                            if (!trigrammLocalObject.ContainsKey(trigramm.TrigrammPos))
                            {
                                trigrammLocalObject.Add(trigramm.TrigrammPos, new List<string>() {word});
                            }
                            else
                            {
                                trigrammLocalObject[trigramm.TrigrammPos].Add(word);
                            }
                        }
                        else
                        {
                            _dictionary.Add(trigramm.TrigrammLine, new Dictionary<int, List<string>>
                            {
                                {trigramm.TrigrammPos, new List<string>() {word}}
                            });
                        }
                    }
                }
            }
        }

        [Params( "добычч", "прявет", "девхшка", "госппдин")]
        public string paramWord;

        [Benchmark]
        public List<string> Test() => Run(paramWord);
        public  List<string> Run(string word) =>
        
            Run(word, _hashSet, _littleHash, _dictionary).ToList();
        
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