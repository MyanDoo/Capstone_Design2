using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeater : MonoBehaviour
{
    public GameObject background1; // 첫 번째 배경 이미지
    public GameObject background2; // 두 번째 배경 이미지

    private float backgroundHeight;

    void Start()
    {
        backgroundHeight = background1.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        if (background1.transform.position.y > Camera.main.transform.position.y + backgroundHeight)
        {
            RepositionBackground(background1);
        }

        if (background2.transform.position.y > Camera.main.transform.position.y + backgroundHeight)
        {
            RepositionBackground(background2);
        }
    }

    void RepositionBackground(GameObject background)
    {
        Vector3 currentPosition = background.transform.position;
        currentPosition.y -= 2 * backgroundHeight;
        background.transform.position = currentPosition;
    }
}
