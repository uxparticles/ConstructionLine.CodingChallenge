using ConstructionLine.CodingChallenge.Comparers;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly Dictionary<Color, List<Shirt>> shirtsByColor;

        private readonly Dictionary<Size, List<Shirt>> shirtsBySize;

        public SearchEngine(List<Shirt> shirts)
        {
            shirtsByColor = shirts.GroupBy(x => x.Color).ToDictionary(x => x.Key, x => x.ToList());
            shirtsBySize = shirts.GroupBy(x => x.Size).ToDictionary(x => x.Key, x => x.ToList());
        }

        public SearchResults Search(SearchOptions options)
        {
            var matchByColor = this.MatchColorOptions(options);
            var matchBySize = this.MatchSizeOptions(options, matchByColor.FilteredSet);

            var countResults = this.GetCounts(matchByColor, matchBySize);

            return new SearchResults
            {
                Shirts = matchBySize.FilteredSet.ToList(),
                SizeCounts = countResults.SizeCounts,
                ColorCounts = countResults.ColorCounts
            };
        }

        private CountResults GetCounts(SearchMatchResults matchByColor, SearchMatchResults matchBySize)
        {
            var matchingSet = matchBySize.FilteredSet;
            var matchingBySize = matchBySize.GroupedMatches;

            var colorCountFound = matchByColor.GroupedMatches.Select(x => new ColorCount { Count = x.Count(matchingSet.Contains), Color = x.First().Color }).ToDictionary(x => x.Color);
            var sizeCountFound = matchingBySize.Select(x => new SizeCount { Count = x.Count, Size = x.FirstOrDefault()?.Size }).Where(x => x.Size != null).ToDictionary(x => x.Size);

            return new CountResults
            {
                ColorCounts = Color.All.Select(x => new ColorCount
                {
                    Color = x,
                    Count = colorCountFound.GetValueOrDefault(x)?.Count ?? 0
                }).ToList(),

                SizeCounts = Size.All.Select(x => new SizeCount
                {
                    Size = x,
                    Count = sizeCountFound.GetValueOrDefault(x)?.Count ?? 0
                }).ToList()
            };
        }

        private SearchMatchResults MatchColorOptions(SearchOptions options)
        {
            var optionColors = (options.Colors == null || !options.Colors.Any()) ?
              Color.All :
              options.Colors;

            var setsOfShirtMatchingByColor = optionColors.Select(this.shirtsByColor.GetValueOrDefault).Where(x => x != null && x.Any()).ToList();
            var completeSetOfMatchingShirts = new HashSet<Shirt>(setsOfShirtMatchingByColor.SelectMany(x => x), ShirtComparer.Instance);

            return new SearchMatchResults
            {
                GroupedMatches = setsOfShirtMatchingByColor,
                FilteredSet = completeSetOfMatchingShirts
            };
        }

        private SearchMatchResults MatchSizeOptions(SearchOptions options, HashSet<Shirt> resultsets)
        {
            var optionSizes = (options.Sizes == null || !options.Sizes.Any()) ? Size.All : options.Sizes;

            var matchingBySize = optionSizes
                .Select(x => this.shirtsBySize.GetValueOrDefault(x)
                                        ?.Where(y => resultsets.Contains(y))
                                        .ToList() ?? new List<Shirt>()
                                        ).ToList();

            var matchingSet = new HashSet<Shirt>(matchingBySize.SelectMany(x => x), ShirtComparer.Instance);

            return new SearchMatchResults
            {
                GroupedMatches = matchingBySize,
                FilteredSet = matchingSet
            };
        }
    }
}