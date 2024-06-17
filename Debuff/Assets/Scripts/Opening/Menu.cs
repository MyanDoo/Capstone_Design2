using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject menu; // �޴� ������ �� ���̵�â �г�
    public Button menuButton; // �޴��� ���� ��ư
    public Button closeButton; // �ݴ� ��ư

    // Start is called before the first frame update
    void Start()
    {
        // ������ �� menu ������Ʈ ��Ȱ��ȭ
        menu.SetActive(false);

        // ��ư Ŭ�� �̺�Ʈ ���
        menuButton.onClick.AddListener(MenuOpen);
        closeButton.onClick.AddListener(MenuClose);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // menu ������Ʈ�� Ȱ��ȭ�ϴ� �޼���
    public void MenuOpen()
    {
        menu.SetActive(true);
    }

    // menu ������Ʈ�� ��Ȱ��ȭ�ϴ� �޼���
    public void MenuClose()
    {
        Debug.Log("MenuClose ȣ���"); // ����� �α� �߰�
        menu.SetActive(false);
    }
}
