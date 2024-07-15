var root =  Environment.CurrentDirectory;

try { 
  Tree tree = new Tree(root, 3);
  tree.Print();
} catch(UnauthorizedAccessException ex) {
  Console.WriteLine(ex.Message);
}