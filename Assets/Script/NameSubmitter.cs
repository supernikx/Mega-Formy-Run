using System;
using UnityEngine;

public class NameSubmitter : MonoBehaviour {

    public void SubmitName(string name)
    {        
        GameManager.instance.SetPlayerName(name);
    }
}
