using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public GameObject menu;
    public GameObject player;

    void Start()
    {
        menu.SetActive(false);
    }

    public void Exit()
    {
        // ������ �����ϴ� �޼���
        Application.Quit();

        // Unity �����Ϳ��� �׽�Ʈ�� ���� �Ʒ� �α׸� Ȯ���� �� �ֽ��ϴ�.
        #if UNITY_EDITOR
        Debug.Log("������ ����Ǿ����ϴ�.");
        #endif
    }

    public void MenuOpen()
    {
        menu.SetActive(true);
        player.GetComponent<P_Move>().enabled = false;
    }

    public void MenuClose()
    {
        menu.SetActive(false);
        player.GetComponent<P_Move>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isActive = menu.activeSelf;
            if (isActive)
            {
                MenuClose();
            }
            else
            {
                MenuOpen();
            }
        }
    }
}
