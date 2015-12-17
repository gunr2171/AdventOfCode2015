using System;
using NUnit.Framework;

namespace AdventOfCode.Day17.Tests
{
    [TestFixture]
    public class Day17Tests
    {
        private Storage storage;

        [OneTimeSetUp]
        public void SetupExample()
        {
            storage = new Storage();

            storage.AddContainer(20);
            storage.AddContainer(15);
            storage.AddContainer(10);
            storage.AddContainer(5);
            storage.AddContainer(5);
        }

        [Test]
        public void GetContainerCombinationCountForExample()
        {
            var totalLiquidSize = 25;
            var expectedComboCount = 4;
            var actual = storage.GetContainerCombinationCount(totalLiquidSize);
            Assert.AreEqual(expectedComboCount, actual);
        }
    }
}
