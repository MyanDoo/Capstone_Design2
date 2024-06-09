using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private int SceneNum = 0;
    public static SceneLoader instance { get; private set; }

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

    IEnumerator OnSceneLoad()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneNum = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(SceneNum);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null) Destroy(this);
        else instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
