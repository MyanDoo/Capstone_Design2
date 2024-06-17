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
        // 게임을 종료하는 메서드
        Application.Quit();

        // Unity 에디터에서 테스트할 때는 아래 로그를 확인할 수 있습니다.
        #if UNITY_EDITOR
        Debug.Log("게임이 종료되었습니다.");
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
