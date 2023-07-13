using System;
using System.Collections.Generic;

class Edge : IComparable<Edge>
{
    public int Source { get; set; }
    public int Destination { get; set; }
    public int Weight { get; set; }

    public int CompareTo(Edge? other)
    {
        if (other == null)
            return 1;

        return Weight.CompareTo(other.Weight);
    }
}

class Graph
{
    public int Vertices { get; }
    private List<Edge> edges;

    public Graph(int v)
    {
        Vertices = v;
        edges = new List<Edge>();
    }

    public void AddEdge(int source, int destination, int weight)
    {
        edges.Add(new Edge { Source = source, Destination = destination, Weight = weight });
    }

    private int Find(int[] parent, int vertex)
    {
        if (parent[vertex] != vertex)
            parent[vertex] = Find(parent, parent[vertex]);

        return parent[vertex];
    }

    private void Union(int[] parent, int x, int y)
    {
        int xSet = Find(parent, x);
        int ySet = Find(parent, y);
        parent[ySet] = xSet;
    }

    public int GetMinimumSpanningTree(out List<Edge> minimumSpanningTree)
    {
        List<Edge> result = new List<Edge>();
        edges.Sort(); 

        int[] parent = new int[Vertices];
        for (int i = 0; i < Vertices; i++)
            parent[i] = i;

        int minimumWeight = 0;
        int edgeCount = 0;
        int index = 0;

        while (edgeCount < Vertices - 1)
        {
            Edge nextEdge = edges[index++];
            int sourceParent = Find(parent, nextEdge.Source);
            int destinationParent = Find(parent, nextEdge.Destination);

            if (sourceParent != destinationParent)
            {
                result.Add(nextEdge);
                Union(parent, sourceParent, destinationParent);
                minimumWeight += nextEdge.Weight;
                edgeCount++;
            }
        }

        minimumSpanningTree = result;
        return minimumWeight;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Graph graph = new Graph(10);

        graph.AddEdge(0, 1, 18);
        graph.AddEdge(0, 2, 19);
        graph.AddEdge(0, 3, 13);
        graph.AddEdge(1, 3, 27);
        graph.AddEdge(1, 4, 17);
        graph.AddEdge(2, 3, 14);
        graph.AddEdge(2, 5, 20);
        graph.AddEdge(3, 4, 11);
        graph.AddEdge(3, 5, 15);
        graph.AddEdge(4, 6, 16);
        graph.AddEdge(4, 7, 25);
        graph.AddEdge(5, 8, 22);
        graph.AddEdge(6, 7, 28);
        graph.AddEdge(6, 8, 26);
        graph.AddEdge(7, 9, 24);
        graph.AddEdge(8, 9, 29);

        List<Edge> minimumSpanningTree;
        int minimumWeight = graph.GetMinimumSpanningTree(out minimumSpanningTree);

        Console.WriteLine("Минимальное остовное дерево:");
        foreach (Edge edge in minimumSpanningTree)
        {
            Console.WriteLine($"{edge.Source} - {edge.Destination} : {edge.Weight}");
        }

        Console.WriteLine("Минимальный вес: " + minimumWeight);
        Console.WriteLine("Количество остовных деревьев: " + Math.Pow(2, graph.Vertices - 1));
    }
}