using Microsoft.Extensions.Logging;

namespace AlmightyShogun.ConsoleCommands;

public class ConsoleCommandHandler : IConsoleCommandHandler
{
    private readonly ILogger<ConsoleCommandHandler> _logger;
    private readonly Dictionary<string, IConsoleCommand> _commands;

    public ConsoleCommandHandler(ILogger<ConsoleCommandHandler> logger, IEnumerable<IConsoleCommand> commands)
    {
        _logger = logger;
        _commands = new Dictionary<string, IConsoleCommand>();
        
        foreach (IConsoleCommand consoleCommand in commands)
        {
            _commands.Add(consoleCommand.Name.ToLowerInvariant(), consoleCommand);

            foreach (string alias in consoleCommand.Aliases)
            {
                if (string.IsNullOrWhiteSpace(alias)) continue;
                
                _commands.TryAdd(alias.ToLowerInvariant(), consoleCommand);
            }
        }
    }

    /// <inheritdoc/>
    public async Task HandleCommandAsync(string input)
    {
        ConsoleUtils.RemoveLastConsoleLine();
        
        string[] parts = input.Split(' ');
        string commandName = parts[0];

        if (_commands.TryGetValue(commandName, out IConsoleCommand? command))
        {
            await command.InternallyExecuteCommandAsync(parts.Skip(1).ToArray());
        }
        else
        {
            _logger.LogWarning("{CommandName:c} is not registered as a console command", commandName);
        }
    }
}
