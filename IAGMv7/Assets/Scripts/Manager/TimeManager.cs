using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public int timeSpeed = 1; // 2�� 2��� ������

    public int OneDay = 600;
    public int DayCount = 1;
    public int MonthCount = 1;
    public int SeasonCount = 1;
    // ��¥ ���� ���� ��, �� ������ �����ϸ� ���� ���� ex) ��(1) 3�� 1�� / ����(2) 6�� 1�� / ����(3) 9�� 1�� / �ܿ�(4) 12�� 1��

    // �ð� ȸ��
    public Transform myRotateArea;
    
    // ChangeDay
    public TMPro.TMP_Text myDayText;
    
    // ChangeMonth
    int OneMonth;
    public TMPro.TMP_Text myMonthText;
    
    // ChangeSeason
    int OneSeason;
    public GameObject Spring;
    public GameObject Summer;
    public GameObject Fall;
    public GameObject Winter;

    void Start()
    {
        // �ð� ȸ��
        StartCoroutine(ArrowRotate(OneDay / timeSpeed));

        // ChangeDay
        myDayText.GetComponent<TMPro.TMP_Text>();
        StartCoroutine(ChangeDay(OneDay / timeSpeed));

        // ChangeMonth
        OneMonth = OneDay * 30;
        myMonthText.GetComponent<TMPro.TMP_Text>();
        StartCoroutine(ChangeMonth(OneMonth / timeSpeed));

        // ChangeSeason
        OneSeason = OneMonth * 3; 
        StartCoroutine(ChangeSeason(OneSeason / timeSpeed));
    }

    void Update()
    {
        if (DayCount > 30)
        {
            DayCount = 1;
        }
        myDayText.text = $"DAY-{DayCount}";

        switch (MonthCount)
        {
            case 1:
                myMonthText.text = "JAN";
                break;
            case 2:
                myMonthText.text = "FEB";
                break;
            case 3:
                myMonthText.text = "MAR";
                break;
            case 4:
                myMonthText.text = "APR";
                break;
            case 5:
                myMonthText.text = "MAY";
                break;
            case 6:
                myMonthText.text = "JUN";
                break;
            case 7:
                myMonthText.text = "JUL";
                break;
            case 8:
                myMonthText.text = "AUG";
                break;
            case 9:
                myMonthText.text = "SEP";
                break;
            case 10:
                myMonthText.text = "OCT";
                break;
            case 11:
                myMonthText.text = "NOV";
                break;
            case 12:
                myMonthText.text = "DEC";
                break;
            case 13:
                MonthCount = 1;
                break;
        }

        switch (SeasonCount)
        {
            case 1:
                Spring.SetActive(true);
                Summer.SetActive(false);
                Fall.SetActive(false);
                Winter.SetActive(false);
                break;
            case 2:
                Spring.SetActive(false);
                Summer.SetActive(true);
                Fall.SetActive(false);
                Winter.SetActive(false);
                break;
            case 3:
                Spring.SetActive(false);
                Summer.SetActive(false);
                Fall.SetActive(true);
                Winter.SetActive(false);
                break;
            case 4:
                Spring.SetActive(false);
                Summer.SetActive(false);
                Fall.SetActive(false);
                Winter.SetActive(true);
                break;
            case 5:
                SeasonCount = 1;
                break;
        }
    }

    IEnumerator ArrowRotate(int Day) // �ð�ٴ� ȸ��
    {
        while (Day > 0.0f)
        {
            myRotateArea.Rotate(Vector3.back * (360.0f / Day) * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator ChangeDay(int Day)
    {
        yield return new WaitForSeconds(Day);
        
        DayCount++;

        StartCoroutine(ChangeDay(Day));
    }

    IEnumerator ChangeMonth(int Month)
    {
        yield return new WaitForSeconds(Month);

        MonthCount++;

        StartCoroutine(ChangeMonth(Month));
    }

    IEnumerator ChangeSeason(int Season)
    {
        yield return new WaitForSeconds(Season);

        SeasonCount++;

        StartCoroutine(ChangeSeason(Season));
    }
}
