List<string> marks = ["├", "─", "└", "│"];

var root =  Environment.CurrentDirectory;
var entries =  Directory.EnumerateFileSystemEntries(root).ToList();

int deepLevel = 3;

void Tree(List<string> entries, string indent, int level) {
  if (deepLevel == level) return;
  
  for (var i = 0; i < entries.Count(); i++) {
    var entry = entries[i];
    bool isLastEntry = i == entries.Count - 1;

    if (IsDirectory(entry)) {
      var directory = new DirectoryInfo(entry);
      if ((directory.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) continue;

      Console.WriteLine(indent +(isLastEntry ? "└─" : "├─") + directory.Name);
      Tree(Directory.EnumerateFileSystemEntries(entry).ToList(), indent + (isLastEntry ? "    " : "│   "), level + 1);
    } else {
      var file = new FileInfo(entry);
      if ((file.Attributes  & FileAttributes.Hidden) == FileAttributes.Hidden) continue;

      Console.WriteLine(indent + (isLastEntry ? "└─" : "├─") + file.Name);
    }
  }
}

bool IsDirectory(string path) 
{
  return Directory.Exists(path);
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