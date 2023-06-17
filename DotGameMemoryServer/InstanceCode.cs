namespace DotGameMemoryServer;

public struct InstanceCode
{
    private string _code;

    public InstanceCode()
    {
        _code = Guid.NewGuid().ToString("N").Substring(0, 5);
    }

    public override string ToString()
    {
        return _code;
    }
}