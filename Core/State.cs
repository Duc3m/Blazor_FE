namespace Blazor_FE.Core;

public class State<T>
{
    public T value { get; set; }
    public readonly Action onChange;

    public State(T initialValue, Action onChange)
    {
        value = initialValue;
        this.onChange = onChange;
    }
    
    public T Value => value;

    public void Set(T newValue)
    {
        value = newValue;
        onChange.Invoke();
    }
}