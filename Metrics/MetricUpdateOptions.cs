using System;

namespace IoTDeviceSimulation.Metrics;

public record MetricUpdateOptions
{
    public static readonly MetricUpdateOptions Default = new(MetricUpdateOptionsViewModel.DefaultSecondsBetweenUpdates);
    
    public MetricUpdateOptions(int secondsBetweenUpdates)
    {
        IntervalBetweenUpdates = TimeSpan.FromSeconds(secondsBetweenUpdates);
    }
    
    public TimeSpan IntervalBetweenUpdates { get; }
}