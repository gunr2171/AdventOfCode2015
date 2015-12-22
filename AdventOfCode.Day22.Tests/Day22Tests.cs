using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day22.Tests
{
    [TestFixture]
    public class Day22Tests
    {
        [Test]
        public void SimulateFight_Part1_Example1()
        {
            var playerStats = new FighterStats(10, 0, 250);
            var bossStats = new BossStats(13, 0, 0, 8);

            var spellNamesToCast = new []
            {
                "Poison",
                "Magic Missile"
            };

            var spells = (from spellName in spellNamesToCast
                          join spell in Processor.EnumAllSpells() on spellName equals spell.Name
                          select spell)
                         .ToList();

            var playerWinsActual = Processor.SimulateFight(playerStats, bossStats, spells);

            Assert.IsTrue(playerWinsActual);
        }

        [Test]
        public void SimulateFight_Part1_Example2()
        {
            var playerStats = new FighterStats(10, 0, 250);
            var bossStats = new BossStats(14, 0, 0, 8);

            var spellNamesToCast = new[]
            {
                "Recharge",
                "Shield",
                "Drain",
                "Poison",
                "Magic Missile",
            };

            var spells = (from spellName in spellNamesToCast
                          join spell in Processor.EnumAllSpells() on spellName equals spell.Name
                          select spell)
                         .ToList();

            var playerWinsActual = Processor.SimulateFight(playerStats, bossStats, spells);

            Assert.IsTrue(playerWinsActual);
        }
    }
}
