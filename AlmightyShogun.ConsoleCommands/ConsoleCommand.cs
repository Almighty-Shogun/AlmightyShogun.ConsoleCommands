namespace AlmightyShogun.ConsoleCommands;

public class ConsoleCommand
{
    public string Name { get; }
    public string Description { get; }
    public string[] Aliases { get; }
    public string? Usage { get; }
    public string? Example { get; set; }

    public ConsoleCommand(string name, string description, string[] aliases, string usage, string? example)
    {
        Name = name;
        Description = description;
        Aliases = aliases;
        Usage = $"{name} {usage}";
        Example = string.IsNullOrWhiteSpace(example) ? null : $"{name} {example}";
    }
}
