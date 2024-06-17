using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOn : MonoBehaviour
{
    public GameObject gameObj;

    void OnMouseEnter()
    {
        if (gameObj != null)
        {
            gameObj.SetActive(true); // b ������Ʈ Ȱ��ȭ
        }
    }

    void OnMouseExit()
    {
        if (gameObj != null)
        {
            gameObj.SetActive(false); // b ������Ʈ ��Ȱ��ȭ
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
