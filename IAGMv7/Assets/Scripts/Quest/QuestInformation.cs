using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestInformation : MonoBehaviour
{
    public GameObject ParentOBJ;
    public GameObject ChildOBJ;
    public GameObject NewsBArea; // NewsBalloon이 자식으로 생성되는 UiCanvas안의 부모 오브젝트

    public Quest.QuestInfo myQuest;
    public TMP_Text Questname;
    public TMP_Text grade;
    public TMP_Text name;
    public TMP_Text info;
    public TMP_Text reward;
    public TMP_Text Result;
    string result;
    bool chk; // 성공 실패 여부 체크
    public GameObject myNpc; // 퀘스트를 준 npc
    public int People;
    public bool IsQuestchk;


    public void Start()
    {
        ShowQuest(myQuest);

        //UI매니저
        ParentOBJ = GameObject.Find("SpawnPoint1");
        ChildOBJ = ParentOBJ.transform.GetChild(0).gameObject;

        NewsBArea = GameObject.Find("NewsBArea");
    }

    public void Update()
    {

    }

    public void ShowQuest(Quest.QuestInfo npc)
    {
        grade.text = npc.questgrade.ToString();
        name.text = "[" + npc.questname + "]";
        if (IsQuestchk)
        {
            //모험가 보고서 용
            int rnd = Random.Range(0, 10);
            if (rnd > 4)
            {
                chk = true;
            }
            else
            {
                chk = false;
            }
            //보고서 용
            Result.text = chk ? "[성공]" : "[실패]"; // 성공 실패 여부 체크후 변경
        }
        else
        {
            //퀘스트 신청용
            info.text = "\"" + npc.information + "\"";
        }
        reward.text = "[골드 : " + npc.rewardgold.ToString() + "G]" + "\n" + "[평판 : " + npc.rewardfame.ToString() + "P]";
    }


    public void AddQuest() // 승낙
    {
        if (People == 0)
        {
            //마을사람
            QuestManager.Instance.PostedQuest(myQuest);
        }
        else
        {
            //모험가
            QuestManager.Instance.ProgressQuest(myQuest, ChildOBJ);
        }
        
        ChildOBJ.GetComponent<Host>().onSmile = true;
    }

    public void onDestroy()
    {
        Destroy(gameObject);
    }

    public void onQuestRfuse()
    {
        ChildOBJ.GetComponent<Host>().onAngry = true;
    }
    public void AddReward()
    {
        Debug.Log(ChildOBJ.GetComponent<Host>().Quest.gameObject);
        Destroy(ChildOBJ.GetComponent<Host>().Quest.gameObject);
        QuestManager.Instance.EndQuest(myQuest);
        if (chk)
        { // 성공시 증가 연산
            GameManager.Instance.Gold += myQuest.rewardgold;
            GameManager.Instance.Fame += myQuest.rewardfame;
            ChildOBJ.GetComponent<Host>().onSmile = true;
        }
        else
        { // 실패시 감소 연산
            GameManager.Instance.Gold -= myQuest.rewardgold;
            GameManager.Instance.Fame -= myQuest.rewardfame;
            ChildOBJ.GetComponent<Host>().onAngry = true;
        }
        //현재 퀘스트 리스트를 완료 퀘스트 리스트에 추가
        Destroy(gameObject); // 확인시 연산 후 페이지 삭제

    }

    public void onNewsBalloon() // 뉴스말풍선 생성 "새로운 퀘스트가 추가되었습니다."
    {
        GameObject obj = Instantiate(Resources.Load("UiPrefabs/NewsBalloon"), NewsBArea.transform) as GameObject;
    }
}
