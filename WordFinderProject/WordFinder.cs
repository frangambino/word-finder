namespace WordFinderProject
{
    /// <summary>
    /// Efficient word finder for character matrix with horizontal and vertical search capabilities.
    /// Optimized for large word streams with performance considerations.
    /// </summary>
    public class WordFinder
    {
        private readonly string[] _matrix;
        private readonly int _rows;
        private readonly int _cols;
        private readonly Dictionary<string, int> _wordCounts;

        /// <summary>
        /// Initializes a new instance of the WordFinder class.
        /// </summary>
        /// <param name="matrix">Character matrix represented as IEnumerable of strings</param>
        /// <exception cref="ArgumentNullException">Thrown when matrix is null</exception>
        /// <exception cref="ArgumentException">Thrown when matrix is empty or has inconsistent row lengths</exception>
        public WordFinder(IEnumerable<string> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix), "Matrix cannot be null");

            _matrix = matrix.ToArray();

            if (_matrix.Length == 0)
                throw new ArgumentException("Matrix cannot be empty", nameof(matrix));

            _rows = _matrix.Length;
            _cols = _matrix[0].Length;

            // Validate matrix consistency
            if (_rows > 64 || _cols > 64)
                throw new ArgumentException("Matrix size cannot exceed 64x64", nameof(matrix));

            for (int i = 1; i < _rows; i++)
            {
                if (_matrix[i].Length != _cols)
                    throw new ArgumentException($"All matrix rows must have the same length. Row 1 has {_cols} characters, row {i + 1} has {_matrix[i].Length} characters", nameof(matrix));
            }

            _wordCounts = new Dictionary<string, int>();
        }

        /// <summary>
        /// Finds the top 10 most repeated words from the word stream that appear in the matrix.
        /// Words can appear horizontally (left to right) or vertically (top to bottom).
        /// Each word is counted only once per occurrence in the matrix, regardless of how many times it appears in the stream.
        /// </summary>
        /// <param name="wordstream">Stream of words to search for in the matrix</param>
        /// <returns>Top 10 most repeated words found in the matrix, or empty set if no words found</returns>
        /// <exception cref="ArgumentNullException">Thrown when wordstream is null</exception>
        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            if (wordstream == null)
                throw new ArgumentNullException(nameof(wordstream), "Word stream cannot be null");

            var words = wordstream.ToArray();
            if (words.Length == 0)
                return Enumerable.Empty<string>();

            // Clear previous results
            _wordCounts.Clear();

            // Create a mapping from lowercase to original case for case-insensitive search
            var wordMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var wordLengths = new HashSet<int>();
            foreach (var word in words)
            {
                wordMapping[word] = word;
                wordLengths.Add(word.Length);
            }

            // Search horizontally and vertically
            SearchHorizontally(wordMapping, wordLengths);
            SearchVertically(wordMapping, wordLengths);

            // Return top 10 most repeated words
            return _wordCounts
                .OrderByDescending(kvp => kvp.Value)
                .ThenBy(kvp => kvp.Key)
                .Take(10)
                .Select(kvp => kvp.Key);
        }

        /// <summary>
        /// Finds the top 10 most repeated words from the word stream that appear in the matrix.
        /// Returns both words and their occurrence counts.
        /// </summary>
        /// <param name="wordstream">Stream of words to search for in the matrix</param>
        /// <returns>Top 10 most repeated words with their counts, or empty set if no words found</returns>
        /// <exception cref="ArgumentNullException">Thrown when wordstream is null</exception>
        public IEnumerable<KeyValuePair<string, int>> FindWithCounts(IEnumerable<string> wordstream)
        {
            if (wordstream == null)
                throw new ArgumentNullException(nameof(wordstream), "Word stream cannot be null");

            var words = wordstream.ToArray();
            if (words.Length == 0)
                return Enumerable.Empty<KeyValuePair<string, int>>();

            // Clear previous results
            _wordCounts.Clear();

            // Create a mapping from lowercase to original case for case-insensitive search
            var wordMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var wordLengths = new HashSet<int>();
            foreach (var word in words)
            {
                wordMapping[word] = word;
                wordLengths.Add(word.Length);
            }

            // Search horizontally and vertically
            SearchHorizontally(wordMapping, wordLengths);
            SearchVertically(wordMapping, wordLengths);

            // Return top 10 most repeated words with their counts
            return _wordCounts
                .OrderByDescending(kvp => kvp.Value)
                .ThenBy(kvp => kvp.Key)
                .Take(10);
        }

        /// <summary>
        /// Searches for words horizontally (left to right) in the matrix.
        /// </summary>
        /// <param name="wordMapping">Mapping of words to search for</param>
        /// <param name="wordLengths">Set of word lengths to search for</param>
        private void SearchHorizontally(Dictionary<string, string> wordMapping, HashSet<int> wordLengths)
        {
            for (int row = 0; row < _rows; row++)
            {
                for (int col = 0; col < _cols; col++)
                {
                    // Try only the lengths of words in the stream
                    foreach (int length in wordLengths)
                    {
                        if (col + length <= _cols)
                        {
                            string word = _matrix[row].Substring(col, length);
                            if (wordMapping.TryGetValue(word, out string originalWord))
                            {
                                _wordCounts[originalWord] = _wordCounts.GetValueOrDefault(originalWord, 0) + 1;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Searches for words vertically (top to bottom) in the matrix.
        /// </summary>
        /// <param name="wordMapping">Mapping of words to search for</param>
        /// <param name="wordLengths">Set of word lengths to search for</param>
        private void SearchVertically(Dictionary<string, string> wordMapping, HashSet<int> wordLengths)
        {
            for (int col = 0; col < _cols; col++)
            {
                for (int row = 0; row < _rows; row++)
                {
                    // Try only the lengths of words in the stream
                    foreach (int length in wordLengths)
                    {
                        if (row + length <= _rows)
                        {
                            string word = BuildVerticalWord(row, col, length);
                            if (wordMapping.TryGetValue(word, out string originalWord))
                            {
                                _wordCounts[originalWord] = _wordCounts.GetValueOrDefault(originalWord, 0) + 1;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Builds a word by reading vertically from the specified position.
        /// </summary>
        /// <param name="startRow">Starting row position</param>
        /// <param name="col">Column position</param>
        /// <param name="length">Length of the word to build</param>
        /// <returns>Word built by reading vertically</returns>
        private string BuildVerticalWord(int startRow, int col, int length)
        {
            var chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = _matrix[startRow + i][col];
            }
            return new string(chars);
        }
    }
}