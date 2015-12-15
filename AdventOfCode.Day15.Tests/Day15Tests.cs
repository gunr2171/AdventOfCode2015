using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace AdventOfCode.Day15.Tests
{
    [TestFixture]
    public class Day15Tests
    {
        Recipe recipe;

        [OneTimeSetUp]
        public void SetUpExampleRecipe()
        {
            recipe = new Recipe();

            recipe.AddIngredient("Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8");
            recipe.AddIngredient("Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3");
        }

        [Test]
        public void GetCookieScoreForExample()
        {
            var ratios = new Dictionary<string, int>();
            ratios.Add("Butterscotch", 44);
            ratios.Add("Cinnamon", 56);

            var actual = recipe.GetCookieScore(ratios);
            var expected = 62842880;

            Assert.AreEqual(expected, actual);
        }
    }
}
