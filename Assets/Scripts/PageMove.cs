using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * 현재 씬 정보 가져오기
 * SceneManager.GetActiveScene();
 * SceneManager.GetActiveScene().buildIndex = 현재 빌드된 인덱스
 * SceneManager.LoadScene(씬번호);
 */
/**
 * 씬 이동
 */
public class PageMove : MonoBehaviour {

    public static PageMove Instance;

    public GameData gameData = new GameData();

    //인스턴스 없을 시 생성
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // Use this for initialization
    void Start () {
	}
	
    /**
     * 0.0_splash -> 0.1_WaitingRoom
     */
    public static void MoveToWaitingRoom() {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(1);
        }
    }

    /**
     * 0.1_WaitingRoom -> 1.0_IntroAnimation
     */
    public void MoveToIntro()
    {
        SceneManager.LoadScene(2);
        gameData.SetData(); //게임 데이터 세팅
    }

    /**
     * 1.0_IntroAnimation -> 1.1_CharSelect
     */
    public void MoveToCharSelect()
    {
        SceneManager.LoadScene(3);
    }

    /**
     * 1.1_CharSelect -> 1.2_CharOrder
     */
    public void MoveToCharOrder()
    {
        SceneManager.LoadScene(4);
    }

    /**
     * 1.2_CharOrder -> 1.3_InsulinInput
     */
    public void MoveToInsulinInput()
    {
        SceneManager.LoadScene(5);
    }

    /**
     * 1.5_Roulette 으로 이동
     */
    public static void MoveToRoulette()
    {
        SceneManager.LoadScene(6);
    }

    /**
     * 퀴즈 보상화면으로 넘기기
     */
    public static void MoveToQuizReward()
    {
        SceneManager.LoadScene(12);
    }

    /**
     * 룰렛결과에 따라 
     */
    public static void Roulette()
    {
        //SceneManager.LoadScene(7); //선잇기 퀴즈 (미완성)
        //SceneManager.LoadScene(8); //4지선다 선택형 퀴즈
        //SceneManager.LoadScene(9); //드래그 앤 드랍
        SceneManager.LoadScene(10); //OX퀴즈  
        //SceneManager.LoadScene(11); //비밀의 사물함
        //SceneManager.LoadScene(Random.Range(8, 12));

        //현재 위치
        /*switch (GameData.board[GameData.GetCharByOrder(GameData.currentOrder).position])
        {
            case "퀴즈":
                //퀴즈 씬으로 보내기 - 유형 4개 중 랜덤 Scene 넘버 7~10
                //SceneManager.LoadScene(Random.Range(7, 11));
                
                break;
        }*/
    }

    // Update is called once per frame
    void Update () {
		
	}
}
