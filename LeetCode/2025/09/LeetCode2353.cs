using LeetCode.Common;
using Xunit;

namespace LeetCode2353;

[Folder]
public class Tests
{
    // [Theory]
    // [InlineData(param1, param2, expected)]
    // public void Method(int param1, int param2, int expected) =>
    //     Expect.Equal(expected, new Solution().Method(param1, param2));
}

public class Solution
{
    public class FoodRatings
    {

        Dictionary<string, SortedSet<(int rating, string food)>> cuisineMap = new Dictionary<string, SortedSet<(int rating, string food)>>();
        Dictionary<string, (string cuisine, int rating)> foodMap = new Dictionary<string, (string cuisine, int rating)>();

        public FoodRatings(string[] foods, string[] cuisines, int[] ratings)
        {
            for (int i = 0; i < foods.Length; i++)
            {
                var food = foods[i];
                var cuisine = cuisines[i];
                var rating = ratings[i];
                if (!cuisineMap.ContainsKey(cuisine))
                {
                    cuisineMap[cuisine] = new SortedSet<(int rating, string food)>(Comparer<(int rating, string food)>.Create((a, b) =>
                    {
                        if (a.rating != b.rating)
                        {
                            return b.rating.CompareTo(a.rating); // Descending order by rating
                        }
                        return a.food.CompareTo(b.food); // Ascending order by food name
                    }));
                }
                cuisineMap[cuisine].Add((rating, food));
                foodMap[food] = (cuisine, rating);
            }
        }

        public void ChangeRating(string food, int newRating)
        {
            if (foodMap.ContainsKey(food))
            {
                var (cuisine, oldRating) = foodMap[food];
                cuisineMap[cuisine].Remove((oldRating, food));
                cuisineMap[cuisine].Add((newRating, food));
                foodMap[food] = (cuisine, newRating);
            }
        }

        public string HighestRated(string cuisine)
        {
            return cuisineMap[cuisine].First().food;
        }
    }
}
