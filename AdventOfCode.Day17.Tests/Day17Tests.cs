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
            storage = new Storage(25);

            storage.AddContainer(20);
            storage.AddContainer(15);
            storage.AddContainer(10);
            storage.AddContainer(5);
            storage.AddContainer(5);
        }

        [Test]
        public void GetContainerCombinationCountForExample()
        {
            var expectedComboCount = 4;
            var actual = storage.GetTotalWorkingContainerCombinationCount();
            Assert.AreEqual(expectedComboCount, actual);
        }

        [Test]
        public void GetMinimumNumberOfContainersToHoldLiquidForExample()
        {
            var expectedMinimumContainersNeeded = 2;
            var actual = storage.GetMinimumNumberOfContainersToHoldLiquid();
            Assert.AreEqual(expectedMinimumContainersNeeded, actual);
        }

        [Test]
        public void GetCountOfWorkingCombosForContainerCountForExample()
        {
            var expectedCombosAtMinContainerCount = 3;
            var actual = storage.GetCountOfWorkingCombosForContainerCount(2);
            Assert.AreEqual(expectedCombosAtMinContainerCount, actual);
        }
    }
}
