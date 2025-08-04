using WordFinderProject;

namespace WordFinderChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== WordFinder Challenge Demo ===\n");

            // Correct matrix from the challenge
            var matrix = new List<string>
            {
                "dabcccmobiholas",
                "orgwiocareadios",
                "gchilloeqeperro",
                "zpqnsdtopehoaax",
                "xuvdogredoagggd"
            };

            // Example word stream
            var wordStream = new List<string>
            {
                "chill", "cold", "wind", "dog", "red", "car"
            };

            Console.WriteLine("Matrix:");
            DisplayMatrix(matrix);

            Console.WriteLine("\nWord Stream:");
            Console.WriteLine(string.Join(", ", wordStream));

            try
            {
                var finder = new WordFinder(matrix);
                var results = finder.FindWithCounts(wordStream);

                Console.WriteLine("\nResults (Top 10 most repeated words found):");
                if (results.Any())
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine($"- {result.Key} ({result.Value})");
                    }
                }
                else
                {
                    Console.WriteLine("No words found in the matrix.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// Displays the matrix with separators for better readability
        /// </summary>
        /// <param name="matrix">The matrix to display</param>
        private static void DisplayMatrix(List<string> matrix)
        {
            if (matrix == null || matrix.Count == 0)
                return;

            int cols = matrix[0].Length;

            // Print top border with column separators
            Console.Write("┌");
            for (int i = 0; i < cols; i++)
            {
                Console.Write("─");
                if (i < cols - 1)
                    Console.Write("┬");
            }
            Console.WriteLine("┐");

            // Print matrix rows with separators
            for (int rowIndex = 0; rowIndex < matrix.Count; rowIndex++)
            {
                var row = matrix[rowIndex];
                Console.Write("│");
                for (int i = 0; i < row.Length; i++)
                {
                    Console.Write(row[i]);
                    if (i < row.Length - 1)
                        Console.Write("│");
                }
                Console.WriteLine("│");

                // Print row separator (except for last row)
                if (rowIndex < matrix.Count - 1)
                {
                    Console.Write("├");
                    for (int i = 0; i < cols; i++)
                    {
                        Console.Write("─");
                        if (i < cols - 1)
                            Console.Write("┼");
                    }
                    Console.WriteLine("┤");
                }
            }

            // Print bottom border with column separators
            Console.Write("└");
            for (int i = 0; i < cols; i++)
            {
                Console.Write("─");
                if (i < cols - 1)
                    Console.Write("┴");
            }
            Console.WriteLine("┘");
        }
    }
}