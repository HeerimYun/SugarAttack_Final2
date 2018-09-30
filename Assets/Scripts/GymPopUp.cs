using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 운동완료 팝업을 일정 시간동안 띄우는 스크립트
 */
public class GymPopUp : MonoBehaviour {

    /*현재 시간*/
    float currentTime = 0;
    /*지속 시간*/
    float duraition = 2;
    /*현재 캐릭터*/
    Character currentChar;

    /*correct_particle*/
    ParticleSystem correctParticle;

    // Use this for initialization
    void Start () {
        currentChar = GameData.GetCharByOrder(GameData.currentOrder);
        //현재 순서의 캐릭터 팝업으로 바꿔줌
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Gym/" + currentChar.name + "_gym_popup");
        correctParticle = GameObject.Find("WorkDonePopup/correct_panpare").GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        if (this.transform.localScale == GameData.open)
        {
            if(correctParticle.isPaused || correctParticle.isStopped) {
                correctParticle.Play();
            }
            currentTime += Time.deltaTime;

            //화면 터치 시 바로 넘어감
            if (Input.GetMouseButtonDown(0))
            {
                this.GetComponent<Animator>().SetTrigger("RemovePop");
                correctParticle.Stop();
                Gym.isWorkDone = true;
            }
        }

        if (currentTime > duraition)
        {
            this.GetComponent<Animator>().SetTrigger("RemovePop");
            correctParticle.Stop();
            Gym.isWorkDone = true;
        }
	}
}
