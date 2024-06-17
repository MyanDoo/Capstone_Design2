using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingText : MonoBehaviour
{
    public static EndingText instance { get; private set; }

    public Animator animator; // Animator 컴포넌트 참조

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
