/*
В файле input.txt хранится последовательность целых чисел. 
По входной последовательности построить дерево бинарного поиска и:
удалить из дерева все узлы с нечетным значением так, чтобы дерево осталось деревом бинарного поиска
*/

using System;
using System.IO;

// Класс Node описывает структуру узла дерева
public class Node
{
    public int Value; // значение узла
    public Node Left; // ссылка на левый дочерний узел
    public Node Right; // ссылка на правый дочерний узел

    public Node(int value) // Конструктор создает узел с заданным значением
    {
        Value = value;
        Left = null;
        Right = null;
    }
}

// Класс BinaryTree описывает само бинарное дерево поиска
public class BinaryTree
{
    public Node root; // Корень дерева

    // Метод Insert вставляет значение value в дерево
    public void Insert(int value)
    {
        root = InsertRec(root, value);
    }

    // Рекурсивный вспомогательный метод, который размещает новое значение по правилам дерева бинарного поиска
    private Node InsertRec(Node root, int value)
    {
        if (root == null) // создается новый узел, который становится корнем дерева
        {
            root = new Node(value);
            return root;
        }

        if (value < root.Value) // значение добавляется в левое поддерево
        {
            root.Left = InsertRec(root.Left, value);
        }

        else if (value > root.Value) // значение добавляется в правое поддерево
        {
            root.Right = InsertRec(root.Right, value);
        }
        return root;
    }

    // Метод удаляет узел по его значению, соблюдая структуру бинарного дерева поиска
    private static void DeleteNode(ref Node t, int key)
    {
        if (t == null)
        {
            return;
        }

        // Поиск узла, который нужно удалить
        if (key < t.Value) // нужный узел находится в левом поддереве
        {
            DeleteNode(ref t.Left, key);
        }
        else if (key > t.Value) // нужный узел находится в правом поддереве
        {
            DeleteNode(ref t.Right, key);
        }
        // Удаление найденного узла
        else
        {
            if (t.Left == null) // У узла нет левого поддерева
            {
                t = t.Right; // переназначаем узел t на его правого потомка
            }
            else if (t.Right == null) // У узла нет правого поддерева
            {
                t = t.Left; // заменяем узел его левым потомком
            }
            else // У узла есть оба поддерева
            {
                Del(t, ref t.Left);
            }
        }
    }

    //Метод Del находит самый правый (максимальный) узел в левом поддереве,
    //заменяет значение узла t найденным значением и удаляет этот узел
    private static void Del(Node t, ref Node tr)
    {
        if (tr.Right != null) // Если у узла tr есть правый потомок, продолжаем поиск правого потомка
        {
            Del(t, ref tr.Right);
        }
        else
        {
            t.Value = tr.Value; // Когда самый правый узел найден (tr.Right == null), его значение копируется в удаляемый узел t
            tr = tr.Left; // Затем tr (правый потомок в левой части дерева) заменяется его левым потомком, что фактически удаляет его из дерева
        }
    }

    // Метод, который удаляет узлы с нечетными значениями
    public void DeleteOddNodes(ref Node t)
    {
        if (t != null)
        {
            // Oбходит дерево в глубину
            DeleteOddNodes(ref t.Left);
            DeleteOddNodes(ref t.Right);

            // Удаляем узел, если его значение нечетное
            if (t.Value % 2 != 0)
            {
                DeleteNode(ref t, t.Value);
            }
        }
    }
}

class Program
{
    static void Main()
    {
        // Создается пустое бинарное дерево
        BinaryTree tree = new BinaryTree();

        // Чтение чисел из файла и добавление их в дерево
        string[] lines = File.ReadAllText("input.txt").Split(' ');
        foreach (string line in lines)
        {
            tree.Insert(int.Parse(line));
        }

        Console.WriteLine("Дерево до удаления нечетных узлов:");
        PrintInOrder(tree.root);
        Console.WriteLine();

        // Удаляем узлы с нечетными значениями
        tree.DeleteOddNodes(ref tree.root);

        Console.WriteLine("Дерево после удаления нечетных узлов:");
        PrintInOrder(tree.root);
    }

    // Метод обходит дерево в порядке (левое поддерево → узел → правое поддерево) и выводит все значения узлов
    static void PrintInOrder(Node node)
    {
        if (node != null)
        {
            PrintInOrder(node.Left);
            Console.Write(node.Value + " ");
            PrintInOrder(node.Right);
        }
    }
}
