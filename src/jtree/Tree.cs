class Tree 
{
  readonly string _root;
  readonly int _deepLevel;
  readonly TreeOptions _treeOption;

  public Tree(string root, TreeOptions treeOption)
  {
    _root = root;
    _treeOption = treeOption;
    _deepLevel = _treeOption.Depth;
  }

  public void Print() 
  {
    PrintRootName();
    var entries = GetDirectoryFileSystemEntries(_root); //Directory.EnumerateFileSystemEntries(_root);
    PrintTreeStructure(entries, " ", 0);
  }
  
  private void PrintTreeStructure(IEnumerable<string> entries, string indent, int level)
  {
    if (_deepLevel == level) return; 
    
    List<string> filteredEntries = entries.Where(e => !IsHidden(e)).ToList();
    if (_treeOption.ShowHidden) 
      filteredEntries = entries.ToList();
  
    int count = filteredEntries.Count;
    
    for (var i = 0; i < count; i++) 
    {
      var entry = filteredEntries[i];
      bool isLastEntry = i == filteredEntries.Count - 1;
      bool isDirectory = IsDirectory(entry);
      
      OutputEntry(entry, indent, isLastEntry, isDirectory);

      if (IsDirectory(entry)) {
        PrintTreeStructure(GetDirectoryFileSystemEntries(entry), indent + (isLastEntry ? "    " : "│   "), level + 1);
      } 
    } 
  }
  
  private IEnumerable<string> GetDirectoryFileSystemEntries(string path) 
  {
    var entries =Directory.EnumerateFileSystemEntries(path); 
    if (_treeOption.IsOrder)
    { 
      var directories = entries.Where(IsDirectory).OrderBy(e => e).ToList();
      var files = entries.Where(e => !IsDirectory(e)).OrderBy(e => e).ToList();
      var sortedEntries = directories.Concat(files);
      return sortedEntries;
    }

    return entries;


  }
  
  private void OutputEntry(string entry, string indent, bool isLastEntry, bool isDirectory) {
    var name = isDirectory ? new DirectoryInfo(entry).Name : new FileInfo(entry).Name; 
    string icon = isDirectory ? "📁" : "📄";
    if (!_treeOption.IsShowIcon) icon = string.Empty;
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