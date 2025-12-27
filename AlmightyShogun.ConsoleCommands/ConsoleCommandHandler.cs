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

    /// <inheritdoc />
    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            Console.TreatControlCAsInput = false;

            try
            {
                string input = await Console.In.ReadLineAsync(cancellationToken) ?? string.Empty;

                if (string.IsNullOrWhiteSpace(input))
                {
                    ConsoleUtils.RemoveLastConsoleLine();
                    continue;
                }
            
                await HandleCommandAsync(input);
            }
            catch (OperationCanceledException) { }
        }
    }

    /// <summary>
    /// Handles the execution of a console command based on the provided input.
    /// </summary>
    /// 
    /// <param name="input">The user's input representing the console command and its arguments.</param>
    /// 
    /// <returns>A task that represents the asynchronous operation of handling the command.</returns>
    ///
    /// <author>Almighty-Shogun</author>
    /// <since>1.0.0</since>
    private async Task HandleCommandAsync(string input)
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
            if (_logger.IsEnabled(LogLevel.Warning))
            {
                _logger.LogWarning("{CommandName:c} is not registered as a console command", commandName);
            }
        }
    }
}
