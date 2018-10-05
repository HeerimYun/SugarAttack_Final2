using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/**
 * 주사기의 머리부분 드래그
 * 참고 영상 : https://www.youtube.com/watch?v=oV9JE5K_uiM
 */
public class NeedleHeadController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Text volume;

    public static int value = 0;

    float dragDist = 100;

    Vector2 startPoint;
    Vector2 endPoint;

    Button setBtn;

    // Use this for initialization
    void Start()
    {
        volume = GameObject.Find("volume").GetComponent<Text>();
        value = 0; // static 이기 때문에 한번씩 초기화 필요
    }

    /**
     * 드래그 시작 시 한 번 호출
     */
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPoint = eventData.position;
    }

    /**
     * 드래그 끝날 시 한 번 호출
     */
    public void OnEndDrag(PointerEventData eventData)
    {
        endPoint = eventData.position;
        GetDragMove();
        SetVolumeText();
    }

    public void SetVolumeText()
    {
        volume.text = value + "";
    }

    public void GetDragMove()
    {
        if (startPoint.y - endPoint.y < -1 *dragDist)
        {
            //Debug.Log("아래서 위로 드래그, 숫자 증가 : " + (startPoint.y - endPoint.y));
            value += 2;
        }
        else if (startPoint.y - endPoint.y > dragDist)
        {
            //Debug.Log("위에서 아래로 드래그, 숫자 감소" + (startPoint.y - endPoint.y));
            
            if (value > 0)
            {
                value -= 2;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
