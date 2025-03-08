using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Base : MonoBehaviour
{
<<<<<<< HEAD
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
=======
    protected virtual void Start()
    {
        UIManager.Instance.CurrentMainUI = this;
>>>>>>> origin/main
    }
}
