List<string> marks = ["├", "─", "└", "│"];

var root =  Environment.CurrentDirectory;
var entries =  Directory.EnumerateFileSystemEntries(root).ToList();

bool IsDirectory(string path) 
{
  return Directory.Exists(path);
}

bool IsHidden(string path) 
{
  var attributes = File.GetAttributes(path);
  
  return (attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
}

void OutputDirectoryEntry(string entry, string indent, bool isLastEntry) {
    var directory = new DirectoryInfo(entry);
    Console.WriteLine(indent + (isLastEntry ? "└─" : "├─") + directory.Name);
}

void OutputFileEntry(string entry, string indent, bool isLastEntry) {
    var file = new FileInfo(entry);
    Console.WriteLine(indent + (isLastEntry ? "└─" : "├─") + file.Name);
}

int deepLevel = 3;

void Tree(List<string> entries, string indent, int level) {
  if (deepLevel == level) return;
  
  var filteredEntries = entries.Where(e => !IsHidden(e)).ToList();
  
  int count = filteredEntries.Count;
  
  for (var i = 0; i < count; i++) {
    var entry = filteredEntries[i];
    bool isLastEntry = i == filteredEntries.Count - 1;

    if (IsDirectory(entry)) {
      OutputDirectoryEntry(entry, indent, isLastEntry);
      Tree(Directory.EnumerateFileSystemEntries(entry).ToList(), indent + (isLastEntry ? "    " : "│   "), level + 1);
    } else {
      OutputFileEntry(entry, indent, isLastEntry);
    }
  }
}


try { 
  if (IsDirectory(root)) {
    var dir = new DirectoryInfo(root);    
    Console.WriteLine(dir.Name);
  } else {
    var file = new FileInfo(root);    
    Console.WriteLine(file.Name);
  }
  Tree(entries, " ", 0);
} catch(UnauthorizedAccessException ex) {
  Console.WriteLine(ex.Message);
}