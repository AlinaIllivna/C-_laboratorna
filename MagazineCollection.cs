using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1
{
    public class MagazineCollection
    {
        private List<Magazine> _magazines = new List<Magazine>();

       public void AddDefaults(){
        _magazines.Add(new Magazine("Forbes", Frequency.Monthly, new DateTime(2024, 1, 1), 5000));
        _magazines.Add(new Magazine("Science", Frequency.Weekly, new DateTime(2023, 5, 10), 3000));
    }

        public void AddMagazines(params Magazine[] mags)
        {
            _magazines.AddRange(mags);
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