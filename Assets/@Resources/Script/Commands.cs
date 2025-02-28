using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMode : ICommand
{
    int modeNumber;
    public void Execute()
    {
        UserManager.Instance.UserMode = (UserManager.UserState)modeNumber;
    }
    public ChangeMode(int _modeNumber)
    {
        this.modeNumber = _modeNumber;
    }
}
