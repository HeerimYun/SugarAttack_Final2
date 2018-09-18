 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * 게임 관련 데이터 입/출력 스크립트
 * - 음식, 간식, 몬스터, 비밀의 사물함, 운동 클래스 생성
 * - 캐릭터 클래스의 혈당을 리스트로 변경
 */
public class GameData {

    public static GameData Instance; //single tone pattern 이용(:객체 하나만 생성)

    //몬스터 카드 개수
    public const int MONSTER_CARD = 18;
    //퀴즈 개수
    public const int QUIZ = 58;
    //음식 개수
    public const int FOOD = 16;
    //간식 개수
    public const int SNACK = 12;
    //운동 개수
    public const int EXERCISE = 13;
    //아이템 종류 개수
    public const int ITEM = 4;

    //보드 정보: 총 54칸
    public static string[] board = new string[54] {
    "출발", "몬스터", "식사", "퀴즈", "몬스터", "사물함", "퀴즈", "운동",
    "상황", "사물함", "몬스터","간식","퀴즈","몬스터","식사","급식","퀴즈","몬스터",
    "찬스","사물함", "몬스터","운동","사물함","몬스터","상황","실험실","몬스터","퀴즈","급식"
    ,"찬스", "사물함","퀴즈","몬스터","운동","몬스터","사물함","퀴즈","식사","퀴즈","운동",
    "급식","상황","찬스","몬스터","퀴즈","몬스터","간식","실험실","운동","몬스터","퀴즈","도착",
    "도서관","보건실"};

    //*************************************각 칸 별 정보*************************************//

    //급식실 배열
    string[] cafeteriaName = new string[23] { "쫄면 (1인분)", "샌드위치 (1개)", "라면 (1봉지)", "국수 (1인분)", "피자 (1조각)", "햄버거 (1개)", "시리얼 (1인분)", "우동 (1그릇)", "파스타 (1인분)", "만두 (100g)", "짜장면 (반그릇-어린이)", "메밀국수 (반그릇-어린이용)", "후렌치후라이 (100g)", "콘샐러드", "팥밥 (1인분)", "잡곡밥 (1공기)", "백미 (1공기)", "흰죽 (1인분)", "쌀국수 (1그릇)", "현미밥 (1공기)", "보리밥", "흰 바게트 빵", "흰 빵 (밀가루)" };
    int[] cafeteriaCa = new int[23] { 43, 27, 56, 40, 26, 32, 27, 47, 52, 20, 49, 52, 35, 23, 51, 73, 53, 44, 37, 32, 44, 16, 14 };

    //식사 칸 배열
    string[] foodName = new string[FOOD] { "감 (1개)", "귤 (3개)", "딸기 (1접시)", "망고 (1개)", "멜론 (1개)", "복숭아 (3개)", "사과 (2개)", "오렌지 (2개)", "파인애플 (반 개)", "포도 (1컵)", "수박 (1 큰 반달조각)", "자몽 (3개)", "옥수수 (1개)", "삶은 감자 (1개)", "삶은 고구마 (1개)", "단호박 (1개)" };
    int[] foodCa = new int[FOOD] { 31, 27, 27, 35, 45, 27, 38, 30, 30, 28, 43, 36, 30, 20, 28, 14 };
    string[] foodQuantity = new string[FOOD] { "1개", "3개", "1접시", "1개", "1개", "3개", "2개", "2개", "반 개", "1컵", "1 큰 반달조각", "3개", "1개", "1개", "1개", "1개" };

    //간식 칸 배열
    //string[] snackName = new string[SNACK] { "도넛 (1개)", "쿠키 (1개)", "스낵과자 (1개)", "핫도그 (1개)", "카스테라 (1개)", "초코파이 (1개)", "팥빵 (1개)", "찹쌀떡 (1개)", "초콜릿 (1개)", "아이스크림 (1개)", "콜라", "포도주스" };
    string[] snackName = new string[SNACK] { "도넛", "쿠키", "스낵과자", "핫도그", "카스테라", "초코파이", "팥빵", "찹쌀떡", "초콜릿", "아이스크림", "콜라", "포도주스" };
    int[] snackCa = new int[SNACK] { 23, 10, 18, 15, 31, 23, 45, 25, 24, 13, 25, 25 };

