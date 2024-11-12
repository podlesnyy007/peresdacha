using System;

namespace Serialization
{
    [Serializable]
    public class Abiturient : Persona
    {
        public Abiturient(string name, DateTime dateOfBirth, string faculty) : base(name, dateOfBirth, faculty) { }

        public override int GetAge()
        {
            int age = DateTime.Now.Year - DateOfBirth.Year;
            if (DateOfBirth.AddYears(age) > DateTime.Now)
                --age;
            return age;
        }

        public override string GetInformation()
        {
            return
                $"Имя абитуриента: {Name}\n" +
                $"Дата рождения: {DateOfBirth.ToShortDateString()}\n" +
                $"Возраст: {GetAge()}\n" +
                $"Факультет: {Faculty}";
        }
    }
}
