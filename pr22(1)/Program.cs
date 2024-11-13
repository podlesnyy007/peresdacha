/*
В входном файле указывается количество вершин графа/орграфа и матрица смежности:
Для заданного графа:
добавить в граф ребро, соединяющее вершину а и b; 
*/

using System;
using System.IO;

class Graph
{
    private int[,] adMatrix;
    private int vCount;

    public Graph(int vCount)
    {
        this.vCount = vCount;
        adMatrix = new int[vCount, vCount];
    }

    public static Graph ReadMatrix()
    {
        using (StreamReader reader = new StreamReader("d:/pr22_1/input.txt"))
        {
            int vCount = int.Parse(reader.ReadLine());//стр с верш графа

            Graph graph = new Graph(vCount);
            for (int i = 0; i < vCount; i++)//переб строки
            {
                string[] row = reader.ReadLine().Split(' ');
                for (int j = 0; j < vCount; j++)//переб элм в стр
                {
                    graph.adMatrix[i, j] = int.Parse(row[j]);
                }
            }
            return graph;
        }
    }

    public void AddEdge(int source, int destination, int weight)
    {
        if (source <= 0 && source < vCount && destination <= 0 && destination < vCount)
        {
            adMatrix[source, destination] = weight;//соед верш с весом
        }
        else
        {
            Console.WriteLine("Неверные индексы вершин");
        }
    }

    public void PrintMatrix()
    {
        Console.WriteLine("Матрица смежности:");
        for (int i = 0; i < vCount; i++)
        {
            for (int j = 0; j < vCount; j++)
            {
                Console.Write(adMatrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Graph graph = Graph.ReadMatrix();

        graph.PrintMatrix();

        Console.WriteLine("Добавление ребра. Введите номера вершин и вес ребра (через пробел):");
        string[] input = Console.ReadLine().Split(' ');
        int source = int.Parse(input[0]);
        int destination = int.Parse(input[1]);
        int weight = int.Parse(input[2]);

        graph.AddEdge(source, destination, weight);
        graph.PrintMatrix();
    }
}
