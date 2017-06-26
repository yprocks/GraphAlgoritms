﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgoritms
{
    public class Graph
    {
        // Kruskals Implementation
        public Edge[] Edge { get; set; }
        public Sets[] Sets { get; set; }

        // Prims Implementation
        public QData[] QData { get; set; }
        public Node[] Nodes { get; set; }

        // Floyd Warshall Implementation
        public int V { get; set; }
        public int E { get; set; }
        public int[][] Matrtix { get; set; }

        // Dijkstra Algorithm

    }


    public class QData
    {
        public int Vertex { get; set; }
        public int KeyValue { get; set; }
        public int ParentValue { get; set; }
    }

    public class Node
    {
        public int Vertex { get; set; }
        public int Distance { get; set; }
        public Node[] Neighbours { get; set; }
    }

    public class Neighbours
    {
        public int Vertex { get; set; }
        public int Distance { get; set; }
    }

    public class Edge
    {
        public int ENum { get; set; }
        public int EWeight { get; set; }
        public int StartVertex { get; set; }
        public int EndVertex { get; set; }
    }

    public class Sets
    {
        public int[] Vertices { get; set; }
    }
}
