namespace SpaceTraders.Shared.Utilities;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddTransientServicesWithInterface<TInterface>(this IServiceCollection services, bool useInterfaceForServiceType = false)
	{
		AddWithInterface<TInterface>(services.AddTransient, useInterfaceForServiceType);
		return services;
	}
	
	public static IServiceCollection AddSingletonServicesWithInterface<TInterface>(this IServiceCollection services, bool useInterfaceForServiceType = false)
	{
		AddWithInterface<TInterface>(services.AddSingleton, useInterfaceForServiceType);
		return services;
	}

	private static void AddWithInterface<TInterface>(Func<Type, Type, IServiceCollection> addMethod, bool useInterfaceForServiceType)
	{
		IEnumerable<Type> typesToRegister = DiscoverTypes<TInterface>();

		foreach (Type type in typesToRegister)
		{
			Type serviceType = type;
			if (useInterfaceForServiceType)
			{
				serviceType = type.GetSpecificInterface(typeof(TInterface)) ?? serviceType;
			}
			
			addMethod.Invoke(serviceType! , type!);
		}
	}
	private static IEnumerable<Type> DiscoverTypes<TInterface>()
	{
		return typeof(TInterface).Assembly
			.GetTypes()
			.Where(t => t.IsClass && t.IsAssignableTo(typeof(TInterface)) && !t.IsAbstract);
	}

	private static Type? GetSpecificInterface(this Type type, Type interfaceType)
	{
		Type[] interfaces = type.GetInterfaces();
		interfaces = interfaces.Where((Type i) => i.IsAssignableTo(interfaceType) && i.IsConstructedGenericType).ToArray();
		return interfaces.FirstOrDefault();
	}
}