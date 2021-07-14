using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputButtonClicksJey : MonoBehaviour
{
    
    void Start()
    {
        
    }

    public Text inputFiled; 
    public void Clicled(string name)
    {
        inputFiled.text += name;
       



    }
}
