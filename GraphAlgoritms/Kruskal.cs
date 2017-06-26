using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GraphAlgoritms
{
    class Kruskal
    {
        public Graph graph;
        public int spanNodes;
        private string[] GraphEdges = {
            "0 1 3",
            "0 2 4",
            "0 3 1",
            "1 2 2",
            "1 4 6",
            "2 4 2",
            "2 5 3",
            "3 5 5",
            "4 5 1",
        };

        public Edge[] ConnectingEdges;

        public Kruskal(int vertex, int edges)
        {
            spanNodes = -1;
            graph = new Graph();
            graph.V = vertex;
            graph.E = edges;
            graph.Matrtix = new int[vertex][];
            graph.Edge = new Edge[edges];
            ConnectingEdges = new Edge[edges];
            graph.Sets = new Sets[vertex];
            InitializeMatrix();
            InitializeSets();
        }

        private void InitializeMatrix()
        {
            for (int i = 0; i < graph.V; i++)
            {
                graph.Matrtix[i] = new int[graph.V];
                for (int j = 0; j < graph.V; j++)
                    graph.Matrtix[i][j] = -1;
            }
        }

        private void InitializeSets()
        {
            for (int i = 0; i < graph.Sets.Length; i++)
            {
                graph.Sets[i] = new Sets();
                graph.Sets[i].Vertices = new int[graph.V];
                for (int j = 0; j < graph.Sets[i].Vertices.Length; j++)
                    graph.Sets[i].Vertices[j] = -1;
                graph.Sets[i].Vertices[0] = i;
            }
        }

        public void FillMatrix()
        {
            for (int i = 0; i < graph.E; i++)
            {
                //Console.WriteLine("Enter Edge " + i + ". Vertex A to Vertex B and Edge Weight");
                //string[] a = Console.ReadLine().Split(' ');
                //if (a.Length > 3)
                //{
                //    Console.WriteLine("Please Enter From Vertex, To Vertex and Distance respectively.");
                //    i--;
                //    continue;
                //}

                string[] a = GraphEdges[i].Split(' ');
                graph.Edge[i] = new Edge();
                graph.Edge[i].ENum = i;
                graph.Edge[i].EWeight = Convert.ToInt32(a[2]);
                graph.Edge[i].StartVertex = Convert.ToInt32(a[0]);
                graph.Edge[i].EndVertex = Convert.ToInt32(a[1]);
                graph.Matrtix[Convert.ToInt32(a[0])][Convert.ToInt32(a[1])] = Convert.ToInt32(a[2]);
            }
        }

        public void PrintStats()
        {
            PrintMatrix();
            Console.WriteLine("\nEdge Data\n");
            PrintEdgeWeights();
            Console.WriteLine("\n\nSets\n");
            PrintSets();
            Console.WriteLine("\nConnecting Edges");
            PrintConnectingEdges();
        }

        public void FindMinimumSpanningTree()
        {
            SortEdges();
            for (int i = 0; i < graph.Edge.Length; i++)
            {
                int u = graph.Edge[i].StartVertex;
                int v = graph.Edge[i].EndVertex;
                if (!CheckIfInSameSet(u, v))
                {
                    spanNodes = UnionSet(u, v);
                    ConnectingEdges[i] = graph.Edge[i];
                }
            }
        }

        private bool CheckIfInSameSet(int u, int v)
        {
            bool gotU = false, gotV = false;
            for (int i = 0; i < graph.Sets.Length; i++)
            {
                for (int j = 0; j < graph.Sets[i].Vertices.Length; j++)
                {
                    if (graph.Sets[i].Vertices[j] == u)
                        gotU = true;
                    if (graph.Sets[i].Vertices[j] == v)
                        gotV = true;

                    if (gotU && gotV)
                        return true;
                }
                if (gotU || gotV)
                    return false;
            }

            return false;
        }

        private int UnionSet(int u, int v)
        {
            int uRow = FindRowFor(u);
            int vRow = FindRowFor(v);
            graph.Sets[uRow].Vertices = graph.Sets[uRow].Vertices.Union(graph.Sets[vRow].Vertices).Where(c => c != -1).ToArray();
            RemoveSetV(v);
            return u;
        }

        private int FindRowFor(int vertex)
        {
            for (int i = 0; i < graph.Sets.Length; i++)
            {
                for (int j = 0; j < graph.Sets[i].Vertices.Length; j++)
                {
                    if (graph.Sets[i].Vertices[j] == vertex)
                        return i;
                }
            }

            return -1;
        }

        private void RemoveSetV(int v)
        {
            for (int i = 0; i < graph.Sets[v].Vertices.Length; i++)
                graph.Sets[v].Vertices[i] = -1;
        }

        private void PrintMatrix()
        {
            for (int i = 0; i < graph.V; i++)
            {
                for (int j = 0; j < graph.V; j++)
                    Console.Write(graph.Matrtix[i][j] + "   ");
                Console.Write("\n");
            }
        }

        private void PrintSets()
        {
            for (int i = 0; i < graph.Sets.Length; i++)
            {
                Console.Write("Set " + i + ": ");
                for (int j = 0; j < graph.Sets[i].Vertices.Length; j++)
                    Console.Write(graph.Sets[i].Vertices[j] + "   ");
                Console.Write("\n");
            }
        }

        private void PrintEdgeWeights()
        {
            Console.Write("Edge:   ");
            foreach (var edge in graph.Edge)
                Console.Write(edge.ENum + " ");

            Console.Write("\nWeight: ");
            foreach (var edge in graph.Edge)
                Console.Write(edge.EWeight + " ");
        }

        private void SortEdges()
        {
            graph.Edge = graph.Edge.OrderBy(c => c.EWeight).ToArray();
        }

        public void PrintSpanningTree()
        {
            for (int j = 0; j < graph.Sets[spanNodes].Vertices.Length; j++)
                Console.Write(graph.Sets[spanNodes].Vertices[j] + "   ");
        }

        public void PrintSpanningTree(int spanNode)
        {
            for (int j = 0; j < graph.Sets[spanNode].Vertices.Length; j++)
                Console.Write(graph.Sets[spanNode].Vertices[j] + "   ");
        }

        private void PrintConnectingEdges()
        {
            foreach (var edge in ConnectingEdges)
                if (edge != null)
                    Console.Write(edge.ENum + " ");
        }
    }

   
}
