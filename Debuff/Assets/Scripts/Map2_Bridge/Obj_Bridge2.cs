using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Obj_Bridge2 : MonoBehaviour
{
    public static Obj_Bridge2 instance { get; private set; }

    public TextMeshProUGUI dialogueText; // 대화창에 사용할 텍스트 UI
    public Button nextButton; // 다음 대화로 넘어가는 버튼
    public GameObject dialoguePanel; // 대화창 패널
    public GameObject player; // 플레이어 오브젝트
    private List<string> dialogues = new List<string> // 대화 내용 리스트
    {
        "여기도 똑같다.",
        "다른 가로등에는 <color=red>불</color>이 다 <color=red>들어와</color> 있는데..."
    };
    private int currentDialogueIndex = 0; // 현재 대화 인덱스
    private bool isTypingEffect = false; // 타이핑 중인지 확인
    public float typingSpeed = 0.1f; // 타이핑 속도

    public AudioSource typingSound; // 타이핑 소리 재생을 위한 AudioSource 컴포넌트

    void Start()
    {
        dialoguePanel.SetActive(false); // 시작할 때 대화창 비활성화
        if (dialogues.Count > 0)
        {
            dialogueText.text = ""; // 초기에는 텍스트를 비워둡니다.
        }
        nextButton.onClick.AddListener(ShowNextDialogue); // 버튼 클릭 리스너 추가
    }

    public void OnMouseDown()
    {
        if (!dialoguePanel.activeSelf) // 대화창이 이미 열려 있지 않은 경우에만 대화 시작
        {
            dialoguePanel.SetActive(true); // 오브젝트 클릭 시 대화창 활성화
            StartCoroutine(TypeDialogue(dialogues[currentDialogueIndex])); // 대화 텍스트 타이핑 시작
        }
        // 대화 시작 시 플레이어의 움직임을 멈추기
        player.GetComponent<P_Move>().enabled = false;
    }

    void ShowNextDialogue()
    {
        // 만약 타이핑 중이라면 모든 텍스트를 한 번에 표시
        if (isTypingEffect)
        {
            StopAllCoroutines();
            dialogueText.text = dialogues[currentDialogueIndex];
            isTypingEffect = false;
            return;
        }

        currentDialogueIndex++;

        // 모든 대사를 말했다면 대화창 닫기
        if (currentDialogueIndex >= dialogues.Count)
        {
            dialoguePanel.SetActive(false);
            currentDialogueIndex = 0; // 대화 종료 후 인덱스 초기화

            // 대화가 끝나면 플레이어의 움직임 다시 활성화
            player.GetComponent<P_Move>().enabled = true;
            return;
        }

        // 다음 대사 표시
        dialogueText.text = dialogues[currentDialogueIndex];

        // 다음 대사에 대한 타이핑 효과 시작
        StartCoroutine(TypeDialogue(dialogues[currentDialogueIndex]));
    }

    // 대화 텍스트를 타이핑하는 코루틴
    IEnumerator TypeDialogue(string sentence)
    {
        isTypingEffect = true;
        dialogueText.text = "";
        bool inTag = false; // 태그 내부에 있는지 여부
        string currentText = ""; // 현재 출력된 텍스트
        string tagText = ""; // 현재 태그 문자열
        int tagStartIndex = -1; // 태그 시작 위치

        for (int i = 0; i < sentence.Length; i++)
        {
            char c = sentence[i];

            if (c == '<')
            {
                inTag = true;
                tagText = ""; // 새로운 태그 시작
                tagStartIndex = i; // 태그 시작 위치 기록
            }

            if (inTag)
            {
                tagText += c;
                if (c == '>')
                {
                    inTag = false;
                    currentText += tagText; // 태그를 출력 텍스트에 추가
                }
            }
            else
            {
                currentText += c;
                dialogueText.text = currentText; // 태그를 포함하여 텍스트 설정
                yield return new WaitForSeconds(typingSpeed); // 텍스트 출력 속도 조절

                typingSound.Play(); // 타이핑 소리 재생
            }
        }

        isTypingEffect = false;
    }
}