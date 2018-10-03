using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 코치마크 스크립트
 */
public class CoachMark : MonoBehaviour {
    
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if(transform.localScale == GameData.open)
        {
            if (Input.GetMouseButtonDown(0))
            {
                transform.localScale = GameData.close;
            }
        }
	}
}
