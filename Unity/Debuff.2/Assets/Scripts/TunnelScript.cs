using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TunnelScript : MonoBehaviour
{
    public static TunnelScript instance { get; private set; }

    [Header("Dialogue UI Components")]
    public TextMeshProUGUI dialogueText1; // 첫 번째 대화창에 사용할 텍스트 UI
    public TextMeshProUGUI dialogueText2; // 두 번째 대화창에 사용할 텍스트 UI
    public Button nextButton; // 대화창의 다음 대화로 넘어가는 버튼
    public Button nextButton2; // 대화창의 다음 대화로 넘어가는 버튼
    public GameObject dialoguePanel1; // 첫 번째 대화창 패널
    public GameObject dialoguePanel2; // 두 번째 대화창 패널

    [Header("Tunnel Specific Components")]
    public GameObject wall1; // wall1
    public GameObject wall2; // wall2
    public GameObject player; // 플레이어 오브젝트
    public float activationDistance = 1.0f; // 대화창 활성화 거리

    private List<string> dialogues = new List<string> // 대화 내용 리스트
    {
        "아득해지는 기분이다.",
        "더 이상 돌아갈 수 없을 것 같은 느낌이 든다."
    };
    private int currentDialogueIndex = 0; // 현재 대화 인덱스
    private bool isTypingEffect = false; // 타이핑 중인지 확인
    private bool hasActivatedFirstDialogue = false; // 첫 번째 대화창이 한번만 활성화되도록 하는 플래그
    private bool hasActivatedSecondDialogue = false; // 두 번째 대화창이 한번만 활성화되도록 하는 플래그
    public float typingSpeed = 0.1f; // 타이핑 속도

    private Coroutine typingCoroutine; // 텍스트 타이핑 코루틴

    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        dialoguePanel1.SetActive(false); // 시작할 때 첫 번째 대화창 비활성화
        dialoguePanel2.SetActive(false); // 시작할 때 두 번째 대화창 비활성화
        nextButton.gameObject.SetActive(false); // 시작할 때 버튼 비활성화
        nextButton2.gameObject.SetActive(false);

        if (dialogues.Count > 0)
        {
            dialogueText1.text = ""; // 초기에는 첫 번째 텍스트를 비워둡니다.
            dialogueText2.text = ""; // 초기에는 두 번째 텍스트를 비워둡니다.
        }

        nextButton.onClick.AddListener(OnNextButtonClick); // 버튼 클릭 리스너 추가
        nextButton2.onClick.AddListener (OnNextButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasActivatedFirstDialogue)
        {
            float distanceToWall1 = Vector3.Distance(player.transform.position, wall1.transform.position);
            if (distanceToWall1 <= activationDistance && !dialoguePanel1.activeSelf)
            {
                ActivateDialogue(0, dialoguePanel1, dialogueText1); // 첫 번째 대화 활성화
                hasActivatedFirstDialogue = true;
            }
        }
        else if (!hasActivatedSecondDialogue)
        {
            float distanceToWall2 = Vector3.Distance(player.transform.position, wall2.transform.position);
            if (distanceToWall2 <= activationDistance && !dialoguePanel2.activeSelf)
            {
                ActivateDialogue(1, dialoguePanel2, dialogueText2); // 두 번째 대화 활성화
                hasActivatedSecondDialogue = true;
            }
        }
    }

    void ActivateDialogue(int dialogueIndex, GameObject dialoguePanel, TextMeshProUGUI dialogueText)
    {
        dialoguePanel.SetActive(true);
        nextButton.gameObject.SetActive(true); // 버튼 활성화
        nextButton2.gameObject.SetActive(true);
        typingCoroutine = StartCoroutine(TypeDialogue(dialogues[dialogueIndex], dialogueText));

        // 대화 시작 시 플레이어의 움직임을 멈추기
        player.GetComponent<P_Move>().enabled = false;
    }

    void OnNextButtonClick()
    {
        if (isTypingEffect)
        {
            // 타이핑 중이라면 모든 텍스트를 한 번에 표시
            StopCoroutine(typingCoroutine);
            if (currentDialogueIndex == 0)
            {
                dialogueText1.text = dialogues[currentDialogueIndex];
            }
            else if (currentDialogueIndex == 1)
            {
                dialogueText2.text = dialogues[currentDialogueIndex];
            }
            isTypingEffect = false;
        }
        else
        {
            ShowNextDialogue();
        }
    }

    void ShowNextDialogue()
    {
        if (currentDialogueIndex == 0)
        {
            dialoguePanel1.SetActive(false);
            wall1.SetActive(false); // 첫 번째 오브젝트 비활성화
            currentDialogueIndex++;
            nextButton.gameObject.SetActive(false); // 첫 번째 대화 종료 후 버튼 비활성화
            nextButton2.gameObject.SetActive(false);

            // 첫 번째 대화가 끝나면 플레이어의 움직임 다시 활성화
            player.GetComponent<P_Move>().enabled = true;
        }
        else if (currentDialogueIndex == 1)
        {
            dialoguePanel2.SetActive(false);
            wall2.SetActive(false); // 두 번째 오브젝트 비활성화
            currentDialogueIndex = 0;
            nextButton.gameObject.SetActive(false); // 두 번째 대화 종료 후 버튼 비활성화
            nextButton2.gameObject.SetActive(false);

            // 두 번째 대화가 끝나면 플레이어의 움직임 다시 활성화
            player.GetComponent<P_Move>().enabled = true;
        }
    }

    // 대화 텍스트를 타이핑하는 코루틴
    IEnumerator TypeDialogue(string sentence, TextMeshProUGUI dialogueText)
    {
        isTypingEffect = true;
        dialogueText.text = "";
        bool inTag = false; // 태그 내부에 있는지 여부
        string currentText = ""; // 현재 출력된 텍스트
        string tagText = ""; // 현재 태그 문자열

        for (int i = 0; i < sentence.Length; i++)
        {
            char c = sentence[i];

            if (c == '<')
            {
                inTag = true;
                tagText = ""; // 새로운 태그 시작
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
            }
        }

        isTypingEffect = false;
    }
}
