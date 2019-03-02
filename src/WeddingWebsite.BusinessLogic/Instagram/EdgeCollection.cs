using System.Collections;
using System.Collections.Generic;

namespace WeddingWebsite.BusinessLogic.Instagram
{
    public class EdgeCollection //: IEnumerable<Edge>
    {
        public EdgeCollection(long count, IReadOnlyList<Edge> edges)
        {
            Count = count;
            Edges = edges;
        }

        public long Count { get; }

        public IReadOnlyList<Edge> Edges { get; }

        //public IEnumerator<Edge> GetEnumerator()
        //{
        //    return Edges.GetEnumerator();
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return GetEnumerator();
        //}
    }
}