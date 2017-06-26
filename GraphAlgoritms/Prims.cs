using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphAlgoritms
{
    class Prims
    {
        private const int INF = StaticClass.INF;
        public Graph graph;
        public QData RootNode;
        public Queue<int> VertexQueue;
        private string[] GraphEdges = {
            "1 2 3",
            "1 3 4",
            "2 3 2",
            "2 1 3",
            "3 4 5",
            "3 2 2",
            "3 1 4",
            "2 5 4",
            "2 4 7",
            "4 5 2",
            "4 6 1",
            "4 2 7",
            "4 3 5",
            "5 2 4",
            "5 4 2",
            "5 6 6",
            "6 4 1",
            "6 5 6"
        };

        public Prims(int vertices, int edges)
        {
            graph = new Graph();
            graph.V = vertices;
            graph.E = edges;
            graph.QData = new QData[vertices];
            graph.Nodes = new Node[vertices];
            VertexQueue = new Queue<int>();
            //graph.Matrtix = new int[vertices][];
            //graph.Edge = new Edge[edges];
            //graph.Sets = new Sets[vertices];
            InitializeMinSpanTreeNodes();

        }

        private void InitializeMinSpanTreeNodes()
        {
            for (int i = 0; i < graph.QData.Length; i++)
            {
                graph.QData[i] = new QData();
                graph.QData[i].Vertex = (i + 1);
                graph.QData[i].KeyValue = INF;
                graph.QData[i].ParentValue = -1;
                VertexQueue.Enqueue(i);
            }
            graph.QData[0].KeyValue = 0;
            RootNode = graph.QData[0];
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

        public void FindMinimumSpanningTree()
        {
            QData[] data = new QData[graph.QData.Length];
            graph.QData.CopyTo(data, 0);
            while (VertexQueue.Count != 0)
            {
                VertexQueue.Dequeue();
                QData curdata = data.Where(c => c != null).OrderBy(c => c.KeyValue).FirstOrDefault();
                int u = curdata.Vertex;

                data.SetValue(null, u - 1);

                Node node = graph.Nodes.FirstOrDefault(c => c != null && c.Vertex == u);
                int j = 0;
                if (node == null)
                    continue;

                while (node.Neighbours[j] != null)
                {
                    if (data.FirstOrDefault(c => c != null && c.Vertex == node.Neighbours[j].Vertex) != null)
                    {
                        int v = node.Neighbours[j].Vertex - 1;
                        if (node.Neighbours[j].Distance < graph.QData[v].KeyValue)
                        {
                            graph.QData[v].KeyValue = node.Neighbours[j].Distance;
                            graph.QData[v].ParentValue = u;
                        }
                    }
                    j++;
                }
            }
        }


        public void PrintData()
        {
            Console.WriteLine("Nodes ");
            PrintNodes();
            Console.WriteLine("\nQData ");
            PrintQData();
            Console.WriteLine("\nSpanning Tree");
            PrintSpanningTree();
        }

        private void PrintQData()
        {
            Console.WriteLine("Vertex | Key | Parent");
            for (int i = 0; i < graph.QData.Length; i++)
            {
                Console.WriteLine(graph.QData[i].Vertex + " | " + graph.QData[i].KeyValue + " | " + graph.QData[i].ParentValue);
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

        private void PrintSpanningTree()
        {
            foreach (var item in graph.QData)
            {
                if (item.ParentValue == -1)
                    continue;
                Console.WriteLine("Visit V"+ item.Vertex + " via V" + item.ParentValue + " by distance = " + item.KeyValue);
            }
        }
    }

}
