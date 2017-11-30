using System;
using UnityEngine;

public class UIWindow : MonoBehaviour 
{

    public event Action<UIWindow> onOpeningStart;
    public event Action<UIWindow> onOpeningComplete;

    public event Action<UIWindow> onClosingStart;
    public event Action<UIWindow> onClosingComplete;

    public void TriggerOnOpeningStartEvent()
    {
        if (onOpeningStart != null) onOpeningStart(this);
    }

    public void TriggerOnOpeningCompleteEvent()
    {
        if (onOpeningComplete != null) onOpeningComplete(this);
    }

    public void TriggerOnClosingStartEvent()
    {
        if (onClosingStart != null) onClosingStart(this);
    }

    public void TriggerOnClosingCompleteEvent()
    {
        if (onClosingComplete != null) onClosingComplete(this);
    }


    public event Action onOpened;
    public event Action onClosed;
    public event Action onPaused;

    protected void TriggerOnOpenedEvent()
    {
        if (onOpened != null) onOpened();
    }

    protected void TriggerOnClosedEvent()
    {
        if (onClosed != null) onClosed();
    }

    protected void TriggerOnPausedEvent()
    {
        if (onPaused != null) onPaused();
    }

    public event Action onUpdate;

    public virtual void OnUpdate()
    {
#if DEBUG_SCRIPT
            if (n < 5)
	        {
	            UnityEngine.Debug.Log(string.Format("{0} Update", gameObject.name));
	            n++;
	        }    
#endif

        if (onUpdate != null) onUpdate();
    }

}

