using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Move : MonoBehaviour
{
    public static P_Move instance { get; private set; }

    public float moveSpeed = 5f; // 캐릭터 이동 속도
    public float jumpPwoer = 10f; //점프 힘

    public float fallMultiplier = 2.5f; // 낙하 가속도 배수
    public float lowJumpMultiplier = 2.0f; // 짧은 점프 가속도 배수

    private Rigidbody rb; // 캐릭터의 Rigidbody 컴포넌트
    private Animator animator; //애니메이터 조작을 위한 변수

    private SpriteRenderer spriteRenderer; // 캐릭터의 SpriteRenderer 컴포넌트

    private Vector3 originalScale; //스케일고정

    public bool isMovementStopped = false; // 움직임이 멈췄는지 여부

    public GameObject _wall_right; //오른쪽 벽을 오브젝트로 가져옴

    public GameObject NextMapButtonImage; //다음 씬으로 넘어가는 UI 호출

    void Start()
    {
        if (instance != null) Destroy(this);
        else instance = this;

        // 캐릭터의 Rigidbody 컴포넌트 가져오기
        rb = GetComponent<Rigidbody>();
        // 캐릭터의 Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>();

        //캐릭터의 SpriteRenderer 컴포넌트 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>();

        originalScale = transform.localScale;

        //다음맵 넘어가는 UI 닫아놓기
        NextMapButtonImage.SetActive(false);
    }

    // 키보드에서 손을 뗐을 때 완전 stop
    void Update()
    {
        CharacterMove();
    }

    public void ChattingCloseButtonClick()
    {
        Chatting.instance.ChattingOff();

        if (isMovementStopped)
        {
            //대화창 전부 꺼주기
            Chatting.instance.ChattingOff();
            isMovementStopped = false;
        }
    }

    public void CharacterMove()
    {
        //점프
        if (Input.GetKey(KeyCode.W) && !animator.GetBool("isJumping"))
        {
            rb.AddForce(Vector2.up * jumpPwoer, ForceMode.Impulse);
            animator.SetBool("isJumping", true);
        }

        // 캐릭터의 이동 방향 초기화
        Vector2 movement = Vector2.zero;

        // 왼쪽 방향키 입력 감지
        if (Input.GetKey(KeyCode.A))
        {
            // 왼쪽으로 이동
            movement = Vector2.left;
            spriteRenderer.flipX = false;
        }
        // 오른쪽 방향키 입력 감지
        else if (Input.GetKey(KeyCode.D))
        {
            // 오른쪽으로 이동
            movement = Vector2.right;
            spriteRenderer.flipX = true;
        }

        // 캐릭터 이동
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);

        // 속도가 0 == 멈춤
        if (rb.velocity.normalized.x == 0)
            animator.SetBool("isWalking", false); // isWalking 변수 : false 
        // 이동중
        else
            animator.SetBool("isWalking", true); // isWalking 변수 : true

        // 스케일 고정
        transform.localScale = originalScale;

        // 중력 조절 코드
        if (rb.velocity.y < 0)
        {
            rb.AddForce(Vector3.down * fallMultiplier, ForceMode.Acceleration);
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector3.down * lowJumpMultiplier, ForceMode.Acceleration);
        }
    }

    // 충돌감지
    void OnCollisionEnter(Collision other)
    {
        //충돌이 발생한 오브젝트의 태그 로그 출력
        Debug.Log("Player-OnCollisionEnter is called" + " / " + other.gameObject.name);

        if (other.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isJumping", false);
        }

        if (other.gameObject.name == "Wall_right")
        {
            Debug.Log("오른쪽 벽과 충돌");
            NextMapButtonImage.SetActive(true);
        }
    }

    //(일단은)터널맵에서 벽과 충돌했을때  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "tunnel1")
        {
            Debug.Log("터널 벽1과 충돌함");

            //못 걷게 처리
            isMovementStopped = true;
            //가만히 서있는 모션 실행
            animator.SetBool("isWalking", false);
            //터널 대화창1 오픈
            Chatting.instance.tunnel_Player_Chatting_Img1.SetActive(true);
            Chatting.instance.ChattingCloseButton.SetActive(true);
            Chatting.instance.TextSend_tunnel1(); //텍스트 초기화 작업 함수 호출

        }

        if (other.gameObject.tag == "tunnel2")
        {
            Debug.Log("터널 벽2와 충돌함");

            //못 걷게 처리
            isMovementStopped = true;
            //가만히 서있는 모션 실행
            animator.SetBool("isWalking", false);
            //터널 대화창2 오픈
            Chatting.instance.tunnel_Player_Chatting_Img2.SetActive(true);
            Chatting.instance.ChattingCloseButton.SetActive(true);
            Chatting.instance.TextSend_tunnel2();
        }
    }

    public void closeButton()
    {
        NextMapButtonImage.SetActive(false);
    }

    public void EndSceneCall()
    {
        NextMapButtonImage.SetActive(true);
    }
}

