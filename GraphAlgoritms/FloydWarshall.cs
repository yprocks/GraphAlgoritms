using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgoritms
{
    class FloydWarshall
    {
        private const int INF = StaticClass.INF;
        public Graph graph;

        public FloydWarshall(int vertex, int edges)
        {
            graph = new Graph();
            graph.V = vertex;
            graph.E = vertex;
            graph.Matrtix = new int[vertex][];
            InitializeMatrix();
        }

        private void InitializeMatrix()
        {
            for (int i = 0; i < graph.V; i++)
            {
                graph.Matrtix[i] = new int[graph.V];
                for (int j = 0; j < graph.V; j++)
                {
                    if (i != j)
                        graph.Matrtix[i][j] = INF;
                    else
                        graph.Matrtix[i][j] = 0;
                }
            }
        }

        public void FillMatrix()
        {
            for (int i = 0; i < graph.E; i++)
            {
                string[] a = Console.ReadLine().Split(' ');
                if (a.Length > 3)
                {
                    Console.WriteLine("Please Enter From Vertex, To Vertex and Distance respectively.");
                    i--;
                    continue;
                }
                graph.Matrtix[Convert.ToInt32(a[0])][Convert.ToInt32(a[1])] = Convert.ToInt32(a[2]);
            }
        }

        public void Print()
        {
            for (int i = 0; i < graph.V; i++)
            {
                for (int j = 0; j < graph.V; j++)
                    Console.Write(graph.Matrtix[i][j] + "   ");
                Console.Write("\n");
            }
        }

        public void FindSolution()
        {
            for (int k = 0; k < graph.V; k++)
                for (int i = 0; i < graph.V; i++)
                    for (int j = 0; j < graph.V; j++)
                        if (graph.Matrtix[i][k] + graph.Matrtix[k][j] < graph.Matrtix[i][j])
                            graph.Matrtix[i][j] = graph.Matrtix[i][k] + graph.Matrtix[k][j];
        }
    }

   
}