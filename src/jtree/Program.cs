var root =  Environment.CurrentDirectory;
ArgumentParser parser = new ArgumentParser();
var isOk = parser.Parser(args, out List<Option> options);

if (!isOk) {
  Console.WriteLine("🤔 No Command or wrong command format!");
  return;
}

try { 
  var treeOption = new TreeOptions();
  foreach(var option in options) 
  {
    if (option.Name == "--no-icons") {
      treeOption.IsShowIcon = false;
    } else if (option.Name == "--depth") 
    {
      treeOption.Depth = Convert.ToInt32(option.Parameters[0]);
    } else if (option.Name == "--show-hidden")
    {
      treeOption.ShowHidden = true;
    } else if (option.Name == "--order")
    {
      treeOption.IsOrder = true;
    }
  }
  
  Tree tree = new Tree(root, treeOption);
  tree.Print();
} catch(UnauthorizedAccessException ex) {
  Console.WriteLine(ex.Message);
}

class TreeOptions
{
  public bool IsShowIcon { get; set; } = true;
  public int Depth { get; set; } = 2;
  public bool ShowHidden{ get; set; }  = false;
  public bool IsOrder{ get; set; } = false;
}