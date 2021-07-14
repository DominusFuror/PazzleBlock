using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CodeBLockManager : MonoBehaviour
{
    public static string Code;
    void Start()
    {

        MainServerManager.SendMQTTMess(2, 5, "");

        Code = new StreamReader("CodeBlockConfig.txt").ReadToEnd();

        inputFileds = inputFiled;
        standartTextColor = inputFiled.color;
    }
    public Color standartTextColor;

    public Text inputFiled;
    public static Text inputFileds;
   

    public void Clear()
    {

        inputFiled.text = "";
    }

    public void SendCode()
    {

        StopAllCoroutines();

        if (inputFileds.text == Code)
        {

            MainServerManager.SendMQTTMess(2, 15, "");
            inputFiled.color = Color.green;

            foreach (var item in gameObject.GetComponentsInChildren<Button>())
            {

                Destroy(item);

            }
        }

        else
        {

            StartCoroutine(WrongCodeEnter());

        }

    }

    
    
    IEnumerator WrongCodeEnter()
    {


   
        yield return new WaitForSeconds(0.5f);
        inputFiled.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        inputFiled.color = standartTextColor;
        yield return new WaitForSeconds(0.5f);
        inputFiled.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        inputFiled.color = standartTextColor;
        yield return new WaitForSeconds(0.5f);
        inputFiled.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        inputFiled.color = standartTextColor;
    }

}
