using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1
{
    public class MagazineCollection
    {
        private List<Magazine> _magazines = new List<Magazine>();
        public string? CollectionName { get; set; }

        public event MagazineListHandler? MagazineAdded;
        public event MagazineListHandler? MagazineReplaced;


        protected virtual void OnMagazineAdded(string changeType, int index)
        {
            MagazineAdded?.Invoke(this, new MagazineListHandlerEventArgs(CollectionName ?? "Unknown", changeType, index));
             }

        protected virtual void OnMagazineReplaced(string changeType, int index)
        {
           MagazineReplaced?.Invoke(this, new MagazineListHandlerEventArgs(CollectionName ?? "Unknown", changeType, index));
            }

        public bool Replace(int j, Magazine magazine)
{
    if (j < 0 || j >= _magazines.Count)
        return false;

    _magazines[j] = magazine;

    OnMagazineReplaced("Element replaced", j);

    return true;
}

    public bool RemoveAt(int index)
{
    if (index < 0 || index >= _magazines.Count)
        return false;

    _magazines.RemoveAt(index);
    return true;
}

     public Magazine this[int index]
{
    get
    {
        return _magazines[index];
    }
    set
    {
        _magazines[index] = value;
        OnMagazineReplaced("Element replaced via indexer", index);
    }
}

     public void AddDefaults(){
    _magazines.Add(new Magazine("Forbes", Frequency.Monthly, new DateTime(2024, 1, 1), 5000));
    OnMagazineAdded("Added default", _magazines.Count - 1);

    _magazines.Add(new Magazine("Science", Frequency.Weekly, new DateTime(2023, 5, 10), 3000));
    OnMagazineAdded("Added default", _magazines.Count - 1);
}

       public void AddMagazines(params Magazine[] mags)
{
    foreach (var m in mags)
    {
        _magazines.Add(m);
        OnMagazineAdded("Added", _magazines.Count - 1);
    }
}

        public override string ToString()
        {
            string result = "";
            foreach (var m in _magazines)
                result += m.ToString() + "\n";
            return result;
        }

        public string ToShortString()
        {
            string result = "";
            foreach (var m in _magazines)
                result += m.ToShortString() + "\n";
            return result;
        }

        //  СОРТУВАННЯ

        public void SortByName()
        {
            _magazines.Sort();
        }

        public void SortByDate()
        {
            _magazines.Sort(new EditionDateComparer());
        }

        public void SortByCirculation()
        {
            _magazines.Sort(new EditionCirculationComparer());
        }

        //  LINQ

        public double MaxRating
        {
            get
            {
                if (_magazines.Count == 0) return 0;
                return _magazines.Max(m => m.AverageRating);
            }
        }

        public IEnumerable<Magazine> MonthlyMagazines
        {
            get
            {
                return _magazines.Where(m => m.Frequency == Frequency.Monthly);
            }
        }

        public List<Magazine> RatingGroup(double value)
        {
            return _magazines.Where(m => m.AverageRating >= value).ToList();
        }
    }
}