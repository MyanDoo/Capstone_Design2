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
        Debug.Log("���콺 �Է� ����");
        //SceneNum = 0;
        Wait();
    }

    public void Wait()
    {
        Debug.Log("Wait�Լ� ����");

        if (animator != null)
            animator.Play("FadeOut");
        else
            Debug.Log("animator�� null��");
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
