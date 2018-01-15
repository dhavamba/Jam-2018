using UnityEngine;

public abstract class AsynchronousEvent : MonoBehaviour, IAsynchronous
{
    private int asynchronous;

    public void AddControlFinishWork()
    {
        asynchronous++;
        FinishWork();
    }

    private void FinishWork()
    {
        if (asynchronous >= 2)
        {
            asynchronous = 0;
            FinishAsynchronousWork();
        }
    }

    public void SendAsynchronous(AsynchronousEvent send)
    {
        send?.AddControlFinishWork();
    }

    protected void ControlFinishWork(Collider2D collider)
    {
        if (collider.GetComponent<IAsynchronous>() == null)
        {
            asynchronous++;
        }

        SendAsynchronous(collider.GetComponent<AsynchronousEvent>());
        AddControlFinishWork();
    }

    private void OnDisable()
    {
        asynchronous = 0;
    }

    protected abstract void FinishAsynchronousWork();
}