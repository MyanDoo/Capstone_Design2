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
        //호출할 씬의 정보를 딕셔너리에 추가
        loadScenes.Add("Level1", LoadSceneMode.Additive);
        //loadScenes.Add("Player", LoadSceneMode.Additive);
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        InitSceneInfo();

        //처음 알파값을 검은색으로 설정
        fadeCg.alpha = 1f;
        yield return StartCoroutine(Fade(1f));

        //여러개의 씬을 코루틴으로 호출
        foreach (var _loadScene in loadScenes)
        {
            yield return StartCoroutine(LoadScene(_loadScene.Key, _loadScene.Value));
        }

        yield return new WaitForSeconds(1f); // 1초 대기
        //서서히 밝아짐
        StartCoroutine(Fade(0f));
    }

    IEnumerator LoadScene(string sceneName, LoadSceneMode mode)
    {
        //비동기 방식으로 씬을 로드하고 로드가 완료될때까지 대기함
        yield return SceneManager.LoadSceneAsync(sceneName, mode);

        Scene loadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(loadedScene);
    }

    //Fade IN - 밝아지기
    IEnumerator Fade(float finalAlpha)
    {
        //라이트맵이 깨지는 것을 방지하기 위해 스테이지 씬을 활성화
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Level1"));
        fadeCg.blocksRaycasts = true;

        //절대값 함수로 백분율을 계산
        //float fadeSpeed = Mathf.Abs(fadeCg.alpha - finalAlpha) / fadeDuration;
        float fadeSpeed = 0.4f;

        //알파값을 조정
        while (!Mathf.Approximately(fadeCg.alpha, finalAlpha)) //fadeCg.alpha부터 finalAlpha값까지 서서히 근접하도록
        {
            //MoveToward 함수는 Lerp 함수와 동일한 함수로 알파값을 보간한다.
            fadeCg.alpha = Mathf.MoveTowards(fadeCg.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
            yield return null;
        }

        fadeCg.blocksRaycasts = false;

        //fade In이 완료된 후 SceneLoader 씬은 삭제(Unload)
        if (finalAlpha == 0f) //페이드 아웃이 완료된 후에 SceneLoader 씬을 언로드한다.
            SceneManager.UnloadSceneAsync("SceneLoader");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
