using System.Collections.Generic;
namespace ConstructionLine.CodingChallenge.Comparers
{
    public class ShirtComparer : IEqualityComparer<Shirt>
    {
        public static ShirtComparer Instance = new ShirtComparer();
        public bool Equals(Shirt x, Shirt y)
        {
            if (x == null) return false;

            if (y == null) return false;

            if (object.ReferenceEquals(x, y))
            {
                return true;
            }

            return x.Id == y.Id;
        }

        public int GetHashCode(Shirt obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}