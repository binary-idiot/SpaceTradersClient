namespace SpaceTraders.Shared.Utilities;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection RegisterTransientServices<TInterface>(this IServiceCollection services)
	{
		IEnumerable<Type> typesToRegister = typeof(TInterface).Assembly
			.GetTypes()
			.Where(t => t.IsClass && t.IsAssignableTo(typeof(TInterface)) && !t.IsAbstract);

		foreach (var type in typesToRegister)
		{
			services.AddTransient(type.GetInterfaces().First(), type);
		}

		return services;
	}
}