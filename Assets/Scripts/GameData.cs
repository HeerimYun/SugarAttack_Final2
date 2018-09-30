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
    
    //퀴즈 타입
    public const int OX_QUIZ = 0;
    public const int CHOICE_QUIZ = 1;
    public const int DRAG_QUIZ = 2;
    public const int LINE_QUIZ = 4;


    //보드 정보: 총 54칸
    public static string[] board = new string[54] {
    "출발", "몬스터", "급식실", "퀴즈", "몬스터",
    "사물함", "퀴즈", "운동", "별별상황실", "사물함",
    "몬스터","급식실","퀴즈","급식실","몬스터",
    "급식실(stop)","퀴즈","몬스터", "별별상황실","사물함",
    "몬스터","운동","사물함","몬스터", "별별상황실",
    "과학실","몬스터","퀴즈","급식실(stop)" , "몬스터",
    "사물함","퀴즈","몬스터","운동", "몬스터",
    "사물함","퀴즈","급식실","퀴즈", "운동",
    "급식실(stop)","별별상황실","퀴즈","몬스터","퀴즈",
    "급식실", "몬스터","과학실","운동","몬스터",
    "퀴즈","도착","도서관","보건실"};

    //

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
    string[] exerciseName = new string[EXERCISE] { "걷기", "걷기", "테니스", "테니스", "자전거타기", "자전거타기", "자전거타기", "자전거타기", "배드민턴", "달리기", "계단 오르내리기", "체조", "수영" };
    string[] exerciseType = new string[EXERCISE] { "(느리게)", "(빠르게)", "(혼자)", "(같이)", "(느리게)", "(보통)", "(빠르게)", "(격하게)", "", "", "", "", ""};
    int[] exerciseCa = new int[EXERCISE] { 17, 28, 28, 20, 17, 25, 28, 40, 20, 40, 19, 11, 45 };

    string[] exerciseAdvantage = new string[] { //어디에 좋을까? 에 들어가는 내용
        "가벼운 걷기 운동은 근육과 지방이\n활발히 움직이면서 혈당을 낮춰주는\n아주 좋은 운동이에요!\n가볍게 걸으면서 몸을 움직이면\n식사 조절의 스트레스도 사라져요.", //걷기
        "테니스는 팔, 다리 몸의 모든 근육을\n골고루 사용해서 활동량이 매우 큰\n운동이에요! 활동량이 많기 때문에\n심장혈관을 튼튼하게 해주고,\n혈당도 금방 낮아져요.", //테니스
        "자전거는 관절에 무리가 없고,\n다리의 근육을 만들어 주는\n운동이에요! 심장과 폐 기능에\n좋아서 부담 없이 혈당을 조절\n하기 위해 할 수 있는 운동이에요.", //자전거타기
        "배드민턴은 상체로는 공을 치고,\n하체는 공을 향해 뛰는 운동으로\n근육 발달에 좋은 운동이에요!\n공을 칠 때 복부의 힘이\n생겨 복근을 만들어줘요!", //배드민턴
        "달리기는 지방을 사라지게 해주고,\n식욕을 낮춰서 혈당 조절에 좋은\n운동이에요! 뇌를 깨워주고, 피를\n깨끗하게 만들어주는 운동이에요!", //달리기
        "계단 오르내리기는 허벅지, 종아리,\n발목의 근육을 만들어 주는\n운동이에요! 심장 기능이 좋아져서\n몸 전체에 골고루 피를 전달할 수\n있도록 도와줘요!", //계단 오르내리기
        "체조는 몸 전체의 근육을 골고루\n발달시켜주는 운동이에요!\n꾸준한 체조 운동은 몸을 유연하게\n만들어줘서 부상을 예방할 수 있어요!", //체조
        "수영은 다른 운동에 비해 짧은 시간\n동안 혈당 조절을 할 수 있는\n운동이에요! 몸 전체의 피를 골고루\n전달해줘서 심장과 폐 기능을\n강하게 해줘요!", //수영
    };
    string[] exerciseCaution = new string[] { //주의사항 에 대한 내용
        "가볍게 걸을 때도 허리를 곧게 펴고,\n바닥을 뒤꿈치부터 내딛어야 해요!", //걷기
        "공을 사용한 운동이기 때문에\n스트레칭을 꼭 해주고, 너무 무리한\n운동은 저혈당의 위험이 있어요!", //테니스
        "자전거를 탈 때는 머리와 무릎,\n팔꿈치를 보호할 수 있는 보호\n장비를 꼭 착용해야 해요!", //자전거타기
        "라켓을 사용하는 운동이기 때문에\n주변 사람들과 부딪치지 않도록\n조심해야 해요!", //배드민턴
        "너무 빠르게 달리면 심장에 무리가\n갈 수 있으니 자신의 몸 상태에 맞게\n잘 체크해줘야 해요!", //달리기
        "상체가 앞으로 구부러지지 않게\n허리를 꼿꼿이 펴고, 발바닥 전체를\n디디면서 오르내려야 해요!", //계단 오르내리기
        "체조의 순서와 동작을 정확하게\n따라해야 운동의 효과를 더욱 크게\n만들어줘요!", //체조
        "수영을 하면서 체온이 금방 떨어질\n수 있으니 조심해야 해요!" //수영
    };


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
    string[] itemNames = { "Juice", "Honey", "Yogurt", "Candy" };
    int[] itemGI = { 14, 9, 7, 3 };
    public const int JUICE = 0;
    public const int HONEY = 1;
    public const int YOGURT = 2;
    public const int CANDY = 3;

    public const int OX_QUIZ_NUM = 31;
    //퀴즈
    //OX QUIZ
    public string[] quiz_ox_question = new string[OX_QUIZ_NUM]
    {
        "고혈당의 증상 중 하나는 식은 땀이 나는 거야!",
        "저혈당의 증상 중 하나는 몸이 으슬으슬 떨리는 거야!",
        "30분 전에 피자를 먹었어요, 입이 심심하니 도넛을 먹을까요?",
        "고혈당별에서 온 몬스터들은 포도당캔디를 먹고 건강해진대!",
        "혈당에 좋은 음식을 먹으면 불규칙한 시간에 먹어도 될까요?",
        "운동을 많이 하면 그만큼 많이 간식을 먹어도 된대!",
        "운동을 하면서 힘들면 중간중간 휴식을 취해줘요.",
        "상한 인슐린을 맞으면 오히려 혈당이 올라가요.",
        "매일 똑같은 부위에 주사를 맞아야해요!",
        "주사 바늘은 계속 사용할 수 있어요!",
        "매일 같은 시간에 식사를 하는 게 좋아요!",
        "주사 바늘은 45~90도 사이에서 맞아야 해요!",
        "인슐린 주사는 근육에 주사해야해요!",
        "몸이 아프면 혈당체크를 하지 않아도 돼요!",
        "인슐린 주사를 맞고 운동하면 혈당을 낮추는데 효과적이예요!",
        "인슐린 주사를 맞을 때 공기를 제거하지 않아도 돼!",
        "운동은 식사 후보다 식사 전에 하는 게 더 좋아!",
        "혈당이 올라갈 수 있어서 과일은 절대 먹으면 안돼!",
        "자신이 할 수 있는 만큼의 운동을 꾸준히 하는 게 좋아요!",
        "과식을 했을 때는 다음 끼니를 굶어야해!",
        "혈당이 70이하로 떨어졌을 때를 저혈당이라고해!",
        "저혈당의 증상이 느껴지면 하던 일을 멈추고 혈당체크를 해야해!",
        "과도한 운동은 고혈당의 위험을 높여요!",
        "힘들고 오래 하는 운동은 혈당이 적게 떨어져요!",
        "설사를 해서 음식을 적게 먹으면 주사의 양을 줄여야 해요!",
        "갈증이 나면 저혈당이 올 수 있어서 단 음료를 먹어야 해요!",
        "운동할 때 땀을 많이 흘리면 혈당이 더 잘 내려가요!",
        "당뇨에 걸리면 감기약을 먹으면 안돼요!",
        "당이 적은 음식은 폭식을 해도 괜찮아요!",
        "인슐린의 양은 내 마음대로 바꿔도 돼요!",
        "밥 먹기 전 혈당이 120 이상인 상태를 고혈당이라고 해!"
    };

    //ox퀴즈 답
    public string[] quiz_ox_answer = new string[OX_QUIZ_NUM]
    {
        "X",
        "O",
        "X",
        "X",
        "X",
        "X",
        "O",
        "O",
        "X",
        "X",
        "O",
        "O",
        "X",
        "X",
        "X",
        "X",
        "X",
        "X",
        "O",
        "X",
        "O",
        "O",
        "X",
        "X",
        "O",
        "X",
        "X",
        "X",
        "X",
        "X",
        "O"
    };

    //ox퀴즈 해설
    public string[] quiz_ox_library = new string[OX_QUIZ_NUM]
    {
        "식은땀이 나는 건 고혈당 상태가 아니라 저혈당 상태예요.",
        "몸이 으슬으슬 떨리면 저혈당 상태이기 때문에 간단한 간식을 먹어줘야해요.",
        "식사 후에 바로 간식을 먹으면 고혈당이 될 위험이 높아져요!",
        "포도당 사탕을 먹으면 혈당이 높아지기 때문에, 고혈당인 몬스터들에게 치명적이예요!",
        "음식은 규칙적으로 먹는 게 좋아요.",
        "운동을 많이했다고 간식을 많이 먹으면 혈당이 갑자기 올라갈 수 있어요.",
        "힘들게 운동하다 보면 혈당이 갑자기 내려가서 저혈당이 올 수 있어요",
        "상한 인슐린을 맞으면 오히려 혈당이 갑자기 올라가게 돼요.",
        "주사 부위는 조금씩 바꿔주는 게 좋아요.",
        "주사 바늘은 한 번 쓰고 버려야해요.",
        "규칙적인 식사를 하면 정상적인 혈당을 유지할 수 있어요.",
        "주사 바늘은 45~90도 사이에서 맞아야 인슐린이 잘 들어가요.",
        "인슐린을 근육에 맞으면 저혈당이 올 수 있어요.",
        "몸이 아플 때는 평소보다 자주 혈당을 체크해야 해요.",
        "인슐린 주사를 맞고 바로 운동을 하게 되면 혈당이 갑자기 낮아져서 저혈당이 올 수 있어요",
        "인슐린 주사를 맞을 때는 공기를 충분히 빼주어야 아프지 않게 맞을 수 있어요.",
        "식사 후 1시간 뒤에 운동을 하는 것이 가장 좋아요.",
        "조금씩 과일을 먹는 건 괜찮지만 너무 많이 먹으면 혈당이 올라갈 수 있어요.",
        "운동을 꾸준히 하면 혈당을 낮춰줄 수 있어요.",
        "과식을 했을 때는 다음 끼니를 굶지말고 가벼운 운동을 통해 혈당을 내려주는 게 좋아요.",
        "혈당이 70 이하로 떨어졌을 때를 저혈당이라고 해요.",
        "저혈당의 증상이 느껴지면 하던 일이 멈추고 혈당체크를 해서 혈당을 확인해주는게 좋아요.",
        "과도한 운동은 저혈당의 위험이 있어요.",
        "힘들고 오래하는 운동은 오히려 혈당이 더 많이 떨어져서 저혈당이 발생할 수 있어요.",
        "음식을 적게 먹었는데 주사를 똑같이 맞으면 저혈당이 올 수 있어요!",
        "갈증이 나면 혈당을 갑자기 올리는 단 음료보다 당분이 적은 이온음료가 좋아요!",
        "운동할 때 땀을 많이 흘리게 되면 오히려 탈수 증상이 올 수 있어요!",
        "종합감기약은 먹어도 괜찮아요!",
        "당이 적은 음식이어도 폭식은 안돼요!",
        "인슐린 용량은 의사선생님께 물어보고 결정해야해요!",
        "혈당을 80 ~ 120으로 유지해 주는게 가장 좋아요!"
    };

    public const int CHOICE_QUIZ_NUM = 12;
    //4지 선다형 퀴즈
    public string[] quiz_choice_question = new string[CHOICE_QUIZ_NUM]
    {
        "머릿 속이 멍해요! 잠깐 쉬어갈까요?",
        "보관이 잘 된 인슐린은 무엇일까요?",
        "엄지와 검지를 (  )CM 정도 벌려 피부를 잡아주어야해요!",
        "보관이 잘못된 인슐린을 골라주세요!",
        "인슐린 주사 방법 중 빈칸의 순서는 무엇일까요?\n인슐린 용기 소독하기 / (   )제거하기 / 용량 설정하기 / 인슐린 주사하기",
        "운동 하기 가장 좋은 시간은 언제일까?",
        "몸이 아플 때 어떻게 해야하는지 정답을 골라주세요!",
        "식사는 (  )분 이상 천천히 해야해요!",
        "운동은 일주일에 (  )일 이상 하는 것이 좋아요!",
        "올바른 운동법에 대해 골라보세요!",
        "주사를 맞았던 자리는 (  )일 동안 맞지 않는게 좋아요!",
        "물을 많이 먹고 화장실을 자주 가는 것은 어떤 상태일까요?"
    };
    
    public string[] quiz_choice_choice = new string[CHOICE_QUIZ_NUM * 4]
    {
        "열심히 몬스터를 잡아야해","인슐린 주사를 맞을까?","잠시 앉아서 휴식을 취하자","운동을 해야해",
        "3년된 인슐린", "냉동 보관된 인슐린", "덩어리가 없는 인슐린", "유효기한이 지난 인슐린",
        "2","3","5","7",
        "덩어리가 없는 인슐린","냉장보관된 인슐린","유효기간이 지난 인슐린","투명한 인슐린",
        "바늘","인슐린","공기","물",
        "식사 후에","식사 전에","자기 전에","꿈 속에서",
        "혈당체크를 자주한다.","밥을 안먹고 인슐린 주사를 맞는다.","운동을 무리하게 한다.","달콤한 간식을 먹는다.",
        "5","10","15","20",
        "0","1","2","3",
        "힘든 운동을 오래 하기","식사 하고 운동하기","인슐린 주사 맞고 바로 운동하기","아예 운동 하지 않기",
        "5","10","15","30",
        "저혈당","고혈당","정상혈당","아무 상태 아니다."

    };
    public string[] quiz_choice_answer = new string[CHOICE_QUIZ_NUM]
    {
        "잠시 앉아서 휴식을 취하자",
        "덩어리가 없는 인슐린",
        "5",
        "유효기간이 지난 인슐린",
        "공기",
        "식사 후에",
        "혈당체크를 자주한다.",
        "20",
        "3",
        "식사 하고 운동하기",
        "30",
        "고혈당"
    };

    public string[] quiz_choice_library = new string[CHOICE_QUIZ_NUM]
    {
        "머릿 속이 멍한 건 저혈당 증상이예요. 그렇기 때문에 잠시 휴식을 취하는 게 좋아요.",
        "냉동으로 얼린 인슐린이나 2년 이상된 인슐린은 상한 인슐린이예요.",
        "손가락으로 5CM 피부를 잡아 올려주어야 인슐린이 잘 들어가요.",
        "덩어리가 있는 인슐린과 유효기간이 지난 인슐린은 상한 인슐린이라서 오히려 혈당이 갑자기 높아질 수 있어요.",
        "공기를 반드시 제거해줘야해요!",
        "운동은 식사 후 1시간 뒤가 가장 좋아요.",
        "식사를 안 했을때는 인슐린 주사를 조금만 맞아야 해요!",
        "식사는 20분 이상 천천히 하는게 좋아요!",
        "규칙적인 운동은 혈당 관리 많은 도움을 줘요!",
        "밥을 먹지 않고 힘들게 하거나 오래 운동하게 되면 저혈당이 올 수 있어요!",
        "주사를 맞았던 자리에 다시 주사를 놓으면 더 아파요!",
        "고혈당은 물을 많이 먹고 화장실을 자주 가게 돼요!"
    };

    public const int LINE_QUIZ_NUM = 3;
    //선 잇기 퀴즈
    public string[] quiz_line_question = new string[LINE_QUIZ_NUM]
    {
        "상태와 증상을 알맞게 연결해주세요.",
        "고혈당과 저혈당의 상태를 알맞게 연결해주세요.",
        "운동에 맞는 올바른 효과를 골라주세요!"
    };
    public string[] quiz_line_choices1 = new string[LINE_QUIZ_NUM * 2]
    {
        "피에 당이 많은 상태","피에 당이 부족한 상태",
        "고혈당","저혈당",
        "짧은 운동","긴 운동"
    };
    public string[] quiz_line_choices2 = new string[LINE_QUIZ_NUM * 2]
    {
        "저혈당","고혈당",
        "목이 마르다","식은땀이 난다",
        "몸무게가 줄어든다","혈당이 떨어진다"
    };
    public string[] quiz_line_answerMatch = new string[LINE_QUIZ_NUM * 2]
    {
        "고혈당","저혈당",
        "목이 마르다","식은땀이 난다",
        "혈당이 떨어진다","몸무게가 줄어든다"
    };
    public string[] quiz_line_library = new string[LINE_QUIZ_NUM]
    {
        "당이 많으면 고혈당, 적으면 저혈당이예요.",
        "고혈당일 때는 목이 자주 마르고, 저혈당일 때는 식은 땀이 나요.",
        "짧은운동이 혈당을 떨어지게 하는데에 좋아요!"
    };

    public const int DRAG_QUIZ_NUM = 7;
    //드래드앤드랍 퀴즈
    public string[] quiz_drag_question = new string[DRAG_QUIZ_NUM]
    {
        "저혈당 상태가 온 것 같아요. \n저혈당 상태에 나타나는 내 몸의 증상은 무엇일까요?",
        "모든 일에 집중이 되지 않아요. \n뭔가를 먹어볼까요?",
        "저혈당이 왔을 때 먹을 간식을 챙겨보아요!",
        "식후 운동을 하려고 해요. \n어떤 운동을 하는게 좋을까요?",
        "고혈당 상태가 온 것 같아요. 고혈당 상태에 나타나는 내 몸의 증상은 무엇일까요?",
        "인슐린 주사를 맞을 때 올바른 부위를 선택해주세요.",
        "혈당을 낮추는 데에 도움이 되는 운동을 골라보세요!"
    };

    public string[] quiz_drag_postit = new string[DRAG_QUIZ_NUM * 6]
    {
        "기침","발저림","식은땀","집중력\n저하","치통","복통",
        "물","쨈","포도당\n사탕","초콜릿","오렌지\n주스","우유",
        "포도당\n사탕","치킨","짜장면","피자","꿀물","햄버거",
        "한강\n수영하기","한라산\n오르기","가벼운\n걷기","자전거\n타기","5시간\n뛰기","체조\n백번하기",
        "목이\n말라요","계속\n먹어요","몸이\n떨려요","기침이\n나와요","이가\n아파요","배가\n아파요",
        "배","손","허벅지","가슴","발","목",
        "에어로빅","윗몸\n일으키기","체조\n백번","숨쉬기","팔굽혀\n펴기","앉기"
    };

    public string[] quiz_drag_answer = new string[]
    {
        "식은땀","집중력\n저하",
        "포도당\n사탕","오렌지\n주스",
        "포도당\n사탕","꿀물",
        "가벼운\n걷기","자전거\n타기",
        "목이\n말라요","계속\n먹어요",
        "배","허벅지",
        "에어로빅"
    };

    public int[] quiz_drag_answer_num = new int[DRAG_QUIZ_NUM] { 2,2,2,2,2,2,1 };

    public string[] quiz_drag_library = new string[DRAG_QUIZ_NUM]
    {
        "기침과 치통, 복통은 저혈당의 증세가 아니예요.",
        "포도당 사탕과 오렌지 주스가 저혈당 예방에 더 좋은 간식이예요.",
        "치킨,짜장면,햄버거,피자는 간식으로 먹으면 혈당이 갑자기 올라갈 수 있어요.",
        "너무 힘든 운동은 오히려 저혈당의 위험이 있어요.",
        "몸이 떨리는 증상은 저혈당 증상이예요.",
        "손, 가슴, 발, 목은 근육이 있어서 저혈당이 올 수 있어요.",
        "에어로빅이 혈당을 낮추는 데 더 도움이 돼요!"
    };

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

    //선택형 퀴즈 객체 배열
    public static ChoiceQuiz[] choiceQuizzes;
    //OX 퀴즈 객체 배열
    public static OXQuiz[] oxQuizzes;
    //Line 퀴즈 객체 배열
    public static LineQuiz[] lineQuizzes;
    //드래그 퀴즈 객체 배열
    public static DragQuiz[] dragQuizzes;

    /*리스트*/
    List<int> bloodSugar;
    List<int> monsterCard;

    /*열고 닫기*/
    public static Vector3 open = new Vector3(1, 1, 1);
    public static Vector3 close = new Vector3(0, 1, 1);

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
     * 게임 내 필요한 데이터를 생성
     * 인트로애니메이션에서 실행
     */
    public void SetData()
    {
        //SetMonsters(); //몬스터 카드
        //SetFood(); //음식
        //SetSnack(); //간식
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
        int index;
        //배열 초기화
        for (int i=0; i<EXERCISE; i++)
        {
            index = -1;
            switch(exerciseName[i])
            {
                case "걷기":
                    index = 0;
                    break;
                case "테니스":
                    index = 1;
                    break;
                case "자전거타기":
                    index = 2;
                    break;
                case "배드민턴":
                    index = 3;
                    break;
                case "달리기":
                    index = 4;
                    break;
                case "계단 오르내리기":
                    index = 5;
                    break;
                case "체조":
                    index = 6;
                    break;
                case "수영":
                    index = 7;
                    break;
            }

            exercise[i] = new Exercise(exerciseName[i], exerciseCa[i], exerciseType[i], exerciseAdvantage[index],exerciseCaution[index]);
        }
    }

    /**
     * 퀴즈 객체 배열 생성
     */
    private void SetQuiz()
    {
        SetQuestion();
        SetOXQuiz();
        SetChoiceQuiz();
        SetLineQuiz();
        SetDragQuiz();
    }

    /**
     * 모든 유형의 문제 앞에 문제 단어를 붙임
     */
    public void SetQuestion()
    {
        AddText(quiz_ox_question);
        AddText(quiz_line_question);
        AddText(quiz_choice_question);
        AddText(quiz_drag_question);
    }

    /**
     * 모든 문제 앞에 "문제. "를 붙이는 작업
     */
    public void AddText(string[] questionArray)
    {
        for(int i=0; i<questionArray.Length; i++)
        {
            questionArray[i] = "문제. " + questionArray[i];
        }
    }

    /**
     * 선택형 퀴즈 객체 배열
     */
    private void SetChoiceQuiz()
    {
        choiceQuizzes = new ChoiceQuiz[quiz_choice_question.Length];
        
        for (int i=0; i<quiz_choice_question.Length; i++)
        {
            string[] choices = new string[4];
            //정답 배열 만들기
            for (int k=0; k<4; k++)
            {
                choices[k] = quiz_choice_choice[(i * 4) + k];
            }

            choiceQuizzes[i] = new ChoiceQuiz(quiz_choice_question[i], choices, quiz_choice_answer[i], 0, quiz_choice_library[i]);
        }
    }

    /**
     * OX 퀴즈 객체 배열 생성
     */
    private void SetOXQuiz()
    {
        oxQuizzes = new OXQuiz[quiz_ox_question.Length];

        for (int i = 0; i < quiz_ox_question.Length; i++)
        {
            oxQuizzes[i] = new OXQuiz(quiz_ox_question[i], quiz_ox_answer[i], 0, quiz_ox_library[i]);
        }
    }

    /**
     * Line 퀴즈 객체 배열 생성
     */
    private void SetLineQuiz()
    {
        lineQuizzes = new LineQuiz[quiz_line_question.Length];

        for (int i=0; i<quiz_line_question.Length; i++)
        {
            string[] c_left = new string[2]; //왼쪽 선택지
            string[] c_right = new string[2]; //오른쪽 선택지
            string[] answer = new string[2]; //선택지에 대한 답

            for (int k=0; k<2; k++)
            {
                c_left[k] = quiz_line_choices1[(i * 2) + k];
                c_right[k] = quiz_line_choices2[(i * 2) + k];
                answer[k] = quiz_line_answerMatch[(i * 2) + k];
            }

            lineQuizzes[i] = new LineQuiz(quiz_line_question[i], c_left, c_right, answer, quiz_line_library[i], 0);
        }
    }

    /**
     * 드래그앤 드랍 퀴즈
     * answer 배열 사이즈 다른 것 유의
     */
    private void SetDragQuiz()
    {
        dragQuizzes = new DragQuiz[quiz_drag_question.Length];

        for (int i=0; i<quiz_drag_question.Length; i++)
        {
            string[] postits = new string[quiz_drag_postit.Length]; //한 문제에 대한 포스트잇 배열 생성
            string[] answers = new string[quiz_drag_answer_num[i]]; //답 배열 생성

            for(int k=0; k<6; k++) //포스트잇 텍스트
            {
                postits[k] = quiz_drag_postit[i * 6 + k];
            }

            for (int n=0; n< quiz_drag_answer_num[i]; n++) //답 배열 텍스트
            {
                answers[n] = quiz_drag_answer[i * 2 + n];
            }

            dragQuizzes[i] = new DragQuiz(quiz_drag_question[i], postits, answers, quiz_drag_library[i], 0);
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
            item[i] = new Item(itemNames[i],itemKNames[i], itemGI[i]);
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
            character[i] = new Character(playerList[i], MakeKName(playerList[i]), false, 0, 0, 0, 0, 0, 5, monsterCard, 0, bloodSugar, 0, 0, false, 0, false); //만든 객체 초기화
        }

        //Debug.Log("캐릭터 생성완료");
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

    /**
     * 변화한 혈당값을 리스트의 맨 끝에 추가
     */
    public static void UpdateBloodSugar(Character currentChar, int newBloodSugar)
    {
        currentChar = GetCharByOrder(currentOrder);
        int lastIndex = currentChar.bloodSugar.Count - 1;
        int lastValue = currentChar.bloodSugar[lastIndex];

        currentChar.bloodSugar.Add(lastValue + newBloodSugar);
        Debug.Log(currentChar.name + "의 혈당 변화 : " + lastValue + "->" + currentChar.bloodSugar[currentChar.bloodSugar.Count - 1]);
    }

    /**
     * 현재 칸에 현재 캐릭터가 처음 도착했는가?
     */
    public static void IsFirstTime()
    {
        //보드 객체 생성 후, 해당 칸에 대한 속성확인

    }

    //혈당 매번 체크하여 저혈당인지 고혈당인지 체크할 것
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
    /*도서관 한 턴 쉬기*/
    public bool libraryTurn;

    //생성자
    public Character (string name, string kName , bool abilityUsed, int order, int score, int honey, int juice, int yogurt, int candy, 
        List<int> monsterCard, int position, List<int> bloodSugar, int highOrLowCount, int nurseCount, bool isCurrent, int inputInsulin, bool libraryTurn)
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
        this.libraryTurn = libraryTurn;
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
 * 4지 선다형 퀴즈 클래스
 */
public class ChoiceQuiz
{
    /*퀴즈 질문*/
    public string question;
    /*퀴즈 선택지*/
    public string[] choice = new string[4];
    /*실제 정답*/
    public string answer;
    /*퀴즈 등장 여부*/
    public int appeard;
    /*해설*/
    public string library;

    //생성자
    public ChoiceQuiz(string question, string[] choice, string answer, int appeard, string library)
    {
        this.question = question;
        this.choice = choice;
        this.answer = answer;
        this.appeard = appeard;
        this.library = library;
    }
}

/**
 * OXQuiz
 */
public class OXQuiz
{
    /*퀴즈 질문*/
    public string question;
    /*실제 정답*/
    public string answer;
    /*퀴즈 등장 여부*/
    public int appeard;
    /*도서관 해설*/
    public string library;

    //생성자
    public OXQuiz(string question,  string answer, int appeard, string library)
    {
        this.question = question;
        this.answer = answer;
        this.appeard = appeard;
        this.library = library;
    }
}

/**
 * 선잇기 퀴즈
 */
public class LineQuiz
{
    /*퀴즈 질문*/
    public string question;
    /*선잇기 왼쪽 영역*/
    public string[] choice_left = new string[2];
    /*선잇기 오른쪽 영역*/
    public string[] choice_right = new string[2];
    /*정답*/
    public string[] answers = new string[4];
    /*해설*/
    public string library;
    /*퀴즈 등장 횟수*/
    int appear;

    public LineQuiz(string question, string[] choice_left, string[] choice_right, string[] answers, string library, int appear)
    {
        this.question = question;
        this.choice_left = choice_left;
        this.choice_right = choice_right;
        this.answers = answers;
        this.library = library;
        this.appear = appear;
    }
}

/**
 * 드래그 퀴즈
 */
public class DragQuiz
{
    /*퀴즈 질문*/
    public string question;
    /*6개 포스트잇 보기 텍스트*/
    public string[] postit = new string[6];
    /*정답*/
    public string[] answer; //사이즈가 다름
    /*해설*/
    public string library;
    /*퀴즈 등장 횟수*/
    public int appear;

    public DragQuiz(string question, string[] postit, string[] answer, string library, int appear)
    {
        this.question = question;
        this.postit = postit;
        this.answer = answer;
        this.library = library;
        this.appear = appear;
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
    /*운동 종류*/
    public string type;
    /*어디에 좋을까? 설명*/
    public string advantage;
    /*주의할 점*/
    public string caution;

    //생성자
    public Exercise(string name, int GI, string type, string advantage, string caution)
    {
        this.name = name;
        this.GI = GI;
        this.type = type;
        this.advantage = advantage;
        this.caution = caution;
    }
}

public class Item
{
    /*아이템 이름*/
    public string name;
    /*아이템 한글 이름*/
    public string kName;
    /*GI 지수*/
    public int itemGI;

    //생성자
    public Item(string name, string kName, int itemGI)
    {
        this.name = name;
        this.kName = kName;
        this.itemGI = itemGI;
    }
}

/**
 * 보드판 클래스
 */
public class Board
{
    /*칸 이름*/
    public string name;
    /*방문자 수*/
    public int visitor;

    public Board(string name, int visitor)
    {
        this.name = name;
        this.visitor = visitor;
    }
}