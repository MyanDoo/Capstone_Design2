using System.Collections;
using UnityEngine;
using TMPro;
using System.Threading;
using System.Text;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.UI;

public class Chatting : MonoBehaviour
{
    public static Chatting instance { get; private set; }

    private bool isOpen = false; //ä�� UI ����

    [Header("�ͳ�")]
    public GameObject tunnel_Player_Chatting_Img1; //�ͳ�: �Ƶ������� ����̴�.
    [Tooltip("�ͳ�: �Ƶ������� ����̴�.")] //���콺 �����ϸ� �� �ؽ�Ʈ ������ִ� ���.
    public TMP_Text tunnel_text1;
    public GameObject tunnel_Player_Chatting_Img2; //�ͳ�: �� �̻� ���ư� �� ���� �� ���� ������ ���.
    [Tooltip("�ͳ�: �� �̻� ���ư� �� ���� �� ���� ������ ���.")]
    public TMP_Text tunnel_text2;
    public GameObject ChattingCloseButton; //�ͳο��� ä�ôݴ¹�ư

    private Coroutine typingCoroutine; // �ؽ�Ʈ Ÿ���� �ڷ�ƾ
    private bool isTyping = false; // �ؽ�Ʈ Ÿ���� ������ ����

    public GameObject player; // �÷��̾� ������Ʈ
    private bool hasActivatedDialogue = false; // ��ȭâ�� �ѹ��� Ȱ��ȭ�ǵ��� �ϴ� �÷���

    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;

        //��ȭâ SetActive off �ϴ� �Լ� ȣ��
        ChattingOff();

        tunnel_text1.text = "�Ƶ������� ����̴�.";
        tunnel_text2.text = "�� �̻� ���ư� �� ���� �� ���� ������ ���.";
    }

    //P_Move���� ȣ���� �Լ�.
    //�ؽ�Ʈ ���� ���� �ڵ� �߰��ؾ� ��.
    public void TextSend_tunnel1()
    {
        if (!isTyping)
        {
            Debug.Log("TextSend_tunnel1() ȣ���");
            ChattingCloseButton.SetActive(true);
            tunnel_Player_Chatting_Img1.SetActive(true);
            typingCoroutine = StartCoroutine(PrintText(tunnel_text1.text, tunnel_text1)); //tunnel1
            hasActivatedDialogue = false;
        }
    }

    public void TextSend_tunnel2()
    {
        if (!isTyping)
        {
            Debug.Log("TextSend_tunnel2() ȣ���");
            ChattingCloseButton.SetActive(true);
            tunnel_Player_Chatting_Img2.SetActive(true);
            typingCoroutine = StartCoroutine(PrintText(tunnel_text2.text, tunnel_text2)); //tunnel2
            hasActivatedDialogue = false;
        }
    }

    //�ͳ�1
    IEnumerator PrintText(string content, TMP_Text textComponent)
    {
        Debug.Log("PrintText() ȣ���");
        isTyping = true;
        textComponent.text = string.Empty;

        if (!string.IsNullOrEmpty(content))
        {
            Debug.Log("if��");
            for (int i = 0; i < content.Length; i++)
            {
                //Debug.Log("for�� ����� " + i);

                textComponent.text += content[i];

                yield return new WaitForSeconds(0.08f);
            }
        }
        else Debug.Log("context is null");

        isTyping = false;
    }

    // ��� ��ȭâ off�ϴ� �Լ�
    public void ChattingOff()
    {
        isOpen = !isOpen; //t>f

        // ��ȭâ ������ŭ SetActive(false) �� ��
        // �ͳ�
        tunnel_Player_Chatting_Img1.SetActive(false);
        tunnel_Player_Chatting_Img2.SetActive(false);
        ChattingCloseButton.SetActive(false);
        // �ٸ�1
        // �����ȵ���1
        // �ٸ�2
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasActivatedDialogue)
        {
            ActivateDialogue();
        }
    }

    void ActivateDialogue()
    {
        hasActivatedDialogue = true; // ��ȭâ�� �� �� Ȱ��ȭ�Ǹ� �ٽ� Ȱ��ȭ���� �ʵ��� ����

        
    }

    // ��ư Ŭ�� �̺�Ʈ
    public void OnChattingCloseButtonClick()
    {
        if (isTyping)
        {
            // ��ȭ ���� �� �÷��̾��� �������� ���߱�
            player.GetComponent<P_Move>().enabled = false;

            // Ÿ���� ���̸� ��� �ؽ�Ʈ�� �� ���� ǥ��
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            if (tunnel_Player_Chatting_Img1.activeSelf)
            {
                Debug.Log("ä�� ��ũ��Ʈ - OnChattingCloseButtonClick() if�� ����");
                tunnel_text1.text = "�Ƶ������� ����̴�.";
            }
            else if (tunnel_Player_Chatting_Img2.activeSelf)
            {
                Debug.Log("ä�� ��ũ��Ʈ - OnChattingCloseButtonClick() else if�� ����");
                tunnel_text2.text = "�� �̻� ���ư� �� ���� �� ���� ������ ���.";
            }

            isTyping = false;
        }
        else
        {
            // Ÿ������ �Ϸ�� �� ��ư Ŭ�� �� ��ȭâ�� �ݽ��ϴ�.
            ChattingOff();
        }
    }

    public void Reset()
    {
        if (tunnel_text1 != null)
        {
            Debug.Log("Chatting-Reset() ȣ���");
        }
    }
}
