using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    float _movespeed = 6f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        float _moveAmout = _movespeed * Time.deltaTime; //FPS를 맞추기 위해 Time.deltaTime 사용
        float _hori = Input.GetAxis("Horizontal");  //horizontal(a,d,좌/우화살표) 입력을 받는 변수 선언
        transform.Translate(Vector3.forward * _moveAmout * _hori);  //입력이 들어오면, tranform.Translate 컴포넌트의 값을 입력값에 맞게 변경
    }
}
