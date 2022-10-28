using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ADQuestInfo : MonoBehaviour // ����Ʈ ��û��
{
    public GameObject ParentOBJ;
    public GameObject ChildOBJ;
    //��ũ��Ʈ QuestInformation�̶� ����κ��� ���� >> AddQuest�κ��� �� �κ����� ������ �����ϸ� �ɰͰ���. �Ŀ� ������� ���谡 �����տ� ����
    // �ٸ� ��ũ��Ʈ�� ADQuestInfo��� ��ũ��Ʈ ����Ǿ��ִ��� Ȯ��
    public Quest.QuestInfo myQuest;
    public TMP_Text Questname;
    public TMP_Text grade;
    public TMP_Text name;
    public TMP_Text info;
    public TMP_Text reward;
    public int index;

    GameObject QuestWindowArea; // AdDataWindow�� �ڽ����� �����Ǵ� UiCanvas���� �θ� ������Ʈ

    public void Start()
    {
        QuestWindowArea = GameObject.Find("QuestWindowArea");// ���谡 �������⸦ ������ �� �θ�� ���� ������Ʈ�� �̸� ������

        ShowQuest(myQuest);

        ParentOBJ = GameObject.Find("SpawnPoint1");
        ChildOBJ = ParentOBJ.transform.GetChild(0).gameObject;
    }

    public void ShowQuest(Quest.QuestInfo npc)
    {
        grade.text = npc.questgrade.ToString();
        name.text = "[" + npc.questname + "]";
        info.text = "\"" + npc.information + "\"";
        reward.text = "[��� : " + npc.rewardgold.ToString() + "G]" + "\n" + "[���� : " + npc.rewardfame.ToString() + "P]";
    }

    public void AddQuest() // �³�
    {
        QuestManager.Instance.ProgressQuest(myQuest,ChildOBJ);
        ChildOBJ.GetComponent<Host>().exitchk = true;
        ChildOBJ.GetComponent<Host>().onSmile = true;
    }

    public void onDestroy()
    {
        Destroy(gameObject);
    }

    public void onQuestRfuse()
    {
        ChildOBJ.GetComponent<Host>().onAngry = true;
        ChildOBJ.GetComponent<Host>().exitchk = true;
        QuestManager.Instance.PostedQuest(myQuest);
    }

    public void onAdDataWindow() // ���谡 ����â ����
    {
        GameObject obj = Instantiate(Resources.Load("UiPrefabs/AdDataWindow"), QuestWindowArea.transform) as GameObject;
    }
}
