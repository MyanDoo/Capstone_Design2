using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGuide : MonoBehaviour
{
    public static StartGuide instance { get; private set; }

    public GameObject guidePanel; // 가이드창 패널
    public Button closeButton; // X 버튼
    public GameObject player; // 플레이어 오브젝트

    // Start is called before the first frame update
    void Start()
    {
        guidePanel.SetActive(false); // 시작할 때 가이드창 비활성화
        closeButton.onClick.AddListener(CloseGuidePanel); // X 버튼 클릭 리스너 추가
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        guidePanel.SetActive(true); // 오브젝트 클릭 시 가이드창 활성화
        // 대화 시작 시 플레이어의 움직임을 멈추기
        player.GetComponent<P_Move>().enabled = false;
    }

    void CloseGuidePanel()
    {
        guidePanel.SetActive(false); // X 버튼 클릭 시 가이드창 비활성화
        // 대화가 끝나면 플레이어의 움직임 다시 활성화
        player.GetComponent<P_Move>().enabled = true;
    }
}
