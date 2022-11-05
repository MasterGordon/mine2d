interface IBackend
{
    public void Process(double dt);
    public void ProcessPacket(ValueType packet);
    public void Init();
}
