using System;
using System.Collections;
using System.Text;

namespace Lab1
{
public class Magazine : Edition, IRateAndCopy, IEnumerable
{
    private Frequency _frequency;
    private ArrayList _editors = new();
    private ArrayList _articles = new();

    public Magazine(string name, Frequency frequency, DateTime releaseDate, int circulation,
                    ArrayList? editors, ArrayList? articles)
        : base(name, releaseDate, circulation)
    {
        Frequency = frequency;
        Editors = editors ?? new ArrayList();
        Articles = articles ?? new ArrayList();
    }

    public Magazine(string name, Frequency frequency, DateTime releaseDate, int circulation)
        : this(name, frequency, releaseDate, circulation, new ArrayList(), new ArrayList())
    {
    }

    public Magazine() : this("Default Magazine", Frequency.Monthly, DateTime.Now, 1000)
    {
    }

    public Frequency Frequency
    {
        get => _frequency;
        init => _frequency = value;
    }

    public ArrayList Editors
    {
        get => _editors ??= new ArrayList();
        init => _editors = value ?? new ArrayList();
    }

    public ArrayList Articles
    {
        get => _articles ??= new ArrayList();
        init => _articles = value ?? new ArrayList();
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
        init
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
        if (newArticles == null || newArticles.Length == 0)
            return;

        foreach (Article article in newArticles)
        {
            if (article != null)
                Articles.Add(article);
        }
    }

    public void AddEditors(params Person[] newEditors)
    {
        if (newEditors == null || newEditors.Length == 0)
            return;

        foreach (Person editor in newEditors)
        {
            if (editor != null)
                Editors.Add(editor);
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
        ArrayList editorsCopy = new ArrayList();
        foreach (Person editor in Editors)
        {
            editorsCopy.Add(editor.DeepCopy());
        }

        ArrayList articlesCopy = new ArrayList();
        foreach (Article article in Articles)
        {
            articlesCopy.Add(article.DeepCopy());
        }

        return new Magazine(Name, Frequency, ReleaseDate, Circulation, editorsCopy, articlesCopy);
    }

    public IEnumerator GetEnumerator()=> new MagazineEnumerator(this);
    

    public class MagazineEnumerator : IEnumerator
    {
        private readonly ArrayList _list;
        private int _position = -1;

        public MagazineEnumerator(Magazine magazine)
        {
            _list = new ArrayList();

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
}
}