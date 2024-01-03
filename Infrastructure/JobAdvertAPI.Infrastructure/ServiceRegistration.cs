
using JobAdvertAPI.Aplication.Abstractions.Storage;
using JobAdvertAPI.Aplication.Abstractions.Token;
using JobAdvertAPI.Infrastructure.Enums;
using JobAdvertAPI.Infrastructure.Services.Storage;
using JobAdvertAPI.Infrastructure.Services.Storage.Azure;
using JobAdvertAPI.Infrastructure.Services.Storage.Local;
using JobAdvertAPI.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace JobAdvertAPI.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
       serviceCollection.AddScoped<IStorageService,StorageService>();
       serviceCollection.AddScoped<ITokenHandler,TokenHandler>();
        
    }

    public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
    {
        serviceCollection.AddScoped<IStorage, T>();
    }
    public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType) 
    {
        switch (storageType)
        {
            case StorageType.Local:
                serviceCollection.AddScoped<IStorage, LocalStorage>();
                break;
            case StorageType.Azure:
                serviceCollection.AddScoped<IStorage, AzureStorage>();
                break;
            case StorageType.AWS:
                break;

            default:
                serviceCollection.AddScoped<IStorage, LocalStorage>();
                break;
        
        }
    }
}
