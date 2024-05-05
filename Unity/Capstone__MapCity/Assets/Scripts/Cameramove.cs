using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameramove : MonoBehaviour
{
    public Transform target;
    public float speed;

    public Vector3 offset; // 카메라와 타겟 간의 오프셋

    public Vector2 center;
    public Vector2 size;
    float height;
    float width;

    // Update is called once per frame

    // Start is called before the first frame update
    void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // 타겟의 위치에 offset을 더하여 카메라의 목표 위치를 계산
            Vector3 targetPosition = target.position + offset;

            // 부드러운 이동을 위해 Lerp 사용
            Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

            // 클램핑
            float lx = size.x * 0.5f - width;
            float clampX = Mathf.Clamp(newPosition.x, -lx + center.x, lx + center.x);

            float ly = size.y * 0.5f - height;
            float clampY = Mathf.Clamp(newPosition.y, -ly + center.y, ly + center.y);

            transform.position = new Vector3(clampX, clampY, transform.position.z);
        }
    }
}
