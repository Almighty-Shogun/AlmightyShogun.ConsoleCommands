namespace AlmightyShogun.ConsoleCommands;

public interface IConsoleCommandHandler
{
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
    public Task HandleCommandAsync(string input);
}
