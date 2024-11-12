/*
В файле input.txt хранится последовательность целых чисел. 
По входной последовательности построить дерево бинарного поиска и найти для него:
количество узлов, значение которых больше среднего арифметического.
*/

using System;
using System.IO;

// Класс Node описывает отдельный узел дерева
public class Node
{
    public int data; // Значение узла
    public Node left, right; // Ссылки на левый и правый дочерние узлы

    public Node(int item) // Конструктор класса Node для инициализации узла с заданным значением
    {
        data = item; // Присваиваем значению узла переданный параметр
        left = right = null; // Устанавливаем ссылки на поддеревья как пустые
    }
}

// Построение и работа с деревом
public class BinaryTree
{
    public Node root; // Корень дерева

    public void Insert(int data) // Метод вставки нового элемента в дерево
    {
        root = InsertRec(root, data);
    }

    // Рекурсивный метод вставки нового элемента в дерево
    private Node InsertRec(Node root, int data)
    {
        if (root == null) // Если корень пуст, создаем новый узел
        {
            root = new Node(data);
            return root;
        }

        if (data < root.data) // Если значение меньше корня, вставляем в левое поддерево
        {
            root.left = InsertRec(root.left, data);
        }
        else if (data > root.data) // Если значение больше корня, вставляем в правое поддерево
        {
            root.right = InsertRec(root.right, data);
        }

        return root;
    }

    // Метод подсчета количества узлов со значением больше среднего арифметического
    public int CountNodesGreaterThanAverage()
    {
        int sum = 0, count = 0;
        CalculateSumAndCount(root, ref sum, ref count);

        if (count == 0) // Если нет узлов в дереве, возвращаем 0
        {
            return 0;
        }

        int average = sum / count;
        return CountNodesGreaterThanAverageRec(root, average);
    }

    // Рекурсивный метод подсчета количества узлов со значением больше среднего арифметического
    private int CountNodesGreaterThanAverageRec(Node root, int average)
    {
        if (root == null)
        {
            return 0;
        }

        int count = 0;
        if (root.data > average) // Если значение узла больше среднего, увеличиваем счетчик
        {
            count++;
        }

        count += CountNodesGreaterThanAverageRec(root.left, average); // Рекурсивный вызов для левого поддерева
        count += CountNodesGreaterThanAverageRec(root.right, average); // Рекурсивный вызов для правого поддерева

        return count;
    }

    // Вспомогательный метод для вычисления суммы и количества узлов в дереве
    private void CalculateSumAndCount(Node root, ref int sum, ref int count)
    {
        if (root == null)
        {
            return;
        }

        sum += root.data; // Добавляем значение текущего узла к сумме
        count++;

        CalculateSumAndCount(root.left, ref sum, ref count); // Рекурсивный вызов для левого поддерева
        CalculateSumAndCount(root.right, ref sum, ref count); // Рекурсивный вызов для правого поддерева
    }
}

public class Program
{
    public static void Main()
    {
        // Считываем содержимое файла input.txt и разделяем его по пробелам на отдельные строки
        string[] input = File.ReadAllText("input.txt").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        // Создаем объект бинарного дерева (Создаёт пустое бинарное дерево)
        BinaryTree tree = new BinaryTree();

        // Преобразуем каждую строку в целое число и вставляем в дерево (добавляется в дерево методом Insert)
        foreach (string s in input)
        {
            tree.Insert(int.Parse(s));
        }

        // Считаем количество узлов, значение которых больше среднего арифметического
        int count = tree.CountNodesGreaterThanAverage();
        Console.WriteLine("количество узлов, значение которых больше среднего арифметического: " + count);
    }
}
