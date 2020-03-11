using System.Collections.Generic;
namespace ConstructionLine.CodingChallenge
{
    public class SearchMatchResults
    {
        public List<List<Shirt>> GroupedMatches { get; set; }

        public HashSet<Shirt> FilteredSet { get; set; }
    }

}