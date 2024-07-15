List<string> marks = ["├", "─", "└", "│"];

var root =  Environment.CurrentDirectory;
var entries =  Directory.EnumerateFileSystemEntries(root).ToList();

int startLevel = 3;
void Print(List<string> entries, string indent, int level) {
  if (startLevel == level) return;
  
  for (var i = 0; i < entries.Count() - 1; i++) {
    var entry = entries[i];
    if (IsDirectory(entry)) {
      var directory = new DirectoryInfo(entry);
      if ((directory.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) continue;
      Console.WriteLine(indent + "├─" + directory.Name);
      Print(Directory.EnumerateFileSystemEntries(entry).ToList(), indent + "│   ", level + 1);
    } else {
      var file = new FileInfo(entry);
      if ((file.Attributes  & FileAttributes.Hidden) == FileAttributes.Hidden) continue;
      Console.WriteLine(indent + "├─" + file.Name);
    }
  }
  
  var finalEntry = entries[entries.Count() - 1];
    if (IsDirectory(finalEntry)) {
      var directory = new DirectoryInfo(finalEntry);
      Console.WriteLine(indent + "└─" + directory.Name);
      Print(Directory.EnumerateFileSystemEntries(finalEntry).ToList(), indent + "   ", level + 1);
    } else {
      var file = new FileInfo(finalEntry);
      Console.WriteLine(indent + "└─" + file.Name);
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
  Print(entries, " ", 0);
} catch(UnauthorizedAccessException ex) {
  Console.WriteLine(ex.Message);
}