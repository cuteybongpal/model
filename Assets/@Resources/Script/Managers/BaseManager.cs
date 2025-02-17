using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BaseManager
{
    //메소드 넘버에 따라 매니저안에 있는 함수를 반환해줌(action, func 형식)
    public T GetMethod<T>(int methodNum) where T : System.Delegate;
}
