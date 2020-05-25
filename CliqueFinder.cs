﻿// C# implementation of the approach 
using Cliques;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

class Clique
{
    static int v;
    static int[,] edges;

    static int findCliques(List<int> R, List<int> P, List<int> X)
    {
        if (P.Count == 0 && X.Count == 0)
        {
            Console.WriteLine("Clique found: " + String.Join(",", R));
            return 1;
        }
        int found = 0;
        foreach (var v in P.ToList())
        {
            var N = new List<int>(Neighbour(v));
            var new_R = new List<int>(R);
            var new_P = new List<int>(P.Intersect(N).ToList());
            var new_X = new List<int>(X.Intersect(N).ToList());
            new_R.Add(v);
            found += findCliques(new_R, new_P, new_X);
            P.Remove(v);
            X.Add(v);
        }
        return found;
    }

    static List<int> Neighbour(int vertex)
    {
        List<int> neighbours = new List<int>();
        for(int i = 0; i < v; ++i)
        {
            if(edges[vertex-1,i] == 1)
            {
                neighbours.Add(i+1);
            }
        }
        return neighbours;
    }
    
    static int[,] readFile()
    {
        int e;
        int x = 0;
        using (StreamReader sr = new StreamReader("graph.txt"))
        {
            String line;
            String[] tokens = sr.ReadLine().Split();
            v = Int32.Parse(tokens[0]);
            e = Int32.Parse(tokens[1]);
            int[,] edges = new int[e, 2];

            //Console.WriteLine("Grafas su " + v + " viršūnėmis ir " + e + " briaunomis");
             
            while ((line = sr.ReadLine()) != null)
            {
                String[] edge = line.Split();
                edges[x, 0] = Int32.Parse(edge[0]);
                edges[x, 1] = Int32.Parse(edge[1]);
                ++x;
            }
            return edges;
        }
    }

    static void createMatrixFromFile()
    {
        int[,] e = readFile();
        int size = e.GetLength(0);
        edges = new int[v, v];

        //sudaroma matrica
        for (int i = 0; i < size; i++)
        {
            edges[e[i, 0] - 1, e[i, 1] - 1] = 1;
            edges[e[i, 1] - 1, e[i, 0] - 1] = 1;
        }
        for (int i = 0; i < v; ++i)
        {
            edges[i, i] = 0;
        }

        /*
        int rowLength = edges.GetLength(0);
        int colLength = edges.GetLength(1);

        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                Console.Write(string.Format("{0} ", edges[i, j]));
            }
            Console.Write(Environment.NewLine + Environment.NewLine);
        }
        */
    }

    static long runAlgorithm()
    {
        //virsuniu sarasas
        List<int> R = new List<int>();
        List<int> P = new List<int>();
        List<int> X = new List<int>();
        for (int i = 0; i < v; ++i)
        {
            P.Add(i + 1);
        }

        //algoritmo laiko skaiciavimas
        var timer = new Stopwatch();
        timer.Start();
        //algoritmas
        //findCliques(R, P, X);
        Console.WriteLine("Found cliques: " + findCliques(R, P, X));
        timer.Stop();
        //TimeSpan timeTaken = timer.Elapsed;
        var elapsedTime = timer.ElapsedMilliseconds;
        return elapsedTime;
    }

    public static void Main(String[] args)
    {
        Console.WriteLine("Pasirinkti: ");
        Console.WriteLine("1. Generuoti grafa ir paleisti algoritma (sukurti graph.txt faila)");
        Console.WriteLine("2. Naudoti grafa apibrezta graph.txt faile");
        Console.WriteLine("3. Automatinis algoritmo testavimas algoritmo analizei");
        int sel = Int32.Parse(Console.ReadLine());
        if(sel == 1)
        {
            GraphGenerator gg = new GraphGenerator();
            gg.generateGraph();
            createMatrixFromFile();
            Console.WriteLine("Algoritmas uztruko: " + runAlgorithm() + "ms");
        }
        else if(sel == 2)
        {
            createMatrixFromFile();
            Console.WriteLine("Algoritmas uztruko: " + runAlgorithm() + "ms");
        }
        else if (sel == 3)
        {
            for(int i = 1000; i <= 10000; i+= 1000)
            {
                var times = new List<long>();
                for(int j = 0; j < 10; ++j)
                {
                    GraphGenerator gg = new GraphGenerator();
                    gg.generateGraph(i, 2 * i);
                    createMatrixFromFile();
                    long elapsedTime = runAlgorithm();
                    times.Add(elapsedTime);
                    //Console.WriteLine(i + " algoritmas uztruko: " + elapsedTime + "ms");
                }
                Console.WriteLine("Su " + i + " virsunemis algoritmas vidutiniskai uztruko " + times.Average() + " ms");
            }
        }
        
        Console.ReadLine();
    }
}
