using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Running;

namespace ConsoleApp2
{
    class Program
    {
        public static void Main(string[] args)
        {
            //var summary = BenchmarkRunner.Run<TestClass>();
            var testClass = new TestClass();
            testClass.SetUp();


            //var list = testClass.Run("добычч", true);
            var a1 = CheckResult("./Rus1.txt",testClass);
            Console.WriteLine($"2------------------------");
            var a2 =CheckResult("./Rus2.txt",testClass);
            Console.WriteLine($"3------------------------");
            var a3 =CheckResult("./RusDouble.txt",testClass);
            Console.WriteLine($"4------------------------");
            var a4 =CheckResult("./RusLose.txt",testClass);
            Console.WriteLine($"5------------------------");
            var a5 =CheckResult("./RusSwap.txt",testClass);
            Console.WriteLine($"Dlev------------------------");
            var d1 = CheckResult("./Rus1.txt",testClass,true);
            Console.WriteLine($"2------------------------");
            var d2 =CheckResult("./Rus2.txt",testClass,true);
            Console.WriteLine($"3------------------------");
            var d3 =CheckResult("./RusDouble.txt",testClass,true);
            Console.WriteLine($"4------------------------");
            var d4 =CheckResult("./RusLose.txt",testClass,true);
            Console.WriteLine($"5------------------------");
            var d5 =CheckResult("./RusSwap.txt",testClass,true);
            Console.WriteLine($"{a1} {a2} {a3} {a4}  {a5}");
            Console.WriteLine($"{d1} {d2} {d3} {d4}  {d5}");
        }

        private static double CheckResult(string path, TestClass test, bool isDlev = false)
        {
            var count = 0.0;
            var res = 0.0;
            var next = new Random().Next(10000);
            var lines = File.ReadAllLines(path).Skip(next).Take(5000);
            foreach (var line in lines)
            {
                var strings = line.Split(" ", 2, StringSplitOptions.RemoveEmptyEntries);
                if (strings[0] == strings[1])
                    continue;
                var list = test.Run(strings[1],isDlev);
                if (list == null || list.Count == 0)
                {
                    continue;
                }
                var result = list.First();
                count++;
                if (strings[0] == result)
                    res++;
                Console.WriteLine(strings[0]);
                Console.WriteLine(res / count * 100);
            }

            return res / count * 100;
        }
    }
}