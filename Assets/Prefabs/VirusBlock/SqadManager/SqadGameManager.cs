using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SqadGameManager : MonoBehaviour
{
 
    public  List<NeonManSc> sqadMans = new List<NeonManSc>();
    public NeonManSc virusMan;
    public  static GameObject effect;
    public GameObject peffect;
    public  int virusMans = 10;

    


    string text = "VIRUSATTACK";
    public static TextMesh textMesh;

    float timer = 0;




    public  class SqadGameConfiger
    {
        public int virusDuration = 5;
        public int virusReload = 1;
        public float squadMoveSpeed = 1.5f;

     

    }
    public  SqadGameConfiger  sqadGameConfiger = new SqadGameConfiger();
    private void Awake()
    {
        textMesh = gameObject.GetComponentInChildren<TextMesh>();
        effect = peffect;
        virusMans = text.Length;

        StreamReader s = new StreamReader("SqadPhaseConfig.txt");
        sqadGameConfiger = JsonUtility.FromJson<SqadGameConfiger>(s.ReadToEnd());
        s.Close();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= sqadGameConfiger.virusReload && virusMan== null)
        {

            virusMan = sqadMans[Random.Range(0, sqadMans.Count)];
            virusMan.isVirus = true;
            virusMan.ColorChange();
            virusMan.gameObject.layer = 0;

        }
        if (timer >= sqadGameConfiger.virusReload+ sqadGameConfiger.virusDuration)
        {

            virusMan.isVirus = false;
            virusMan.ColorChange();
            timer = 0;
         
            virusMan.gameObject.layer = 2;
            virusMan = null;
        }


      
    }
    public  void RemoveWord()
    {

        text = text.Remove(Random.Range(0,text.Length),1);
        textMesh.text = text;

        if (text.Length == 0)
        {
            MainServerManager.SendMQTTMess(2, 16, "");

        }
    }
}
