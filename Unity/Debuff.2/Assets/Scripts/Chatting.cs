using System.Collections;
using UnityEngine;
using TMPro;
using System.Threading;

public class Chatting : MonoBehaviour
{
    public static Chatting instance {  get; private set; }

    private string text; //�� ����=�ؽ�Ʈ ������ ������ ���ڿ� �迭

    private string[] texts;

    //private TMP_Text[] textObjects;
    private float delay = 0.125f; //���ڿ� ��� ���� ���� �ʱ�ȭ
    private int currentTextIndex = 0; // ���� ��� ���� �ؽ�Ʈ�� �ε���

    //private GameObject[] parentObjects; //�ؽ�Ʈ�� �ֻ��� ������Ʈ �迭

    //public int count = 0;

    //������ �ٸ��� ä���̹����� �ֱ� ���� public���� ����
    //�ʿ��� ä�� ������ŭ �÷��� ����� ��
    //<���>
    //���_���_Chatting_Img����; //���: ���

    //�ͳ�
    [Header ("�ͳ�")]
    public GameObject tunnel_Player_Chatting_Img1; //�ͳ�: �Ƶ������� ����̴�.
    [Tooltip ("�ͳ�: �Ƶ������� ����̴�.")] //���콺 �����ϸ� �� �ؽ�Ʈ ������ִ� ���.
    public TMP_Text tunnel_text1;
    public TMP_Text[] tunnel_text1s;
    public GameObject tunnel_Player_Chatting_Img2; //�ͳ�: �� �̻� ���ư� �� ���� �� ���� ������ ���.
    [Tooltip("�ͳ�: �� �̻� ���ư� �� ���� �� ���� ������ ���.")]
    public TMP_Text tunnel_text2;
    public TMP_Text[] tunnel_text2s;


    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;

        //��ȭâ SetActive off �ϴ� �Լ� ȣ��
        ChattingOff();

        //�ؽ�Ʈ â���� ���� �ؽ�Ʈ �Է��ϱ�
        // +a: �ؽ�Ʈ �Ӽ����� Wrapping�� Disabled�� �ٲ��ָ�, �ڽ��� ũ�⸦ �Ѵ� �ؽ�Ʈ�� ���ٷ� ó��
    }


    //P_Move���� ȣ���� �Լ�.
    //�ؽ�Ʈ ���� ���� �ڵ� �߰��ؾ� ��.
    public void TextSend()
    {
        text = tunnel_text1.text.ToString();
        tunnel_text1.text = " ";
        text = tunnel_text2.text.ToString();
        tunnel_text2.text = " ";

        texts = new string[tunnel_text1s.Length];
        for(int i = 0; i < tunnel_text1s.Length; i++)
        {
            texts[i] = tunnel_text1s[i].text;
            tunnel_text1s[i].text = string.Empty;
        }
        currentTextIndex = 0;

        StartCoroutine(PrintText_tunnel1(delay));
        //StartCoroutine(PrintText_tunnel2(delay));
    }

    //�� ���ھ� ����ϰ� �ϴ� �Լ�
    //�ؽ�Ʈ ������ŭ �����ؾ� ��

    //�ͳ�1
    IEnumerator PrintText_tunnel1(float delay)
    {
        /*int count = 0;

        while (count != text.Length) //�ؽ�Ʈ ������ 0�� �ƴϸ�, ��� �ؽ�Ʈ ��ü�� ó���� ������
        {
            if (count < text.Length) //�ؽ�Ʈ ������ 0���� ũ��
            {
                tunnel_text1.text = text[count].ToString();
                count++;
            }
            yield return new WaitForSeconds(delay);
        }*/

        while (currentTextIndex < texts.Length)
        {
            string currentText = texts[currentTextIndex];
            TMP_Text currentTMP_Text = tunnel_text1s[currentTextIndex];
            for (int i = 0; i < currentText.Length; i++)
            {
                currentTMP_Text.text += currentText[i]; // ���� �ؽ�Ʈ ��ü�� �� ���ھ� �߰�
                yield return new WaitForSeconds(delay);
            }
            currentTextIndex++;
        }
    }
    //�ͳ�2
    IEnumerator PrintText_tunnel2(float delay)
    {
        int count = 0;

        while (count != text.Length)
        {
            if (count < text.Length)
            {
                tunnel_text2.text = text[count].ToString();
                count++;
            }
            yield return new WaitForSeconds(delay);
        }
    }

    //��ȭâ�� ���� �Լ�
    void OnChatting()
    {
        //��ȭâ ������ŭ SetActive(false) �� ��
        //��
        //����1
        //�ͳ�
        {
            tunnel_Player_Chatting_Img1.SetActive(false);
            tunnel_Player_Chatting_Img2.SetActive(false);
        }
        //�ٸ�1
        //�����ȵ���1
        //�ٸ�2
    }


    //��� ��ȭâ off�ϴ� �Լ�
    public void ChattingOff()
    {
        //��ȭâ ������ŭ SetActive(false) �� ��
        //��
        //����1
        //�ͳ�
        tunnel_Player_Chatting_Img1.SetActive(false);
        tunnel_Player_Chatting_Img2.SetActive(false);
        //�ٸ�1
        //�����ȵ���1
        //�ٸ�2
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
