using System;
using NUnit.Framework;

namespace AdventOfCode.Day21.Tests
{
    [TestFixture]
    public class Day21Tests
    {
        [Test]
        public void SimulateFight_Example()
        {
            var playerStats = new PlayerStats { HitPoints = 8, Damage = 5, Armor = 5 };
            var bossStats = new PlayerStats { HitPoints = 12, Damage = 7, Armor = 2 };

            var doesPlayerWin = FightSimulator.SimulateFight(playerStats, bossStats);

            Assert.IsTrue(doesPlayerWin);
        }

        //player attacks
        [TestCase(12, 5, 2, 9)]
        [TestCase(9, 5, 2, 6)]
        [TestCase(6, 5, 2, 3)]
        [TestCase(3, 5, 2, 0)]
        //boss attacks
        [TestCase(8, 7, 5, 6)]
        [TestCase(6, 7, 5, 4)]
        [TestCase(4, 7, 5, 2)]
        public void CalculateNewHitPointsAfterAttack(int startingHitPoints, int attackerAttackValue, int defenderArmorValue, int expectedNewHP)
        {
            var actual = FightSimulator.CalculateNewHitPointsAfterAttack(startingHitPoints, attackerAttackValue, defenderArmorValue);
            Assert.AreEqual(expectedNewHP, actual);
        }
    }
}
