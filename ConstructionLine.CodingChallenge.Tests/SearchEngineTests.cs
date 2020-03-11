using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {
        [Test]
        public void Test()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> {Color.Red},
                Sizes = new List<Size> {Size.Small}
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [TestCaseSource(nameof(GetTestCases))]
        public void ThatCanFindAllRedRegardlessOfSize(SearchOptions searchOptions)
        {
            var shirts = this.GetTestSet();

            var searchEngine = new SearchEngine(shirts);

         
            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]

        public void ThatWorksForMissingColorValues()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Yellow },
                Sizes = new List<Size>()
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]

        public void ThatWorksForMissingSizeValues()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Small", Size.Small, Color.Blue),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color>(),
                Sizes = new List<Size>() { Size.Large }
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]

        public void ThatWorksForMissingSizeAndColorValues()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Small", Size.Small, Color.Blue),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color>() { Color.Yellow },
                Sizes = new List<Size>() { Size.Large }
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        private static IEnumerable<TestCaseData>  GetTestCases()
        {
            // one color 
            yield return new TestCaseData(new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size>()
            }).WithName("That works for One Color");

            // multiple colors
            yield return new TestCaseData(new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Blue },
                Sizes = new List<Size>()
            }).WithName("That works for multiple Colors");

            // one size , any colors
            yield return new TestCaseData(new SearchOptions
            {
                Colors = new List<Color>(),
                Sizes = new List<Size> { Size.Small }
            }).WithName("That works for One Color and any size");

            // one size , one color
             yield return new TestCaseData(new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Small }
            }).WithName("That works for One Color and one size");

            // one size , many colors
             yield return new TestCaseData(new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Blue },
                Sizes = new List<Size> { Size.Small }
            }).WithName("That works for One Size, many colors");

            // many sizes , many colors
            yield return new TestCaseData(new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Blue },
                Sizes = new List<Size> { Size.Small, Size.Medium }
            }).WithName("That works for many Colors and many sizes");
            
        }

        private   List<Shirt> GetTestSet()
        {
            return new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };
        }
    }
}
