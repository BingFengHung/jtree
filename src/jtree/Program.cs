using System.Text;

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
    } else if (option.Name == "--help") {
      StringBuilder stringBuilder = new StringBuilder();
      
      stringBuilder.AppendLine("jtree Print full folder tree structure.");
      stringBuilder.AppendLine(" --depth [n] : Print tree with deep. <Default is 2>");
      stringBuilder.AppendLine(" --no-icon ： Print tree without folder and structure icon. <Default is true>");
      stringBuilder.AppendLine(" --order ： Print tree with folder then file order. <Default is false>");
      stringBuilder.AppendLine(" --show-hidden ： Print tree with folder then file order. <Default is false>");

      Console.Write(stringBuilder.ToString());
      return;
    }
  }
  
  Tree tree = new Tree(root, treeOption);
  tree.Print();
} catch(UnauthorizedAccessException ex) {
  Console.WriteLine(ex.Message);
}
