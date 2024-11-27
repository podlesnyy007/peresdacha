using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serialization
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Persona> personas = new List<Persona>();
            Print(personas);

            Console.WriteLine("Заполнение...\n");
            personas.Add(new Abiturient("Иванов", DateTime.Now.AddYears(-18), "КНиИТ"));
            personas.Add(new Student("Петров", DateTime.Now.AddYears(-20), "Психологии", 3));
            personas.Add(new Teacher("Сидоров", DateTime.Now.AddYears(-40), "ППИСО", "Преподаватель", 15));
            personas.Add(new Teacher("Михайлов", DateTime.Now.AddYears(-31).AddDays(1), "КНиИТ", "Лаборант", 5));

            Print(personas);

            Console.WriteLine("Сортировка...\n");
            personas.Sort();
            Print(personas);

            Console.WriteLine("Сериализация...\n");
            Serialize("input.dat", personas);

            Console.WriteLine("Фильтрация...\n");
            Console.WriteLine("Возраст от 20 до 30.");
            personas = FilterByAge(personas, 20, 30);
            Print(personas);

            Console.WriteLine("Десериализация...\n");
            personas = Deserialize("input.dat");
            Print(personas);
        }

        static List<Persona> FilterByAge(List<Persona> personas, int fromAge, int toAge)
        {
            List<Persona> filteredPersonas = new List<Persona>();
            foreach(Persona persona in personas)
                if (fromAge <= persona.GetAge() && persona.GetAge() <= toAge)
                    filteredPersonas.Add(persona);
            return filteredPersonas;
        }

        static void Print(List<Persona> personas)
        {
            if(personas.Count == 0)
            {
                Console.WriteLine("Список персон пуст.");
            }
            else
            {
                Console.WriteLine("Список персон:");
                foreach(Persona persona in personas)
                {
                    Console.WriteLine(persona.GetInformation());
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }

        static List<Persona> Deserialize(string path)
        {
            List<Persona> personas = new List<Persona>();
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                if (fileStream.Length > 0)
                {
                    personas = (List<Persona>)formatter.Deserialize(fileStream);
                }
            }
            return personas;
        }

        static void Serialize(string path, List<Persona> personas)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fileStream, personas);
            }
        }
    }
}
