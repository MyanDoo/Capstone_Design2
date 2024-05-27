using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using TMPro;
using UnityEngine.UI;

public class AI : MonoBehaviour
{
    public static AI instance { get; private set; }

    private OpenAIApi openAI = new OpenAIApi();
    private List<ChatMessage> messages = new List<ChatMessage>();

    public GameObject AIquestion;
    public GameObject AIanswer;


    public TMP_Text printAiText;
    public TMP_InputField inputField;

    //private bool isAsking = false;
    [SerializeField] private int AskingCount = 7;

    public GameObject AiMenuButton;
    public GameObject AiNotice;
    public GameObject AiClose;

    //public GameObject Player;
    private P_Move playerMove; // P_Move ��ũ��Ʈ ������ ���� ����


    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;

        AIquestion.SetActive(false);
        AIanswer.SetActive(false); 
        //AIanswer.SetActive(false); //���߿� ������ ����
        AiNotice.SetActive(false);
        AiClose.SetActive(false);
    }

    public void AiMenuButtonClick()
    {
        if (AskingCount > 0)
        {
            AiNotice.SetActive(true);
        }
    }

    public void AiMenuButtonClose()
    {
        Debug.Log("AiMenuButtonClose()");
        AiNotice.SetActive(false);
        AIanswer.SetActive(false);
        AIquestion.SetActive(false);
        AiClose.SetActive(false);
    }

    //AI �̹����� �޷��ִ� ��ư Ŭ���� �߻��� �̺�Ʈ �Լ�
    public void OnAiImageYesClick()
    {
        AIquestion.SetActive(true);
        AiNotice.SetActive(false);
        AiClose.SetActive(true);
    }

    public void OnAiImageNoClick()
    {
        AiNotice.SetActive(false);
    }


    public async void AskChatGPT(string newText)
    {
        AIquestion.SetActive(false);
        AIanswer.SetActive(true);
        AiNotice.SetActive(false);
        AskingCount--;

        ChatMessage newMessage = new ChatMessage();
        newMessage.Content = newText;
        newMessage.Role = "user";

        messages.Add(newMessage);

        CreateChatCompletionRequest request = new CreateChatCompletionRequest();
        request.Messages = messages;
        request.Model = "gpt-3.5-turbo";

        var response = await openAI.CreateChatCompletion(request);

        if(response.Choices != null && response.Choices.Count > 0)
        {
            var chatResponse = response.Choices[0].Message;
            messages.Add(chatResponse);

            Debug.Log(chatResponse.Content);
            printAiText.text = chatResponse.Content; //openAI ����� text�� ���
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {
            AIanswer.SetActive(false); //Ŭ�� �̺�Ʈ ������ ai �亯 ��� ��
            AIquestion.SetActive(true);
            printAiText.text = ""; //���� �亯�� ���� �������� �����
        }

        //=================================================================
        //���� �ּ�
        //ȭ�� Ŭ���ϸ� ��ȭâ �ݰ��ϴ°� instance�� Chatting.cs�� �����س���.
            //����1
            //AI.instance.AIanswer.SetActive(false);
            //AI.instance.AIquestion.SetActive(false);
        //=================================================================
    }
}
