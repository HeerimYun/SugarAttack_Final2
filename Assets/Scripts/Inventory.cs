using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Inventory : MonoBehaviour {

    //인벤토리 토글 버튼
    Toggle bag_toggle;

    //가방 내용 (펼쳐진 인벤토리 창)
    GameObject inventory_content;

    /*아이템을 골라주세요! 텍스트가 나타나는 부분*/
    GameObject inven_guide;

    /*어둡게 깔리는 판넬*/
    GameObject panel;

    /*아이템 팝업*/
    GameObject itemPopup;

    /*아이템 팝업에 쓰이는 이미지*/
    Image itemImg;

    /*아이템 이름, 설명*/
    Text itemName, itemGI;

    /*각 아이템의 개수 알림 text*/
    GameObject[] itemNum = new GameObject[5];

    /*아이템 버튼*/
    Button[] itemBtn = new Button[4];

    /*인벤토리 열 시 나타나는 캐릭터와 그래프*/
    GameObject charAndChart;

    /*인벤토리의 이미지*/
    Image invenChar;
    
    //인덱스 정의
    const int JUICE = 0;
    const int HONEY = 1;
    const int YOGURT = 2;
    const int CANDY = 3;
    const int BAG = 4;

    //열린 상태, 닫힌 상태 미리 정의
    Vector3 open = new Vector3(1, 1, 1);
    Vector3 close = new Vector3(0, 1, 1);

    //현재 차례인 캐릭터
    Character currentChar;

    //bag audio
    AudioSource bag_audio;

    // Use this for initialization
    void Start () {
        currentChar = GameData.GetCharByOrder(GameData.currentOrder); //현재 순서 캐릭터 객체 가져오기

        GetUI();
        UpdateItemNum();
    }

    /**
     * 아이템 개수 업데이트
     */
    public void UpdateItemNum()
    {
        itemNum[JUICE].GetComponent<Text>().text = currentChar.juice + "";
        itemNum[HONEY].GetComponent<Text>().text = currentChar.honey + "";
        itemNum[YOGURT].GetComponent<Text>().text = currentChar.yogurt + "";
        itemNum[CANDY].GetComponent<Text>().text = currentChar.candy + "";
        itemNum[BAG].GetComponent<Text>().text = (currentChar.juice + currentChar.honey + currentChar.yogurt + currentChar.candy) + "";

        for (int i = 0; i < itemNum.Length - 1; i++)
        {
            if (itemNum[i].GetComponent<Text>().text.Equals("0"))
            {
                itemNum[i].transform.localScale = close;
                itemBtn[i].interactable = false;
            }
            else
            {
                itemNum[i].transform.localScale = open;
                itemBtn[i].interactable = true;
            }
        }

        if (itemNum[BAG].GetComponent<Text>().text.Equals("0"))
        {
            GameObject.Find("ItemCount").transform.localScale = close;
        }
        else
        {
            GameObject.Find("ItemCount").transform.localScale = open;
        }
    }

    /**
     * 게임 오브젝트 가져오기
     */
    public void GetUI()
    {
        bag_toggle = GameObject.Find("Bag_toggle").GetComponent<Toggle>();
        bag_toggle.isOn = false;

        inventory_content = GameObject.Find("Inventory_content");
        inventory_content.transform.localScale = close;

        inven_guide = GameObject.Find("InvenTextArea");
        inven_guide.transform.localScale = close;

        panel = GameObject.Find("InvenPanel");
        panel.transform.localScale = close;

        itemPopup = GameObject.Find("Item_using");
        itemPopup.transform.localScale = close;

        itemImg = GameObject.Find("Item_Image").GetComponent<Image>();
        itemName = GameObject.Find("Item_name").GetComponent<Text>();
        itemGI = GameObject.Find("Item_text").GetComponent<Text>();
        //Debug.Log(itemImg + ", " + itemName + ", " + itemGI);

        itemNum[JUICE] = GameObject.Find("juiceNum");
        itemNum[HONEY] = GameObject.Find("honeyNum");
        itemNum[YOGURT] = GameObject.Find("yogurtNum");
        itemNum[CANDY] = GameObject.Find("candyNum");
        itemNum[BAG] = GameObject.Find("itemNum");

        itemBtn[JUICE] = GameObject.Find("JuiceBtn").GetComponent<Button>();
        itemBtn[HONEY] = GameObject.Find("HoneyBtn").GetComponent<Button>();
        itemBtn[YOGURT] = GameObject.Find("YogurtBtn").GetComponent<Button>();
        itemBtn[CANDY] = GameObject.Find("CandyBtn").GetComponent<Button>();

        bag_audio = GameObject.Find("Bag_toggle").GetComponent<AudioSource>();

        charAndChart = GameObject.Find("CharAndChart");
        charAndChart.transform.localScale = close;
        invenChar = GameObject.Find("CharImg").GetComponent<Image>();
        invenChar.sprite = Resources.Load<Sprite>("Characters/" + currentChar.name + "/idle/" + currentChar.name.ToLower() + "_idle_01");
    }
	// Update is called once per frame
	void Update () {
        UpdateItemNum();
    }

    /**
     * 가방 버튼 누를 시 동작
     * 눌러서 인벤토리 펼치기, 접기 가능
     */
    public void OnClickBagToggle()
    {
        bag_audio.Play();

        if (bag_toggle.isOn) //가방이 닫혀있으면,
        {
            OpenBag();
        }
        else //가방이 열려있으면
        {
            CloseBag();
        }
    }

    /**
     * 가방 여는 함수
     */
    public void OpenBag()
    {
        //인벤토리 창을 펼친다.
        //inventory_content.transform.localScale = open;

        //애니메이션으로 인벤토리 창 열기
        inventory_content.GetComponent<Animator>().SetTrigger("InvenOpen");

        //가이드 텍스트도 펼친다
        inven_guide.transform.localScale = open;

        //판넬 펼친다.
        panel.transform.localScale = open;

        //캐릭터 그래프 펼친다.
        charAndChart.transform.localScale = open;
    }

    /**
     * 가방 닫는 함수
     */
    public void CloseBag()
    {
        //인벤토리 창을 닫는다.
        //inventory_content.transform.localScale = close;

        //애니메이션으로 인벤토리 창 열기
        inventory_content.GetComponent<Animator>().SetTrigger("InvenClose");

        //가이드 텍스트를 닫는다
        inven_guide.transform.localScale = close;

        //판넬 닫는다.
        panel.transform.localScale = close;

        //캐릭터 그래프 닫는다.
        charAndChart.transform.localScale = close;

        //팝업이 열려있으면 닫는다.
        if (itemPopup.transform.localScale == open)
        {
            itemPopup.transform.localScale = close;
        }
    }

    /**
     * 주스, 꿀, 요구르트, 캔디 아이템 버튼
     */
    public void OnClickItemBtn()
    {
        //아이템 팝업 열기
        itemPopup.transform.localScale = open;
        //아이템 이름, 이미지, 혈당 상승값 세팅
        switch(EventSystem.current.currentSelectedGameObject.name)
        {
            case "JuiceBtn":
                itemName.text = GameData.item[JUICE].kName;
                itemGI.text = GameData.item[JUICE].itemGI + "";
                itemImg.sprite = Resources.Load<Sprite>("Inventory/juice");
                break;
            case "HoneyBtn":
                itemName.text = GameData.item[HONEY].kName;
                itemGI.text = GameData.item[HONEY].itemGI + "";
                itemImg.sprite = Resources.Load<Sprite>("Inventory/honey");
                break;
            case "YogurtBtn":
                itemName.text = GameData.item[YOGURT].kName;
                itemGI.text = GameData.item[YOGURT].itemGI + "";
                itemImg.sprite = Resources.Load<Sprite>("Inventory/yogurt");
                break;
            case "CandyBtn":
                itemName.text = GameData.item[CANDY].kName;
                itemGI.text = GameData.item[CANDY].itemGI + "";
                itemImg.sprite = Resources.Load<Sprite>("Inventory/candy");
                break;
        }

        itemImg.SetNativeSize();
    }

    /**
     * 아이템 팝업을 닫는 X버튼
     */
    public void OnClickXBtn()
    {
        //아이템 팝업 닫기
        itemPopup.transform.localScale = close;
    }

    /**
     * 아이템 사용 버튼
     */
    public void OnClickItemUseBtn()
    {
        switch (itemName.text)
        {
            case "주스":
                currentChar.juice -= 1; //아이템 개수 감소
                GameData.UpdateBloodSugar(currentChar, GameData.item[GameData.JUICE].itemGI); //혈당 올리기
                break;
            case "꿀":
                currentChar.honey -= 1;
                GameData.UpdateBloodSugar(currentChar, GameData.item[GameData.HONEY].itemGI);
                break;
            case "요구르트":
                currentChar.yogurt -= 1;
                GameData.UpdateBloodSugar(currentChar, GameData.item[GameData.YOGURT].itemGI);
                break;
            case "포도당사탕":
                currentChar.candy -= 1;
                GameData.UpdateBloodSugar(currentChar, GameData.item[GameData.CANDY].itemGI);
                break;
        }

        //아이템 개수 업데이트
        //UpdateItemNum();

        //팝업 닫기
        itemPopup.transform.localScale = close;
    }
}
