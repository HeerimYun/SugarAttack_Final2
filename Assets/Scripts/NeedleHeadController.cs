using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/**
 * 주사기의 머리부분 드래그
 */
public class NeedleHeadController : MonoBehaviour {

    /**
     * 드래그 하는 동안 호출되는 함수
     */
    private void OnMouseDrag(PointerEventData eventData)
    {
        Debug.Log("x 좌표: " + Input.mousePosition.x + " .y 좌표: " + Input.mousePosition.y);
        Debug.Log("드래그중");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
