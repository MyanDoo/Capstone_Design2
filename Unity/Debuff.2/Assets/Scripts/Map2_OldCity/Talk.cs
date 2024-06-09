using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talk : MonoBehaviour
{
    public GameObject canvas; // Ȱ��ȭ�� ĵ����
    public GameObject player; // �÷��̾� ������Ʈ
    public GameObject NPC; // NPC ������Ʈ
    private bool isPlayerNearby = false; // �÷��̾�� ������ �ִ��� ����
    private Coroutine canvasDisableCoroutine; // ĵ���� ��Ȱ��ȭ �ڷ�ƾ

    void Start()
    {
        canvas.SetActive(false); // ������ �� ĵ���� ��Ȱ��ȭ
        player = GameObject.FindGameObjectWithTag("Player"); // �÷��̾� ������Ʈ ã��
        NPC = this.gameObject; // NPC ������Ʈ ã�� (�ڱ� �ڽ�)
    }

    void Update()
    {
        // �÷��̾�� NPC ������Ʈ ������ �Ÿ� ����
        float distance = Vector3.Distance(NPC.transform.position, player.transform.position);

        // ���� �Ÿ� ���� �÷��̾ ������ ĵ���� Ȱ��ȭ
        if (distance <= 3f && !isPlayerNearby)
        {
            isPlayerNearby = true;
            canvas.SetActive(true); // ĵ���� Ȱ��ȭ

            // ĵ������ �ڵ����� ��Ȱ��ȭ�ϱ� ���� �ڷ�ƾ ����
            canvasDisableCoroutine = StartCoroutine(DisableCanvasAfterDelay(3f));
        }
        // �÷��̾ ���� �Ÿ� �ۿ� ������ ĵ���� ��Ȱ��ȭ
        else if (distance > 3f && isPlayerNearby)
        {
            isPlayerNearby = false;
            canvas.SetActive(false);

            // �ڷ�ƾ�� ���� ���̸� ����
            if (canvasDisableCoroutine != null)
            {
                StopCoroutine(canvasDisableCoroutine);
            }
        }
    }

    IEnumerator DisableCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // ������ �ð� ���� ���
        canvas.SetActive(false); // ĵ���� ��Ȱ��ȭ
    }
}