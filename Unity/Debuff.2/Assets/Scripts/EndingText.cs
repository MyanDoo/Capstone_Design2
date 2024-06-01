using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingText : MonoBehaviour
{
    public static EndingText instance { get; private set; }

    public Animator animator; // Animator ������Ʈ ����

    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
    }

    public void FadeInOut()
    {

    }

    private void Update()
    {
        bool start = CameraZoomOut.instance.isDone;
        if (start)
        {
            animator.SetTrigger("start");
        }
    }
}
