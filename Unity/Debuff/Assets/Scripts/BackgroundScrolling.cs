using System.Collections;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public float scrollSpeed = 0.1f; // 배경 스크롤 속도
    public float tileSizeY; // 타일의 세로 크기 (이미지의 세로)

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // 시작 위치 저장
        tileSizeY = GetComponent<SpriteRenderer>().bounds.size.y; // 이미지의 너비 가져오기
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeY);
        transform.position = startPosition + Vector3.up * newPosition;
    }
}
