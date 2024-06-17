using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundController : MonoBehaviour
{

    public AudioSource audioSource; //오디오 소스 컴포넌트 참조
    public AudioClip footstepClip;  //걷는 소리 오디오 클립
    public AudioClip jumpClip;      //점프 소리 오디오 클립
    public float footstepDelay = 0.5f; //연속적인 걷는 소리 사이의 지연 시간 

    private bool isJumping = false;  //현재 점프 중인지 여부
    private bool isWalking = false;  //현재 걷고 있는지 여부
    private float footstepTimer = 0.0f; //다음 걸음 소리를 재생할 때까지의 타이머

    private void Update()
    {
        bool wasWalking = isWalking;  //이전 프레임에서 걷고 있었는지 여부

        //A,D키를 누르면 isWalking
        isWalking = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);

        //W키가 눌리고 현재 점프 중이라면 점프 소리 재생
        if (Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            PlayJumpSound();
        }

        //걷고 있고 점프 중이 아니라면 걷는 소리 재생
        if (isWalking && !isJumping)
        {
            footstepTimer -= Time.deltaTime; //타이머 감소
            if (footstepTimer <= 0.0f)  //타이머가 0이하라면 걸음 소리 재생
            {
                PlayFootstepSound();
                footstepTimer = footstepDelay; //타이머 재설정
            }
        }
        else
        {
            footstepTimer = 0.0f; //걷지 않는 경우 타이머 초기화
        }
    }

    //걸음 소리 재생 함수
    private void PlayFootstepSound()
    {
        //현재 재생 중인 소리가 없거나 재생 중인 소리가 걸음 소리가 아니라면 새로 재생
        if (!audioSource.isPlaying || audioSource.clip != footstepClip)
        {
            audioSource.clip = footstepClip;
            audioSource.Play();
        }
    }
    //점프 소리 재생 함수
    private void PlayJumpSound()
    {
        isJumping = true; //점프 상태로 설정
        audioSource.clip = jumpClip;
        audioSource.Play();
        Invoke("ResetJump", jumpClip.length); //점프 소리 길이만큼 후에 점프 상태 초기화
    }

    //점프 상태 초기화 함수
    private void ResetJump()
    {
        isJumping = false; //점프 상태 해제
    }
}

