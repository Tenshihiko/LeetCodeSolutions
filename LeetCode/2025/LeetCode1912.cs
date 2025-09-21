namespace LeetCode1912;

public class Solution
{
    public void Run()
    {
        string[] commands = ["MovieRentingSystem", "search", "rent", "rent", "report", "drop", "search"];
        var n = 3;
        int[][] init = [[0, 1, 5], [0, 2, 6], [0, 3, 7], [1, 1, 4], [1, 2, 7], [2, 1, 5]];
        int[][] parameters = [[],[1], [0, 1], [1, 2], [], [1, 2], [2]];

        var movieRentingSystem = new MovieRentingSystem(n, init);

        for(int i = 1; i < commands.Length; i++)
        {
            if (commands[i] == "search")
            {
                var res = movieRentingSystem.Search(parameters[i][0]);
                Console.WriteLine($"[{string.Join(",", res)}]");
            }
            else if (commands[i] == "rent")
            {
                movieRentingSystem.Rent(parameters[i][0], parameters[i][1]);
                Console.WriteLine("null");
            }
            else if (commands[i] == "drop")
            {
                movieRentingSystem.Drop(parameters[i][0], parameters[i][1]);
                Console.WriteLine("null");
            }
            else if (commands[i] == "report")
            {
                var res = movieRentingSystem.Report();
                var formattedRes = res.Select(r => $"[{string.Join(",", r)}]").ToArray();
                Console.WriteLine($"[{string.Join(",", formattedRes)}]");
            }
        }
    }

    public class MovieRentingSystem
    {
        record Movie(int shop, int movie, int price);
        Dictionary<int, int> Prices;
        Dictionary<int, SortedSet<(int shop, int price)>> ShopsForMovies;
        SortedSet<Movie> RentedMovies;


        public MovieRentingSystem(int n, int[][] entries)
        {
            Prices = new Dictionary<int, int>();
            ShopsForMovies = new Dictionary<int, SortedSet<(int shop, int price)>>();
            RentedMovies = new SortedSet<Movie>(Comparer<Movie>.Create((a, b) =>
            {
                if (a.price != b.price) return a.price - b.price;
                if (a.shop != b.shop) return a.shop - b.shop;
                return a.movie - b.movie;
            }));
            var shopComparer = Comparer<(int shop, int price)>.Create((a, b) =>
            {
                if (a.price != b.price) return a.price - b.price;
                return a.shop - b.shop;
            });

            foreach (var entry in entries)
            {
                var shop = entry[0];
                var movie = entry[1];
                var price = entry[2];
                Prices[movie] = price;
                if (!ShopsForMovies.ContainsKey(movie))
                {
                    ShopsForMovies[movie] = new SortedSet<(int shop, int price)>(shopComparer);
                }
                ShopsForMovies[movie].Add((shop, price));
            }
        }

        public IList<int> Search(int movie)
        {
            if(!ShopsForMovies.ContainsKey(movie))
            {
                return new List<int>();
            }

            return ShopsForMovies[movie].Take(5).Select(x => x.shop).ToList();
        }

        public void Rent(int shop, int movie)
        {
            var price = Prices[movie];
            ShopsForMovies[movie].Remove((shop, price));
            RentedMovies.Add(new Movie(shop, movie, price));
        }

        public void Drop(int shop, int movie)
        {
            var price = Prices[movie];
            ShopsForMovies[movie].Add((shop, price));
            RentedMovies.Remove(new Movie(shop, movie, price));
        }

        // TODO something is wrong here. On a big test  case it returns wrong answer, maybe something with sorting?
        public IList<IList<int>> Report()
        {
            return RentedMovies.Take(5).Select(m => (IList<int>)new List<int> { m.shop, m.movie }).ToList();
        }
    }

}

