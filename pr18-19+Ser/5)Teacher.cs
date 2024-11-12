using System;

namespace Serialization
{
    [Serializable]
    public class Teacher : Persona
    {
        public string Post { get; set; }

        public int Expirience { get; set; }

        public Teacher(string name, DateTime dateOfBirth, string faculty, string post, int expirience)
            : base(name, dateOfBirth, faculty)
        {
            Post = post;
            Expirience = expirience;
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
                $"Имя преподавателя: {Name}\n" +
                $"Дата рождения: {DateOfBirth.ToShortDateString()}\n" +
                $"Возраст: {GetAge()}\n" +
                $"Факультет: {Faculty}\n" +
                $"Должность: {Post}\n" +
                $"Стаж: {Expirience}";
        }
    }
}
