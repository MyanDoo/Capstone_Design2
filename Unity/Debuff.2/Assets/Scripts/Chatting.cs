using System.Collections;
using UnityEngine;
using TMPro;
using System.Threading;

public class Chatting : MonoBehaviour
{
    public static Chatting instance {  get; private set; }

    private string text; //각 문장=텍스트 정보를 저장할 문자열 배열

    private string[] texts;

    //private TMP_Text[] textObjects;
    private float delay = 0.125f; //문자열 출력 지연 변수 초기화
    private int currentTextIndex = 0; // 현재 출력 중인 텍스트의 인덱스

    //private GameObject[] parentObjects; //텍스트의 최상위 오브젝트 배열

    //public int count = 0;

    //씬마다 다르게 채팅이미지를 넣기 위해 public으로 선언
    //필요한 채팅 개수만큼 늘려서 사용할 것
    //<양식>
    //장소_대상_Chatting_Img숫자; //장소: 대사

    //터널
    [Header ("터널")]
    public GameObject tunnel_Player_Chatting_Img1; //터널: 아득해지는 기분이다.
    [Tooltip ("터널: 아득해지는 기분이다.")] //마우스 오버하면 이 텍스트 출력해주는 기능.
    public TMP_Text tunnel_text1;
    public TMP_Text[] tunnel_text1s;
    public GameObject tunnel_Player_Chatting_Img2; //터널: 더 이상 돌아갈 수 없을 것 같은 느낌이 든다.
    [Tooltip("터널: 더 이상 돌아갈 수 없을 것 같은 느낌이 든다.")]
    public TMP_Text tunnel_text2;
    public TMP_Text[] tunnel_text2s;


    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;

        //대화창 SetActive off 하는 함수 호출
        ChattingOff();

        //텍스트 창에서 직접 텍스트 입력하기
        // +a: 텍스트 속성에서 Wrapping을 Disabled로 바꿔주면, 박스의 크기를 넘는 텍스트를 한줄로 처리
    }


    //P_Move에서 호출할 함수.
    //텍스트 개수 마다 코드 추가해야 함.
    public void TextSend()
    {
        text = tunnel_text1.text.ToString();
        tunnel_text1.text = " ";
        text = tunnel_text2.text.ToString();
        tunnel_text2.text = " ";

        texts = new string[tunnel_text1s.Length];
        for(int i = 0; i < tunnel_text1s.Length; i++)
        {
            texts[i] = tunnel_text1s[i].text;
            tunnel_text1s[i].text = string.Empty;
        }
        currentTextIndex = 0;

        StartCoroutine(PrintText_tunnel1(delay));
        //StartCoroutine(PrintText_tunnel2(delay));
    }

    //한 글자씩 출력하게 하는 함수
    //텍스트 개수만큼 생성해야 함

    //터널1
    IEnumerator PrintText_tunnel1(float delay)
    {
        /*int count = 0;

        while (count != text.Length) //텍스트 개수가 0이 아니면, 모든 텍스트 객체를 처리할 때까지
        {
            if (count < text.Length) //텍스트 개수가 0보다 크면
            {
                tunnel_text1.text = text[count].ToString();
                count++;
            }
            yield return new WaitForSeconds(delay);
        }*/

        while (currentTextIndex < texts.Length)
        {
            string currentText = texts[currentTextIndex];
            TMP_Text currentTMP_Text = tunnel_text1s[currentTextIndex];
            for (int i = 0; i < currentText.Length; i++)
            {
                currentTMP_Text.text += currentText[i]; // 현재 텍스트 객체에 한 글자씩 추가
                yield return new WaitForSeconds(delay);
            }
            currentTextIndex++;
        }
    }
    //터널2
    IEnumerator PrintText_tunnel2(float delay)
    {
        int count = 0;

        while (count != text.Length)
        {
            if (count < text.Length)
            {
                tunnel_text2.text = text[count].ToString();
                count++;
            }
            yield return new WaitForSeconds(delay);
        }
    }

    //대화창을 위한 함수
    void OnChatting()
    {
        //대화창 개수만큼 SetActive(false) 할 것
        //숲
        //도시1
        //터널
        {
            tunnel_Player_Chatting_Img1.SetActive(false);
            tunnel_Player_Chatting_Img2.SetActive(false);
        }
        //다리1
        //오래된도시1
        //다리2
    }


    //모든 대화창 off하는 함수
    public void ChattingOff()
    {
        //대화창 개수만큼 SetActive(false) 할 것
        //숲
        //도시1
        //터널
        tunnel_Player_Chatting_Img1.SetActive(false);
        tunnel_Player_Chatting_Img2.SetActive(false);
        //다리1
        //오래된도시1
        //다리2
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
