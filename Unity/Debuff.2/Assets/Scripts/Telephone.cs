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

    private string currentNum = ""; // 현재 입력된 번호를 저장

    string number = "15449030";
    string totalNum = "";

    public void OnNumberButtonClick(GameObject button)
    {
        // 버튼의 이름을 숫자로 간주하고 currentNum에 추가
        currentNum += button.name;
        // 업데이트된 번호를 표시
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

        //if문으로 받아서 SceneLoader의 OnNextButtonClick를 instance로 호출하기
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
