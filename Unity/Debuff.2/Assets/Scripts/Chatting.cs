using System.Collections;
using UnityEngine;
using TMPro;
using System.Threading;
using System.Text;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.UI;

public class Chatting : MonoBehaviour
{
    public static Chatting instance { get; private set; }

    private bool isOpen = false; //채팅 UI 상태

    [Header("터널")]
    public GameObject tunnel_Player_Chatting_Img1; //터널: 아득해지는 기분이다.
    [Tooltip("터널: 아득해지는 기분이다.")] //마우스 오버하면 이 텍스트 출력해주는 기능.
    public TMP_Text tunnel_text1;
    public GameObject tunnel_Player_Chatting_Img2; //터널: 더 이상 돌아갈 수 없을 것 같은 느낌이 든다.
    [Tooltip("터널: 더 이상 돌아갈 수 없을 것 같은 느낌이 든다.")]
    public TMP_Text tunnel_text2;
    public GameObject ChattingCloseButton; //터널에서 채팅닫는버튼

    private Coroutine typingCoroutine; // 텍스트 타이핑 코루틴
    private bool isTyping = false; // 텍스트 타이핑 중인지 여부

    public GameObject player; // 플레이어 오브젝트
    private bool hasActivatedDialogue = false; // 대화창이 한번만 활성화되도록 하는 플래그

    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;

        //대화창 SetActive off 하는 함수 호출
        ChattingOff();

        tunnel_text1.text = "아득해지는 기분이다.";
        tunnel_text2.text = "더 이상 돌아갈 수 없을 것 같은 느낌이 든다.";
    }

    //P_Move에서 호출할 함수.
    //텍스트 개수 마다 코드 추가해야 함.
    public void TextSend_tunnel1()
    {
        if (!isTyping)
        {
            Debug.Log("TextSend_tunnel1() 호출됨");
            ChattingCloseButton.SetActive(true);
            tunnel_Player_Chatting_Img1.SetActive(true);
            typingCoroutine = StartCoroutine(PrintText(tunnel_text1.text, tunnel_text1)); //tunnel1
            hasActivatedDialogue = false;
        }
    }

    public void TextSend_tunnel2()
    {
        if (!isTyping)
        {
            Debug.Log("TextSend_tunnel2() 호출됨");
            ChattingCloseButton.SetActive(true);
            tunnel_Player_Chatting_Img2.SetActive(true);
            typingCoroutine = StartCoroutine(PrintText(tunnel_text2.text, tunnel_text2)); //tunnel2
            hasActivatedDialogue = false;
        }
    }

    //터널1
    IEnumerator PrintText(string content, TMP_Text textComponent)
    {
        Debug.Log("PrintText() 호출됨");
        isTyping = true;
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

        isTyping = false;
    }

    // 모든 대화창 off하는 함수
    public void ChattingOff()
    {
        isOpen = !isOpen; //t>f

        // 대화창 개수만큼 SetActive(false) 할 것
        // 터널
        tunnel_Player_Chatting_Img1.SetActive(false);
        tunnel_Player_Chatting_Img2.SetActive(false);
        ChattingCloseButton.SetActive(false);
        // 다리1
        // 오래된도시1
        // 다리2
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasActivatedDialogue)
        {
            ActivateDialogue();
        }
    }

    void ActivateDialogue()
    {
        hasActivatedDialogue = true; // 대화창이 한 번 활성화되면 다시 활성화되지 않도록 설정

        
    }

    // 버튼 클릭 이벤트
    public void OnChattingCloseButtonClick()
    {
        if (isTyping)
        {
            // 대화 시작 시 플레이어의 움직임을 멈추기
            player.GetComponent<P_Move>().enabled = false;

            // 타이핑 중이면 모든 텍스트를 한 번에 표시
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            if (tunnel_Player_Chatting_Img1.activeSelf)
            {
                Debug.Log("채팅 스크립트 - OnChattingCloseButtonClick() if문 실행");
                tunnel_text1.text = "아득해지는 기분이다.";
            }
            else if (tunnel_Player_Chatting_Img2.activeSelf)
            {
                Debug.Log("채팅 스크립트 - OnChattingCloseButtonClick() else if문 실행");
                tunnel_text2.text = "더 이상 돌아갈 수 없을 것 같은 느낌이 든다.";
            }

            isTyping = false;
        }
        else
        {
            // 타이핑이 완료된 후 버튼 클릭 시 대화창을 닫습니다.
            ChattingOff();
        }
    }

    public void Reset()
    {
        if (tunnel_text1 != null)
        {
            Debug.Log("Chatting-Reset() 호출됨");
        }
    }
}
