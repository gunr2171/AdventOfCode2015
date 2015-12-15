using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TCL.Extensions;
using AdventOfCode.Common;
using System.IO;

namespace AdventOfCode.Day15
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    public class Recipe
    {
        private List<Ingredient> ingredients = new List<Ingredient>();

        public void AddIngredient(string rawIngredientListing)
        {
            ingredients.Add(new Ingredient(rawIngredientListing));
        }

        /// <summary>
        /// Determines the total score of a cookie with the given ratios.
        /// </summary>
        /// <param name="ingredientRatios">Key value is the name of the ingredient. Value is the 0-100 ratio of the cookie.</param>
        /// <returns></returns>
        public int GetCookieScore(Dictionary<string, int> ingredientRatios)
        {
            var summedIngredientScores = ingredientRatios
                .Join(ingredients,
                      (x) => x.Key,
                      (y) => y.Name,
                      (x, y) => new { Ingredient = y, Ratio = x.Value })
                .Select(x => new
                {
                    Capacity = x.Ingredient.Capacity * x.Ratio,
                    Durrability = x.Ingredient.Durrability * x.Ratio,
                    Flavor = x.Ingredient.Flavor * x.Ratio,
                    Texture = x.Ingredient.Texture * x.Ratio
                })
                .Aggregate((x, y) => new
                {
                    Capacity = x.Capacity + y.Capacity,
                    Durrability = x.Durrability + y.Durrability,
                    Flavor = x.Flavor + y.Flavor,
                    Texture = x.Texture + y.Texture
                });

            // if any of the properties is 0 or lower, the result of the multiplication
            // will be 0, so just return now
            if (summedIngredientScores.Capacity <= 0) return 0;
            if (summedIngredientScores.Durrability <= 0) return 0;
            if (summedIngredientScores.Flavor <= 0) return 0;
            if (summedIngredientScores.Texture <= 0) return 0;

            var totalCookieScore =
                summedIngredientScores.Capacity *
                summedIngredientScores.Durrability *
                summedIngredientScores.Flavor *
                summedIngredientScores.Texture;

            return totalCookieScore;
        }

        public int DetermineBestCookieScoreForIngredientsList()
        {
            var bestRatio = EnumPossibleRatios()
                .Select(x => GetCookieScore(x))
                .Max();

            return bestRatio;
        }

        public IEnumerable<Dictionary<string, int>> EnumPossibleRatios()
        {
            var ingredientNames = ingredients.Select(x => x.Name);
            var ratioNumbers = Enumerable.Range(1, 100);

            var ratioPermutations = ratioNumbers
                .GetPermutationsWithRept(ingredients.Count)
                .Where(x => x.Sum() == 100);

            foreach (var ratioPermutation in ratioPermutations)
            {
                var ratioInfo = ratioPermutation
                    .Zip(ingredientNames, (x, y) => new { Name = y, Ratio = x })
                    .ToDictionary(x => x.Name, x => x.Ratio);

                yield return ratioInfo;
            }
        }
    }

    public class Ingredient
    {
        public string Name { get; set; }

        public int Capacity { get; set; }
        public int Durrability { get; set; }
        public int Flavor { get; set; }
        public int Texture { get; set; }
        public int Calories { get; set; }

        public Ingredient(string rawIngredientListing)
        {
            var match = Regex.Match(rawIngredientListing, @"(\w+): \w+ ([-\d]+), \w+ ([-\d]+), \w+ ([-\d]+), \w+ ([-\d]+), \w+ ([-\d]+)");

            Name = match.Groups[1].Value;
            Capacity = match.Groups[2].Value.Parse<int>();
            Durrability = match.Groups[3].Value.Parse<int>();
            Flavor = match.Groups[4].Value.Parse<int>();
            Texture = match.Groups[5].Value.Parse<int>();
            Calories = match.Groups[6].Value.Parse<int>();
        }
    }
}
