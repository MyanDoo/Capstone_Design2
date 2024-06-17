using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Call : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // ��ȭâ�� ����� �ؽ�Ʈ UI
    public Button nextButton; // ���� ��ȭ�� �Ѿ�� ��ư
    public GameObject dialoguePanel; // ��ȭâ �г�
    public GameObject TelephoneButtons; // ��ȭ�� ��ư���� �����ϴ� ���� ������Ʈ
    public TMP_Text displayText; // ��ȭ��ȣ ���÷��̸� ��Ÿ���� TMP �ؽ�Ʈ
    [NonSerialized] public int num1, num2, num3, num4, num5, num6, num7, num8, num9, num0;
    private AudioSource audioSource; // ��ȭ ȿ������ ����� ����� �ҽ�

    public AudioSource typingSound; // Ÿ���� �Ҹ� ����� ���� AudioSource ������Ʈ

    private List<string> dialogues = new List<string>
    {
        "��ȭ�Ⱑ �ִ�.",
        "�Խ��ǿ� �ִ� ���ڴ�.",
        "��ȭ��ȣ���� ����̴�.",
        "�߰� ��ȣ�� ������ �־�.",
        "... ...",
        "�и� ��� �� �����ܺ���� �߾���, ��Ʈ�� �־��� �ž�.",
        "����. ��ȭ�� ����."
    };

    private int currentDialogueIndex = 0;
    private bool isTypingEffect = false;
    public float typingSpeed = 0.1f;

    private string currentNum = ""; // ���� �Էµ� ��ȣ�� ����
    private string number = "15449030";
    private string totalNum = "";

    void Start()
    {
        dialoguePanel.SetActive(false); // ������ �� ��ȭâ ��Ȱ��ȭ
        TelephoneButtons.SetActive(false); // ������ �� ��ȭ�� ��ư�� ��Ȱ��ȭ
        audioSource = GetComponent<AudioSource>(); // ����� �ҽ� ��������
        nextButton.onClick.AddListener(ShowNextDialogue); // ���� ��ȭ ��ư�� ������ �߰�
    }

    public void OnMouseDown()
    {
        if (!dialoguePanel.activeSelf) // ��ȭâ�� �̹� ���� ���� ���� ��쿡�� ��ȭ ����
        {
            dialoguePanel.SetActive(true); // ��ȭâ Ȱ��ȭ
            StartCoroutine(TypeDialogue(dialogues[currentDialogueIndex])); // ��ȭ �ؽ�Ʈ Ÿ���� ����
        }
    }

    void ShowNextDialogue()
    {
        // ���� Ÿ���� ���̶�� ��� �ؽ�Ʈ�� �� ���� ǥ��
        if (isTypingEffect)
        {
            StopAllCoroutines();
            dialogueText.text = dialogues[currentDialogueIndex];
            isTypingEffect = false;
            return;
        }

        currentDialogueIndex++;

        // ��� ��縦 ���ߴٸ� ��ȭâ �ݰ� TelephoneButtons�� Ȱ��ȭ�մϴ�.
        if (currentDialogueIndex >= dialogues.Count)
        {
            dialoguePanel.SetActive(false); // ��ȭâ�� �ݽ��ϴ�.
            currentDialogueIndex = 0; // ��ȭ�� ����Ǹ� ��ȭ �ε����� �ʱ�ȭ�մϴ�.
            StartCoroutine(ActivateTelephoneButtons()); // TelephoneButtons�� Ȱ��ȭ�ϴ� �ڷ�ƾ�� �����մϴ�.
            return;
        }

        dialogueText.text = dialogues[currentDialogueIndex]; // ���� ��ȭ ������ ǥ���մϴ�.
        StartCoroutine(TypeDialogue(dialogues[currentDialogueIndex])); // ���� ��ȭ ���뿡 ���� Ÿ���� ȿ���� �����մϴ�.
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

                typingSound.Play(); // Ÿ���� �Ҹ� ���
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
            audioSource.Play(); // ��ȭ��ȣ�� ���� �� �Ҹ��� ����մϴ�.
            SceneLoader.instance.Wait(); // SceneLoader�� Wait �Լ� ȣ��
            // SceneManager.LoadScene("Ending"); // �ڸ�Ʈ �ƿ��� �κ�
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

    // TelephoneButtons�� Ȱ��ȭ�ϴ� �ڷ�ƾ�Դϴ�.
    IEnumerator ActivateTelephoneButtons()
    {
        yield return new WaitForEndOfFrame(); // �� �������� ��ٸ� �� TelephoneButtons�� Ȱ��ȭ�մϴ�.
        TelephoneButtons.SetActive(true);
    }
}
