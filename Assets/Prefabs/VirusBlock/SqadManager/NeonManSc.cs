using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonManSc : MonoBehaviour 
{
    public bool isVirus = false;

    Color startColor;
    SpriteRenderer thisSprite;
    public SqadGameManager sqadGameManager;
    void Start()
    {
 
        thisSprite = this.GetComponent<SpriteRenderer>();
        sqadGameManager = GameObject.FindObjectOfType<SqadGameManager>();
        startColor = thisSprite.color;
    }

    float step = 0;
    private void Update()
    {
        if (isVirus)
        {
            thisSprite.color = Color.Lerp(startColor, Color.yellow, step);
            step += 1f / 600f;
        }
        if (!isVirus)
        {
            step = 0;
        }
        

    }
    public void ColorChange()
    {
        if (isVirus)
        {

          
            var s = Instantiate(SqadGameManager.effect, this.transform.position, this.transform.rotation);
            s.transform.parent = this.transform;
            s.GetComponent<ParticleSystem>().startColor = new Color(1,1,0,0.05f);
            s.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = startColor;
        }
    }
    
    private void OnMouseDown()
    {
        print('Y');
        if (isVirus)
        {
        
            Instantiate(SqadGameManager.effect, this.transform.position, this.transform.rotation,this.transform);
            isVirus = false;
            ColorChange();
            sqadGameManager.RemoveWord();
     
        }
    }
}
