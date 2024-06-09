using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject menu; // 메뉴 누르면 뜰 가이드창 패널
    public Button menuButton; // 메뉴를 여는 버튼
    public Button closeButton; // 닫는 버튼

    // Start is called before the first frame update
    void Start()
    {
        // 시작할 때 menu 오브젝트 비활성화
        menu.SetActive(false);

        // 버튼 클릭 이벤트 등록
        menuButton.onClick.AddListener(MenuOpen);
        closeButton.onClick.AddListener(MenuClose);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // menu 오브젝트를 활성화하는 메서드
    public void MenuOpen()
    {
        menu.SetActive(true);
    }

    // menu 오브젝트를 비활성화하는 메서드
    public void MenuClose()
    {
        Debug.Log("MenuClose 호출됨"); // 디버깅 로그 추가
        menu.SetActive(false);
    }
}
