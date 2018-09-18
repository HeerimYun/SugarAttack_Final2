using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashController : MonoBehaviour
{
    Scrollbar loading;
    Text loadingText;
    Image progHead;
    float moveFloat;
    int i = 0;

    float currentTime = 0;
    // Use this for initialization
    void Start()
    {
        //UI 요소 찾아오기
        loading = GameObject.Find("Loading").GetComponent<Scrollbar>();
        loadingText = GameObject.Find("LoadingText").GetComponent<Text>();
        progHead = GameObject.Find("progress_head").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (i <= 100)
        {
            loadingText.text = "학교 가는 중..(" + i + "%)";
        }
        else
        {
            i = 101;
            currentTime += Time.deltaTime;

            if (currentTime > 3)
            {
                PageMove.MoveToWaitingRoom(); //로딩 다 되고 3초 후 다음 화면으로 이동
            }
        }
        loading.size += Time.deltaTime * 0.5f;
        moveFloat += Time.deltaTime * 0.04f;
        progHead.rectTransform.position = Vector3.Lerp(progHead.transform.position, new Vector3(2430, progHead.transform.position.y, 0), moveFloat);
        i++;   
    }
}
