namespace SpaceTraders.Shared.Utilities;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddTransientServicesWithInterface<TInterface>(this IServiceCollection services)
	{
		IEnumerable<Type> typesToRegister = DiscoverTypes<TInterface>();

		foreach (var type in typesToRegister)
		{
			services.AddTransient(type, type);
		}

		return services;
	}
	
	public static IServiceCollection AddSingletonServicesWithInterface<TInterface>(this IServiceCollection services)
	{
		IEnumerable<Type> typesToRegister = DiscoverTypes<TInterface>();

		foreach (var type in typesToRegister)
		{
			services.AddSingleton(type, type);
		}

		return services;
	} 

	private static IEnumerable<Type> DiscoverTypes<TInterface>()
	{
		return typeof(TInterface).Assembly
			.GetTypes()
			.Where(t => t.IsClass && t.IsAssignableTo(typeof(TInterface)) && !t.IsAbstract);
	}
}