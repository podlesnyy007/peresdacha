/*
В входном файле указывается количество вершин графа/орграфа и матрица смежности:
Для заданного графа:
найти все вершины графа, попадающие в N-периферию для вершины А; 
*/

using System;
using System.Collections.Generic;
using System.IO;

class Graph
{
    private int[,] adMatrix;
    private bool[] visited;

    public Graph(int[,] matrix)
    {
        adMatrix = matrix;
        visited = new bool[matrix.GetLength(0)];
    }

    public List<int> FindNode(int startV, int weight)
    {
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(startV);//добав верш в очередь
        visited[startV] = true;//помеч начал верш как посещ

        List<int> result = new List<int>();//соед реб задан веса

        while (queue.Count > 0)
        {
            int currentV = queue.Dequeue();//извл верш из начала очереди

            for (int i = 0; i < adMatrix.GetLength(0); i++)//i сосед верш
            {
                if (adMatrix[currentV, i] == weight && !visited[i])
                {
                    queue.Enqueue(i);//добав i в очере
                    visited[i] = true;
                    result.Add(i);// добав в спис резул
                }
            }
        }

        return result;
    }

    public void NovSet()
    {
        for (int i = 0; i < visited.Length; i++)
        {
            visited[i] = false;
        }
    }

    public long[] Dijkstr(int v, out int[] p)
    {
        long[] d = new long[adMatrix.GetLength(0)];//кратчай расстоян
        p = new int[adMatrix.GetLength(0)];//откуда мы пришли в дан верш
        Array.Fill(d, int.MaxValue);
        d[v] = 0;

        for (int i = 0; i < adMatrix.GetLength(0); i++)
        {
            int minIndex = -1;
            for (int j = 0; j < adMatrix.GetLength(0); j++)
            {
                if (!visited[j] && (minIndex == -1 || d[j] < d[minIndex]))
                {
                    minIndex = j;
                }
            }

            if (d[minIndex] == int.MaxValue)
            {
                break;
            }

            visited[minIndex] = true;//помеч как посещ

            for (int j = 0; j < adMatrix.GetLength(0); j++)
            {
                if (adMatrix[minIndex, j] > 0 && d[minIndex] + adMatrix[minIndex, j] < d[j])
                {
                    d[j] = d[minIndex] + adMatrix[minIndex, j];
                    p[j] = minIndex;
                }
            }
        }

        return d;
    }

    public void InMaxDistance(int v)
    {
        NovSet();
        int[] p;
        long[] d = Dijkstr(v, out p);//кратч расстоян
        long max = 0;

        for (int i = 0; i < p.Length; i++)
        {
            if (d[i] > max && d[i] != int.MaxValue)
            {
                max = d[i];
            }
        }

        if (max == 0)
        {
            Console.WriteLine("Из заданной вершины другие вершины недостижимы");
        }
        else
        {
            Console.Write($"На наибольшем удалении от вершины {v} находятся вершины: ");
            for (int i = 0; i < d.Length; i++)
            {
                if (d[i] == max)
                {
                    Console.Write($"{i} ");
                }
            }
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        string fileName = "d:/pr22_2/input.txt";

        int[,] adMatrix;
        int size;

        using (StreamReader reader = new StreamReader(fileName))
        {
            size = int.Parse(reader.ReadLine());
            adMatrix = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                string[] row = reader.ReadLine().Split(' ');
                for (int j = 0; j < size; j++)
                {
                    adMatrix[i, j] = int.Parse(row[j]);
                }
            }
        }

        Graph graph = new Graph(adMatrix);
        int startV = 4;
        int weightT = 1;

        List<int> nodesW = graph.FindNode(startV, weightT);
        Console.WriteLine($"Вершины с весом ребер {weightT} относительно вершины {startV}:");
        foreach (int node in nodesW)
        {
            Console.Write(node + " ");
        }

        // Нахождение наиболее удаленных точек
        graph.InMaxDistance(startV);
    }
}
