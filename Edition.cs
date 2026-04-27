using System;
using System.Collections.Generic;
namespace Lab1
{
public class Edition : IComparable<Edition>
{
    protected string _name= null!;
    protected DateTime _releaseDate;
    protected int _circulation;


     public int CompareTo(Edition? other)
     {
        if (other == null) return 1;
        return string.Compare(Name, other.Name, StringComparison.Ordinal);
        }

    public Edition(string name, DateTime releaseDate, int circulation)
    {
        Name = name;
        ReleaseDate = releaseDate;
        Circulation = circulation;
    }

    public Edition() : this("Default edition", DateTime.Now, 1) { }

    public string Name
    {
        get => _name;
        set => _name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public DateTime ReleaseDate
    {
        get => _releaseDate;
        set => _releaseDate = value;
    }

    public int Circulation
    {
        get => _circulation;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Тираж має бути більшим за 0.");
            _circulation = value;
        }
    }

    public virtual object DeepCopy() => new Edition(Name, ReleaseDate, Circulation);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is not Edition other) return false;

        return Name == other.Name &&
               ReleaseDate == other.ReleaseDate &&
               Circulation == other.Circulation;
    }

    public override int GetHashCode() => HashCode.Combine(Name, ReleaseDate, Circulation);

    public static bool operator ==(Edition? e1, Edition? e2)
    {
        if (ReferenceEquals(e1, e2)) return true;
        if (e1 is null || e2 is null) return false;
        return e1.Equals(e2);
    }

    public static bool operator !=(Edition? e1, Edition? e2) => !(e1 == e2);

    public override string ToString() => $"Name: {Name}, Release date: {ReleaseDate:d}, Circulation: {Circulation}";

     public virtual string ToShortString()=> ToString();
        
}

public class EditionDateComparer : IComparer<Edition>
{
    public int Compare(Edition? x, Edition? y)
    {
        if (x == null || y == null) return 0;
        return x.ReleaseDate.CompareTo(y.ReleaseDate);
    }
}

public class EditionCirculationComparer : IComparer<Edition>
{
    public int Compare(Edition? x, Edition? y)
    {
        if (x == null || y == null) return 0;
        return x.Circulation.CompareTo(y.Circulation);
    }
}

}

