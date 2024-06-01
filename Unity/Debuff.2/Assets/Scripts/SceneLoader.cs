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

    //�ִϸ��̼� �̺�Ʈ�� �ɾ�� �Լ��� ������ ���� ���� ����
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
