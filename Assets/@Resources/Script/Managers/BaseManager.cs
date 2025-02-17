using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BaseManager
{
    //�޼ҵ� �ѹ��� ���� �Ŵ����ȿ� �ִ� �Լ��� ��ȯ����(action, func ����)
    public T GetMethod<T>(int methodNum) where T : System.Delegate;
}
