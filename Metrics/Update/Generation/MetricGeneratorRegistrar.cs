using System;
using IoTDeviceSimulation.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace IoTDeviceSimulation.Metrics.Update.Generation;

public static class MetricGeneratorRegistrar
{
    public static IServiceCollection AddMetricGenerators(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<IDefaultsProvider<MetricGeneratorOptions>>(_ =>
                new AsIsDefaultsProvider<MetricGeneratorOptions>(new(MetricGeneratorType.Random)))
            .AddSingletonWithImplementedInterfaces<
                IMetricGeneratorOptionsProvider, IObserver<MetricGeneratorOptions>, MetricGeneratorOptionsProvider>()
            .AddSingleton<Random>()
            .AddSingleton<IMetricGeneratorFactory, MetricGeneratorFactory>()
            .AddSingleton<IMetricGeneratorProvider, MetricGeneratorProvider>()
            .AddSingletonWithImplementedInterface<IObservable<MetricGeneratorOptions>, MetricGeneratorOptionsViewModel>()
            .AddSingleton<ISubscriber, DefaultSubscriber<MetricGeneratorOptions>>();
    }
}