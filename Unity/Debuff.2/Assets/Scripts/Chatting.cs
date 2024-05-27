using System.Collections;
using UnityEngine;
using TMPro;
using System.Threading;
using System.Text;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.UI;

public class Chatting : MonoBehaviour
{
    public static Chatting instance {  get; private set; }

    //private string text; //각 문장=텍스트 정보를 저장할 문자열 배열

    //private float delay = 0.125f; //문자열 출력 지연 변수 초기화

    private bool isOpen = false; //채팅 UI 상태

    //씬마다 다르게 채팅이미지를 넣기 위해 public으로 선언
    //필요한 채팅 개수만큼 늘려서 사용할 것
    //<양식>
    //장소_대상_Chatting_Img숫자; //장소: 대사

    //터널
    [Header ("터널")]
    public GameObject tunnel_Player_Chatting_Img1; //터널: 아득해지는 기분이다.
    [Tooltip ("터널: 아득해지는 기분이다.")] //마우스 오버하면 이 텍스트 출력해주는 기능.
    public TMP_Text tunnel_text1;
    public GameObject tunnel_Player_Chatting_Img2; //터널: 더 이상 돌아갈 수 없을 것 같은 느낌이 든다.
    [Tooltip("터널: 더 이상 돌아갈 수 없을 것 같은 느낌이 든다.")]
    public TMP_Text tunnel_text2;
    public GameObject ChattingCloseButton; //터널에서 채팅닫는버튼

    //도시1


    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
        
        //대화창 SetActive off 하는 함수 호출
        ChattingOff();

        tunnel_text1.text = "아득해지는 기분이다.";
        tunnel_text2.text = "더 이상 돌아갈 수 없을 것 같은 느낌이 든다.";

        // +a: 텍스트 속성에서 Wrapping을 Disabled로 바꿔주면, 박스의 크기를 넘는 텍스트를 한줄로 처리
    }

    //P_Move에서 호출할 함수.
    //텍스트 개수 마다 코드 추가해야 함.
    public void TextSend_tunnel1()
    {
        //isOpen = !isOpen; //f>t
        //tunnel_Player_Chatting_Img1.SetActive(isOpen);
        if (true)
        {
            Debug.Log("TextSend_tunnel1() 호출됨");
            ChattingCloseButton.SetActive(true);
            StartCoroutine(PrintText(tunnel_text1.text, tunnel_text1)); //tunnel1
        }
    }

    public void TextSend_tunnel2()
    {
        if (true)
        {
            Debug.Log("TextSend_tunnel2() 호출됨");
            ChattingCloseButton.SetActive(true);
            StartCoroutine(PrintText(tunnel_text2.text, tunnel_text2)); //tunnel2
        }
        
    }

    //터널1
    IEnumerator PrintText(string content, TMP_Text textComponent)
    {
        Debug.Log("PrintText() 호출됨");
        textComponent.text = string.Empty;

        if (!string.IsNullOrEmpty(content))
        {
            Debug.Log("if문");
            for (int i = 0; i < content.Length; i++)
            {
                //Debug.Log("for문 실행됨 " + i);

                textComponent.text += content[i];

                yield return new WaitForSeconds(0.08f);
            }
        }
        else Debug.Log("context is null");
    }

    //모든 대화창 off하는 함수
    public void ChattingOff()
    {
        isOpen = !isOpen; //t>f

        //대화창 개수만큼 SetActive(false) 할 것
        //숲
        //도시1
        //AI.instance.AIanswer.SetActive(false);
        //AI.instance.AIquestion.SetActive(false);
        //터널
        tunnel_Player_Chatting_Img1.SetActive(false);
        tunnel_Player_Chatting_Img2.SetActive(false);
        ChattingCloseButton.SetActive(false);
        //다리1
        //오래된도시1
        //다리2
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Reset()
    {
        //tunnel_text1.text = "제발제바렞바렙잛ㅈㄹ";

        if (tunnel_text1 != null)
        {
            Debug.Log("Chatting-Reset() 호출됨");
        }
    }


}
