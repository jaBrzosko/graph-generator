using System.Text;

namespace GraphGenerator.Domain.Graphs;

/// <summary>
/// Directed representation of a graph using an adjacency matrix.
/// </summary>
public class MatrixGraph
{
    private readonly double[,] matrix;

    public MatrixGraph(double[,] matrix)
    {
        this.matrix = matrix;
    }
    
    public MatrixGraph(int nodes)
    {
        matrix = new double[nodes, nodes];
    }
    
    public int VertexCount => matrix.GetLength(0);
    public int EdgeCount => matrix.Cast<double>().Count(d => d > 0);
    
    public double this[int i, int j] => matrix[i, j];
    public bool HasEdge(int i, int j) => matrix[i, j] > 0;
    public IEnumerable<int> Neighbours(int i) => Enumerable.Range(0, VertexCount).Where(j => HasEdge(i, j));
    public int Degree(int i) => Neighbours(i).Count();
    
    public void AddEdge(int i, int j, double weight)
    {
        matrix[i, j] = weight;
    }
    
    public void RemoveEdge(int i, int j)
    {
        matrix[i, j] = 0;
    }
    

    public string ToString(int precision)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < VertexCount; i++)
        {
            for (var j = 0; j < VertexCount; j++)
            {
                sb.Append(matrix[i, j].ToString($"F{precision}"));
                if (j < VertexCount - 1)
                {
                    sb.Append(" ");
                }
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }
    
    public override string ToString() => ToString(2);
}
