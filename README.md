# WordFinder Challenge

A high-performance C# solution for finding words in a character matrix. This project implements an efficient algorithm to search for words horizontally and vertically within a 2D character grid, optimized for large word streams.

## Project Overview

The WordFinder is designed to solve the following challenge:
- Given a character matrix (2D grid of characters)
- Given a stream of words to search for
- Find the top 10 most repeated words that appear in the matrix
- Words can be found horizontally (left to right) or vertically (top to bottom)

## Features

- **Efficient Search Algorithm**: Optimized for performance with large word streams
- **Bidirectional Search**: Finds words both horizontally and vertically
- **Case-Insensitive Matching**: Handles different letter cases seamlessly
- **Top 10 Results**: Returns the most frequently found words
- **Comprehensive Validation**: Robust input validation and error handling
- **Extensive Testing**: Full test coverage with NUnit framework
- **Modern C#**: Built with .NET 8.0 and latest C# features

## Quick Start

### Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022, VS Code, or any .NET-compatible IDE

### Installation

1. Clone the repository:
```bash
git clone <repository-url>
cd WordFinderProject
```

2. Build the project:
```bash
dotnet build
```

3. Run the application:
```bash
dotnet run --project WordFinderProject
```

## 📖 Usage

### Basic Example

```csharp
// Create a character matrix
var matrix = new List<string>
{
    "dabcccmobiholas",
    "orgwiocareadios",
    "gchilloeqeperro",
    "zpqnsdtopehoaax",
    "xuvdogredoagggd"
};

// Define words to search for
var wordStream = new List<string>
{
    "chill", "cold", "wind", "dog", "red", "car"
};

// Create WordFinder instance
var finder = new WordFinder(matrix);

// Find words and get results with counts
var results = finder.FindWithCounts(wordStream);

// Display results
foreach (var result in results)
{
    Console.WriteLine($"- {result.Key} ({result.Value})");
}
```

### API Reference

#### WordFinder Class

**Constructor:**
```csharp
public WordFinder(IEnumerable<string> matrix)
```
- `matrix`: Character matrix represented as a collection of strings
- Throws `ArgumentNullException` if matrix is null
- Throws `ArgumentException` if matrix is empty, inconsistent, or exceeds 64x64

**Methods:**

1. **Find** - Returns top 10 words without counts:
```csharp
public IEnumerable<string> Find(IEnumerable<string> wordstream)
```

2. **FindWithCounts** - Returns top 10 words with occurrence counts:
```csharp
public IEnumerable<KeyValuePair<string, int>> FindWithCounts(IEnumerable<string> wordstream)
```

## Testing

The project includes comprehensive unit tests covering:

- Constructor validation
- Horizontal and vertical word finding
- Edge cases and error conditions
- Performance with large matrices
- Case-insensitive matching
- Duplicate word handling

### Running Tests

```bash
# Run all tests
dotnet test

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test project
dotnet test TestProject/
```

## Example Output

When you run the application, you'll see output like this:

```
=== WordFinder Challenge Demo ===

Matrix:

dabcccmobiholas
orgwiocareadios
gchilloeqeperro
zpqnsdtopehoaax
xuvdogredoagggd


Word Stream:
chill, cold, wind, dog, red, car

Results (Top 10 most repeated words found):
- chill (1)
- cold (1)
- dog (1)
- red (1)
- wind (1)
```

## 🔧 Project Structure

```
WordFinderProject/
├── WordFinderProject/          # Main application
│   ├── Program.cs             # Console application entry point
│   ├── WordFinder.cs          # Core WordFinder implementation
│   └── WordFinderProject.csproj
├── TestProject/               # Unit tests
│   ├── WordFinderTests.cs     # Comprehensive test suite
│   └── TestProject.csproj
└── README.md                  # This file
```

## Algorithm Details

The WordFinder uses an optimized approach:

1. **Input Validation**: Validates matrix consistency and size limits
2. **Word Mapping**: Creates case-insensitive mapping for efficient lookup
3. **Length Optimization**: Only searches for word lengths present in the stream
4. **Bidirectional Search**: 
   - Horizontal search: Left to right in each row
   - Vertical search: Top to bottom in each column
5. **Result Ranking**: Returns top 10 most frequent words with counts

## Performance Considerations

- **Matrix Size Limit**: Maximum 64x64 characters for optimal performance
- **Memory Efficient**: Uses dictionaries for word lookups
- **Optimized Search**: Only checks relevant word lengths
- **Case-Insensitive**: Single lookup per word regardless of case

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

---

**Built with using .NET 8.0 and C#** 
