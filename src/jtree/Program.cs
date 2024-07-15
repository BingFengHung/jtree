List<string> marks = ["├", "─", "└", "│"];

var root =  Environment.CurrentDirectory;

bool IsDirectory(string path) 
{
  return Directory.Exists(path);
}

bool IsHidden(string path) 
{
  var attributes = File.GetAttributes(path);
  
  return (attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
}

void OutputEntry(string entry, string indent, bool isLastEntry, bool isDirectory) {
  var name = isDirectory ? new DirectoryInfo(entry).Name : new FileInfo(entry).Name; 
  Console.WriteLine(indent + (isLastEntry ? "└─" : "├─") + name);
}

int deepLevel = 5;

void Tree(IEnumerable<string> entries, string indent, int level) {
  if (deepLevel == level) return;
  
  var filteredEntries = entries.Where(e => !IsHidden(e)).ToList();
  
  int count = filteredEntries.Count;
  
  for (var i = 0; i < count; i++) 
  {
    var entry = filteredEntries[i];
    bool isLastEntry = i == filteredEntries.Count - 1;
    bool isDirectory = IsDirectory(entry);
    
    OutputEntry(entry, indent, isLastEntry, isDirectory);

    if (IsDirectory(entry)) {
      Tree(Directory.EnumerateFileSystemEntries(entry).ToList(), indent + (isLastEntry ? "    " : "│   "), level + 1);
    } 
  }
}

void PrintRootName() 
{
  if (IsDirectory(root)) {
    var dir = new DirectoryInfo(root);    
    Console.WriteLine(dir.Name);
  } else {
    var file = new FileInfo(root);    
    Console.WriteLine(file.Name);
  }
}


try { 
  var entries =  Directory.EnumerateFileSystemEntries(root);
  PrintRootName();
  Tree(entries, " ", 0);
} catch(UnauthorizedAccessException ex) {
  Console.WriteLine(ex.Message);
}