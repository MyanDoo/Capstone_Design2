using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public GameObject menu;

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
            // ExitMenu Canvas 활성화/비활성화
            menu.SetActive(true);
        }
    }
}
