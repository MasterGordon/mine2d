using mine2d.core.extensions;
using mine2d.engine.system;
using Mine2d.engine.system;
using Mine2d.engine.system.annotations;

namespace Mine2d.frontend;

internal struct EventAction
{
    public EventPriority priority;
    public Delegate del;
}

public class EventPublisher
{
    private readonly Dictionary<EventType, List<EventAction>> eventListeners = new();

    public EventPublisher()
    {
        this.Scan();
    }

    public void Scan()
    {
        var types = this.GetType().Assembly
            .GetTypesSafe()
            .Where(t => t.Namespace != null && t.Namespace.StartsWith("Mine2d.frontend.events", StringComparison.Ordinal));
        foreach (var type in types)
        {
            var methods = type.GetMethods()
                .Where(m => m.GetCustomAttributes(typeof(EventListenerAttribute), false).Length > 0);
            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes(typeof(EventListenerAttribute), false);
                foreach (var eventType in from EventListenerAttribute attribute in attributes
                                          let eventType = attribute.Type
                                          select eventType)
                {
                    if (!this.eventListeners.ContainsKey(eventType))
                    {
                        this.eventListeners.Add(eventType, new List<EventAction>());
                    }

                    var del = method.GetParameters().Length == 0 ?
                        Delegate.CreateDelegate(typeof(Action), method) :
                        Delegate.CreateDelegate(
                        typeof(Action<>).MakeGenericTypeSafely(method.GetParameters()[0].ParameterType),
                        method
                    );
                    this.eventListeners[eventType].Add(new EventAction
                    {
                        priority = ((EventListenerAttribute)attributes[0]).Priority,
                        del = del
                    });
                }
            }
        }

        foreach (var (_, value) in this.eventListeners)
        {
            value.Sort((a, b) => a.priority.CompareTo(b.priority));
        }
    }

    public void Dump()
    {
        foreach (var eventType in this.eventListeners.Keys)
        {
            Console.WriteLine(eventType);
            foreach (var action in this.eventListeners[eventType])
            {
                Console.WriteLine(action.del.Method.Name);
            }
        }
    }

    public void Publish(EventType eventType, SDL_Event e)
    {
        if (this.eventListeners.ContainsKey(eventType))
        {
            try
            {
                foreach (var action in this.eventListeners[eventType])
                {
                    if (action.del.Method.GetParameters().Length == 0)
                    {
                        action.del.DynamicInvoke();
                    }
                    else
                    {
                        action.del.DynamicInvoke(e);
                    }
                }
            }
            catch (CancelEventException)
            {

            }
        }
    }
}

public class CancelEventException : Exception
{
    public CancelEventException(string message) : base(message)
    {
    }
}
