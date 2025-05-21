using Application.Abstractions;
using Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddApplication(this IServiceCollection services) => AddUseCases(services);

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IGetAllProductsUseCase, GetAllProductsUseCase>();
        services.AddScoped<IAddProductUseCase, AddProductUseCase>();
    }
}