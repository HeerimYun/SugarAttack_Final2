using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/**
 * 물음표 버튼을 누르면 펼쳐지는 HelpMenu
 */
public class HelpMenu : MonoBehaviour {

    /* 페이지 번호 */
    const int GAME_PAGE = 0; // 게임진행 페이지
    const int KAN_PAGE = 1; // 칸 설명 페이지
    const int CHAR_PAGE = 2; // 캐릭터 페이지

    int[] pageNum = new int[3]; //각 페이지 별 전체 페이지 수

    /* 게임진행, 칸 설명, 캐릭터 */
    Toggle[] menu = new Toggle[3];

    /* 현재 페이지 */
    int currentMenu = 0;
    int curPage = 1;

    //open, close 상태 지정
    Vector3 open = new Vector3(1, 1, 1);
    Vector3 close = new Vector3(0, 1, 1);

    //캐릭터 설명 부분 요소
    Image charImg; //케릭터 설명 페이지

    /* 페이지네이션 부분 텍스트 */
    Text currentPage; //현재 보고 있는 페이지
    Text allPage; // 해당 메뉴의 전체 페이지 수
    Button[] pageBtn = new Button[2]; //페이지 버튼
    const int LEFT = 0;
    const int RIGHT = 1;

    const int MARIE = 0;
    const int MATT = 1;
    const int LUCY = 2;
    const int VICTOR = 3;

    string[] charNames = new string[4];

	// Use this for initialization
	void Start () {
        GetUI();
        SetUI(GAME_PAGE);
        OnClickCloseBtn();
    }

    public void GetUI()
    {
        menu[GAME_PAGE] = GameObject.Find("Game_btn").GetComponent<Toggle>();
        menu[KAN_PAGE] = GameObject.Find("Kan_btn").GetComponent<Toggle>();
        menu[CHAR_PAGE] = GameObject.Find("Char_btn").GetComponent<Toggle>();

        charImg = GameObject.Find("Char_img").GetComponent<Image>();

        currentPage = GameObject.Find("CurrentPageText").GetComponent<Text>();
        allPage = GameObject.Find("AllPageText").GetComponent<Text>();

        pageNum[GAME_PAGE] = 3;
        pageNum[KAN_PAGE] = 2;
        pageNum[CHAR_PAGE] = 4;

        charNames[MARIE] = "Marie";
        charNames[MATT] = "Matt";
        charNames[LUCY] = "Lucy";
        charNames[VICTOR] = "Victor";

        pageBtn[0] = GameObject.Find("Left_btn").GetComponent<Button>();
        pageBtn[1] = GameObject.Find("Right_btn").GetComponent<Button>();
    }

    public void SetUI(int curMenu)
    {
        //맨 처음 활성화 할 메뉴는 게임진행
        currentMenu = curMenu;
        curPage = 1;

        //아래 페이지네이션 설정
        allPage.text = pageNum[curMenu] + "";
        currentPage.text = curPage + "";
    }
	
    /**
     * 게임진행, 칸 설명, 캐릭터 메뉴를 누를 때 호출
     * 눌린 버튼에 대한 이미지 변경, 페이지 상태 변경
     */
     public void OnClickMenuToggle()
    {
        switch (EventSystem.current.currentSelectedGameObject.name)
        {
            case "Game_btn":
                currentMenu = GAME_PAGE;
                break;
            case "Kan_btn":
                currentMenu = KAN_PAGE;
                break;
            case "Char_btn":
                currentMenu = CHAR_PAGE;
                break;
        }

        SetUI(currentMenu);
        DisplayContent();
    }

    /**
     * 열기 버튼
     */
    public void OpenHelpMenu()
    {
        SetUI(GAME_PAGE);
        gameObject.transform.localScale = open;
        DisplayContent();
    }

    /**
     * 닫기 버튼
     */
    public void OnClickCloseBtn()
    {
        gameObject.transform.localScale = close;
    }

    /**
     * 페이지네이션 버튼 (페이지 왼쪽, 오른쪽)
     */
    public void OnClickPageBtn()
    {
        if (EventSystem.current.currentSelectedGameObject.name.Equals("Left_btn"))
        {
            if (curPage > 1)
            {
                curPage--;
                currentPage.text = curPage + "";
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name.Equals("Right_btn"))
        {
            if (curPage < pageNum[currentMenu])
            {
                curPage++;
                currentPage.text = curPage + "";
            }
        }

        DisplayContent();
    }

    public void DisplayContent()
    {
        //해당 페이지 보여줄 것
        if (currentMenu == GAME_PAGE)
        {
            //게임페이지 관련 설명 넣을 예정
            charImg.sprite = Resources.Load<Sprite>("helpMenu/game_menu_1");
        }
        else if (currentMenu == KAN_PAGE)
        {
            //칸 관련 설명 넣을 예정
            charImg.sprite = Resources.Load<Sprite>("helpMenu/board_notComplited");
        }
        else
        {
            //캐릭터 설명
            charImg.sprite = Resources.Load<Sprite>("helpMenu/helpMenu_" + charNames[curPage-1]);
        }
    }

    /**
     * 캐릭터 설명 부분 페이지마다 바꿔주기
     */
    public void SetCharMenu()
    {

    }

	// Update is called once per frame
	void Update () {
		
	}
}
