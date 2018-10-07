using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/**
 * curtain 길이 조정하는 스크립트
 */
public class CurtainController : MonoBehaviour {

    /*현재 스크립트가 붙어있는 curtain의 transform*/
    Transform curtain;

    /*curtain의 width, hegiht*/
    float width;
    float height;

    public Slider blueHandler; //inspector 창에서 연결

    public void ChangeWidth()
    {
        curtain.GetComponent<RectTransform>().sizeDelta = new Vector2(width * (1 - blueHandler.value), height);
    }

    // Use this for initialization
    void Start () {
        curtain = gameObject.transform;
        width = curtain.GetComponent<RectTransform>().rect.width;
        height = curtain.GetComponent<RectTransform>().rect.height;
        curtain.GetComponent<RectTransform>().sizeDelta = new Vector2(width * (1 - blueHandler.value), height);
    }
	
	// Update is called once per frame
	void Update () {

    }
}
