using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundController : MonoBehaviour
{

    public AudioSource audioSource; //����� �ҽ� ������Ʈ ����
    public AudioClip footstepClip;  //�ȴ� �Ҹ� ����� Ŭ��
    public AudioClip jumpClip;      //���� �Ҹ� ����� Ŭ��
    public float footstepDelay = 0.5f; //�������� �ȴ� �Ҹ� ������ ���� �ð� 

    private bool isJumping = false;  //���� ���� ������ ����
    private bool isWalking = false;  //���� �Ȱ� �ִ��� ����
    private float footstepTimer = 0.0f; //���� ���� �Ҹ��� ����� �������� Ÿ�̸�

    private void Update()
    {
        bool wasWalking = isWalking;  //���� �����ӿ��� �Ȱ� �־����� ����

        //A,DŰ�� ������ isWalking
        isWalking = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);

        //WŰ�� ������ ���� ���� ���̶�� ���� �Ҹ� ���
        if (Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            PlayJumpSound();
        }

        //�Ȱ� �ְ� ���� ���� �ƴ϶�� �ȴ� �Ҹ� ���
        if (isWalking && !isJumping)
        {
            footstepTimer -= Time.deltaTime; //Ÿ�̸� ����
            if (footstepTimer <= 0.0f)  //Ÿ�̸Ӱ� 0���϶�� ���� �Ҹ� ���
            {
                PlayFootstepSound();
                footstepTimer = footstepDelay; //Ÿ�̸� �缳��
            }
        }
        else
        {
            footstepTimer = 0.0f; //���� �ʴ� ��� Ÿ�̸� �ʱ�ȭ
        }
    }

    //���� �Ҹ� ��� �Լ�
    private void PlayFootstepSound()
    {
        //���� ��� ���� �Ҹ��� ���ų� ��� ���� �Ҹ��� ���� �Ҹ��� �ƴ϶�� ���� ���
        if (!audioSource.isPlaying || audioSource.clip != footstepClip)
        {
            audioSource.clip = footstepClip;
            audioSource.Play();
        }
    }
    //���� �Ҹ� ��� �Լ�
    private void PlayJumpSound()
    {
        isJumping = true; //���� ���·� ����
        audioSource.clip = jumpClip;
        audioSource.Play();
        Invoke("ResetJump", jumpClip.length); //���� �Ҹ� ���̸�ŭ �Ŀ� ���� ���� �ʱ�ȭ
    }

    //���� ���� �ʱ�ȭ �Լ�
    private void ResetJump()
    {
        isJumping = false; //���� ���� ����
    }
}

