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
        private  HashSet<string> _hashSet;
        private  HashSet<string> _littleHash = new HashSet<string>();

        private  Dictionary<string, Dictionary<int, List<string>>> _dictionary =
            new Dictionary<string, Dictionary<int, List<string>>>();

        [GlobalSetup]
        public void SetUp()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _hashSet = new HashSet<string>(File.ReadAllLines("Rus.txt", Encoding.GetEncoding(1251)));
            ComputateBaseTrigrams();
        }

        private  void ComputateBaseTrigrams()
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
        public string ParamWord { get; set; }

        
        public int Test1() => 1;
        [Benchmark]
        public List<string> Test() => Run(ParamWord);
       
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