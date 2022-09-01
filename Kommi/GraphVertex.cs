    using System;
using System.Collections.Generic;
using System.Text;

namespace Kommi
{
    class GraphVertex

    {
        public string name { get; set; }
        public List<GraphEdge> edges { get; set; }
        public List<GraphVertex> neighbours = new List<GraphVertex>();
        public GraphVertex(string vertexName)
        {
            name = vertexName;
            edges = new List<GraphEdge>();
        }

        public void AddEdge(GraphVertex BeginVertex, GraphVertex EndVertex, int edgeWeight)

        {
            edges.Add(new GraphEdge(BeginVertex, EndVertex, edgeWeight));
        }

        public override string ToString()

        {
            return name;
        }

    }
}
