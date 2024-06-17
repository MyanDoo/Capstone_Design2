using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CityAiNPCTextTwo : MonoBehaviour
{
    public static CityAiNPCTextOne instance { get; private set; }

    public TextMeshProUGUI dialogueText; // 대화창에 사용할 텍스트 UI
    public Button nextButton; // 다음 대화로 넘어가는 버튼
    public GameObject dialoguePanel; // 대화창 패널
    public GameObject Ainpc; // Ainpc
    public GameObject player; // 플레이어 오브젝트
    public float activationDistance = 3.0f; // 대화창 활성화 거리
    private List<string> dialogues = new List<string> // 대화 내용 리스트
    {
        "둘러보니 어때?",
        "... ...",
        "하하 그렇지?",
        "하지만 생각보다 살만해.",
        "너는 더 앞으로 가야 하는 거지?",
        "미안하지만 난 더 이상 같이 갈 수 없어.",
        "조언 하나 할게.",
        "터널을 지나거든, 모든 것을 유심히 살펴보도록 해.",
        "그것이 너를 도와줄 거야."
    };
    private int currentDialogueIndex = 0; // 현재 대화 인덱스
    private bool isTypingEffect = false; // 타이핑 중인지 확인
    private bool hasActivatedDialogue = false; // 대화창이 한번만 활성화되도록 하는 플래그
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

    void Update()
    {
        if (!hasActivatedDialogue)
        {
            float distance = Vector3.Distance(player.transform.position, Ainpc.transform.position);
            if (distance <= activationDistance && !dialoguePanel.activeSelf)
            {
                ActivateDialogue();
            }
        }
    }

    void ActivateDialogue()
    {
        dialoguePanel.SetActive(true);
        StartCoroutine(TypeDialogue(dialogues[currentDialogueIndex]));
        hasActivatedDialogue = true; // 대화창이 한 번 활성화되면 다시 활성화되지 않도록 설정

        // 대화 시작 시 플레이어의 움직임을 멈추기
        player.GetComponent<P_Move>().enabled = false;
    }
    /*
    void OnMouseDown()
    {
        if (!dialoguePanel.activeSelf) // 대화창이 이미 열려 있지 않은 경우에만 대화 시작
        {
            dialoguePanel.SetActive(true); // 오브젝트 클릭 시 대화창 활성화
            StartCoroutine(TypeDialogue(dialogues[currentDialogueIndex])); // 대화 텍스트 타이핑 시작
        }
    }
    */
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
            Ainpc.SetActive(false); // 오브젝트 비활성화

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
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);

            typingSound.Play(); // 타이핑 소리 재생
        }
        isTypingEffect = false;
    }
}