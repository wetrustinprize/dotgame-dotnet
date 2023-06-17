using Timer = System.Timers.Timer;

namespace DotGameMemoryServer;

public class ServerConfiguration
{
    /// <summary>
    /// The time in milliseconds between each wipe check.
    ///
    /// If set to 0, the server will not check for wipes.
    /// </summary>
    public double WipeCheckAfter { get; init; } = TimeSpan.FromMinutes(5).Milliseconds;

    /// <summary>
    /// Wipes the instance after this time in milliseconds.
    ///
    /// Only used if <see cref="WipeCheckAfter"/> is greater than 0.
    /// </summary>
    public double WipeAfter { get; init; } = TimeSpan.FromHours(1).Milliseconds;
}

public class Server
{
    private List<Instance> Instances { get; } = new();
    private Dictionary<InstanceCode, Instance> InstancesByCode { get; } = new();

    private Timer? WipeTimer { get; init; }
    private double WipeAfter { get; init; }

    public Server(ServerConfiguration configuration)
    {
        if (configuration.WipeCheckAfter > 0)
        {
            WipeTimer = new Timer(configuration.WipeCheckAfter);
            WipeTimer.AutoReset = true;
            WipeTimer.Elapsed += (_, _) => CheckWipe();
            WipeTimer.Enabled = true;

            WipeAfter = configuration.WipeAfter;
        }
    }

    #region Instances

    public Instance? GetInstance(Guid id) => Instances.Find(instance => instance.Id == id);

    public Instance? GetInstance(InstanceCode code) =>
        InstancesByCode.TryGetValue(code, out var instance) ? instance : null;

    public Instance CreateInstance()
    {
        var instance = new Instance();

        Instances.Add(instance);
        InstancesByCode.Add(new InstanceCode(), instance);

        return instance;
    }

    #endregion

    #region Wipe

    private void CheckWipe()
    {
        Instances.RemoveAll(instance => instance.CreatedAt.AddMilliseconds(WipeAfter) < DateTime.Now);
    }

    #endregion
}