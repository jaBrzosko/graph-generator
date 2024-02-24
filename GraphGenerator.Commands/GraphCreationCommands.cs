using Cocona;
using GraphGenerator.Domain.Graphs;
using GraphGenerator.Logic;
using GraphGenerator.Shared.Extensions;

namespace GraphGenerator.Commands;

public class GraphCreationCommands
{
    [Command("random", Description = "Generate a random graph")]
    public void GenerateGraph(
        [Option("nodes")] int nodes,
        [Option("edges")] int? edges,
        [Option("density")] double? density,
        [Option("directed")] bool directed = false,
        [Option("precision")] int precision = 0,
        [Option("filename")] string? filename = null,
        [Option("seed")] int seed = 0)
    {
        #region Validation 
        if (edges.HasValue && density.HasValue)
        {
            throw new CoconaException("Cannot specify both edges and density.");
        }

        if (!edges.HasValue && !density.HasValue)
        {
            throw new CoconaException("Must specify either edges or density.");
        }
        
        if (nodes < 1)
        {
            throw new CoconaException("Number of nodes must be at least 1.");
        }
        
        if (edges.HasValue && edges.Value < 0)
        {
            throw new CoconaException("Number of edges must be at least 0.");
        }
        
        if (density.HasValue && (density.Value < 0 || density.Value > 1))
        {
            throw new CoconaException("Density must be between 0 and 1.");
        }

        if (edges.HasValue && edges.Value > nodes * (nodes - 1) / (directed ? 1 : 2))
        {
            throw new CoconaException("Too many edges for the number of nodes.");
        }
        
        if (precision < 0)
        {
            throw new CoconaException("Precision must be at least 0.");
        }
        
        if (precision > 15)
        {
            throw new CoconaException("Precision must be at most 15.");
        }
        
        #endregion

        var random = new Random(seed);
        var generator = new RandomGraphGenerator(random);

        var graph = edges.HasValue
            ? generator.GenerateGraph(nodes, edges.Value, directed)
            : generator.GenerateGraph(nodes, density!.Value, directed);

        SaveGraph(graph, filename, precision);
    }
    
    [Command("tree", Description = "Generate a tree")]
    public void GenerateTree(
        [Option("nodes")] int nodes,
        [Option("directed")] bool directed = false,
        [Option("precision")] int precision = 0,
        [Option("max-degree")] int? maxDegree = null,
        [Option("filename")] string? filename = null,
        [Option("seed")] int seed = 0)
    {
        #region Validation 
        if (nodes < 1)
        {
            throw new CoconaException("Number of nodes must be at least 1.");
        }
        
        if (precision < 0)
        {
            throw new CoconaException("Precision must be at least 0.");
        }
        
        if (precision > 15)
        {
            throw new CoconaException("Precision must be at most 15.");
        }
        
        #endregion

        var random = new Random(seed);
        var generator = new TreeGraphGenerator(random);

        var graph = generator.GenerateTree(nodes, directed, maxDegree);

        SaveGraph(graph, filename, precision);
    }
    
    private static void SaveGraph(MatrixGraph graph, string? filename, int precision)
    {
        if (!filename.IsNullOrEmpty())
        {
            File.WriteAllText(filename!, graph.ToString(precision));
        }
        else
        {
            Console.WriteLine(graph.ToString(precision));
        }
    }
}
