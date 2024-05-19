using System.Collections;
using UnityEngine;

public class LogoMoveXY : MonoBehaviour
{
    public float speed = 2.0f;  // �̵� �ӵ�
    public float range = 0.5f;  // �̵� ����

    private float startX;
    private float startY;

    private Vector2 direction;
    private float changeDirectionTime;
    private float changeInterval = 2.0f;  // ������ �ٲٴ� �ð� ����

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
            case 0: return new Vector2(1, 1);    // ������ ��
            case 1: return new Vector2(-1, 1);   // ���� ��
            case 2: return new Vector2(-1, -1);  // ���� �Ʒ�
            case 3: return new Vector2(1, -1);   // ������ �Ʒ�
            default: return Vector2.zero;       // �⺻ �� (���� ������)
        }
    }

    /*public float speed = 2.0f;  // �̵� �ӵ�
    public float range = 0.5f;  // �̵� ����
    public float directionChangeInterval = 0.5f;  // ������ �ٲٴ� �ð� ����

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
            case 0: return new Vector2(1, 1);    // ������ ��
            case 1: return new Vector2(-1, 1);   // ���� ��
            case 2: return new Vector2(-1, -1);  // ���� �Ʒ�
            case 3: return new Vector2(1, -1);   // ������ �Ʒ�
            default: return Vector2.zero;       // �⺻ �� (���� ������)
        }
    }*/



    /*public float range = 0.5f;  // �̵� ����
    public float moveTime = 0.5f;  // �̵� �ð� ����
    public float moveSpeed = 2.0f;  // �̵� �ӵ�

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


    /*public float speed = 2.0f;  // �̵� �ӵ�
    public float range = 0.5f;  // �̵� ����
    public float directionChangeInterval = 0.5f;  // ������ �ٲٴ� �ð� ����

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
        // ���� ����
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
            case 0: return new Vector2(1, 1);    // ������ ��
            case 1: return new Vector2(-1, 1);   // ���� ��
            case 2: return new Vector2(-1, -1);  // ���� �Ʒ�
            case 3: return new Vector2(1, -1);   // ������ �Ʒ�
            default: return Vector2.zero;       // �⺻ �� (���� ������)
        }
    }*/
}
