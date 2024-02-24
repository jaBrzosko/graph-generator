using GraphGenerator.Domain.Graphs;
using GraphGenerator.Shared.Extensions;

namespace GraphGenerator.Logic;

public class RandomGraphGenerator
{
    private readonly Random random;

    public RandomGraphGenerator(Random random)
    {
        this.random = random;
    }

    public MatrixGraph GenerateGraph(int nodes, int edges, bool directed)
    {
        var graph = new MatrixGraph(nodes);
        for (var i = 0; i < edges; i++)
        {
            var weight = random.NextDouble(0.01, 100);

            var from = random.Next(0, nodes);
            var to = random.Next(0, nodes);
            while (from == to || graph.HasEdge(from, to))
            {
                from++;
                if (from != nodes)
                {
                    continue;
                }
                
                from = 0;
                to++;
                
                if (to == nodes)
                {
                    to = 0;
                }
            }

            graph.AddEdge(from, to, weight);
            if (!directed)
            {
                graph.AddEdge(to, from, weight);
            }
        }

        return graph;
    }

    public MatrixGraph GenerateGraph(int nodes, double density, bool directed)
    {
        var edges = (int) (nodes * (nodes - 1) * density);
        if (!directed)
        {
            edges /= 2;
        }
        return GenerateGraph(nodes, edges, directed);
    }
}
