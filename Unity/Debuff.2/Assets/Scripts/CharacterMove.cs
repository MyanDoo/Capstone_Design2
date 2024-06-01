using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterMove : MonoBehaviour
{

    //float _movespeed = 6f;
    public GameObject _wall_right; //오른쪽 벽을 오브젝트로 가져옴

    public GameObject NextMapButtonImage;


    // Start is called before the first frame update
    void Start()
    {
        NextMapButtonImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player-OnCollisionEnter is called" + " / " + collision.gameObject.name); //충돌이 발생한 오브젝트의 태그 로그 출력

        if (collision.gameObject.name == "Wall_right")
        {
            Debug.Log("오른쪽 벽과 충돌");
            NextMapButtonImage.SetActive(true);

        }
    }

    public void EndSceneCall()
    {
        NextMapButtonImage.SetActive(true);
    }

}
