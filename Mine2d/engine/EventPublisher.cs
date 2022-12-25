using System.Reflection;
using Mine2d.engine.system;
using Mine2d.engine.system.annotations;
using Mine2d.game.core.extensions;

namespace Mine2d.engine;

internal struct EventAction
{
    public EventPriority Priority;
    public Delegate Del;
}

public class eventPublisher
{
    private readonly Dictionary<EventType, List<EventAction>> eventListeners = new();

    public eventPublisher()
    {
        this.Scan();
    }

    public void Scan()
    {
        var types = this.GetType().Assembly
            .GetTypesSafe()
            .Where(t => t.Namespace?.StartsWith("Mine2d.game.frontend.events", StringComparison.Ordinal) == true);
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
                        Priority = ((EventListenerAttribute)attributes[0]).Priority,
                        Del = del
                    });
                }
            }
        }

        foreach (var (_, value) in this.eventListeners)
        {
            value.Sort((a, b) => b.Priority.CompareTo(a.Priority));
        }
    }

    public void Dump()
    {
        foreach (var eventType in this.eventListeners.Keys)
        {
            Console.WriteLine(eventType);
            foreach (var action in this.eventListeners[eventType])
            {
                Console.WriteLine(action.Del.Method.Name);
            }
        }
    }

    public void Publish(EventType eventType, SDL_Event e)
    {
        if (this.eventListeners.TryGetValue(eventType, out var value))
        {
            try
            {
                foreach (var action in value)
                {
                    if (action.Del.Method.GetParameters().Length == 0)
                    {
                        action.Del.DynamicInvoke();
                    }
                    else
                    {
                        action.Del.DynamicInvoke(e);
                    }
                }
            }
            catch (TargetInvocationException ex) when (ex.InnerException is CancelEventException)
            {
            }
        }
    }
}

public class CancelEventException : Exception
{
    [System.Diagnostics.DebuggerHidden]
    public CancelEventException() : base("Event cancelled")
    {
    }
}
