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
            gameObj.SetActive(true); // b 오브젝트 활성화
        }
    }

    void OnMouseExit()
    {
        if (gameObj != null)
        {
            gameObj.SetActive(false); // b 오브젝트 비활성화
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
