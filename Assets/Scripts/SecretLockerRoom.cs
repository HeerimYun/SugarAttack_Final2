using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

/**
 * 비밀의 사물함 스크립트
 */
public class SecretLockerRoom : MonoBehaviour {

    /*아이템*/
    Item item;
    /*아이템 이미지*/
    Image itemImg;
    /*현재 캐릭터*/
    Character currentChar;
    /*가이드 텍스트 이미지*/
    GameObject guideTextImg;
    /*왼쪽 위 가이드 텍스트*/
    Text guideText;
    /*1~3개 개수*/
    int numOfItem = 0;
    /*자물쇠*/
    GameObject locker;

    /*Animation*/
    Animator lockeranim;
    Animator lockerDoorAnim;
    Animator getItem;

    /*Audio*/
    public AudioClip lockOpenSound;
    public AudioClip lockerOpenSound;
    AudioSource lockSound;

    //lastAnimation is play?
    Boolean animIsDone = false;

    /*비밀의 사물함 열린 문*/
    GameObject door_opened;
    /*비밀의 사물함 닫힌 문*/
    GameObject door_closed;

    Vector3 open = new Vector3(1, 1, 1);
    Vector3 close = new Vector3(0, 1, 1);

    double currentTime = 0;
    float duraition = 4;

    // Use this for initialization
    void Start () {
        GetUI();
	}

    public void GetUI()
    {
        itemImg = GameObject.Find("Item").GetComponent<Image>();
        guideText = GameObject.Find("LockerRoomText").GetComponent<Text>();
        door_opened = GameObject.Find("RedDoor_opened");
        door_closed = GameObject.Find("RedDoor_closed");     
        locker = GameObject.Find("locker");

        currentChar = GameData.GetCharByOrder(GameData.currentOrder);

        door_opened.transform.localScale = close;
        guideTextImg = GameObject.Find("GuideTextImg");

        lockeranim = GameObject.Find("locker").GetComponent<Animator>();
        lockerDoorAnim = GameObject.Find("RedDoor_closed").GetComponent<Animator>();
        getItem = GameObject.Find("LockerRoom/Item").GetComponent<Animator>();

        lockSound = GameObject.Find("LockerRoom/locker").GetComponent<AudioSource>();
    }

    /**
     * 사물함 열 때 동작 (자물쇠와 문을 클릭 시 동작)
     */
    public void OnClickLockerRoom()
    {
        SetItem();
        OpenLockerDoor();
        SetUI();
        GiveItem();
    }

    public void GiveItem()
    {
        //현재 캐릭터에게 아이템 주기
        switch (item.name)
        {
            case "Juice":
                currentChar.juice += numOfItem;
                break;
            case "Yogurt":
                currentChar.yogurt += numOfItem;
                break;
            case "Honey":
                currentChar.honey += numOfItem;
                break;
            case "Candy":
                currentChar.candy += numOfItem;
                break;
        }
    }

    public void SetUI()
    {
        //가이드 텍스트 세팅
        guideText.text = item.kName + " " + numOfItem + "개를 발견했어요!";

        float newSize = 0;

        //1207.8 -> 꿀, 1214 ->당근주스, 요구르트, 포도당사탕_>1317
        switch (item.kName)
        {
            case "꿀":
                newSize = 1207.8f;
                break;
            case "당근주스":
                newSize = 1214;
                break;
            case "요구르트":
                newSize = 1214;
                break;
            case "포도당사탕":
                newSize = 1317f;
                break;
        }
        guideTextImg.GetComponent<RectTransform>().sizeDelta = new Vector2(newSize, 182);
    }


    /**
     * 사물함 아이템을 랜덤하게 뽑은 후, 이미지 배치
     */
    public void SetItem()
    {
        //랜덤하게 뽑기
        PickItemRandom();
        //아이템 이미지 설정
        itemImg.sprite = Resources.Load<Sprite>("Secret_LockerRoom/" + item.name.ToLower() + "_" + numOfItem);
    }

    public void PickItemRandom()
    {
        //아이템 중 하나 뽑는다.
        item = GameData.item[UnityEngine.Random.Range(0, 4)]; //0~3
        //개수를 뽑는다. 
        numOfItem = UnityEngine.Random.Range(1, 4); //1~3개
    }

    public void OpenLockerDoor()
    {
        lockeranim.SetTrigger("Pressed");
        lockerDoorAnim.SetTrigger("Pressed");
    }
	
	// Update is called once per frame
	void Update () {
        if(!lockeranim.GetCurrentAnimatorStateInfo(0).IsName("lockerStop") && !lockeranim.GetCurrentAnimatorStateInfo(0).IsName("lockerShake")) {
            if(lockSound.clip != lockOpenSound) {
                lockSound.clip = lockOpenSound;
                lockSound.Play();
            }
        }

        if (!lockerDoorAnim.GetCurrentAnimatorStateInfo(0).IsName("lockerDoorClosed") && !lockerDoorAnim.GetCurrentAnimatorStateInfo(0).IsName("lockDoorOpen")) {
            door_opened.transform.localScale = open;
        }

		if (door_opened.transform.localScale == open)
        {
            currentTime += Time.deltaTime;

            if(currentTime > 2) {
                if (!animIsDone)
                {
                    getItem.SetTrigger(item.name.ToLower() + numOfItem);
                    itemImg.sprite = Resources.Load<Sprite>("Secret_LockerRoom/empty_locker");
                    lockSound.PlayOneShot(lockerOpenSound);
                    animIsDone = true;
                }
            }

            if (currentTime > duraition)
            {
                GameData.TurnChange();
                PageMove.MoveToRoulette();
            }
        }
	}
}