    //운동 칸 배열
    string[] exerciseName = new string[EXERCISE] { "느리게 걷기", "빠르게 걷기", "테니스(혼자)", "테니스(같이)", "자전거타기(느리게)", "자전거타기(보통)", "자전거타기(빠르게)", "자전거타기(격하게)", "배드민턴", "달리기", "계단 오르내리기", "체조", "수영" };
    int[] exerciseCa = new int[EXERCISE] { 17, 28, 28, 20, 17, 25, 28, 40, 20, 40, 19, 11, 45 };

    //몬스터 칸 배열
    int[] monsterScore = new int[MONSTER_CARD] { 5, 10, 20, 10, 15, 20, 5, 10, 25, 5, 20, 25, 5, 15, 20, 5, 15, 25 };
    string[] monsterName = new string[MONSTER_CARD] { "카곤", "카곤", "카곤", "글루", "글루", "글루", "케톤", "케톤", "케톤", "고이드", "고이드", "고이드", "에피", "에피", "에피", "노르", "노르", "노르" };
    int[] monsterLevel = new int[MONSTER_CARD] { 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3};
    int[] maxCandy = new int[MONSTER_CARD] { 2, 3, 4, 1, 3, 5, 1, 4, 5, 1, 3, 5, 1, 2, 5, 1, 2, 4 };

    //실험
    int[] experiment = new int[2] { 0, 2 };

    //상황
    public static string[] situation = new string[18] { "몬스터를 잡기 위해 운동장을 뛰었더니 혈당이 내려갔어요.", "학교 옥상으로 도망간 몬스터를 잡기 위해 계단을 올라갔더니 혈당이 내려갔어요.", "학교 수영장으로 도망간 몬스터를 잡기 위해 수영을 했더니 혈당이 내려갔어요.", "숨어 있는 몬스터를 찾으러 다녔더니 혈당이 내려갔어요.", "친구의 간식을 훔쳐 달아나는 몬스터를 잡기 위해 자전거를 탔더니 혈당이 내려갔어요.", "친구의 초콜릿 2개를 빼앗아 먹었더니 혈당이 올라갔어요.", "책상 위에 있는 도넛 1개를 먹었더니 혈당이 올라갔어요.", "가방에 있던 초코파이 2개를 모두 먹었더니 혈당이 올라갔어요.", "시리얼을 먹고 인슐린 주사 맞는 걸 깜빡했더니 혈당이 올라갔어요.", "밥을 먹지 않고 아이스크림 1개와 팥빵 1개를 많이 먹었더니 혈당이 올라갔어요.", "포도당 캔디 1개를 떨어뜨려버렸어요.", "포도당 캔디 2개를 떨어뜨려버렸어요.", "포도당 캔디 1개를 다른 플레이어에게 선물해주세요.", "가지고 있는 아이템(꿀/주스/요구르트) 중 1개를 선택하여 다른 플레이어에게 선물해주세요.", "저혈당으로 혈당이 내려갔어요. 보건실로 이동해야해요.", "고혈당으로 혈당이 올라갔어요. 보건실로 이동해야해요.", "가지고 있는 인슐린이 상했어요.", "혈당체크를 깜빡했어요. 다음 순서까지 혈당을 볼 수 없어요." };
    public static int[] situationVal = new int[18] { -40, -19, -45, -28, -28, 48, 23, 46, 27, 58, 0, 0, 0, 0, 0, 0, 0, 0 };

    //비밀의 사물함
    string[] lockeroom = new string[12] { "주스 1컵을 발견했어요.", "주스 2컵을 발견했어요.", "주스 3컵을 발견했어요.", "꿀 1스푼을 발견했어요.", "꿀 2스푼을 발견했어요.", "꿀 3스푼을 발견했어요.", "요구르트 1개를 발견했어요.", "요구르트 2개를 발견했어요.", "요구르트 3개를 발견했어요.", "포도당 사탕 1개를 발견했어요.", "포도당 사탕 2개를 발견했어요.", "포도당 사탕 3개를 발견했어요." };

