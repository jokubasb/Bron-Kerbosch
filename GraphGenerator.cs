using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliques
{
    class GraphGenerator
    {
        public void generateGraph()
        {
            Console.WriteLine("--------------------Grafo generatorius--------------------");
            string path = "graph.txt";
            Random r = new Random();

            using (StreamWriter sw = File.CreateText(path))
            {
                Console.WriteLine("Iveskite grafo virsuniu skaiciu: ");
                int vertex = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Iveskite briaunu skaiciu: ");
                int edge = Int32.Parse(Console.ReadLine());
                sw.WriteLine(vertex + " " + edge);
                if (edge >= vertex)
                {
                    for (int i = 1; i <= (edge / vertex); ++i)
                    {
                        for (int j = 1; j <= vertex; ++j)
                        {
                            sw.WriteLine(j + " " + r.Next(1, vertex - 1));
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Ne visos virsunes bus sujungtos!");
                }
            }
        }

        public void generateGraph(int v, int e)
        {
            string path = "graph.txt";
            Random r = new Random();

            using (StreamWriter sw = File.CreateText(path))
            {
                int vertex = v;
                int edge = e;
                //Console.WriteLine("Generuojamas grafas su " + v + " virsunemis ir " + e + " briaunomis.");
                sw.WriteLine(vertex + " " + edge);
                if (edge >= vertex)
                {
                    for (int i = 1; i <= (edge / vertex); ++i)
                    {
                        for (int j = 1; j <= vertex; ++j)
                        {
                            sw.WriteLine(j + " " + r.Next(1, vertex - 1));
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Ne visos virsunes bus sujungtos!");
                }
            }
        }
    }
}
