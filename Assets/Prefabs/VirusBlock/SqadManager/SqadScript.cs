using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqadScript : MonoBehaviour
{
    public float speed = 1.5f;
    Vector3 dir = new Vector2(1, 0);
    public Transform startPoint;
    SqadGameManager SqadGameManager;
    void Start()
    {

        SqadGameManager = GameObject.FindObjectOfType<SqadGameManager>();
        speed = SqadGameManager.sqadGameConfiger.squadMoveSpeed;
        foreach (var item in gameObject.GetComponentsInChildren<BoxCollider2D>())
        {
           if(item.gameObject!= this.gameObject)
            {
                var i = item.gameObject.AddComponent<NeonManSc>();
                SqadGameManager.sqadMans.Add(i);
                
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += dir*Time.deltaTime*speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.transform.position = startPoint.transform.position;
    }
}
