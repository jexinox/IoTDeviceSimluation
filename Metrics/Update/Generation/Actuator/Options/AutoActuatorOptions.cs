namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Options;

public record AutoActuatorOptions(double MetricValueLimit = 0.7, double MetricChange = 0.2) : IActuatorOptions
{
    public IActuator Get(IActuatorFactory factory) => factory.GetAutoActuator(this);
}