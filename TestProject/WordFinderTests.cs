using WordFinderProject;

namespace TestProject
{
    [TestFixture]
    public class WordFinderTests
    {
        [Test]
        public void Constructor_ValidMatrix_ShouldInitializeCorrectly()
        {
            // Arrange
            var matrix = new List<string> { "abc", "def", "ghi" };

            // Act
            var finder = new WordFinder(matrix);

            // Assert
            Assert.That(finder, Is.Not.Null);
        }

        [Test]
        public void Constructor_NullMatrix_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new WordFinder(null));
        }

        [Test]
        public void Constructor_EmptyMatrix_ShouldThrowArgumentException()
        {
            // Arrange
            var matrix = new List<string>();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new WordFinder(matrix));
        }

        [Test]
        public void Constructor_InconsistentRowLengths_ShouldThrowArgumentException()
        {
            // Arrange
            var matrix = new List<string> { "abc", "defg", "hi" };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new WordFinder(matrix));
        }

        [Test]
        public void Constructor_MatrixTooLarge_ShouldThrowArgumentException()
        {
            // Arrange
            var matrix = new List<string>();
            for (int i = 0; i < 65; i++)
            {
                matrix.Add(new string('a', 5));
            }

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new WordFinder(matrix));
        }

        [Test]
        public void Find_HorizontalWords_ShouldFindCorrectly()
        {
            // Arrange
            var matrix = new List<string> { "hello", "world" };
            var wordstream = new List<string> { "hello", "world", "notfound" };
            var finder = new WordFinder(matrix);

            // Act
            var results = finder.Find(wordstream).ToList();

            // Assert
            Assert.That(results.Count, Is.EqualTo(2));
            Assert.That(results, Does.Contain("hello"));
            Assert.That(results, Does.Contain("world"));
        }

        [Test]
        public void Find_VerticalWords_ShouldFindCorrectly()
        {
            // Arrange
            var matrix = new List<string> { "abc", "def", "ghi" };
            var wordstream = new List<string> { "adg", "beh", "cfi", "notfound" };
            var finder = new WordFinder(matrix);

            // Act
            var results = finder.Find(wordstream).ToList();

            // Assert
            Assert.That(results.Count, Is.EqualTo(3));
            Assert.That(results, Does.Contain("adg"));
            Assert.That(results, Does.Contain("beh"));
            Assert.That(results, Does.Contain("cfi"));
        }

        [Test]
        public void Find_ChallengeExample_ShouldFindCorrectWords()
        {
            // Arrange
            var matrix = new List<string>
            {
                "abccc",
                "rgwio",
                "chill",
                "pqnsd",
                "uvdxy"
            };
            var wordStream = new List<string> { "chill", "cold", "wind", "notfound", "pet", "blue" };
            var finder = new WordFinder(matrix);

            // Act
            var results = finder.Find(wordStream).ToList();

            // Assert
            Assert.That(results.Count, Is.EqualTo(3));
            Assert.That(results, Does.Contain("chill"));
            Assert.That(results, Does.Contain("cold"));
            Assert.That(results, Does.Contain("wind"));
        }

        [Test]
        public void Find_EmptyWordStream_ShouldReturnEmpty()
        {
            // Arrange
            var matrix = new List<string> { "abc", "def" };
            var wordstream = new List<string>();
            var finder = new WordFinder(matrix);

            // Act
            var results = finder.Find(wordstream);

            // Assert
            Assert.That(results, Is.Empty);
        }

        [Test]
        public void Find_NullWordStream_ShouldThrowArgumentNullException()
        {
            // Arrange
            var matrix = new List<string> { "abc", "def" };
            var finder = new WordFinder(matrix);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => finder.Find(null));
        }

        [Test]
        public void Find_NoWordsFound_ShouldReturnEmpty()
        {
            // Arrange
            var matrix = new List<string> { "abc", "def" };
            var wordstream = new List<string> { "xyz", "notfound" };
            var finder = new WordFinder(matrix);

            // Act
            var results = finder.Find(wordstream);

            // Assert
            Assert.That(results, Is.Empty);
        }

        [Test]
        public void Find_DuplicateWordsInStream_ShouldCountOnlyOnce()
        {
            // Arrange
            var matrix = new List<string> { "hello", "world" };
            var wordstream = new List<string> { "hello", "hello", "world", "world" };
            var finder = new WordFinder(matrix);

            // Act
            var results = finder.Find(wordstream).ToList();

            // Assert
            Assert.That(results.Count, Is.EqualTo(2));
            Assert.That(results, Does.Contain("hello"));
            Assert.That(results, Does.Contain("world"));
        }

        [Test]
        public void Find_CaseInsensitive_ShouldWorkCorrectly()
        {
            // Arrange
            var matrix = new List<string> { "Hello", "World" };
            var wordstream = new List<string> { "hello", "WORLD" };
            var finder = new WordFinder(matrix);

            // Act
            var results = finder.Find(wordstream).ToList();

            // Assert
            Assert.That(results.Count, Is.EqualTo(2));
            Assert.That(results, Does.Contain("hello"));
            Assert.That(results, Does.Contain("WORLD"));
        }

        [Test]
        public void Find_MoreThan10Results_ShouldReturnTop10()
        {
            // Arrange
            var matrix = new List<string> { "abcdefghijklmnop" };
            var wordstream = new List<string>
            {
                "a", "ab", "abc", "abcd", "abcde", "abcdef", "abcdefg", "abcdefgh",
                "abcdefghi", "abcdefghij", "abcdefghijk", "abcdefghijkl"
            };
            var finder = new WordFinder(matrix);

            // Act
            var results = finder.Find(wordstream).ToList();

            // Assert
            Assert.That(results.Count, Is.EqualTo(10));
        }

      
        [Test]
        public void Find_SingleCharacterWords_ShouldFindCorrectly()
        {
            // Arrange
            var matrix = new List<string> { "abc", "def" };
            var wordstream = new List<string> { "a", "b", "c", "d", "e", "f" };
            var finder = new WordFinder(matrix);

            // Act
            var results = finder.Find(wordstream).ToList();

            // Assert
            Assert.That(results.Count, Is.EqualTo(6));
            Assert.That(results, Does.Contain("a"));
            Assert.That(results, Does.Contain("b"));
            Assert.That(results, Does.Contain("c"));
            Assert.That(results, Does.Contain("d"));
            Assert.That(results, Does.Contain("e"));
            Assert.That(results, Does.Contain("f"));
        }

        [Test]
        public void Find_LargeMatrix_ShouldHandleCorrectly()
        {
            // Arrange
            var matrix = new List<string>();
            for (int i = 0; i < 64; i++)
            {
                matrix.Add(new string('a', 64));
            }
            var wordstream = new List<string> { "a", "aa", "aaa" };
            var finder = new WordFinder(matrix);

            // Act
            var results = finder.Find(wordstream).ToList();

            // Assert
            Assert.That(results.Count, Is.EqualTo(3));
            Assert.That(results, Does.Contain("a"));
            Assert.That(results, Does.Contain("aa"));
            Assert.That(results, Does.Contain("aaa"));
        }

    }
}