using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionTimer
{

    public static FunctionTimer Create(Action action, float fltTimer)
    {
        GameObject gameObject = new GameObject("FunctionTimer", typeof(MonoBehaviorHook));
        FunctionTimer functionTimer = new FunctionTimer(action, fltTimer, gameObject);
        gameObject.GetComponent<MonoBehaviorHook>().onUpdate = functionTimer.Update;

        return functionTimer;
    }

    public class MonoBehaviorHook : MonoBehaviour
    {
        public Action onUpdate;
        private void Update()
        {
            if (onUpdate != null) onUpdate();
        }
    }

    private Action action;
    private GameObject gameObject;
    private float fltTimer;
    private bool boolDestroyed;

    private FunctionTimer(Action action, float fltTimer, GameObject gameObject)
    {
        this.action = action;
        this.gameObject = gameObject;
        this.fltTimer = fltTimer;
        boolDestroyed = false;
    }

    public void Update()
    {
        if (!boolDestroyed) {
            fltTimer -= Time.deltaTime;
            if (fltTimer < 0)
            {
                //Trigger the action
                action();
                DestroySelf();
            }
        }
    }

    private void DestroySelf()
    {
        boolDestroyed = true;
        UnityEngine.Object.Destroy(gameObject);
    }

}