    //아이템
    string[] itemKNames = { "당근주스", "꿀", "요구르트", "포도당사탕" };
    int[] itemGI = { 14, 9, 7, 3 };

    //퀴즈
    public static string[] quizQuestion = new string[QUIZ] {
        "상태와 증상을 알맞게 연결해주세요.",
        "저혈당 상태가 온 것 같아요. 저혈당 상태에 나타나는 내 몸의 증상은 무엇일까요?",
        "고혈당의 증상 중 하나는 식은 땀이 나는 거야!",
        "저혈당의 증상 중 하나는 몸이 으슬으슬 떨리는 거야!",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "", "", "",
        "", "", "", "", "", "", "", "", "", "",
        "", "", "", "", "", "", "", "", "", "",
        "", "", "", "", "", "", "", "", "", "",
        "", "", "", "", ""
    };
    public static string[] quizAnswer = new string[QUIZ] {
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "", "", "",
        "", "", "", "", "", "", "", "", "", "",
        "", "", "", "", "", "", "", "", "", "",
        "", "", "", "", "", "", "", "", "", "",
        "", "", "", "", ""
    };
    public static string[] quizType = new string[QUIZ] {
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "", "", "",
        "", "", "", "", "", "", "", "", "", "",
        "", "", "", "", "", "", "", "", "", "",
        "", "", "", "", "", "", "", "", "", "",
        "", "", "", "", ""
    };
    //public static int curQuiz = 0;

    //음식 - 임시로 만든 목록
    /*public static string[] kFoodName = {"샌드위치", "우동" , "짬뽕", "피자"}; //화면에 뜰 글자
    public static string[] foodName = {"sandwich","udong","jjamppong", "pizza"}; //각 음식에 대한 파일명
    public static int curFood = 0;
    public static int[] foodSugar = { 27, 47, 43, 26};*/

    //캐릭터 변수
    public static Character[] character;

    //public static bool isCharSelected = false;

    //public List<int> bloodSugar = new List<int>();

    //********진행 관련 변수*********//

    //게임 참여중인 플레이어 리스트
    public static string[] playerList;
    //현재 순서
    public static int currentOrder = 1;
    //플레이어 수
    public static int playerCount;
    //monster 카드 배열
    public static Monster[] monster;
    //음식 배열
    public static Food[] food;
    //간식 배열
    public static Snack[] snack;
    //운동 배열
    public static Exercise[] exercise;
    //퀴즈 배열
    public static Quiz[] quiz;
    //아이템 배열
    public static Item[] item;

    /*리스트*/
    List<int> bloodSugar;
    List<int> monsterCard;

    //인스턴스 없을 시 생성
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // Use this for initialization
    void Start () {
        SetData(); //게임 내 필요 데이터 생성
	}

    /**
     * 게임 내 필요한 데이터를 생성
     * 인트로애니메이션에서 실행
     */
    public void SetData()
    {
        SetMonsters(); //몬스터 카드
        SetFood(); //음식
        SetSnack(); //간식
        SetExercise(); //운동
        SetQuiz(); //퀴즈
        SetItem(); //아이템

        //PageMove의 MoveToIntro() 에서 한 번 실행
        //Debug.Log("SetData 실행");
    }

    /**
     * 몬스터 초기화 메서드
     */
    private void SetMonsters()
    {
        monster = new Monster[MONSTER_CARD];
        for(int i=0; i<MONSTER_CARD; i++)
        {
            monster[i] = new Monster(monsterName[i], monsterLevel[i] ,monsterScore[i], maxCandy[i]);
        }
    }


    /**
     * 음식 객체 배열 생성
     */
    private void SetFood()
    {
        food = new Food[FOOD]; //음식 배열 생성

        //배열 초기화
        for (int i=0; i<FOOD; i++)
        {
            food[i] = new Food(foodName[i], foodQuantity[i], foodCa[i]); //데이터 넣음
        }
    }

