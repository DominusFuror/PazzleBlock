using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PuzzleGenerator : MonoBehaviour
{

    public  List<GameObject> PuzzleList = new List<GameObject>();

    public GameObject Image;
    void Start()
    {
   

        Texture2D text = new Texture2D(1000, 1000);
        text.LoadImage( File.ReadAllBytes("PuzzleImage.jpg"));

        Image.GetComponent<SpriteRenderer>().sprite = Sprite.Create(text, new Rect(0,0,text.width,text.height), new Vector2(0.5f,0.5f));


       PuzzleGeneratorFunc();
       

    }

    // Update is called once per frame
    void Update()
    {

     

    }

    public void PuzzleGeneratorFunc()
    {
        foreach (var item in PuzzleList)
        {
          var mask =  item.AddComponent<SpriteMask>();
            mask.sprite = item.GetComponent<SpriteRenderer>().sprite;
            item.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
      
           var go = Instantiate(Image, Image.transform.position, Image.transform.rotation);
            go.transform.SetParent(item.transform);
            go.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

        }
     
        foreach (var item in PuzzleList)
        {
            item.transform.position = item.transform.parent.position +  new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0);

        }
        Image.SetActive(false);
    }

    public void ReShake()
    {

        foreach (var item in PuzzleList)
        {
           if(item.GetComponent<DragSC>().locked == false)
            {
                item.transform.position = item.transform.parent.position + new Vector3(Random.Range(-2, 2), Random.Range(-3, 3), 0);
            }

        }

    }
    public  void WinCheck()
    {

     foreach (var item in PuzzleList)
       {
           if (item.GetComponent<DragSC>().locked == false)
            {
                return;

            }

       }

        WinParticle.SetActive(true);
        MainServerManager.SendMQTTMess(2, 12, "");

    }
    public GameObject WinParticle;
}
