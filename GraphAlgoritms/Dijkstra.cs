using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphAlgoritms
{
    class Dijkstra
    {
        private const int INF = StaticClass.INF;
        private Graph graph;
        private string[] GraphEdges = {
            "1 2 10",
            "1 3 5",
            "2 3 2",
            "2 4 1",
            "3 2 3",
            "3 4 9",
            "3 5 2",
            "4 5 4",
            "5 4 6",
            "5 1 7"
        };
        public int RootNode;

        public Dijkstra(int vertices, int edges)
        {
            graph = new Graph();
            graph.V = vertices;
            graph.E = edges;

            graph.DRows = new DRows[vertices];
            graph.Nodes = new Node[vertices];
            RootNode = 0;
            // VertexQueue = new Queue<int>();
            // graph.Matrtix = new int[vertices][];
            // graph.Edge = new Edge[edges];
            // graph.Sets = new Sets[vertices];
            InitializeMinSpanTreeNodes();
        }

        private void InitializeMinSpanTreeNodes()
        {
            for (int i = 0; i < graph.DRows.Length; i++)
            {
                graph.DRows[i] = new DRows();
                graph.DRows[i].Vertex = (i + 1);
                graph.DRows[i].Distance = INF;
                graph.DRows[i].ToParent = -1;
                graph.DRows[i].isPulled = false;
            }
        }

        public void SetRootNode(int root)
        {
            RootNode = root;
            graph.DRows[RootNode - 1].Distance = 0;
        }

        public void FillMatrix()
        {
            for (int i = 0; i < GraphEdges.Length; i++)
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
                int StartVertex = Convert.ToInt32(a[0]);
                int EndVertex = Convert.ToInt32(a[1]);
                int Dist = Convert.ToInt32(a[2]);
                //string[] vertices = a[0].Split(' ');
                //string[] distance = a[1].Split(' ');
                bool vertexEsists = false;
                int vertexNum = i;
                for (int k = 0; k < graph.Nodes.Length; k++)
                {
                    if (graph.Nodes[k] != null)
                        if (graph.Nodes[k].Vertex == StartVertex)
                        {
                            vertexEsists = true;
                            vertexNum = k;
                            break;
                        }
                }
                if (!vertexEsists)
                {
                    vertexNum = vertexNum > 0 ? StartVertex - 1 : vertexNum;
                    graph.Nodes[vertexNum] = new Node();
                    graph.Nodes[vertexNum].Vertex = StartVertex;
                    graph.Nodes[vertexNum].Distance = 0;
                    graph.Nodes[vertexNum].Neighbours = new Node[graph.V];
                }
                int j = 0;
                bool childExists = false;
                while (graph.Nodes[vertexNum].Neighbours[j] != null)
                {
                    if (graph.Nodes[vertexNum].Neighbours[j].Vertex == EndVertex)
                    {
                        childExists = true;
                        break;
                    }
                    j++;
                }

                if (!childExists)
                {
                    graph.Nodes[vertexNum].Neighbours[j] = new Node();
                    graph.Nodes[vertexNum].Neighbours[j].Vertex = EndVertex;
                    graph.Nodes[vertexNum].Neighbours[j].Distance = Dist;
                }
                // graph.Nodes[i].Distance = Convert.ToInt32(a[2]);

                //graph.Edge[i] = new Edge();
                //graph.Edge[i].ENum = i;
                //graph.Edge[i].EWeight = Convert.ToInt32(a[2]);
                //graph.Edge[i].StartVertex = Convert.ToInt32(a[0]);
                //graph.Edge[i].EndVertex = Convert.ToInt32(a[1]);
                //graph.Matrtix[Convert.ToInt32(a[0])][Convert.ToInt32(a[1])] = Convert.ToInt32(a[2]);
            }
        }

        public void FillDijkstraTable()
        {
            var pullNode = RootNode;
            while (!IsAllVisited())
            {
                Node node = graph.Nodes.FirstOrDefault(c => c.Vertex == pullNode);
                int j = 0;
                if (node == null)
                    continue;

                node.isVisited = true;

                int u = node.Vertex - 1;

                var d = graph.DRows.FirstOrDefault(v => v.Vertex == node.Vertex);
                d.isPulled = true;

                while (node.Neighbours[j] != null)
                {
                    int v = node.Neighbours[j].Vertex - 1;

                    if (graph.DRows[v].Distance > graph.DRows[u].Distance + node.Neighbours[j].Distance)
                    {
                        graph.DRows[v].Distance = graph.DRows[u].Distance + node.Neighbours[j].Distance;
                        graph.DRows[v].ToParent = node.Vertex;
                    }

                    j++;
                }

                var nextNode = graph.DRows.Where(c => !c.isPulled).OrderBy(c => c.Distance).FirstOrDefault();

                if (nextNode != null)
                    pullNode = nextNode.Vertex;
                else
                    break;
            }
        }

        private bool IsAllVisited()
        {
            foreach (var item in graph.Nodes)
                if (!item.isVisited)
                    return false;
            return true;
        }

        public void PrintData()
        {
            Console.WriteLine("Nodes ");
            PrintNodes();
            Console.WriteLine("\nQData ");
            PrintDRows();
            Console.WriteLine("\nSpanning Tree");
            PrintShortestPath();
        }

        private void PrintDRows()
        {
            Console.WriteLine("Vert | Dist | ToPar");
            for (int i = 0; i < graph.DRows.Length; i++)
            {
                Console.WriteLine(graph.DRows[i].Vertex + " | " + graph.DRows[i].Distance + " | " + graph.DRows[i].ToParent);
            }
        }

        private void PrintNodes()
        {
            for (int i = 0; i < graph.Nodes.Length; i++)
            {
                if (graph.Nodes[i] != null)
                {
                    Console.Write(graph.Nodes[i].Vertex + ": ");
                    for (int j = 0; j < graph.Nodes[i].Neighbours.Length; j++)
                        if (graph.Nodes[i].Neighbours[j] != null)
                            Console.Write(graph.Nodes[i].Neighbours[j].Vertex + " ");
                    Console.Write("\n");
                }
            }
        }

        private void PrintShortestPath()
        {
            foreach (var item in graph.DRows)
            {
                if (item.ToParent == -1)
                    continue;
                Console.WriteLine("Visit V" + item.Vertex + " via V" + item.ToParent + " by distance = " + item.Distance);
            }
        }

        public void FindShortestPathFromRootToNode(int EndNode)
        {
            Console.WriteLine("Distance from " + RootNode + " to " + EndNode + " via");
            FindPath(RootNode, EndNode);

            Console.Write(EndNode);
            Console.WriteLine("\nTotal Distance " + graph.DRows.FirstOrDefault(c => c.Vertex == EndNode).Distance);
        }

        private void FindPath(int start, int end)
        {
            if (start == end)
                return;
            var parent = graph.DRows.FirstOrDefault(c => c.Vertex == end);

            if (parent == null)
            {
                Console.WriteLine("End of path. No path Found");
                return;
            }
            
            FindPath(start, parent.ToParent);

            Console.Write(parent.ToParent + " -> ");
        
        }
    }
}
