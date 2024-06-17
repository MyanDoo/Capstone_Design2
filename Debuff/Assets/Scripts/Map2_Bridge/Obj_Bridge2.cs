using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Obj_Bridge2 : MonoBehaviour
{
    public static Obj_Bridge2 instance { get; private set; }

    public TextMeshProUGUI dialogueText; // ��ȭâ�� ����� �ؽ�Ʈ UI
    public Button nextButton; // ���� ��ȭ�� �Ѿ�� ��ư
    public GameObject dialoguePanel; // ��ȭâ �г�
    public GameObject player; // �÷��̾� ������Ʈ
    private List<string> dialogues = new List<string> // ��ȭ ���� ����Ʈ
    {
        "���⵵ �Ȱ���.",
        "�ٸ� ���ε�� <color=red>��</color>�� �� <color=red>����</color> �ִµ�..."
    };
    private int currentDialogueIndex = 0; // ���� ��ȭ �ε���
    private bool isTypingEffect = false; // Ÿ���� ������ Ȯ��
    public float typingSpeed = 0.1f; // Ÿ���� �ӵ�

    public AudioSource typingSound; // Ÿ���� �Ҹ� ����� ���� AudioSource ������Ʈ

    void Start()
    {
        dialoguePanel.SetActive(false); // ������ �� ��ȭâ ��Ȱ��ȭ
        if (dialogues.Count > 0)
        {
            dialogueText.text = ""; // �ʱ⿡�� �ؽ�Ʈ�� ����Ӵϴ�.
        }
        nextButton.onClick.AddListener(ShowNextDialogue); // ��ư Ŭ�� ������ �߰�
    }

    public void OnMouseDown()
    {
        if (!dialoguePanel.activeSelf) // ��ȭâ�� �̹� ���� ���� ���� ��쿡�� ��ȭ ����
        {
            dialoguePanel.SetActive(true); // ������Ʈ Ŭ�� �� ��ȭâ Ȱ��ȭ
            StartCoroutine(TypeDialogue(dialogues[currentDialogueIndex])); // ��ȭ �ؽ�Ʈ Ÿ���� ����
        }
        // ��ȭ ���� �� �÷��̾��� �������� ���߱�
        player.GetComponent<P_Move>().enabled = false;
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

        // ��� ��縦 ���ߴٸ� ��ȭâ �ݱ�
        if (currentDialogueIndex >= dialogues.Count)
        {
            dialoguePanel.SetActive(false);
            currentDialogueIndex = 0; // ��ȭ ���� �� �ε��� �ʱ�ȭ

            // ��ȭ�� ������ �÷��̾��� ������ �ٽ� Ȱ��ȭ
            player.GetComponent<P_Move>().enabled = true;
            return;
        }

        // ���� ��� ǥ��
        dialogueText.text = dialogues[currentDialogueIndex];

        // ���� ��翡 ���� Ÿ���� ȿ�� ����
        StartCoroutine(TypeDialogue(dialogues[currentDialogueIndex]));
    }

    // ��ȭ �ؽ�Ʈ�� Ÿ�����ϴ� �ڷ�ƾ
    IEnumerator TypeDialogue(string sentence)
    {
        isTypingEffect = true;
        dialogueText.text = "";
        bool inTag = false; // �±� ���ο� �ִ��� ����
        string currentText = ""; // ���� ��µ� �ؽ�Ʈ
        string tagText = ""; // ���� �±� ���ڿ�
        int tagStartIndex = -1; // �±� ���� ��ġ

        for (int i = 0; i < sentence.Length; i++)
        {
            char c = sentence[i];

            if (c == '<')
            {
                inTag = true;
                tagText = ""; // ���ο� �±� ����
                tagStartIndex = i; // �±� ���� ��ġ ���
            }

            if (inTag)
            {
                tagText += c;
                if (c == '>')
                {
                    inTag = false;
                    currentText += tagText; // �±׸� ��� �ؽ�Ʈ�� �߰�
                }
            }
            else
            {
                currentText += c;
                dialogueText.text = currentText; // �±׸� �����Ͽ� �ؽ�Ʈ ����
                yield return new WaitForSeconds(typingSpeed); // �ؽ�Ʈ ��� �ӵ� ����

                typingSound.Play(); // Ÿ���� �Ҹ� ���
            }
        }

        isTypingEffect = false;
    }
}