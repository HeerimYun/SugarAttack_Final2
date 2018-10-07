using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * 게임 그만두기 버튼
 */
public class StopGame : MonoBehaviour {

    /**
     * 게임 그만하기 버튼 누를 시 동작
     */
    public void OnClickStopGameBtn()
    {
        //대기실로 보내기
        SceneManager.LoadScene(0); //다시 처음 화면으로 간다.
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