    /**
     * 간식 객체 배열 생성
     */
    private void SetSnack()
    {
        snack = new Snack[SNACK]; //간식 배열 생성

        //배열 초기화
        for (int i=0; i<SNACK; i++)
        {
            snack[i] = new Snack(snackName[i], snackCa[i]);
        }
    }

    /**
     * 운동 객체 배열 생성
     */
    private void SetExercise()
    {
        exercise = new Exercise[EXERCISE]; //운동 배열 생성

        //배열 초기화
        for (int i=0; i<EXERCISE; i++)
        {
            exercise[i] = new Exercise(exerciseName[i], exerciseCa[i]);
        }
    }

    /**
     * 퀴즈 객체 배열 생성
     */
    private void SetQuiz()
    {
        quiz = new Quiz[QUIZ]; //퀴즈 배열 생성

        //배열 초기화
        for (int i=0; i<QUIZ; i++)
        {
            quiz[i] = new Quiz(quizQuestion[i], quizAnswer[i], quizType[i], 0);
        }
    }

    /**
     * 아이템 객체 배열 생성
     */
    private void SetItem()
    {
        item = new Item[ITEM];

        //배열 초기화
        for (int i=0; i<ITEM; i++)
        {
            item[i] = new Item(itemKNames[i], itemGI[i]);
        }
    }

    /**
     * 캐릭터 객체 생성 메서드
     */
    public void SetCharacters()
    {
        character = new Character[playerList.Length]; //플레이어가 몇 명인지 받아서 생성

        //혈당 리스트 생성, 초기 혈당은 100
        bloodSugar = new List<int> { 100 };
        //몬스터 카드 리스트 생성
        monsterCard = new List<int>();

        for (int i = 0; i < character.Length; i++)
        {
            character[i] = new Character(playerList[i], MakeKName(playerList[i]), false, 0, 0, 0, 0, 0, 5, monsterCard, 0, bloodSugar, 0, 0, false, 0); //만든 객체 초기화
        }

        Debug.Log("캐릭터 생성완료");
    }

    /**
     * 한글 이름 변환기
     */
    public string MakeKName(string engName)
    {
        string kName = "";

        switch (engName)
        {
            case "Marie":
                kName = "마리";
                break;
            case "Matt":
                kName = "매트";
                break;
            case "Lucy":
                kName = "루시";
                break;
            case "Victor":
                kName = "빅터";
                break;
        }

        return kName;
    }

    /**
     * 해당 순위의 캐릭터를 찾아 리턴
     */
    public static Character GetCharByOrder(int order)
    {
        Character result = null;

        for (int i=0; i<character.Length; i++)
        {
            if (character[i].order == order)
            {
                result = character[i];
            }
        }

        return result;
    }

    /**
     * 턴 넘기는 메서드
     * !주의 : 인원수를 넘기면 다시 돌아오게 해야 한다
     */
    public static void TurnChange()
    {
        //현재 순서 바꿔줌
        currentOrder++;

        //플레이어수를 넘어가면 다시 처음 턴으로!
        if (currentOrder > character.Length)
        {
            currentOrder = 1;
        }

        //for문을 통해 현재 캐릭터의 상태를 바꿔줌
        for(int i=0; i<playerCount; i++)
        {
            if (currentOrder == character[i].order)
            {
                character[i].isCurrent = true;
            }
            else
            {
                character[i].isCurrent = false;
            }
        }
    }
}


//----------------------------------------------------(여기부터는 클래스)------------------------------------------------------

/**
 * 캐릭터 클래스
 */
