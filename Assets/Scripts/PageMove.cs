﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * 현재 씬 정보 가져오기
 * SceneManager.GetActiveScene();
 * SceneManager.GetActiveScene().buildIndex = 현재 빌드된 인덱스
 * SceneManager.LoadScene(씬번호);
 * SceneManager.LoadScene(SceneManager.GetSceneByName("Scene1").buildIndex);
 */
/**
 * 씬 이동
 */
public class PageMove : MonoBehaviour
{

    public static PageMove Instance;

    public GameData gameData = new GameData();

    public static int sceneIndex = 0;
    public static int[] scenes = new int[6] { 8, 9, 10, 11, 12, 13 };

    //인스턴스 없을 시 생성
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // Use this for initialization
    void Start()
    {

    }

    /**
     * @parameter: asset 폴더에서의 씬 이름 ex)0.0_Splash
     */
    public static void LoadSceneByName(string sceneName)
    {
        //Debug.Log(SceneManager.GetSceneByName(sceneName).buildIndex);
        SceneManager.LoadScene(SceneManager.GetSceneByName(sceneName).buildIndex);
    }

    public static string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    /**
     * 0.0_splash -> 0.1_WaitingRoom
     */
    public static void MoveToWaitingRoom()
    {
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
    static public void MoveToCharOrder()
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
        SceneManager.LoadScene(7);
    }

    /**
     * 퀴즈 보상화면으로 넘기기
     */
    public static void MoveToQuizReward()
    {
        SceneManager.LoadScene(6);
    }

    /**
     * 도서관으로 보낼 경우 유형에 맞게 도서관 씬에 보내기
     */
    public static void MoveToLibrary()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "5.0_Quiz_OX":
                SceneManager.LoadScene(15); //11.0_Library_OX
                break;
            case "5.0_Quiz_Line":
                SceneManager.LoadScene(18);
                break;
            case "5.0_Quiz_DragDrop":
                SceneManager.LoadScene(17);
                break;
            case "5.0_Quiz_Choice":
                SceneManager.LoadScene(16); //11.0_Library_Choice
                break;
        }
    }

    /**
     * 4.2_Insulin_volume 인슐린 용량 설정화면으로 가기
     */
    public static void MoveToInsulinInVolume()
    {
        SceneManager.LoadScene(14);
    }

    /**
     * 4.2_Insulin_body 인슐린 맞을 신체부위 보는 화면으로 가기
     */
    public static void MoveToInsulinBody()
    {
        SceneManager.LoadScene(19);
    }

    /**
     * 4.2_Insulin_angle 인슐린 용량 설정화면으로 가기
     */
    public static void MoveToInsulinAngle()
    {
        SceneManager.LoadScene(20);
    }

    /**
     * 4.2_Insulin_inject 인슐린 주입 화면으로 가기
     */
    public static void MoveToInsulinInject()
    {
        SceneManager.LoadScene(21);
    }

    /**
     * 4.2_Insulin_result 인슐린 결과 화면으로 가기
     */
    public static void MoveToInsulinResult()
    {
        SceneManager.LoadScene(22);
    }

    /**
     * 룰렛결과에 따라 
     */
    public static void Roulette()
    {
        /*
        if (sceneIndex > 5)
        {
            sceneIndex = 0;
        }

        Debug.Log("씬넘버" + sceneIndex);
        SceneManager.LoadScene(scenes[sceneIndex]);
        sceneIndex++;
        */
        //SceneManager.LoadScene(14); //인슐린 용량 설정 페이지
        //SceneManager.LoadScene(Random.Range(8, 14)); //퀴즈 유형 4개, 체육관, 비밀의 사물함 중 하나 랜덤
        //SceneManager.LoadScene(14); //인슐린 페이지
        //SceneManager.LoadScene(11); //OX 퀴즈
        //SceneManager.LoadScene(8); //선잇기 퀴즈
        //SceneManager.LoadScene(9); //선택형 퀴즈
        SceneManager.LoadScene(10); //드래그드랍
        //SceneManager.LoadScene(13); //운동
        //SceneManager.LoadScene(12); //비밀의사물함
        


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
    void Update()
    {

    }
}
