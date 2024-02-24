using GraphGenerator.Domain.Graphs;
using GraphGenerator.Shared.Extensions;

namespace GraphGenerator.Logic;

public class TreeGraphGenerator
{
    private readonly Random random;

    public TreeGraphGenerator(Random random)
    {
        this.random = random;
    }

    public MatrixGraph GenerateTree(int nodes, bool directed, int? maxDegree)
    {
        var graph = new MatrixGraph(nodes);
        var parents = new List<int>() { 0 };
        for (var i = 1; i < nodes; i++)
        {
            var parent = random.Pick(parents);
            var weight = random.NextDouble(0.01, 100);
            graph.AddEdge(parent, i, weight);
            if (!directed)
            {
                graph.AddEdge(i, parent, weight);
            }
            parents.Add(i);

            if (!maxDegree.HasValue || graph.Degree(parent) < maxDegree.Value)
            {
                continue;
            }
            
            parents.Remove(parent);
            if (parents.Count == 0)
            {
                break;
            }
        }

        return graph;
    }
}
