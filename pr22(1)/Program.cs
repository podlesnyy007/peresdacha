/*
В входном файле указывается количество вершин графа/орграфа и матрица смежности:
Для заданного графа:
добавить в граф ребро, соединяющее вершину а и b; 
*/

using System;
using System.IO;

class Graph
{
    private int[,] adMatrix; // хранение матрицы смежности графа
    private int vCount; // переменная, хранящая количество вершин графа

    public Graph(int vCount) // конструктор, инициализирует матрицу смежности
    {
        this.vCount = vCount;
        adMatrix = new int[vCount, vCount];
    }

    // метод, который читает граф из файла и возвращает объект класса Graph
    public static Graph ReadMatrix()
    {
        using (StreamReader reader = new StreamReader("d:/pr22_1/input.txt"))
        {
            int vCount = int.Parse(reader.ReadLine());

            Graph graph = new Graph(vCount);
            for (int i = 0; i < vCount; i++)
            {
                string[] row = reader.ReadLine().Split(' ');
                for (int j = 0; j < vCount; j++)
                {
                    graph.adMatrix[i, j] = int.Parse(row[j]);
                }
            }
            return graph;
        }
    }

    // метод добавления ребра в граф
    // source — начальная вершина, destination — конечная вершина, weight — вес ребра
    public void AddEdge(int source, int destination, int weight)
    {
        // проверяет, что индексы вершин находятся в допустимом диапазоне от 0 до vCount-1
        if (source >= 0 && source < vCount && destination >= 0 && destination < vCount)
        {
            adMatrix[source, destination] = weight; // устанавливает вес ребра между вершинами
        }
        else
        {
            Console.WriteLine("Неверные индексы вершин");
        }
    }

    // метод, который выводит матрицу смежности в консоль
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
        Graph graph = Graph.ReadMatrix(); // создает объект graph и заполняет его матрицей смежности из файла

        graph.PrintMatrix();

        Console.WriteLine("Добавление ребра. Введите номера вершин и вес ребра (через пробел):");
        string[] input = Console.ReadLine().Split(' ');
        int source = int.Parse(input[0]); // преобразует первый элемент массива в целое число 
        int destination = int.Parse(input[1]); // преобразует второй элемент массива в целое число
        int weight = int.Parse(input[2]); // преобразует третий элемент массива в целое число

        graph.AddEdge(source, destination, weight); // добавляет ребро в граф с указанными вершинами и весом
        graph.PrintMatrix();
    }
}
