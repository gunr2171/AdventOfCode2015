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

        [TestCase(44, 56, 62842880)]
        [TestCase(40, 60, 57600000)]
        public void CalculateCookieFromRatioListForExample_Score(int ing1Ratio, int ing2Ratio, int expectedScore)
        {
            var ratios = new Dictionary<string, int>();
            ratios.Add("Butterscotch", ing1Ratio);
            ratios.Add("Cinnamon", ing2Ratio);

            var actual = recipe.CalculateCookieFromRatioList(ratios).Score;
            Assert.AreEqual(expectedScore, actual);
        }

        [Test]
        public void CalculateCookieFromRatioListForExample_Calories()
        {
            var ratios = new Dictionary<string, int>();
            ratios.Add("Butterscotch", 40);
            ratios.Add("Cinnamon", 60);

            var actual = recipe.CalculateCookieFromRatioList(ratios).Calories;
            var expected = 500;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DetermineBestCookieScoreForIngredientsListForExample()
        {
            var expected = 62842880;
            var actual = recipe.DetermineBestCookieScoreForIngredientsList();
            Assert.AreEqual(expected, actual);
        }
    }
}
