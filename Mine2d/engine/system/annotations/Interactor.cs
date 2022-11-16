namespace Mine2d.engine.system.annotations;

public enum InteractorKind
{
    Client,
    Server,
    Hybrid
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class InteractorAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class InteractionAttribute : Attribute
{
    public string Type { get; set; }
    public InteractorKind Kind { get; set; }

    public InteractionAttribute(InteractorKind kind, string type)
    {
        this.Type = type;
        this.Kind = kind;
    }
}
