using System;

namespace Serialization
{
    [Serializable]
    public class Student : Persona
    {
        public int Course { get; set; }

        public Student(string name, DateTime dateOfBirth, string faculty, int course)
            : base(name, dateOfBirth, faculty)
        {
            Course = course;
        }

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
                $"Имя студента: {Name}\n" +
                $"Дата рождения: {DateOfBirth.ToShortDateString()}\n" +
                $"Возраст: {GetAge()}\n" +
                $"Факультет: {Faculty}\n" +
                $"Курс:{Course}";
        }
    }
}
