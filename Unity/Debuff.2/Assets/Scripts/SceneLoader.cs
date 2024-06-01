using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance { get; private set; }

    public Animator animator;
    [SerializeField] private int SceneNum = 0;

    public void OnNextButtonClick()
    {
        Debug.Log("마우스 입력 받음");
        //SceneNum = 0;
        Wait();
    }

    public void Wait()
    {
        Debug.Log("Wait함수 실행");

        if (animator != null)
            animator.Play("FadeOut");
        else
            Debug.Log("animator이 null임");
    }

    //애니메이션 이벤트에 걸어둔 함수라 참조가 없는 것이 맞음
    IEnumerator OnSceneLoad()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneNum = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(SceneNum);
    }

    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
