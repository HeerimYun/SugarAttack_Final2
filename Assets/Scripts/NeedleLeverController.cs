using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/**
 * 처음 클릭할 때, 좌표를 얻고
 * 드래그 하는 동안 각도와 x값을 변화시킨다.
 * https://answers.unity.com/questions/1261039/drag-object-in-a-circle-path.html
 * https://answers.unity.com/questions/1256608/move-an-object-in-a-circular-path-according-to-tou.html
 */
public class NeedleLeverController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    Vector2 dir;
    Vector2 objPos;
    float dist, angle;

    Quaternion endRotation;

    public void OnBeginDrag(PointerEventData eventData)
    {
        /*Debug.Log("------------------------드래그 시작-----------------------");
        Debug.Log("dir 값 : " + dir);
        Debug.Log("dist 값 : " + dist);
        Debug.Log("angle 값 : " + angle);
        Debug.Log("rotation 값 : " + transform.rotation);*/
        transform.rotation = endRotation;
    }

    public void OnDrag(PointerEventData eventData)
    {
        objPos = new Vector2(transform.position.x, transform.position.y); //항상 
        dir = (eventData.position - objPos);
        dist = Mathf.Sqrt(dir.x * dir.x + dir.y * dir.y);    
        angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        /*Debug.Log("------------------------드래그 끝-----------------------");
        Debug.Log("dir 값 : " + dir);
        Debug.Log("dist 값 : " + dist);
        Debug.Log("angle 값 : " + angle);
        Debug.Log("rotation 값 : " + transform.rotation);*/
        endRotation = transform.rotation;
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {

    }
}
