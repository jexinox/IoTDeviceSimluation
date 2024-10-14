using System;
using System.Reactive;
using ReactiveUI;

namespace IoTDeviceSimulation.Metrics;

public class MetricViewModel : ReactiveObject, IObserver<Metric>
{
    private readonly Lazy<IObserver<Metric>> _internalObserver;

    private Metric _metric = new();
    
    public MetricViewModel()
    {
        _internalObserver = new(() => Observer.Create<Metric>(metric => Metric = metric.Value));
    }

    public double Metric
    {
        get => _metric.Value;
        private set => _metric = this.RaiseAndSetIfChanged(ref _metric, new(value));
    }

    public void OnCompleted() => _internalObserver.Value.OnCompleted();

    public void OnError(Exception error) => _internalObserver.Value.OnError(error);

    public void OnNext(Metric value) => _internalObserver.Value.OnNext(value);
}