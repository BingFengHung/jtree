class ArgumentParser
{
  public bool Parser(string[] args, out List<Option> options)
  {
    options = new List<Option>();
    if (args.Length == 0) 
    {
      return true;
    } else {
      if (!args[0].StartsWith("--")) return false;

      for (var i = 0; i < args.Length; i++) 
      {
        var startIndex = i + 1;
        i = GetParameters(args, startIndex, out var parameters);
        options.Add(new Option
        {
          Name = args[startIndex - 1],
          Parameters = parameters
        });
      }
      
      return true;
    }
  }
  
  private int GetParameters(string[] args, int startIndex, out List<string> parameters)
  {
    parameters = new List<string>();
    
    int i = startIndex;
    
    for (; i < args.Length; i++)
    {
      if (args[i].StartsWith("--")) break;
      parameters.Add(args[i]);
    }
    
    return i - 1;
  }
}

class Option
{
  public string Name { get; set; }

  public List<string>? Parameters{ get; set; }

  public override string ToString()
  {

    var parameters =  string.Join(",", Parameters!);
    
    return $"{Name} {parameters}";
  }
}