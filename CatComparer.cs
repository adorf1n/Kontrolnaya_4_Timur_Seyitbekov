using System.Collections.Generic;

public class CatComparer : IComparer<Cat>
{
    public int Compare(Cat x, Cat y)
    {
        return y.AverageLifeLevel.CompareTo(x.AverageLifeLevel);
    }
}
