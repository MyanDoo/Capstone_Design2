using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public CanvasGroup fadeCg;

    [Range(0.5f, 2.0f)]
    public float fadeDuration = 1.0f;

    public Dictionary<string, LoadSceneMode> loadScenes = new Dictionary<string, LoadSceneMode>();

    void InitSceneInfo()
    {
        //ȣ���� ���� ������ ��ųʸ��� �߰�
        loadScenes.Add("Level1", LoadSceneMode.Additive);
        //loadScenes.Add("Player", LoadSceneMode.Additive);
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        InitSceneInfo();

        //ó�� ���İ��� ���������� ����
        fadeCg.alpha = 1f;
        yield return StartCoroutine(Fade(1f));

        //�������� ���� �ڷ�ƾ���� ȣ��
        foreach (var _loadScene in loadScenes)
        {
            yield return StartCoroutine(LoadScene(_loadScene.Key, _loadScene.Value));
        }

        yield return new WaitForSeconds(1f); // 1�� ���
        //������ �����
        StartCoroutine(Fade(0f));
    }

    IEnumerator LoadScene(string sceneName, LoadSceneMode mode)
    {
        //�񵿱� ������� ���� �ε��ϰ� �ε尡 �Ϸ�ɶ����� �����
        yield return SceneManager.LoadSceneAsync(sceneName, mode);

        Scene loadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(loadedScene);
    }

    //Fade IN - �������
    IEnumerator Fade(float finalAlpha)
    {
        //����Ʈ���� ������ ���� �����ϱ� ���� �������� ���� Ȱ��ȭ
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Level1"));
        fadeCg.blocksRaycasts = true;

        //���밪 �Լ��� ������� ���
        //float fadeSpeed = Mathf.Abs(fadeCg.alpha - finalAlpha) / fadeDuration;
        float fadeSpeed = 0.4f;

        //���İ��� ����
        while (!Mathf.Approximately(fadeCg.alpha, finalAlpha)) //fadeCg.alpha���� finalAlpha������ ������ �����ϵ���
        {
            //MoveToward �Լ��� Lerp �Լ��� ������ �Լ��� ���İ��� �����Ѵ�.
            fadeCg.alpha = Mathf.MoveTowards(fadeCg.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
            yield return null;
        }

        fadeCg.blocksRaycasts = false;

        //fade In�� �Ϸ�� �� SceneLoader ���� ����(Unload)
        if (finalAlpha == 0f) //���̵� �ƿ��� �Ϸ�� �Ŀ� SceneLoader ���� ��ε��Ѵ�.
            SceneManager.UnloadSceneAsync("SceneLoader");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
