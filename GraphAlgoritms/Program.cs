using System;

namespace GraphAlgoritms
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Enter number of Vertices");
            //int v = Convert.ToInt32(Console.ReadLine());

            //Console.WriteLine("\nEnter number of Edges");
            //int e = Convert.ToInt32(Console.ReadLine());

            #region FloydWarshall
            //FloydWarshall fd = new FloydWarshall(v, e);
            //fd.InitializeMatrix();
            //fd.FillMatrix();
            //fd.Print();

            //Console.WriteLine("After Solution");

            //fd.FindSolution();
            //fd.Print();
            #endregion

            #region Kruskal
            //Kruskal ks = new Kruskal(6, 9);
            //ks.FillMatrix();
            //ks.FindMinimumSpanningTree();

            //Console.WriteLine("After Solution");

            //ks.PrintStats();
            //Console.WriteLine("\nMinimum Spanning Tree: Set[" + ks.spanNodes + "]");

            //ks.PrintSpanningTree(ks.spanNodes);

            #endregion

            #region Prims
            //Prims ps = new Prims(6, 9);
            //ps.FillMatrix();
            //ps.FindMinimumSpanningTree();

            //ps.PrintData();

            #endregion

            #region Dijkstra
            
            #endregion

            Console.ReadLine();
        }
    }
}