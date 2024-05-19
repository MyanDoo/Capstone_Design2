using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterMove : MonoBehaviour
{
    float _movespeed = 6f;
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
        Move();
    }

    public void Move()
    {
        float _moveAmout = _movespeed * Time.deltaTime; //FPS를 맞추기 위해 Time.deltaTime 사용
        float _hori = Input.GetAxis("Horizontal"); //horizontal(a,d,좌/우화살표) 입력을 받는 변수 선언
        transform.Translate(Vector3.forward * _moveAmout * _hori); //입력이 들어오면, tranform.Translate 컴포넌트의 값을 입력값에 맞게 변경
    }

    public void OnCollisionEnter(Collision collision) 
    {
        Debug.Log("Player-OnCollisionEnter is called" + " / " + collision.gameObject.name); //충돌이 발생한 오브젝트의 태그 로그 출력

        if (collision.gameObject.name == "Wall_right")
        {
            Debug.Log("제발돼라 / 오른쪽 벽과 충돌");
            NextMapButtonImage.SetActive(true);

        }

        if(collision.gameObject.name == "Wall_right_end")
        {
            //
        }
    }

    public void EndSceneCall()
    {
        NextMapButtonImage.SetActive(true);
    }

}
