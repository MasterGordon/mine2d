namespace mine2d.engine.system.annotations;

enum InteractorKind
{
    Client,
    Server,
    Hybrid
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
class Interactor : Attribute { }

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
class Interaction : Attribute
{
    public string Type;
    public InteractorKind Kind;

    public Interaction(InteractorKind kind, string type)
    {
        this.Type = type;
        this.Kind = kind;
    }
}