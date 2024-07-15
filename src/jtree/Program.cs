// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

List<string> marks = ["├", "─", "└", "│"];

var root =  Environment.CurrentDirectory;

System.Console.WriteLine("./");

Directory.EnumerateDirectories(root);
Directory.EnumerateFiles(root);

var entries =  Directory.EnumerateFileSystemEntries(root).ToList();

int startLevel = 2;
void Print(List<string> entries, string indent, int level) {
  if (startLevel == level) return;
  
  for (var i = 0; i < entries.Count() - 1; i++) {
    var entry = entries[i];
    if (IsDirectory(entry)) {
      var directory = new DirectoryInfo(entry);
      if ((directory.Attributes  & FileAttributes.Hidden) == FileAttributes.Hidden) continue;
      Console.Write(indent + "├─" + directory.Name);
      Console.WriteLine();
      Print(Directory.EnumerateFileSystemEntries(entry).ToList(), indent + "│   ", level + 1);
    } else {
      var file = new FileInfo(entry);
      if ((file.Attributes  & FileAttributes.Hidden) == FileAttributes.Hidden) continue;
      Console.Write(indent + "├─" + file.Name);
      Console.WriteLine();
    }
  }
  
  var finalEntry = entries[entries.Count() - 1];
    if (IsDirectory(finalEntry)) {
      var directory = new DirectoryInfo(finalEntry);
      Console.Write(indent + "└─" + directory.Name);
      Console.WriteLine();
      Print(Directory.EnumerateFileSystemEntries(finalEntry).ToList(), indent + "   ", level + 1);
    } else {
      var file = new FileInfo(finalEntry);
      Console.Write(indent + "└─" + file.Name);
      Console.WriteLine();
    }
    
}

bool IsDirectory(string path) 
{
  return Directory.Exists(path);
}

try { 
  Print(entries, " ", 0);
} catch(UnauthorizedAccessException ex) {
  Console.WriteLine(ex.Message);
}