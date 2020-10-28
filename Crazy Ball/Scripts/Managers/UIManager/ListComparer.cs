using System.Collections.Generic;


public class ListComparer : IComparer<PlayerScore>
{
    public int Compare(PlayerScore x, PlayerScore y)
    {
        if(x.score > y.score) {
            return -1;
        } else if(x.score < y.score) {
            return 1;
        }

        return 0;
    }
}
