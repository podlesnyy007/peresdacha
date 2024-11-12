using System;

namespace Serialization
{
    [Serializable]
    public abstract class Persona : IComparable<Persona>
    {
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }
        public String Faculty { get; set; }

        public Persona(string name, DateTime dateOfBirth, string faculty)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            Faculty = faculty;
        }

        public int CompareTo(Persona other)
        {
            return this.DateOfBirth.CompareTo(other.DateOfBirth);
        }

        public abstract int GetAge();

        public abstract String GetInformation();
    }
}
