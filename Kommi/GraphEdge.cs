using System;
using System.Collections.Generic;
using System.Text;

namespace Kommi
{
    class GraphEdge

    {
        public GraphVertex EndVertex { get; set; }
        public GraphVertex BeginVertex { get; set; }
        public int EdgeWeight { get; set; }
        public GraphEdge(GraphVertex BeginVertex, GraphVertex EndVertex, int weight)
        {
            this.BeginVertex = BeginVertex;
            this.EndVertex = EndVertex;
            EdgeWeight = weight;
        }

    }
}
