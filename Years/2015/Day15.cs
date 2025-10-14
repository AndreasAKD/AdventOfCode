using System.Text.RegularExpressions;

namespace AdventOfCode.Years._2015
{
    public class Day15
    {
        public void Run(string inputPath)
        {
            var lines = File.ReadAllLines(inputPath);

            int totalScoreOfHighestScoringCookie = TotalScoreOfHighestScoringCookie(lines);
            int totalHighestScoreOfCookieWith500CaloriesTotal = TotalHighestScoreOfCookieWith500CaloriesTotal(lines);

            Console.WriteLine($"Part One: Total Score: {totalScoreOfHighestScoringCookie}");
            Console.WriteLine($"Part Two: Total Score: {totalHighestScoreOfCookieWith500CaloriesTotal}");

        }
        public int TotalScoreOfHighestScoringCookie(string[] lines)
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            var regex = new Regex(@"^(\w+): capacity (-?\d+), durability (-?\d+), flavor (-?\d+), texture (-?\d+), calories (-?\d+)");

            foreach (var line in lines)
            {
                var match = regex.Match(line);
                if (match.Success)
                {
                    ingredients.Add(new Ingredient
                    {
                        Name = match.Groups[1].Value,
                        Capacity = int.Parse(match.Groups[2].Value),
                        Durability = int.Parse(match.Groups[3].Value),
                        Flavor = int.Parse(match.Groups[4].Value),
                        Texture = int.Parse(match.Groups[5].Value)
                    });
                }
            }

            int ingredientCount = ingredients.Count;
            int maxScore = 0;

            // Helper to calculate the score for a given distribution
            int CalculateScore(int[] amounts)
            {
                int totalCapacity = 0;
                int totalDurability = 0;
                int totalFlavor = 0;
                int totalTexture = 0;

                for (int i = 0; i < ingredientCount; i++)
                {
                    totalCapacity += amounts[i] * ingredients[i].Capacity;
                    totalDurability += amounts[i] * ingredients[i].Durability;
                    totalFlavor += amounts[i] * ingredients[i].Flavor;
                    totalTexture += amounts[i] * ingredients[i].Texture;
                }

                // If any property is negative, treat it as zero
                totalCapacity = Math.Max(0, totalCapacity);
                totalDurability = Math.Max(0, totalDurability);
                totalFlavor = Math.Max(0, totalFlavor);
                totalTexture = Math.Max(0, totalTexture);

                return totalCapacity * totalDurability * totalFlavor * totalTexture;
            }

            // Recursively try all possible distributions of 100 teaspoons
            void TryAllDistributions(int[] amountsPerIngredient, int ingredientIndex, int remainingTeaspoons)
            {
                if (ingredientIndex == ingredientCount - 1)
                {
                    amountsPerIngredient[ingredientIndex] = remainingTeaspoons;
                    int score = CalculateScore(amountsPerIngredient);
                    if (score > maxScore)
                        maxScore = score;
                    return;
                }

                for (int teaspoons = 0; teaspoons <= remainingTeaspoons; teaspoons++)
                {
                    amountsPerIngredient[ingredientIndex] = teaspoons;
                    TryAllDistributions(amountsPerIngredient, ingredientIndex + 1, remainingTeaspoons - teaspoons);
                }
            }

            int[] amountsPerIngredient = new int[ingredientCount];
            TryAllDistributions(amountsPerIngredient, 0, 100);

            return maxScore;

        }

        public int TotalHighestScoreOfCookieWith500CaloriesTotal(string[] lines)
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            var regex = new Regex(@"^(\w+): capacity (-?\d+), durability (-?\d+), flavor (-?\d+), texture (-?\d+), calories (-?\d+)");

            foreach (var line in lines)
            {
                var match = regex.Match(line);
                if (match.Success)
                {
                    ingredients.Add(new Ingredient
                    {
                        Name = match.Groups[1].Value,
                        Capacity = int.Parse(match.Groups[2].Value),
                        Durability = int.Parse(match.Groups[3].Value),
                        Flavor = int.Parse(match.Groups[4].Value),
                        Texture = int.Parse(match.Groups[5].Value),
                        Calories = int.Parse(match.Groups[6].Value)
                    });
                }
            }

            int ingredientCount = ingredients.Count;
            int maxScore = 0;

            // Helper to calculate the score for a given distribution
            int CalculateScore(int[] amounts)
            {
                int totalCapacity = 0;
                int totalDurability = 0;
                int totalFlavor = 0;
                int totalTexture = 0;

                for (int i = 0; i < ingredientCount; i++)
                {
                    totalCapacity += amounts[i] * ingredients[i].Capacity;
                    totalDurability += amounts[i] * ingredients[i].Durability;
                    totalFlavor += amounts[i] * ingredients[i].Flavor;
                    totalTexture += amounts[i] * ingredients[i].Texture;
                }

                // If any property is negative, treat it as zero
                totalCapacity = Math.Max(0, totalCapacity);
                totalDurability = Math.Max(0, totalDurability);
                totalFlavor = Math.Max(0, totalFlavor);
                totalTexture = Math.Max(0, totalTexture);

                return totalCapacity * totalDurability * totalFlavor * totalTexture;
            }

            // Helper to calculate the total calories for a given distribution
            int CalculateCalories(int[] amounts)
            {
                int totalCalories = 0;
                for (int i = 0; i < ingredientCount; i++)
                {
                    totalCalories += amounts[i] * ingredients[i].Calories;
                }
                return totalCalories;
            }

            // Recursively try all possible distributions of 100 teaspoons
            void TryAllDistributions(int[] amountsPerIngredient, int ingredientIndex, int remainingTeaspoons)
            {
                // If this is the last ingredient, assign all remaining teaspoons to it
                if (ingredientIndex == ingredientCount - 1)
                {
                    amountsPerIngredient[ingredientIndex] = remainingTeaspoons;

                    // Only consider this combination if the total calories is exactly 500
                    int totalCalories = CalculateCalories(amountsPerIngredient);
                    if (totalCalories == 500)
                    {
                        int score = CalculateScore(amountsPerIngredient);
                        if (score > maxScore)
                            maxScore = score;
                    }
                    return;
                }

                // Try all possible amounts for the current ingredient
                for (int teaspoons = 0; teaspoons <= remainingTeaspoons; teaspoons++)
                {
                    amountsPerIngredient[ingredientIndex] = teaspoons;
                    TryAllDistributions(amountsPerIngredient, ingredientIndex + 1, remainingTeaspoons - teaspoons);
                }
            }

            int[] amountsPerIngredient = new int[ingredientCount];
            TryAllDistributions(amountsPerIngredient, 0, 100);

            return maxScore;

        }


        private sealed class Ingredient
        {
            public string Name { get; set; }
            public int Capacity { get; set; }
            public int Durability { get; set; }
            public int Flavor { get; set; }
            public int Texture { get; set; }
            public int Calories { get; set; }

        }

    }
}