using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Call : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // 대화창에 사용할 텍스트 UI
    public Button nextButton; // 다음 대화로 넘어가는 버튼
    public GameObject dialoguePanel; // 대화창 패널
    public GameObject TelephoneButtons; // 전화기 버튼들을 포함하는 게임 오브젝트
    public TMP_Text displayText; // 전화번호 디스플레이를 나타내는 TMP 텍스트
    [NonSerialized] public int num1, num2, num3, num4, num5, num6, num7, num8, num9, num0;
    private AudioSource audioSource; // 전화 효과음을 재생할 오디오 소스

    public AudioSource typingSound; // 타이핑 소리 재생을 위한 AudioSource 컴포넌트

    private List<string> dialogues = new List<string>
    {
        "전화기가 있다.",
        "게시판에 있던 숫자다.",
        "전화번호였던 모양이다.",
        "중간 번호가 지워져 있어.",
        "... ...",
        "분명 모든 걸 눈여겨보라고 했었지, 힌트가 있었을 거야.",
        "좋아. 전화해 보자."
    };

    private int currentDialogueIndex = 0;
    private bool isTypingEffect = false;
    public float typingSpeed = 0.1f;

    private string currentNum = ""; // 현재 입력된 번호를 저장
    private string number = "15449030";
    private string totalNum = "";

    void Start()
    {
        dialoguePanel.SetActive(false); // 시작할 때 대화창 비활성화
        TelephoneButtons.SetActive(false); // 시작할 때 전화기 버튼들 비활성화
        audioSource = GetComponent<AudioSource>(); // 오디오 소스 가져오기
        nextButton.onClick.AddListener(ShowNextDialogue); // 다음 대화 버튼에 리스너 추가
    }

    public void OnMouseDown()
    {
        if (!dialoguePanel.activeSelf) // 대화창이 이미 열려 있지 않은 경우에만 대화 시작
        {
            dialoguePanel.SetActive(true); // 대화창 활성화
            StartCoroutine(TypeDialogue(dialogues[currentDialogueIndex])); // 대화 텍스트 타이핑 시작
        }
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

        // 모든 대사를 말했다면 대화창 닫고 TelephoneButtons를 활성화합니다.
        if (currentDialogueIndex >= dialogues.Count)
        {
            dialoguePanel.SetActive(false); // 대화창을 닫습니다.
            currentDialogueIndex = 0; // 대화가 종료되면 대화 인덱스를 초기화합니다.
            StartCoroutine(ActivateTelephoneButtons()); // TelephoneButtons를 활성화하는 코루틴을 시작합니다.
            return;
        }

        dialogueText.text = dialogues[currentDialogueIndex]; // 다음 대화 내용을 표시합니다.
        StartCoroutine(TypeDialogue(dialogues[currentDialogueIndex])); // 다음 대화 내용에 대한 타이핑 효과를 시작합니다.
    }

    IEnumerator TypeDialogue(string sentence)
    {
        isTypingEffect = true;
        dialogueText.text = "";
        bool inTag = false;
        string currentText = "";
        string tagText = "";
        int tagStartIndex = -1;

        for (int i = 0; i < sentence.Length; i++)
        {
            char c = sentence[i];

            if (c == '<')
            {
                inTag = true;
                tagText = "";
                tagStartIndex = i;
            }

            if (inTag)
            {
                tagText += c;
                if (c == '>')
                {
                    inTag = false;
                    currentText += tagText;
                }
            }
            else
            {
                currentText += c;
                dialogueText.text = currentText;
                yield return new WaitForSeconds(typingSpeed);

                typingSound.Play(); // 타이핑 소리 재생
            }
        }

        isTypingEffect = false;
    }

    public void OnNumberButtonClick(GameObject button)
    {
        currentNum += button.name;
        displayText.text = currentNum;
    }

    public void btnReset()
    {
        currentNum = "";
        displayText.text = "";
    }

    public void btnCall()
    {
        totalNum = displayText.text;

        if (totalNum == number)
        {
            audioSource.Play(); // 전화번호가 맞을 때 소리를 재생합니다.
            SceneLoader.instance.Wait(); // SceneLoader의 Wait 함수 호출
            // SceneManager.LoadScene("Ending"); // 코멘트 아웃된 부분
        }
        else if (totalNum != number)
        {
            btnReset();
            SceneManager.LoadScene("Bridge1");
        }
    }

    public void EndTelephoneCall()
    {
        TelephoneButtons.SetActive(true);
    }

    // TelephoneButtons를 활성화하는 코루틴입니다.
    IEnumerator ActivateTelephoneButtons()
    {
        yield return new WaitForEndOfFrame(); // 한 프레임을 기다린 후 TelephoneButtons를 활성화합니다.
        TelephoneButtons.SetActive(true);
    }
}
