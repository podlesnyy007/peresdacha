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
    private int[,] adMatrix; // двумерный массив (матрица смежности) для хранения весов ребер между вершинами
    private bool[] visited; // массив для отслеживания посещенных вершин при поиске по графу

    // конструктор, принимающий матрицу смежности matrix и сохраняющий её в поле adMatrix
    public Graph(int[,] matrix)
    {
        adMatrix = matrix;
        visited = new bool[matrix.GetLength(0)]; // создает массив visited длиной, равной количеству вершин графа (количеству строк/столбцов матрицы)
    }

    // Метод FindNode находит вершины, связанные с заданной вершиной startV через ребра с весом weight
    public List<int> FindNode(int startV, int weight)
    {
        Queue<int> queue = new Queue<int>(); // создается очередь для обхода графа
        queue.Enqueue(startV); // начальная вершина startV добавляется в очередь
        visited[startV] = true; // помечаем начальную вершину как посещенную

        List<int> result = new List<int>(); //соединяет ребра заданнного веса

        while (queue.Count > 0) // выполняется, пока есть вершины в очереди
        {
            int currentV = queue.Dequeue(); // извлекает вершину из очереди

            for (int i = 0; i < adMatrix.GetLength(0); i++) // проверяет смежные вершины
            {
                if (adMatrix[currentV, i] == weight && !visited[i]) // если не была посещена, она добавляется в очередь
                                                                    // и помечается как посещенная, а также добавляется в список result
                {
                    queue.Enqueue(i);//добав i в очере
                    visited[i] = true;
                    result.Add(i);// добав в спис резул
                }
            }
        }
        return result;
    }

    // Метод NovSet сбрасывает массив visited, помечая все вершины как непосещенные
    public void NovSet()
    {
        for (int i = 0; i < visited.Length; i++)
        {
            visited[i] = false;
        }
    }

    // Метод Dijkstr реализует алгоритм Дейкстры для нахождения кратчайших путей от вершины v
    public long[] Dijkstr(int v, out int[] p)
    {
        long[] d = new long[adMatrix.GetLength(0)]; // массив расстояний от начальной вершины v до всех остальных
        p = new int[adMatrix.GetLength(0)]; // массив, в котором хранится информация, откуда пришли в каждую вершину
        Array.Fill(d, int.MaxValue); // заполняем
        d[v] = 0;

        // Главный цикл выбирает непосещенную вершину с минимальным расстоянием,
        // помечает её как посещенную и обновляет расстояния до её соседей
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

            visited[minIndex] = true; //помеч как посещ

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

    // Метод InMaxDistance находит вершины, которые находятся на наибольшем расстоянии от вершины v
    public void InMaxDistance(int v)
    {
        NovSet(); // сбрасывает метки посещения с помощью NovSet
        int[] p;
        long[] d = Dijkstr(v, out p); // Вычисляет кратчайшие расстояния d с помощью алгоритма Дейкстры
        long max = 0;

        // Находит максимальное расстояние max среди доступных вершин
        for (int i = 0; i < p.Length; i++)
        {
            if (d[i] > max && d[i] != int.MaxValue)
            {
                max = d[i];
            }
        }

        // Если max == 0, выводит, что другие вершины недостижимы
        if (max == 0)
        {
            Console.WriteLine("Из заданной вершины другие вершины недостижимы");
        }
        // Иначе выводит вершины, удаленные на максимальное расстояние max
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

        // Находит и выводит вершины, наиболее удаленные от startV
        graph.InMaxDistance(startV);
    }
}
