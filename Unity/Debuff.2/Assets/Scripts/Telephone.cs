using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Telephone : MonoBehaviour
{
    public GameObject TelephoneButtons;

    public TMP_Text displayText;
    [NonSerialized] public int num1, num2, num3, num4, num5, num6, num7, num8, num9, num0;

    private string currentNum = ""; // ���� �Էµ� ��ȣ�� ����

    string number = "15449030";
    string totalNum = "";

    public void OnNumberButtonClick(GameObject button)
    {
        // ��ư�� �̸��� ���ڷ� �����ϰ� currentNum�� �߰�
        currentNum += button.name;
        // ������Ʈ�� ��ȣ�� ǥ��
        displayText.text = currentNum;
    }

    public void btnReset()
    {
        currentNum = "";
        displayText.text = "";
    }

    public void btnCall()
    {
        totalNum = displayText.text;

        //if������ �޾Ƽ� SceneLoader�� OnNextButtonClick�� instance�� ȣ���ϱ�
        if (totalNum == number)
        {
            SceneLoader.instance.Wait();
            //SceneManager.LoadScene("Ending");
        }
        else if (totalNum != number)
        {
            btnReset();
            SceneManager.LoadScene("Bridge1");
        }
    }

    public void EndTelephoneCall()
    {
        TelephoneButtons.SetActive(true);
    }

    public void Awake()
    {
        TelephoneButtons.SetActive(false);
    }

}
