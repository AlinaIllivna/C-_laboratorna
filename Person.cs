using System;
// using System.Text;

class Person
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
        init { _name = value; }
    }

    // Властивість для прізвища
    public string Surname
    {
        get { return _surname; }
        init { _surname = value; }
    }

    // Властивість для дати народження
    public DateTime BirthDate
    {
        get { return _birthDate; }
        init { _birthDate = value; }
    }

    // Властивість для року народження
    public int BirthYear
    {
        get { return _birthDate.Year; }
        set { _birthDate = new DateTime(value, _birthDate.Month, _birthDate.Day); }
    }

    // Перевизначення ToString()
    public override string ToString() => $"Name: {Name}, Surname: {Surname}, Birth date: {BirthDate:d}";

    // Короткий рядок
    public virtual string ToShortString()=> $"{Surname} {Name}";
    
}

