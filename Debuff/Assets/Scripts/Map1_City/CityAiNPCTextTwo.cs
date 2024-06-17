using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CityAiNPCTextTwo : MonoBehaviour
{
    public static CityAiNPCTextOne instance { get; private set; }

    public TextMeshProUGUI dialogueText; // ��ȭâ�� ����� �ؽ�Ʈ UI
    public Button nextButton; // ���� ��ȭ�� �Ѿ�� ��ư
    public GameObject dialoguePanel; // ��ȭâ �г�
    public GameObject Ainpc; // Ainpc
    public GameObject player; // �÷��̾� ������Ʈ
    public float activationDistance = 3.0f; // ��ȭâ Ȱ��ȭ �Ÿ�
    private List<string> dialogues = new List<string> // ��ȭ ���� ����Ʈ
    {
        "�ѷ����� �?",
        "... ...",
        "���� �׷���?",
        "������ �������� �츸��.",
        "�ʴ� �� ������ ���� �ϴ� ����?",
        "�̾������� �� �� �̻� ���� �� �� ����.",
        "���� �ϳ� �Ұ�.",
        "�ͳ��� �����ŵ�, ��� ���� ������ ���캸���� ��.",
        "�װ��� �ʸ� ������ �ž�."
    };
    private int currentDialogueIndex = 0; // ���� ��ȭ �ε���
    private bool isTypingEffect = false; // Ÿ���� ������ Ȯ��
    private bool hasActivatedDialogue = false; // ��ȭâ�� �ѹ��� Ȱ��ȭ�ǵ��� �ϴ� �÷���
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
        hasActivatedDialogue = true; // ��ȭâ�� �� �� Ȱ��ȭ�Ǹ� �ٽ� Ȱ��ȭ���� �ʵ��� ����

        // ��ȭ ���� �� �÷��̾��� �������� ���߱�
        player.GetComponent<P_Move>().enabled = false;
    }
    /*
    void OnMouseDown()
    {
        if (!dialoguePanel.activeSelf) // ��ȭâ�� �̹� ���� ���� ���� ��쿡�� ��ȭ ����
        {
            dialoguePanel.SetActive(true); // ������Ʈ Ŭ�� �� ��ȭâ Ȱ��ȭ
            StartCoroutine(TypeDialogue(dialogues[currentDialogueIndex])); // ��ȭ �ؽ�Ʈ Ÿ���� ����
        }
    }
    */
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
            Ainpc.SetActive(false); // ������Ʈ ��Ȱ��ȭ

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
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);

            typingSound.Play(); // Ÿ���� �Ҹ� ���
        }
        isTypingEffect = false;
    }
}