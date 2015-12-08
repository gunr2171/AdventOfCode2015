using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using TCL.Extensions;
using System.Reflection;

namespace AdventOfCode.Day08.Tests
{
    [TestFixture]
    public class Day08Tests
    {
        private List<Example> examples;

        [OneTimeSetUp]
        public void LoadInExamples()
        {
            var samplesFilePath = Path.Combine(AssemblyDirectory, "Samples.csv");

            examples = File.ReadAllLines(samplesFilePath)
                .Select(x => x.Split(','))
                .Skip(1)
                .Select(x => new Example
                {
                    Input = x[0],
                    ExpectedLiterialCharCount = x[1].Parse<int>(),
                    ExpectedMemoryCharCount = x[2].Parse<int>()
                })
                .ToList();
        }

        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void GetLiteralCharacterCount(int exampleIndex)
        {
            var example = examples[exampleIndex];
            var actual = Processor.GetLiteralCharacterCount(example.Input);
            Assert.AreEqual(example.ExpectedLiterialCharCount, actual);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void GetMemoryCharacterCount(int exampleIndex)
        {
            var example = examples[exampleIndex];
            var actual = Processor.GetMemoryCharacterCount(example.Input);
            Assert.AreEqual(example.ExpectedMemoryCharCount, actual);
        }
    }

    public class Example
    {
        public string Input { get; set; }
        public int ExpectedLiterialCharCount { get; set; }
        public int ExpectedMemoryCharCount { get; set; }
    }
}
