using System;

namespace Lab1
{
public class Article : IRateAndCopy
{
    public Person Author { get; init; }
    public string Title { get; init; }
    public double Rating { get; init; }

    public Article(Person author, string title, double rating)
    {
        Author = author;
        Title = title;
        Rating = rating;
    }

    public Article() : this(new Person(), "Title", 0) { }

    public override string ToString() => $"Author: {Author.ToShortString()}, Title: {Title}, Rating: {Rating}";


    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is not Article other) return false;

        return Author.Equals(other.Author) &&
               Title == other.Title &&
               Rating == other.Rating;
    }

    
    public static bool operator ==(Article? a1, Article? a2)
    {
        if (ReferenceEquals(a1, a2)) return true;
        if (a1 is null || a2 is null) return false;
        return a1.Equals(a2);
    }

   
    public static bool operator !=(Article? a1, Article? a2)  => !(a1 == a2);

    
    public override int GetHashCode() => HashCode.Combine(Author, Title, Rating);

    public virtual object DeepCopy()=> new Article(Author.DeepCopy(), Title, Rating);
}
}