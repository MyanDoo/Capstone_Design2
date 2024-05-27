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
    private P_Move playerMove; // P_Move 스크립트 참조를 위한 변수


    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;

        AIquestion.SetActive(false);
        AIanswer.SetActive(false); 
        //AIanswer.SetActive(false); //나중에 수정할 것임
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

    //AI 이미지에 달려있는 버튼 클릭시 발생할 이벤트 함수
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
            printAiText.text = chatResponse.Content; //openAI 대답을 text에 출력
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {
            AIanswer.SetActive(false); //클릭 이벤트 받으면 ai 답변 배경 끔
            AIquestion.SetActive(true);
            printAiText.text = ""; //다음 답변을 위해 공백으로 비워둠
        }

        //=================================================================
        //설명 주석
        //화면 클릭하면 대화창 닫게하는건 instance로 Chatting.cs에 구현해놓음.
            //도시1
            //AI.instance.AIanswer.SetActive(false);
            //AI.instance.AIquestion.SetActive(false);
        //=================================================================
    }
}
