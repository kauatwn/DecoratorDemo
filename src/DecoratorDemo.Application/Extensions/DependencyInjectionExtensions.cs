using DecoratorDemo.Application.Interfaces.UseCases;
using DecoratorDemo.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace DecoratorDemo.Application.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddApplication(this IServiceCollection services) => AddUseCases(services);

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IGetAllProductsUseCase, GetAllProductsUseCase>();
        services.AddScoped<IAddProductUseCase, AddProductUseCase>();
    }
}