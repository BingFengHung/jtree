using System.ComponentModel.Design.Serialization;
using System.Resources;

class Tree 
{
  readonly string _root;
  readonly int _deepLevel;

  public Tree(string root, int deepLevel)
  {
    _root = root;
    _deepLevel = deepLevel;
  }

  public void Print() 
  {
    PrintRootName();
    var entries = Directory.EnumerateFileSystemEntries(_root);
    PrintTreeStructure(entries, " ", 0);
  }
  
  private void PrintTreeStructure(IEnumerable<string> entries, string indent, int level)
  {
    if (_deepLevel == level) return; 
    
    var filteredEntries = entries.Where(e => !IsHidden(e)).ToList();
  
    int count = filteredEntries.Count;
    
    for (var i = 0; i < count; i++) 
    {
      var entry = filteredEntries[i];
      bool isLastEntry = i == filteredEntries.Count - 1;
      bool isDirectory = IsDirectory(entry);
      
      OutputEntry(entry, indent, isLastEntry, isDirectory);

      if (IsDirectory(entry)) {
        PrintTreeStructure(Directory.EnumerateFileSystemEntries(entry), indent + (isLastEntry ? "    " : "│   "), level + 1);
      } 
    } 
  }
  
  private void OutputEntry(string entry, string indent, bool isLastEntry, bool isDirectory) {
    var name = isDirectory ? new DirectoryInfo(entry).Name : new FileInfo(entry).Name; 
    string icon = isDirectory ? "📁" : "📄";
    Console.WriteLine(indent + (isLastEntry ? "└─" : "├─") + icon + " " + name);
  }

  
  private void PrintRootName()
  {
    if (IsDirectory(_root)) {
      var directory = new DirectoryInfo(_root);
      Console.WriteLine(directory.Name);
    } else {
      var file = new FileInfo(_root);
      Console.WriteLine(file.Name);
    }
  }
  
  private bool IsDirectory(string path) 
    => Directory.Exists(path);
  
  private bool IsHidden(string path)
  {
    var attributes = File.GetAttributes(path);
    return (attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
  }
}