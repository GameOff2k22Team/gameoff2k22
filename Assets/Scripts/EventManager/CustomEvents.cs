using UnityEngine.Events;

public class Args
{
    public string message;
}
public class UnityCustomEvents<T> : UnityEvent<T>
{

}
public enum EventsName
{
    StartDialog
}