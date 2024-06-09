using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGuide : MonoBehaviour
{
    public static StartGuide instance { get; private set; }

    public GameObject guidePanel; // ���̵�â �г�
    public Button closeButton; // X ��ư
    public GameObject player; // �÷��̾� ������Ʈ

    // Start is called before the first frame update
    void Start()
    {
        guidePanel.SetActive(false); // ������ �� ���̵�â ��Ȱ��ȭ
        closeButton.onClick.AddListener(CloseGuidePanel); // X ��ư Ŭ�� ������ �߰�
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        guidePanel.SetActive(true); // ������Ʈ Ŭ�� �� ���̵�â Ȱ��ȭ
        // ��ȭ ���� �� �÷��̾��� �������� ���߱�
        player.GetComponent<P_Move>().enabled = false;
    }

    void CloseGuidePanel()
    {
        guidePanel.SetActive(false); // X ��ư Ŭ�� �� ���̵�â ��Ȱ��ȭ
        // ��ȭ�� ������ �÷��̾��� ������ �ٽ� Ȱ��ȭ
        player.GetComponent<P_Move>().enabled = true;
    }
}
