using System.Collections;
using UnityEngine;
using TMPro;
using System.Threading;
using System.Text;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.UI;

public class Chatting : MonoBehaviour
{
    public static Chatting instance {  get; private set; }

    //private string text; //�� ����=�ؽ�Ʈ ������ ������ ���ڿ� �迭

    //private float delay = 0.125f; //���ڿ� ��� ���� ���� �ʱ�ȭ

    private bool isOpen = false; //ä�� UI ����

    //������ �ٸ��� ä���̹����� �ֱ� ���� public���� ����
    //�ʿ��� ä�� ������ŭ �÷��� ����� ��
    //<���>
    //���_���_Chatting_Img����; //���: ���

    //�ͳ�
    [Header ("�ͳ�")]
    public GameObject tunnel_Player_Chatting_Img1; //�ͳ�: �Ƶ������� ����̴�.
    [Tooltip ("�ͳ�: �Ƶ������� ����̴�.")] //���콺 �����ϸ� �� �ؽ�Ʈ ������ִ� ���.
    public TMP_Text tunnel_text1;
    public GameObject tunnel_Player_Chatting_Img2; //�ͳ�: �� �̻� ���ư� �� ���� �� ���� ������ ���.
    [Tooltip("�ͳ�: �� �̻� ���ư� �� ���� �� ���� ������ ���.")]
    public TMP_Text tunnel_text2;
    public GameObject ChattingCloseButton; //�ͳο��� ä�ôݴ¹�ư

    //����1


    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
        
        //��ȭâ SetActive off �ϴ� �Լ� ȣ��
        ChattingOff();

        tunnel_text1.text = "�Ƶ������� ����̴�.";
        tunnel_text2.text = "�� �̻� ���ư� �� ���� �� ���� ������ ���.";

        // +a: �ؽ�Ʈ �Ӽ����� Wrapping�� Disabled�� �ٲ��ָ�, �ڽ��� ũ�⸦ �Ѵ� �ؽ�Ʈ�� ���ٷ� ó��
    }

    //P_Move���� ȣ���� �Լ�.
    //�ؽ�Ʈ ���� ���� �ڵ� �߰��ؾ� ��.
    public void TextSend_tunnel1()
    {
        //isOpen = !isOpen; //f>t
        //tunnel_Player_Chatting_Img1.SetActive(isOpen);
        if (true)
        {
            Debug.Log("TextSend_tunnel1() ȣ���");
            ChattingCloseButton.SetActive(true);
            StartCoroutine(PrintText(tunnel_text1.text, tunnel_text1)); //tunnel1
        }
    }

    public void TextSend_tunnel2()
    {
        if (true)
        {
            Debug.Log("TextSend_tunnel2() ȣ���");
            ChattingCloseButton.SetActive(true);
            StartCoroutine(PrintText(tunnel_text2.text, tunnel_text2)); //tunnel2
        }
        
    }

    //�ͳ�1
    IEnumerator PrintText(string content, TMP_Text textComponent)
    {
        Debug.Log("PrintText() ȣ���");
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
    }

    //��� ��ȭâ off�ϴ� �Լ�
    public void ChattingOff()
    {
        isOpen = !isOpen; //t>f

        //��ȭâ ������ŭ SetActive(false) �� ��
        //��
        //����1
        //AI.instance.AIanswer.SetActive(false);
        //AI.instance.AIquestion.SetActive(false);
        //�ͳ�
        tunnel_Player_Chatting_Img1.SetActive(false);
        tunnel_Player_Chatting_Img2.SetActive(false);
        ChattingCloseButton.SetActive(false);
        //�ٸ�1
        //�����ȵ���1
        //�ٸ�2
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Reset()
    {
        //tunnel_text1.text = "�������َ��ٷ��줸��";

        if (tunnel_text1 != null)
        {
            Debug.Log("Chatting-Reset() ȣ���");
        }
    }


}
