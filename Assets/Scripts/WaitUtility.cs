using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitUtility : MonoBehaviour
{
    static WaitUtility instance = null;
    static WaitUtility Instance {
        get 
        {
            if (instance == null)
            {
                instance = new GameObject("WaitUtility").AddComponent<WaitUtility>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public static void Wait(float seconds, System.Action callback)
    {
        Instance.StartCoroutine(Instance.WaitRoutine(seconds, callback));
    }

    IEnumerator WaitRoutine(float duration, System.Action callback)
    {
        yield return new WaitForSeconds(duration);
        callback?.Invoke();
    }
}
