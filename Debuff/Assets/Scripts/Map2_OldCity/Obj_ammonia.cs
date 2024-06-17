using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Obj_ammonia : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // ��ȭâ�� ����� �ؽ�Ʈ UI
    public GameObject dialoguePanel; // ��ȭâ �г�
    public Button nextButton; // ���� ��ȭ�� �Ѿ�� ��ư
    private string dialogue = "���� ���� ������ ����... ����...?"; // ��ȭ ����
    private int currentCharIndex = 0; // ���� ��ȭ ���� �ε���
    private GameObject player; // �÷��̾� ������Ʈ
    private float dialogDistance = 2f; // ��ȭâ�� ��Ÿ���� �Ÿ�
    private bool dialogueOpen = false; // ��ȭâ�� �����ִ��� ����
    private bool isTyping = false; // �ؽ�Ʈ Ÿ���� ������ ����
    private Coroutine typingCoroutine; // �ؽ�Ʈ Ÿ���� �ڷ�ƾ

    public AudioSource typingSound; // Ÿ���� �Ҹ� ����� ���� AudioSource ������Ʈ

    void Start()
    {
        dialoguePanel.SetActive(false); // ������ �� ��ȭâ ��Ȱ��ȭ
        player = GameObject.FindGameObjectWithTag("Player"); // �÷��̾� ������Ʈ ã��
        nextButton.onClick.AddListener(OnNextButtonClick); // ��ư Ŭ�� ������ �߰�
    }

    void Update()
    {
        if (!dialogueOpen)
        {
            // �÷��̾�� ������Ʈ ������ �Ÿ� ����
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance <= dialogDistance)
            {
                // ���� �Ÿ� ���� �÷��̾ �ְ� ��ȭâ�� �������� ���� ��� ��ȭâ Ȱ��ȭ
                dialogueOpen = true;
                dialoguePanel.SetActive(true);
                // ��ȭ �ؽ�Ʈ ǥ��
                typingCoroutine = StartCoroutine(TypeDialogue());

                // ��ȭ ���� �� �÷��̾��� �������� ���߱�
                player.GetComponent<P_Move>().enabled = false;
            }
        }
    }

    public void OnNextButtonClick()
    {
        if (isTyping)
        {
            // Ÿ���� ���̸� ��� �ؽ�Ʈ�� �� ���� ǥ��
            StopCoroutine(typingCoroutine);
            dialogueText.text = dialogue;
            isTyping = false;
        }
        else
        {
            // Ÿ������ �Ϸ�� �� ��ư Ŭ�� �� ��ȭâ ��Ȱ��ȭ
            dialoguePanel.SetActive(false);
            //dialogueOpen = false;
            currentCharIndex = 0; // ���� �ε��� �ʱ�ȭ

            // ��ȭ�� ������ �÷��̾��� ������ �ٽ� Ȱ��ȭ
            player.GetComponent<P_Move>().enabled = true;
        }
    }

    IEnumerator TypeDialogue()
    {
        isTyping = true;
        dialogueText.text = ""; // ��ȭâ �ʱ�ȭ
        while (currentCharIndex < dialogue.Length)
        {
            dialogueText.text += dialogue[currentCharIndex]; // �� ���ھ� �߰�
            currentCharIndex++; // ���� ���� �ε����� �̵�
            yield return new WaitForSeconds(0.1f); // 0.1�� ���

            typingSound.Play(); // Ÿ���� �Ҹ� ���
        }
        isTyping = false;
    }
}