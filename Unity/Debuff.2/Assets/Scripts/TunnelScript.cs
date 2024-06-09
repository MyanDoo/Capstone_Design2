using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TunnelScript : MonoBehaviour
{
    public static TunnelScript instance { get; private set; }

    [Header("Dialogue UI Components")]
    public TextMeshProUGUI dialogueText1; // ù ��° ��ȭâ�� ����� �ؽ�Ʈ UI
    public TextMeshProUGUI dialogueText2; // �� ��° ��ȭâ�� ����� �ؽ�Ʈ UI
    public Button nextButton; // ��ȭâ�� ���� ��ȭ�� �Ѿ�� ��ư
    public Button nextButton2; // ��ȭâ�� ���� ��ȭ�� �Ѿ�� ��ư
    public GameObject dialoguePanel1; // ù ��° ��ȭâ �г�
    public GameObject dialoguePanel2; // �� ��° ��ȭâ �г�

    [Header("Tunnel Specific Components")]
    public GameObject wall1; // wall1
    public GameObject wall2; // wall2
    public GameObject player; // �÷��̾� ������Ʈ
    public float activationDistance = 1.0f; // ��ȭâ Ȱ��ȭ �Ÿ�

    private List<string> dialogues = new List<string> // ��ȭ ���� ����Ʈ
    {
        "�Ƶ������� ����̴�.",
        "�� �̻� ���ư� �� ���� �� ���� ������ ���."
    };
    private int currentDialogueIndex = 0; // ���� ��ȭ �ε���
    private bool isTypingEffect = false; // Ÿ���� ������ Ȯ��
    private bool hasActivatedFirstDialogue = false; // ù ��° ��ȭâ�� �ѹ��� Ȱ��ȭ�ǵ��� �ϴ� �÷���
    private bool hasActivatedSecondDialogue = false; // �� ��° ��ȭâ�� �ѹ��� Ȱ��ȭ�ǵ��� �ϴ� �÷���
    public float typingSpeed = 0.1f; // Ÿ���� �ӵ�

    private Coroutine typingCoroutine; // �ؽ�Ʈ Ÿ���� �ڷ�ƾ

    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        dialoguePanel1.SetActive(false); // ������ �� ù ��° ��ȭâ ��Ȱ��ȭ
        dialoguePanel2.SetActive(false); // ������ �� �� ��° ��ȭâ ��Ȱ��ȭ
        nextButton.gameObject.SetActive(false); // ������ �� ��ư ��Ȱ��ȭ
        nextButton2.gameObject.SetActive(false);

        if (dialogues.Count > 0)
        {
            dialogueText1.text = ""; // �ʱ⿡�� ù ��° �ؽ�Ʈ�� ����Ӵϴ�.
            dialogueText2.text = ""; // �ʱ⿡�� �� ��° �ؽ�Ʈ�� ����Ӵϴ�.
        }

        nextButton.onClick.AddListener(OnNextButtonClick); // ��ư Ŭ�� ������ �߰�
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
                ActivateDialogue(0, dialoguePanel1, dialogueText1); // ù ��° ��ȭ Ȱ��ȭ
                hasActivatedFirstDialogue = true;
            }
        }
        else if (!hasActivatedSecondDialogue)
        {
            float distanceToWall2 = Vector3.Distance(player.transform.position, wall2.transform.position);
            if (distanceToWall2 <= activationDistance && !dialoguePanel2.activeSelf)
            {
                ActivateDialogue(1, dialoguePanel2, dialogueText2); // �� ��° ��ȭ Ȱ��ȭ
                hasActivatedSecondDialogue = true;
            }
        }
    }

    void ActivateDialogue(int dialogueIndex, GameObject dialoguePanel, TextMeshProUGUI dialogueText)
    {
        dialoguePanel.SetActive(true);
        nextButton.gameObject.SetActive(true); // ��ư Ȱ��ȭ
        nextButton2.gameObject.SetActive(true);
        typingCoroutine = StartCoroutine(TypeDialogue(dialogues[dialogueIndex], dialogueText));

        // ��ȭ ���� �� �÷��̾��� �������� ���߱�
        player.GetComponent<P_Move>().enabled = false;
    }

    void OnNextButtonClick()
    {
        if (isTypingEffect)
        {
            // Ÿ���� ���̶�� ��� �ؽ�Ʈ�� �� ���� ǥ��
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
            wall1.SetActive(false); // ù ��° ������Ʈ ��Ȱ��ȭ
            currentDialogueIndex++;
            nextButton.gameObject.SetActive(false); // ù ��° ��ȭ ���� �� ��ư ��Ȱ��ȭ
            nextButton2.gameObject.SetActive(false);

            // ù ��° ��ȭ�� ������ �÷��̾��� ������ �ٽ� Ȱ��ȭ
            player.GetComponent<P_Move>().enabled = true;
        }
        else if (currentDialogueIndex == 1)
        {
            dialoguePanel2.SetActive(false);
            wall2.SetActive(false); // �� ��° ������Ʈ ��Ȱ��ȭ
            currentDialogueIndex = 0;
            nextButton.gameObject.SetActive(false); // �� ��° ��ȭ ���� �� ��ư ��Ȱ��ȭ
            nextButton2.gameObject.SetActive(false);

            // �� ��° ��ȭ�� ������ �÷��̾��� ������ �ٽ� Ȱ��ȭ
            player.GetComponent<P_Move>().enabled = true;
        }
    }

    // ��ȭ �ؽ�Ʈ�� Ÿ�����ϴ� �ڷ�ƾ
    IEnumerator TypeDialogue(string sentence, TextMeshProUGUI dialogueText)
    {
        isTypingEffect = true;
        dialogueText.text = "";
        bool inTag = false; // �±� ���ο� �ִ��� ����
        string currentText = ""; // ���� ��µ� �ؽ�Ʈ
        string tagText = ""; // ���� �±� ���ڿ�

        for (int i = 0; i < sentence.Length; i++)
        {
            char c = sentence[i];

            if (c == '<')
            {
                inTag = true;
                tagText = ""; // ���ο� �±� ����
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
            }
        }

        isTypingEffect = false;
    }
}
