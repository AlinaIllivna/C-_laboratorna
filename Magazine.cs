using System;
using System.Collections;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;

namespace Lab1
{

[Serializable]
public class Magazine : Edition, IRateAndCopy, IEnumerable
{
    private Frequency _frequency;
    private List<Person> _editors = new();
    private List<Article> _articles = new();
    public Magazine(string name, Frequency frequency, DateTime releaseDate, int circulation,
                    List<Person>? editors, List<Article>? articles)
        : base(name, releaseDate, circulation)
    {
        Frequency = frequency;
        Editors = editors ?? new List<Person>();
        Articles = articles ?? new List<Article>();
    }

    public Magazine(string name, Frequency frequency, DateTime releaseDate, int circulation)
        : this(name, frequency, releaseDate, circulation,  new List<Person>(), new List<Article>())
    {
    }

    public Magazine() : this("Default Magazine", Frequency.Monthly, DateTime.Now, 1000)
    {
    }

    public Frequency Frequency
    {
        get => _frequency;
        set => _frequency = value;
    }

   public List<Person> Editors
   {
    get => _editors ??= new List<Person>();
    set => _editors = value ?? new List<Person>();
    }

   public List<Article> Articles
   {
    get => _articles ??= new List<Article>();
    set => _articles = value ?? new List<Article>();
    }

    public double AverageRating
    {
        get
        {
            if (Articles == null || Articles.Count == 0)
                return 0.0;

            double sum = 0;
            foreach (Article article in Articles)
                sum += article.Rating;

            return sum / Articles.Count;
        }
    }

    public double Rating => AverageRating;

    public Edition Edition
    {
        get => this;
        set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            Name = value.Name;
            ReleaseDate = value.ReleaseDate;
            Circulation = value.Circulation;
        }
    }

    public bool this[Frequency freq] => Frequency == freq;

  public void AddArticles(params Article[] newArticles)
    {
        if (newArticles == null)
            throw new ArgumentNullException(nameof(newArticles));

        foreach (var article in newArticles)
        {
            if (article == null)
                continue;

            if (!_articles.Contains(article))
                _articles.Add(article);
        }
    }

     public void AddEditors(params Person[] newEditors)
    {
        if (newEditors == null)
            throw new ArgumentNullException(nameof(newEditors));

        foreach (var editor in newEditors)
        {
            if (editor == null)
                continue;

            if (!_editors.Contains(editor))
                _editors.Add(editor);
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new();

        sb.AppendLine($"Name: {Name}");
        sb.AppendLine($"Frequency: {Frequency}");
        sb.AppendLine($"ReleaseDate: {ReleaseDate:d}");
        sb.AppendLine($"Circulation: {Circulation}");

        sb.AppendLine("Editors:");
        if (Editors.Count > 0)
        {
            foreach (Person editor in Editors)
                sb.AppendLine(editor.ToString());
        }
        else
        {
            sb.AppendLine("(none)");
        }

        sb.AppendLine("Articles:");
        if (Articles.Count > 0)
        {
            foreach (Article article in Articles)
                sb.AppendLine(article.ToString());
        }
        else
        {
            sb.AppendLine("(none)");
        }

        return sb.ToString();
    }

    public override string ToShortString() => $"Name: {Name}, Frequency: {Frequency}, ReleaseDate: {ReleaseDate:d}, Circulation: {Circulation}, AvgRating: {AverageRating}";
    

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;

        if (obj is not Magazine magazine)
            return false;

        if (!base.Equals(obj) || Frequency != magazine.Frequency)
            return false;

        var editors = Editors;
        var otherEditors = magazine.Editors;
        var articles = Articles;
        var otherArticles = magazine.Articles;

        if (editors.Count != otherEditors.Count || articles.Count != otherArticles.Count)
            return false;

        for (int i = 0; i < editors.Count; i++)
        {
            if (!editors[i]!.Equals(otherEditors[i]))
                return false;
        }

        for (int i = 0; i < articles.Count; i++)
        {
            if (!articles[i]!.Equals(otherArticles[i]))
                return false;
        }

        return true;
    }

    public static bool operator ==(Magazine? m1, Magazine? m2)
    {
        if (ReferenceEquals(m1, m2))
            return true;

        if (m1 is null || m2 is null)
            return false;

        return m1.Equals(m2);
    }

    public static bool operator !=(Magazine? m1, Magazine? m2) =>  !(m1 == m2);
    

    public override int GetHashCode()
    {
        int hash = HashCode.Combine(base.GetHashCode(), Frequency);

        foreach (Person editor in Editors)
        {
            hash = HashCode.Combine(hash, editor);
        }

        foreach (Article article in Articles)
        {
            hash = HashCode.Combine(hash, article);
        }

        return hash;
    }

