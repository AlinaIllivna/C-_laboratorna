using System;
// using System.Text;

namespace Lab1
{
[Serializable]
public class Person : IRateAndCopy
{
    private string _name =null!;
    private string _surname=null!;
    private DateTime _birthDate;

    // Конструктор з параметрами
    public Person (string name, string surname, DateTime birthDate)
    {
      
      Name = name;
      Surname = surname;
      BirthDate = birthDate;
      }

    // Конструктор без параметрів
    public Person() : this(name: "Alina", surname: "Illivna", birthDate: new DateTime(2006, 7, 28))
    {
        
    }
    

    // Властивість для імені
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    // Властивість для прізвища
    public string Surname
    {
        get { return _surname; }
        set { _surname = value; }
    }

    // Властивість для дати народження
    public DateTime BirthDate
    {
        get { return _birthDate; }
        set { _birthDate = value; }
    }

    // Властивість для року народження
    public int BirthYear
    {
        get { return _birthDate.Year; }
        set { _birthDate = new DateTime(value, _birthDate.Month, _birthDate.Day); }
    }

    public double Rating => 0;

    // Перевизначення ToString()
    public override string ToString() => $"Name: {Name}, Surname: {Surname}, Birth date: {BirthDate:d}";

    // Короткий рядок
    public virtual string ToShortString()=> $"{Surname} {Name}";

    // --------------------
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true; //якщо це той самий об’єкт → одразу true
        if (obj is not Person other) return false;
        
        return Name == other.Name &&
            Surname == other.Surname &&
            BirthDate == other.BirthDate;
            }

    public override int GetHashCode() => HashCode.Combine(Name, Surname, BirthDate);
        

    public static bool operator ==(Person? p1, Person? p2)
    {
        if (ReferenceEquals(p1, p2)) return true;
        if (p1 is null || p2 is null) return false;
        return p1.Equals(p2);
        }

    public static bool operator !=(Person? p1, Person? p2) => !(p1 == p2);
    
    public object DeepCopy()=> new Person(Name, Surname, BirthDate);
        
        

}
}




