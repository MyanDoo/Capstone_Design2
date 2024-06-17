using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talk : MonoBehaviour
{
    public GameObject canvas; // 활성화할 캔버스
    public GameObject player; // 플레이어 오브젝트
    public GameObject NPC; // NPC 오브젝트
    private bool isPlayerNearby = false; // 플레이어와 가까이 있는지 여부
    private Coroutine canvasDisableCoroutine; // 캔버스 비활성화 코루틴

    void Start()
    {
        canvas.SetActive(false); // 시작할 때 캔버스 비활성화
        player = GameObject.FindGameObjectWithTag("Player"); // 플레이어 오브젝트 찾기
        NPC = this.gameObject; // NPC 오브젝트 찾기 (자기 자신)
    }

    void Update()
    {
        // 플레이어와 NPC 오브젝트 사이의 거리 측정
        float distance = Vector3.Distance(NPC.transform.position, player.transform.position);

        // 일정 거리 내에 플레이어가 있으면 캔버스 활성화
        if (distance <= 3f && !isPlayerNearby)
        {
            isPlayerNearby = true;
            canvas.SetActive(true); // 캔버스 활성화

            // 캔버스를 자동으로 비활성화하기 위해 코루틴 실행
            canvasDisableCoroutine = StartCoroutine(DisableCanvasAfterDelay(3f));
        }
        // 플레이어가 일정 거리 밖에 있으면 캔버스 비활성화
        else if (distance > 3f && isPlayerNearby)
        {
            isPlayerNearby = false;
            canvas.SetActive(false);

            // 코루틴이 실행 중이면 중지
            if (canvasDisableCoroutine != null)
            {
                StopCoroutine(canvasDisableCoroutine);
            }
        }
    }

    IEnumerator DisableCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 지정된 시간 동안 대기
        canvas.SetActive(false); // 캔버스 비활성화
    }
}