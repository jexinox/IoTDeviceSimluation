using System;
using System.Reactive.Linq;
using System.Threading;
using IoTDeviceSimulation.Metrics.Update.Options;
using Microsoft.Extensions.Options;

namespace IoTDeviceSimulation.Metrics.Update;

public class MetricUpdater(
    IOptions<MetricUpdateOptions> options, CancellationTokenSource cancellationTokenSource) : IObservable<Metric>
{
    private readonly Lazy<IObservable<Metric>> _internalObservable = 
        new(() => CreateInternalObservable(new(Metric.Default, options, cancellationTokenSource.Token)));

    public IDisposable Subscribe(IObserver<Metric> observer) => _internalObservable.Value.Subscribe(observer);

    private static IObservable<Metric> CreateInternalObservable(MetricUpdateState initialState) =>
        Observable.Generate(
            initialState,
            state => !state.CancellationToken.IsCancellationRequested,
            state => state with { Metric = new(Random.Shared.NextDouble()) },
            state => state.Metric,
            state => state.Options.Value.IntervalBetweenUpdates);

    private record MetricUpdateState(Metric Metric, IOptions<MetricUpdateOptions> Options, CancellationToken CancellationToken);
}