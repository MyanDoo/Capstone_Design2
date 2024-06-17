using System.Collections;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public float scrollSpeed = 0.1f; // ��� ��ũ�� �ӵ�
    public float tileSizeY; // Ÿ���� ���� ũ�� (�̹����� ����)

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // ���� ��ġ ����
        tileSizeY = GetComponent<SpriteRenderer>().bounds.size.y; // �̹����� �ʺ� ��������
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeY);
        transform.position = startPosition + Vector3.up * newPosition;
    }
}
