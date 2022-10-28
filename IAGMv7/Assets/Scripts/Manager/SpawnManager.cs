using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public List<VLNpc.VLNPC> vlnpcs = new List<VLNpc.VLNPC>();
    public int hostCount;
    public int maxCount;
    public float spawnTime;
    public float curTime;
    public Transform spawnPoints;
    public GameObject[] VL; // Villager(마을사람)
    public GameObject[] AD; // Adventeur(모험가)
    bool Hostchk;
    public int hcount = 0;
    public int ADcount = 0;
    GameObject Host;
    public VLNpc.VLNPC[] VLarray = new VLNpc.VLNPC[3];
    public int total;
    public bool first = true;
    public enum ChairSloat
    {
        Check, NONE
    }
    public ChairSloat[] _chairsloat = new ChairSloat[] { ChairSloat.NONE, ChairSloat.NONE, ChairSloat.NONE, ChairSloat.NONE, ChairSloat.NONE, ChairSloat.NONE, ChairSloat.NONE, ChairSloat.NONE, ChairSloat.NONE, ChairSloat.NONE, ChairSloat.NONE, ChairSloat.NONE, ChairSloat.NONE };

    public ChairSloat[] chairSloat
    {
        get { return _chairsloat; }
        set { _chairsloat = value; }
    }

    public enum BedSloat
    {
        Check, None
    }
    public BedSloat[] _bedsloat = new BedSloat[] { BedSloat.None, BedSloat.None, BedSloat.None, BedSloat.None, BedSloat.None, BedSloat.None };

    public BedSloat[] bedSloat
    {
        get { return _bedsloat; }
        set { _bedsloat = value; }
    }
    private void Update()
    {
        if (curTime >= spawnTime && hostCount < maxCount)
        {
            Addvlnpc(); // 가중치 값 세팅
            if (first)
            {
                for (int i = 0; i < vlnpcs.Count; i++)
                {
                    total += vlnpcs[i].weight;
                }
                first = false;
            }
            //Debug.Log(total);
            SpawnHost();
        }
        curTime += Time.deltaTime;
    }

    public void SpawnHost()
    {
        int VLnum; // VLnum(마을사람 모델 넘버)는 5개의 모델 넘버 중 랜덤으로 결정됨 // 0~1 마을사람 2 귀족 3~4 왕족
        VLNpc.VLNPC vl;
        vl.NpcJob = RandomVLNPC().NpcJob; // 여기서 신분이 정해짐
        //vl.NpcJob = VLNpc.NPCJOB.NOBILLITY;

        if (vl.NpcJob == VLNpc.NPCJOB.COMMONS)
        {  // 0~1
            VLnum = UnityEngine.Random.Range(0, 2);
        }
        else if (vl.NpcJob == VLNpc.NPCJOB.NOBILLITY)
        {
            VLnum = 2;
        }
        else
        {
            VLnum = UnityEngine.Random.Range(3, 5);
        }
        int ADnum = UnityEngine.Random.Range(0, 7); // ADnum(모험가 모델 넘버)는 7개의 모델 넘버 중 랜덤으로 결정됨

        //마을사람 X  >> 모험가 스폰 X
        // 마을사람, 모험가 구분
        curTime = 0;
        hostCount++;
        if (hcount != 0)
        {
            int Nnum = UnityEngine.Random.Range(0, 2); // (0, 1)=마을 사람만 소환 / (1, 2)=모험가만 소환 / (0, 2)=마을사람과 모험가 랜덤으로 소환 
            if (Nnum == 0)
            { // 마을사람 생성
                // VL[0]을 npc[0]에 넣는다.
                Host = Instantiate(VL[VLnum], spawnPoints) as GameObject; // 생성될때 VLnum를 확률에 따라 조정
                Host.GetComponent<VLNpc>().job = vl.NpcJob;
                Host.GetComponent<Host>().hostchk = true;
            }
            else
            { // 모험가 생성
                Host = Instantiate(AD[ADnum], spawnPoints) as GameObject;

                int j = UnityEngine.Random.Range(0, QuestManager.Instance.RQlist.Count);
                Host.GetComponent<QuestInformation>().myQuest = QuestManager.Instance.RQlist[j];
                QuestManager.Instance.RQlist.RemoveAt(j); // RQ리스트 제거
                Destroy(QuestManager.Instance.RQuest.transform.GetChild(j).gameObject); // RQ프리팹 삭제
                hcount--;
                //Host.GetComponent<Host>().mystat = Host.GetComponent<ADNpc>().myStat; // 스탯 생성
                Host.GetComponent<Host>().hostchk = false; // 호스트말고 QuestIcon으로 넘겨주기 테스트~
            }
            Host.GetComponent<Host>().People = Nnum;
        }
        else
        { // 처음에는 마을사람 생성 됨

            GameObject Host = Instantiate(VL[VLnum], spawnPoints) as GameObject; // 생성될때 VLnum를 확률에 따라 조정
            Host.GetComponent<VLNpc>().job = vl.NpcJob;
            Host.GetComponent<Host>().hostchk = true;
            Host.GetComponent<Host>().People = 0;
        }
    }
    public void Teleport(GameObject host)
    {
        host.transform.position = this.spawnPoints.transform.position;
        host.transform.parent = spawnPoints.transform;
    }

    public void Addvlnpc() // 가중치 랜덤 값 세팅
    {
        //가중치 0이하로 X
        if (GameManager.Instance.Fame >= 0)
        {
            VLarray[0].NpcJob = VLNpc.NPCJOB.COMMONS;
            VLarray[1].NpcJob = VLNpc.NPCJOB.NOBILLITY;
            VLarray[2].NpcJob = VLNpc.NPCJOB.ROYALTY;

            VLarray[0].weight = 100;
            VLarray[1].weight = 0;

            for (int i = 0; i < (GameManager.Instance.Fame / 100); i++)
            {
                if (VLarray[0].weight != 0)
                {
                    VLarray[0].weight -= 5;
                    VLarray[1].weight += ((VLarray[1].weight >= 20) ? 4 : 5);
                }
                else
                {
                    if (VLarray[1].weight != 0)
                    {
                        VLarray[1].weight -= 4;
                    }
                    else if (VLarray[1].weight == 0)
                    {
                        break;
                    }
                }
            }
            VLarray[2].weight = 100 - (VLarray[0].weight + VLarray[1].weight);
        }
        vlnpcs.Clear();
        for (int i = 0; i < 3; i++)
        {
            vlnpcs.Add(VLarray[i]);
        }
    }

    public VLNpc.VLNPC RandomVLNPC()//가중치 랜덤
    {
        int weight = 0;
        int selectNum = 0;

        selectNum = Mathf.RoundToInt(total * UnityEngine.Random.Range(0.0f, 1.0f));
        //Debug.Log(selectNum);
        for (int i = 0; i < vlnpcs.Count; i++)
        {
            weight += vlnpcs[i].weight;
            //Debug.Log(weight);
            if (selectNum <= weight)
            {
                VLNpc.VLNPC temp = new VLNpc.VLNPC(vlnpcs[i]);
                return temp;
            }
        }
        return default;
    }
}
