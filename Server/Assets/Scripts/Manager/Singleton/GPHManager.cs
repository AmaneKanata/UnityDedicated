using Framework.Network;

public class GPHManager : SingletonManager<GPHManager>
{
    public GlobalPacketHandler GPH { get; private set; }

    public void Awake()
    {
        GPH = new GlobalPacketHandler();
    }
}
