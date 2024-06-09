using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public GameObject menu;

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
    }

    public void MenuClose()
    {
        menu.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ExitMenu Canvas Ȱ��ȭ/��Ȱ��ȭ
            menu.SetActive(true);
        }
    }
}
