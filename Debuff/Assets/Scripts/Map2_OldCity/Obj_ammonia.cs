using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Obj_ammonia : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // 대화창에 사용할 텍스트 UI
    public GameObject dialoguePanel; // 대화창 패널
    public Button nextButton; // 다음 대화로 넘어가는 버튼
    private string dialogue = "좋지 않은 냄새가 난다... 설마...?"; // 대화 내용
    private int currentCharIndex = 0; // 현재 대화 문자 인덱스
    private GameObject player; // 플레이어 오브젝트
    private float dialogDistance = 2f; // 대화창이 나타나는 거리
    private bool dialogueOpen = false; // 대화창이 열려있는지 여부
    private bool isTyping = false; // 텍스트 타이핑 중인지 여부
    private Coroutine typingCoroutine; // 텍스트 타이핑 코루틴

    public AudioSource typingSound; // 타이핑 소리 재생을 위한 AudioSource 컴포넌트

    void Start()
    {
        dialoguePanel.SetActive(false); // 시작할 때 대화창 비활성화
        player = GameObject.FindGameObjectWithTag("Player"); // 플레이어 오브젝트 찾기
        nextButton.onClick.AddListener(OnNextButtonClick); // 버튼 클릭 리스너 추가
    }

    void Update()
    {
        if (!dialogueOpen)
        {
            // 플레이어와 오브젝트 사이의 거리 측정
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance <= dialogDistance)
            {
                // 일정 거리 내에 플레이어가 있고 대화창이 열려있지 않은 경우 대화창 활성화
                dialogueOpen = true;
                dialoguePanel.SetActive(true);
                // 대화 텍스트 표시
                typingCoroutine = StartCoroutine(TypeDialogue());

                // 대화 시작 시 플레이어의 움직임을 멈추기
                player.GetComponent<P_Move>().enabled = false;
            }
        }
    }

    public void OnNextButtonClick()
    {
        if (isTyping)
        {
            // 타이핑 중이면 모든 텍스트를 한 번에 표시
            StopCoroutine(typingCoroutine);
            dialogueText.text = dialogue;
            isTyping = false;
        }
        else
        {
            // 타이핑이 완료된 후 버튼 클릭 시 대화창 비활성화
            dialoguePanel.SetActive(false);
            //dialogueOpen = false;
            currentCharIndex = 0; // 문자 인덱스 초기화

            // 대화가 끝나면 플레이어의 움직임 다시 활성화
            player.GetComponent<P_Move>().enabled = true;
        }
    }

    IEnumerator TypeDialogue()
    {
        isTyping = true;
        dialogueText.text = ""; // 대화창 초기화
        while (currentCharIndex < dialogue.Length)
        {
            dialogueText.text += dialogue[currentCharIndex]; // 한 글자씩 추가
            currentCharIndex++; // 다음 문자 인덱스로 이동
            yield return new WaitForSeconds(0.1f); // 0.1초 대기

            typingSound.Play(); // 타이핑 소리 재생
        }
        isTyping = false;
    }
}