  public override object DeepCopy()
{
    var copiedEditors = new List<Person>();
    foreach (var editor in Editors)
    {
        copiedEditors.Add((Person)editor.DeepCopy());
    }

    var copiedArticles = new List<Article>();
    foreach (var article in Articles)
    {
        copiedArticles.Add((Article)article.DeepCopy());
    }

    return new Magazine(Name, Frequency, ReleaseDate, Circulation, copiedEditors, copiedArticles);
}


    public IEnumerator GetEnumerator()=> new MagazineEnumerator(this);
    

    public class MagazineEnumerator : IEnumerator
    {
        private readonly List<Article> _list;
        private int _position = -1;

        public MagazineEnumerator(Magazine magazine)
        {
            _list = new List<Article>();

            foreach (Article article in magazine.Articles)
            {
                if (!magazine.Editors.Contains(article.Author))
                    _list.Add(article);
            }
        }

        public object Current => _list[_position]!;

        public bool MoveNext()
        {
            _position++;
            return _position < _list.Count;
        }

        public void Reset()
        {
            _position = -1;
        }
    }

    public IEnumerable<Article> GetArticlesWithRatingGreaterThan(double rating)
    {
        foreach (Article article in Articles)
        {
            if (article.Rating > rating)
                yield return article;
        }
    }

    public IEnumerable<Article> GetArticlesWithTitleContaining(string substring)
    {
        if (string.IsNullOrEmpty(substring))
            yield break;

        foreach (Article article in Articles)
        {
            if (!string.IsNullOrEmpty(article.Title) &&
                article.Title.Contains(substring, StringComparison.CurrentCultureIgnoreCase))
            {
                yield return article;
            }
        }
    }

    public IEnumerable<Article> GetArticlesByEditors()
    {
        foreach (Article article in Articles)
        {
            if (Editors.Contains(article.Author))
                yield return article;
        }
    }

    public IEnumerable<Person> GetEditorsWithoutArticles()
    {
        foreach (Person editor in Editors)
        {
            bool hasArticle = false;

            foreach (Article article in Articles)
            {
                if (article.Author.Equals(editor))
                {
                    hasArticle = true;
                    break;
                }
            }

            if (!hasArticle)
                yield return editor;
        }

        
    }

    [Serializable]
    private class MagazineData
    {
        public string? Name { get; set; }
        public Frequency Frequency { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Circulation { get; set; }
        public List<Person>? Editors { get; set; }
        public List<Article>? Articles { get; set; }
    }

 // ---------------- SAVE / LOAD ----------------

public bool Save(string filename)
{
    try
    {
        using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var data = new MagazineData
            {
                Name = this.Name,
                Frequency = this.Frequency,
                ReleaseDate = this.ReleaseDate,
                Circulation = this.Circulation,
                Editors = this.Editors,
                Articles = this.Articles
            };

            JsonSerializer.Serialize(fs, data, options);
        }
        return true;
    }
    catch
    {
        return false;
    }
    finally
    {
        Console.WriteLine("Save завершено");
    }
}

public bool Load(string filename)
{
    try
    {
        if (!File.Exists(filename))
            return false;

        using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            MagazineData? data = JsonSerializer.Deserialize<MagazineData>(fs, options);
            if (data == null)
                return false;

            Name = data.Name ?? "Default Magazine";
            Frequency = data.Frequency;
            ReleaseDate = data.ReleaseDate;
            Circulation = data.Circulation;
            Editors = data.Editors ?? new List<Person>();
            Articles = data.Articles ?? new List<Article>();
        }

        return true;
    }
    catch
    {
        return false;
    }
    finally
    {
        Console.WriteLine("Load завершено");
    }
}

public static bool Save<T>(string filename, T obj) where T : Magazine
{
    return obj != null && obj.Save(filename);
}

public static bool Load<T>(string filename, T obj) where T : Magazine
{
    return obj != null && obj.Load(filename);
}

// ---------------- ADD FROM CONSOLE ----------------

public bool AddFromConsole()
    {
        try
        {
            Console.WriteLine("Enter article: Title, AuthorName, AuthorSurname, Rating, /n use comma for writing words and int number for rating");

            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return false;

            string[] parts = input.Split(',');

            if (parts.Length != 4)
            {
                Console.WriteLine("Wrong format!");
                return false;
            }

            string title = parts[0].Trim();
            string name = parts[1].Trim();
            string surname = parts[2].Trim();
            if (!double.TryParse(parts[3], out double rating))
            {
                Console.WriteLine("Rating error!");
                return false;
            }

            Person author = new Person(name, surname, DateTime.Now);
            Article article = new Article(author, title, rating);

            _articles.Add(article);

            Console.WriteLine("Article added!");

            return true;
        }
        catch
        {
            Console.WriteLine("Error input!");
            return false;
        }
    }
}

}

