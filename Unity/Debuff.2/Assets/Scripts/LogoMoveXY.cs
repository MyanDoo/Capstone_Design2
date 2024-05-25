using System.Collections;
using UnityEngine;

public class LogoMoveXY : MonoBehaviour
{
    public float speed = 2.0f;  // 이동 속도
    public float range = 0.5f;  // 이동 범위

    private float startX;
    private float startY;

    private Vector2 direction;
    private float changeDirectionTime;
    private float changeInterval = 2.0f;  // 방향을 바꾸는 시간 간격

    void Start()
    {
        startX = transform.localPosition.x;
        startY = transform.localPosition.y;
        direction = RandomDirection();
        changeDirectionTime = Time.time + changeInterval;
    }

    void Update()
    {
        if (Time.time >= changeDirectionTime)
        {
            direction = RandomDirection();
            changeDirectionTime = Time.time + changeInterval;
        }

        float newX = startX + direction.x * Mathf.PingPong(Time.time * speed, range * 2) - range;
        float newY = startY + direction.y * Mathf.PingPong(Time.time * speed, range * 2) - range;
        transform.localPosition = new Vector3(newX, newY, transform.localPosition.z);
    }

    Vector2 RandomDirection()
    {
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0: return new Vector2(1, 1);    // 오른쪽 위
            case 1: return new Vector2(-1, 1);   // 왼쪽 위
            case 2: return new Vector2(-1, -1);  // 왼쪽 아래
            case 3: return new Vector2(1, -1);   // 오른쪽 아래
            default: return Vector2.zero;       // 기본 값 (오류 방지용)
        }
    }

    /*public float speed = 2.0f;  // 이동 속도
    public float range = 0.5f;  // 이동 범위
    public float directionChangeInterval = 0.5f;  // 방향을 바꾸는 시간 간격

    private Vector2 startPos;
    private Vector2 direction;
    private float nextDirectionChangeTime;

    void Start()
    {
        startPos = transform.localPosition;
        direction = RandomDirection();
        nextDirectionChangeTime = Time.time + directionChangeInterval;

        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            SetRandomDirection();
            float elapsedTime = 0f;

            while (elapsedTime < directionChangeInterval)
            {
                float newX = startPos.x + Mathf.Sin(Time.time * speed) * range * direction.x;
                float newY = startPos.y + Mathf.Sin(Time.time * speed) * range * direction.y;
                transform.localPosition = new Vector3(newX, newY, transform.localPosition.z);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.localPosition = startPos;
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    void SetRandomDirection()
    {
        direction = RandomDirection();
        nextDirectionChangeTime = Time.time + directionChangeInterval;
    }

    Vector2 RandomDirection()
    {
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0: return new Vector2(1, 1);    // 오른쪽 위
            case 1: return new Vector2(-1, 1);   // 왼쪽 위
            case 2: return new Vector2(-1, -1);  // 왼쪽 아래
            case 3: return new Vector2(1, -1);   // 오른쪽 아래
            default: return Vector2.zero;       // 기본 값 (오류 방지용)
        }
    }*/



    /*public float range = 0.5f;  // 이동 범위
    public float moveTime = 0.5f;  // 이동 시간 간격
    public float moveSpeed = 2.0f;  // 이동 속도

    private Vector3 startPos;
    private Vector3 targetPos;

    void Start()
    {
        startPos = transform.localPosition;
        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            SetRandomTarget();
            float elapsedTime = 0f;

            while (elapsedTime < moveTime)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, (elapsedTime / moveTime) * moveSpeed);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.localPosition = targetPos;
            yield return new WaitForSeconds(moveTime);
        }
    }

    void SetRandomTarget()
    {
        float randomX = Random.Range(startPos.x - range, startPos.x + range);
        float randomY = Random.Range(startPos.y - range, startPos.y + range);
        targetPos = new Vector3(randomX, randomY, startPos.z);
    }*/


    /*public float speed = 2.0f;  // 이동 속도
    public float range = 0.5f;  // 이동 범위
    public float directionChangeInterval = 0.5f;  // 방향을 바꾸는 시간 간격

    private Vector2 startPos;
    private Vector2 direction;
    private float nextDirectionChangeTime;

    void Start()
    {
        startPos = transform.localPosition;
        direction = RandomDirection();
        nextDirectionChangeTime = Time.time + directionChangeInterval;
    }

    void Update()
    {
        // 방향 변경
        if (Time.time >= nextDirectionChangeTime)
        {
            direction = RandomDirection();
            nextDirectionChangeTime = Time.time + directionChangeInterval;
        }

        float newX = startPos.x + Mathf.Sin(Time.time * speed) * range * direction.x;
        float newY = startPos.y + Mathf.Sin(Time.time * speed) * range * direction.y;
        transform.localPosition = new Vector3(newX, newY, transform.localPosition.z);
    }

    Vector2 RandomDirection()
    {
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0: return new Vector2(1, 1);    // 오른쪽 위
            case 1: return new Vector2(-1, 1);   // 왼쪽 위
            case 2: return new Vector2(-1, -1);  // 왼쪽 아래
            case 3: return new Vector2(1, -1);   // 오른쪽 아래
            default: return Vector2.zero;       // 기본 값 (오류 방지용)
        }
    }*/
}
