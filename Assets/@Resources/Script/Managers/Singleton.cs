using OpenCover.Framework.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : BaseManager, new()
{
    protected static T instance;
    public static T Intstance
    {
        get { return instance; }
    }

    public static J GetMethod<J>(int methodNum) where J : System.Delegate
    {
        if (instance == null)
            return default(J);

        J method = instance.GetMethod<J>(methodNum);
        return method;
    }
}
