using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class DragSC : MonoBehaviour
{


    public bool locked = false;
    void Start()
    {

  
    }

    private void OnMouseDrag()
    {
        if (!locked)
        {
            this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 20));
        }
    }

    private void OnMouseUp()
    {
        Ray ray=  Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray,out hit,maxDistance:100);
        if (hit.collider!=null)
        {
            string puzzleName = this.gameObject.name;
            string slotName = hit.collider.gameObject.name;
            if (puzzleName.Substring(puzzleName.Length - 2, 2) == slotName.Substring(slotName.Length - 2, 2))
            {
                locked = true;
                this.transform.position = hit.collider.gameObject.transform.position;
                this.transform.GetComponent<SortingGroup>().sortingOrder = -1;
                this.gameObject.layer = 2;
            }
        }
        PuzzleGen.WinCheck();
   

    }
    public PuzzleGenerator PuzzleGen ;
    void Update()
    {
        
    }
}
