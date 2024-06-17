using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonLight : MonoBehaviour
{
    public GameObject buttonLight;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonLight != null)
        {
            buttonLight.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonLight != null)
        {
            buttonLight.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        buttonLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
