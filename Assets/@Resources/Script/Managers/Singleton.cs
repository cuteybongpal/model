using OpenCover.Framework.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : BaseManager, new()
{
    private static T instance = new T();
    public static T Intstance
    {
        get { return instance; }
    }

    public J GetMethod<J>(int methodNum) where J : System.Delegate
    {
        if (instance == null)
            return default(J);

        J method = instance.GetMethod<J>(methodNum);
        return method;
    }
}
