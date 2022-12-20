namespace Mine2d.engine.system.annotations;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class EventListenerAttribute : Attribute
{
    public EventType Type { get; }
    public EventPriority Priority { get; set; }

    public EventListenerAttribute(EventType type)
    {
        this.Type = type;
    }

    public EventListenerAttribute(EventType type, EventPriority priority)
    {
        this.Type = type;
        this.Priority = priority;
    }
}
