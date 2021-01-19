using System.Collections.Generic;
using System.Linq;

namespace ValorantOverlay.Core.Models
{
    public interface IRankList
    {
        string GetRankByIndex(int index);
    }

    public class RankList : List<string>, IRankList
    {
        public string GetRankByIndex(int index)
        {
            return this.ElementAtOrDefault(index);
        }

        public new string this[int i] => GetRankByIndex(i);
    }
}
