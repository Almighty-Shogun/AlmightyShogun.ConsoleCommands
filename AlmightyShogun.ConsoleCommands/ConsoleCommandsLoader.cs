using Microsoft.Extensions.DependencyInjection;

namespace AlmightyShogun.ConsoleCommands;

public static class ConsoleCommandsLoader
{
    /// <summary>
    /// Registers console command services with the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// 
    /// <param name="serviceCollection">The service collection used to register dependencies related to console command functionality.</param>
    ///
    /// <returns>The <see cref="IServiceCollection"/> instance with the console commands handlers registered.</returns>
    /// 
    /// <author>Almighty-Shogun</author>
    /// <since>1.0.0</since>
    public static IServiceCollection AddConsoleCommands(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<IConsoleCommandHandler, ConsoleCommandHandler>();
    }
}
