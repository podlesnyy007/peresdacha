using System;
using System.Collections.Generic;
using System.IO;

class Graph
{
    private int[,] adMatrix; // Матрица смежности
    private bool[] visited; // Массив для отслеживания посещённых вершин

    // Конструктор, принимающий матрицу смежности
    public Graph(int[,] matrix)
    {
        adMatrix = matrix;
        visited = new bool[matrix.GetLength(0)];
    }

    // Сбрасывает массив visited, помечая все вершины как непосещённые
    public void ResetVisited()
    {
        for (int i = 0; i < visited.Length; i++)
        {
            visited[i] = false;
        }
    }

    // Алгоритм Дейкстры: находит кратчайшие пути от вершины v
    public long[] Dijkstra(int v, out int[] p) // v - заданная вершина, p[] — массив предшествующих вершин.
    {
        int n = adMatrix.GetLength(0); // // Получаем количество вершин в графе
        long[] d = new long[n]; // d[] — массив, в котором хранится кратчайшее расстояние от начальной вершины до каждой вершины
        p = new int[n]; // массив, в котором хранится информация, откуда пришли в каждую вершину
        Array.Fill(d, int.MaxValue); // Инициализация расстояний бесконечностью
        d[v] = 0; // // Расстояние от начальной вершины до самой себя всегда равно 0

        // Главный цикл выбирает непосещенную вершину с минимальным расстоянием,
        // помечает её как посещенную и обновляет расстояния до её соседей
        for (int i = 0; i < n; i++)
        {
            int minIndex = -1;
            // Поиск непосещенной вершины с минимальным расстоянием
            for (int j = 0; j < n; j++)
            {
                if (!visited[j] && (minIndex == -1 || d[j] < d[minIndex]))
                {
                    minIndex = j;
                }
            }

            // Если все вершины были посещены или минимальное расстояние стало бесконечным, завершаем цикл
            if (minIndex == -1 || d[minIndex] == int.MaxValue)
            {
                break;
            }

            visited[minIndex] = true; // Помечаем найденную вершину как посещенную

            // Обновляем расстояния для соседей текущей вершины
            for (int j = 0; j < n; j++)
            {
                // Если существует ребро между вершинами и новое расстояние меньше текущего, обновляем расстояние
                if (adMatrix[minIndex, j] > 0 && d[minIndex] + adMatrix[minIndex, j] < d[j])
                {
                    d[j] = d[minIndex] + adMatrix[minIndex, j]; // Обновление расстояния
                    p[j] = minIndex; // Установка предшествующей вершины
                }
            }
        }
        return d; // Возвращаем массив с кратчайшими расстояниями от вершины v до всех остальных
    }

    // Метод для поиска вершин в N-периферии
    public List<int> FindNPeriphery(int startV, int distance)
    {
        ResetVisited(); // сбрасывается массив, чтобы избежать ошибок
        int[] p; // Массив предшествующих вершин
        long[] d = Dijkstra(startV, out p); // Получаем массив кратчайших расстояний от startV до всех вершин
        List<int> result = new List<int>(); // Список для хранения вершин на заданном расстоянии

        // Находим вершины, расстояние до которых равно distance
        for (int i = 0; i < d.Length; i++)
        {
            if (d[i] == distance)
            {
                result.Add(i);
            }
        }

        return result; // Возвращаем список вершин, которые находятся на расстоянии distance от startV
    }
}
}

class Program
{
    static void Main(string[] args)
    {
        string fileName = "d:/pr22_2/input.txt";

        int[,] adMatrix;
        int size;  // Переменная для хранения размера графа (число вершин)

        // Чтение матрицы смежности из файла
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

        // Заданная вершина A и расстояние N
        Console.Write("Введите расстояние startV: "); // Задать номер вершины A (0 — индекс первой вершины)
        int startV = int.Parse(Console.ReadLine());

        Console.Write("Введите расстояние N: "); // Задать значение N (расстояние для периферии)
        int N = int.Parse(Console.ReadLine());

        // Поиск N-периферии
        List<int> nPeriphery = graph.FindNPeriphery(startV, N);
        Console.WriteLine($"Вершины на расстоянии {N} от вершины {startV}:");

        // Вывод результатов
        if (nPeriphery.Count == 0)
        {
            Console.WriteLine("Нет вершин в указанной N-периферии.");
        }
        else
        {
            foreach (int node in nPeriphery)
            {
                Console.Write(node + " ");
            }
            Console.WriteLine();
        }
    }
}
