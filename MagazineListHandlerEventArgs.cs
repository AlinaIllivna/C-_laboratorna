//  клас для передачі інформації про подію
using System;

// Делегат
public delegate void MagazineListHandler(object source, MagazineListHandlerEventArgs args);

public class MagazineListHandlerEventArgs : EventArgs
{
    public string CollectionName { get; set; }
    public string ChangeType { get; set; }
    public int ElementIndex { get; set; }

    public MagazineListHandlerEventArgs(string collectionName, string changeType, int elementIndex)
    {
        CollectionName = collectionName;
        ChangeType = changeType;
        ElementIndex = elementIndex;
    }

    public override string ToString()=> $"Collection: {CollectionName}, Change: {ChangeType}, Index: {ElementIndex}";
    

    
}