public class Character
{
    /*이름*/
    public string name;
    /*한글 이름*/
    public string kName;
    /*능력*/
    public bool abilityUsed;
    /*순서*/
    public int order;
    /*점수*/
    public int score;
    /*아이템*/
    public int honey, juice, yogurt, candy;
    /*몬스터 카드*/
    public List<int> monsterCard;
    /*보건카드*/
    public int bogunCard;
    /*보드판에서의 위치*/
    public int position;
    /*혈당*/
    public List<int> bloodSugar;
    /*이상 상태 걸린 횟수*/
    public int highOrLowCount;
    /*보건실 도착 횟수*/
    public int nurseCount;
    /*현재 순서인가?*/
    public bool isCurrent;
    /*입력 혈당*/
    public int inputInsulin;

    //생성자
    public Character (string name, string kName , bool abilityUsed, int order, int score, int honey, int juice, int yogurt, int candy, 
        List<int> monsterCard, int position, List<int> bloodSugar, int highOrLowCount, int nurseCount, bool isCurrent, int inputInsulin)
    {
        this.name = name;
        this.kName = kName;
        this.abilityUsed = abilityUsed;
        this.order = order;
        this.score = score;
        this.honey = honey;
        this.juice = juice;
        this.yogurt = yogurt;
        this.candy = candy;
        this.monsterCard = monsterCard;
        this.position= position;
        this.bloodSugar= bloodSugar;
        this.highOrLowCount= highOrLowCount;
        this.nurseCount = nurseCount;
        this.isCurrent = isCurrent;
        this.inputInsulin = inputInsulin;
    }   
}

/**
 * 몬스터 클래스
 */
public class Monster
{
    /*몬스터 이름*/
    public string name;
    /*몬스터 레벨*/
    public int level;
    /*몬스터 점수*/
    public int score;
    /*최대 캔디 개수*/
    public int candy;

    //생성자
    public Monster(string name, int level ,int score, int candy)
    {
        this.name = name;
        this.level = level;
        this.score = score;
        this.candy = candy;
    }
}

/**
 * 음식 클래스
 */
public class Food
{
    /*음식 이름*/
    public string name;
    /*음식 양*/
    public string quantity;
    /*음식 혈당 지수*/
    public int foodGI;

    //생성자
    public Food(string name, string quantity, int foodGI)
    {
        this.name = name;
        this.quantity = quantity;
        this.foodGI = foodGI;
    }
}

/**
 * 간식 클래스
 */
 public class Snack
{
    /*간식 이름*/
    public string name;
    /*간식 혈당 지수*/
    public int snackGI;

    public Snack(string name, int snackGI)
    {
        this.name = name;
        this.snackGI = snackGI;
    }
}

/**
 * 퀴즈 유형 enum으로 분류
 */
public class EnumScript : MonoBehaviour
{
    //OX, 선택(괄호포함), 드래그앤드랍, 선긋기
    enum QuizType { OX, Choose, DragDrop, Line };
}

public class Quiz
{
    /*퀴즈 질문*/
    public string question;
    /*퀴즈 정답*/
    public string answer;
    /*퀴즈 유형*/
    public string type;
    /*퀴즈 등장 여부*/
    public int appeard;

    //생성자
    public Quiz(string question, string answer, string type, int appeard)
    {
        this.question = question;
        this.answer = answer;
        this.type = type;
        this.appeard = appeard;
    }
}

/**
 * 비밀의 사물함 클래스
 */
 public class SecretLocker
{
    /*비밀의 사물함 가이드 멘트*/
    public string guideText;
    /*획득 아이템*/
    public string prize;
    /*획득 아이템 개수*/
    public int prizeNum;

    //생성자
    public SecretLocker(string guideText, string prize, int prizeNum)
    {
        this.guideText = guideText;
        this.prize = prize;
        this.prizeNum = prizeNum;
    }
}

public class Exercise
{
    /*운동 이름*/
    public string name;
    /*운동 소모 칼로리*/
    public int GI;

    //생성자
    public Exercise(string name, int GI)
    {
        this.name = name;
        this.GI = GI;
    }
}

public class Item
{
    /*아이템 한글 이름*/
    public string kName;
    /*GI 지수*/
    public int itemGI;

    //생성자
    public Item(string kName, int itemGI)
    {
        this.kName = kName;
        this.itemGI = itemGI;
    }
}