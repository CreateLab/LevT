using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BenchmarkDotNet.Running;

namespace ConsoleApp2
{
    class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<TestClass>();
            /*var testClass = new TestClass();
            testClass.SetUp();
            var list = testClass.Run("добычч");
            foreach (var s in list)
            {
                Console.WriteLine(s);
            }*/
        }
    }
}