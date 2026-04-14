//накопичення інформації про зміни в колекціях
using System.Collections.Generic;
using System.Text;

public class Listener
{
    private List<ListEntry> _entries = new List<ListEntry>();

    public void OnMagazineAdded(object source, MagazineListHandlerEventArgs args)
    {
        _entries.Add(new ListEntry(args.CollectionName, args.ChangeType, args.ElementIndex));
    }

    public void OnMagazineReplaced(object source, MagazineListHandlerEventArgs args)
    {
        _entries.Add(new ListEntry(args.CollectionName, args.ChangeType, args.ElementIndex));
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        foreach (var entry in _entries)
        {
            sb.AppendLine(entry.ToString());
        }

        return sb.ToString();
    }
}