using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 설정메뉴 스크립트
 * 배경음, 효과음의 볼륨 변경
 */
public class SettingMenu : MonoBehaviour {
    /*bgm을 재생하는 오브젝트 (from donotDestroyOnLoad)*/
    GameObject bgm;
    /*bgm 슬라이더*/
    Slider bgmVolumeSlider;
    /*효과음 슬라이더*/
    Slider effectVolumeSlider;
    /*현재 씬의 모든 audioSource 객체 배열*/
    AudioSource[] audioInThisScene;

    // Use this for initialization
    void Start () {
        transform.localScale = GameData.close;
        bgm = GameObject.Find("BGM");
        bgmVolumeSlider = GameObject.Find("Bgm_slider/BlueHandler").GetComponent<Slider>();
        effectVolumeSlider = GameObject.Find("Effect_slider/BlueHandler").GetComponent<Slider>();

        GetAllAudioSources(); //현재씬의 모든  AudioSource 컴포넌트 찾기
    }

    /**
     * 현재 씬의 모든 AudioSource를 찾는다
     * 배열 생성
     */
    private void GetAllAudioSources()
    {
        audioInThisScene = new AudioSource[FindObjectsOfType<AudioSource>().Length];
        audioInThisScene = FindObjectsOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void OpenSettingMenu()
    {
        transform.localScale = GameData.open;
    }

    /**
     * X button 누를 경우 -> 설정 메뉴를 닫는다.
     */
    public void OnClickXButton()
    {
        transform.localScale = GameData.close;
    }

    public void ChangeBGMVolume()
    {
        if (bgm != null) //bgm 오브젝트가 있다면,
        {
            bgm.GetComponent<AudioSource>().volume = bgmVolumeSlider.value;
        }
    }

    public void ChangeEffectVolume()
    {
        for (int i=0; i< FindObjectsOfType<AudioSource>().Length; i++)
        {
            //활성화 된 놈들만 가져오기 && bgm 이 아닌 객체들만 가져오기
            if (audioInThisScene[i].enabled && !(audioInThisScene[i].gameObject.name.Equals("BGM")))
            {
                audioInThisScene[i].volume = effectVolumeSlider.value; //볼륨 변경
            }
        }
    }
}
