using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static CharState;


//�޸� ������ Ŭ����(������ �÷��� �ʿ�), �ӵ��� ����ü(���� �����÷ο� �߻� ����)
public class QuestManager : Singleton<QuestManager>
{
    public List<Quest.QuestInfo> RQlist = new List<Quest.QuestInfo>(); //����X ����Ʈ ���
    public List<Quest.QuestInfo> PQlist = new List<Quest.QuestInfo>(); //����O ����Ʈ ���
    public List<Quest.QuestInfo> FQlist = new List<Quest.QuestInfo>(); //�Ϸ�� ����Ʈ ���
    public GameObject RQuest; // ���� ��ġ
    public GameObject PQuest; // ���� ��ġ
    public GameObject FQuest; // ���� ��ġ
    GameObject RQ;
    GameObject PQ;
    GameObject FQ;
    TMP_Text grade;
    TMP_Text name;
    TMP_Text info;
    TMP_Text reward;
    public Quest.QuestInfo npcQuest;

    List<Quest> questlist = new List<Quest>();
    void Start()
    {

    }
    //���� ����Ʈ ��� >> ���� ��� ������ Ŭ�� ��
    void Update()
    {

    }
    public void PostedQuest(Quest.QuestInfo npc)
    {
        RQlist.Add(npc);
        SpawnManager.Instance.hcount++;

        GameObject RQ = Instantiate(Resources.Load("Prefabs/PostQuest"), RQuest.transform) as GameObject;
        RQ.GetComponentInChildren<QuestInformation>().Questname.text = npc.questname;

    }

    public void ReShow()
    {
        Debug.Log("����Ʈ Ŭ����");
    }


    public void ProgressQuest(Quest.QuestInfo npc, GameObject host)
    {
        SpawnManager.Instance.hcount--;
        SpawnManager.Instance.ADcount++;
        PQlist.Add(npc);
        GameObject PQ = Instantiate(Resources.Load("Prefabs/ProgressQuest"), PQuest.transform) as GameObject; // PQ�� ȣ��Ʈ 
        host.GetComponent<Host>().Quest = PQ;
        PQ.GetComponentInChildren<QuestInformation>().Questname.text = npc.questname;

    }

    public void EndQuest(Quest.QuestInfo npc)
    {
        SpawnManager.Instance.ADcount--;
        FQlist.Add(npc);
        GameObject FQ = Instantiate(Resources.Load("Prefabs/FinishQuest"), FQuest.transform) as GameObject;
        FQ.GetComponentInChildren<QuestInformation>().Questname.text = npc.questname;
    }



    //���谡���� �� ����Ʈ ��� >> ���� ����Ʈ ��Ͽ��� ���谡���� ����Ʈ �й�
    //�Ϸ�� ����Ʈ ��� >> ���谡 ��湮�� ����Ʈ ���� ���� ���� Ȯ�ο� ���� �Ϸ�
}
