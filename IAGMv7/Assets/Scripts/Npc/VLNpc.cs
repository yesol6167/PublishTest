using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using static Quest;
using static CharState;
using static VLNpc;
using System;

public class VLNpc : MonoBehaviour
{
    public NPCJOB job;
    public Quest.QuestInfo myQuest;
    public enum NPCJOB
    {
        COMMONS, NOBILLITY, ROYALTY
    }

    [Serializable]
    public struct VLNPC
    {
        [SerializeField] public string Name;
        [SerializeField] public NPCJOB NpcJob;
        [SerializeField] public Quest.QuestInfo myQuestInfo; // ������ �� ���� ���� ���� ����Ʈ �й�
        [SerializeField] public int weight;

        public VLNPC(VLNPC vlnpc)
        {
            this.Name = vlnpc.Name;
            this.NpcJob = vlnpc.NpcJob;
            this.myQuestInfo = vlnpc.myQuestInfo;
            this.weight = vlnpc.weight;
        }
    }

    public static VLNPC RandomQuest(NPCJOB job)
    {
        //������� F~D ���� C~B ���� A ����Ʈ �����Ŵ����� ������Ű��
        //�迭 ����
        Quest.QuestGRADE[] C_RandomSet = new Quest.QuestGRADE[] { Quest.QuestGRADE.D, Quest.QuestGRADE.E, Quest.QuestGRADE.F };
        Quest.QuestGRADE[] N_RandomSet = new Quest.QuestGRADE[] { Quest.QuestGRADE.C, Quest.QuestGRADE.B };
        Quest.QuestGRADE[] R_RandomSet = new Quest.QuestGRADE[] { Quest.QuestGRADE.A };

        VLNPC Qnpc = new VLNPC();
        int n = UnityEngine.Random.Range(0, 2);

        Qnpc.NpcJob = job;
        switch (Qnpc.NpcJob) // �źп� ���� ���̽��� ��������
        {
            case VLNpc.NPCJOB.COMMONS:
                {
                    // F/E/D
                    switch (C_RandomSet[W_Random(C_RandomSet)])
                    {
                        case Quest.QuestGRADE.F:
                            switch (n)
                            {
                                case 0:
                                    Qnpc.Name = "���";
                                    Qnpc.myQuestInfo.questname = "ä��";
                                    Qnpc.myQuestInfo.questgrade = Quest.QuestGRADE.F;
                                    Qnpc.myQuestInfo.information = "���ʸ� �����ּ���!";
                                    Qnpc.myQuestInfo.rewardgold = 10;
                                    Qnpc.myQuestInfo.rewardfame = 200;
                                    break;
                                case 1:
                                    Qnpc.Name = "���";
                                    Qnpc.myQuestInfo.questname = "��ġ";
                                    Qnpc.myQuestInfo.questgrade = Quest.QuestGRADE.F;
                                    Qnpc.myQuestInfo.information = "���ǿ� �ʿ��� ����� ������ 10������ ��ġ�� �ּ���!";
                                    Qnpc.myQuestInfo.rewardgold = 10;
                                    Qnpc.myQuestInfo.rewardfame = 200;
                                    break;
                            }
                            break;
                        case Quest.QuestGRADE.E:
                            switch (n)
                            {
                                case 0:
                                    Qnpc.Name = "���";
                                    Qnpc.myQuestInfo.questname = "ä��";
                                    Qnpc.myQuestInfo.questgrade = Quest.QuestGRADE.E;
                                    Qnpc.myQuestInfo.information = "���� 5���� �����ּ���!";
                                    Qnpc.myQuestInfo.rewardgold = 20;
                                    Qnpc.myQuestInfo.rewardfame = 200;
                                    break;
                                case 1:
                                    Qnpc.Name = "���";
                                    Qnpc.myQuestInfo.questname = "��ġ";
                                    Qnpc.myQuestInfo.questgrade = Quest.QuestGRADE.E;
                                    Qnpc.myQuestInfo.information = "���� ���������� ��� 10������ ��ġ�� �ּ���!";
                                    Qnpc.myQuestInfo.rewardgold = 20;
                                    Qnpc.myQuestInfo.rewardfame = 200;
                                    break;
                            }
                            break;
                        case Quest.QuestGRADE.D:
                            switch (n)
                            {
                                case 0:
                                    Qnpc.Name = "���";
                                    Qnpc.myQuestInfo.questname = "ä��";
                                    Qnpc.myQuestInfo.questgrade = Quest.QuestGRADE.D;
                                    Qnpc.myQuestInfo.information = "�������� ������ 3�� �����ּ���!";
                                    Qnpc.myQuestInfo.rewardgold = 30;
                                    Qnpc.myQuestInfo.rewardfame = 200;
                                    break;
                                case 1:
                                    Qnpc.Name = "���";
                                    Qnpc.myQuestInfo.questname = "��ġ";
                                    Qnpc.myQuestInfo.questgrade = Quest.QuestGRADE.D;
                                    Qnpc.myQuestInfo.information = "�̱ÿ� �� �������� 15������ ��ġ�� �ּ���!";
                                    Qnpc.myQuestInfo.rewardgold = 30;
                                    Qnpc.myQuestInfo.rewardfame = 200;
                                    break;
                            }
                            break;
                    }
                }
                break;
            case VLNpc.NPCJOB.NOBILLITY:
                {
                    // B/C
                    switch (N_RandomSet[W_Random(N_RandomSet)])
                    {
                        case Quest.QuestGRADE.C:
                            switch (n)
                            {
                                case 0:
                                    Qnpc.Name = "����";
                                    Qnpc.myQuestInfo.questname = "ä��";
                                    Qnpc.myQuestInfo.questgrade = Quest.QuestGRADE.C;
                                    Qnpc.myQuestInfo.information = "�̳�Ÿ��ν��� ���� �����ּ���!";
                                    Qnpc.myQuestInfo.rewardgold = 50;
                                    Qnpc.myQuestInfo.rewardfame = 500;
                                    break;
                                case 1:
                                    Qnpc.Name = "����";
                                    Qnpc.myQuestInfo.questname = "��ġ";
                                    Qnpc.myQuestInfo.questgrade = Quest.QuestGRADE.C;
                                    Qnpc.myQuestInfo.information = "����� ��ġ�� ��ũ 15������ ��ġ�� �ּ���!";
                                    Qnpc.myQuestInfo.rewardgold = 50;
                                    Qnpc.myQuestInfo.rewardfame = 500;
                                    break;
                            }
                            break;
                        case Quest.QuestGRADE.B:
                            switch (n)
                            {
                                case 0:
                                    Qnpc.Name = "����";
                                    Qnpc.myQuestInfo.questname = "ä��";
                                    Qnpc.myQuestInfo.questgrade = Quest.QuestGRADE.B;
                                    Qnpc.myQuestInfo.information = "��Ż���� ���� 3�� �����ּ���!";
                                    Qnpc.myQuestInfo.rewardgold = 100;
                                    Qnpc.myQuestInfo.rewardfame = 1000;
                                    break;
                                case 1:
                                    Qnpc.Name = "����";
                                    Qnpc.myQuestInfo.questname = "��ġ";
                                    Qnpc.myQuestInfo.questgrade = Quest.QuestGRADE.B;
                                    Qnpc.myQuestInfo.information = "�̱��� ��Ű�� ��Ż���� 15������ ��ġ�� �ּ���!";
                                    Qnpc.myQuestInfo.rewardgold = 100;
                                    Qnpc.myQuestInfo.rewardfame = 1000;
                                    break;
                            }
                            break;
                    }
                }
                break;
            case VLNpc.NPCJOB.ROYALTY:
                {
                    //A
                    switch (R_RandomSet[W_Random(R_RandomSet)])
                    {
                        case Quest.QuestGRADE.A:
                            switch (n)
                            {
                                case 0:
                                    Qnpc.Name = "����";
                                    Qnpc.myQuestInfo.questname = "ä��";
                                    Qnpc.myQuestInfo.questgrade = Quest.QuestGRADE.A;
                                    Qnpc.myQuestInfo.information = "�������� ���� 1�� �����ּ���!";
                                    Qnpc.myQuestInfo.rewardgold = 300;
                                    Qnpc.myQuestInfo.rewardfame = 300;
                                    break;
                                case 1:
                                    Qnpc.Name = "����";
                                    Qnpc.myQuestInfo.questname = "��ġ";
                                    Qnpc.myQuestInfo.questgrade = Quest.QuestGRADE.A;
                                    Qnpc.myQuestInfo.information = "������ �ĵ��� �巡���� ��ġ�� �ּ���!";
                                    Qnpc.myQuestInfo.rewardgold = 300;
                                    Qnpc.myQuestInfo.rewardfame = 300;
                                    break;
                            }
                            break;
                    }
                }
                break;
        }
        return Qnpc;
    }

    void Start()
    {
        myQuest = RandomQuest(job).myQuestInfo; // ��������Ʈ �Լ��� myQuestInfo��� ������ ��������Ʈ�� ���� -> ��������Ʈ ������ �ν����Ϳ� ����
    }

    public void AddQuest() // �³�
    {
        QuestManager.Instance.PostedQuest(myQuest);
    }

    public static int W_Random(Quest.QuestGRADE[] RandomSet) // ����ġ ������ �Լ�
    {
        int Lenght = RandomSet.Length;
        int fame = GameManager.Instance.Fame;
        int[] weights = new int[Lenght];

        switch (Lenght)
        {
            case 3:
                {
                    if (fame >= 0)
                    {
                        weights[0] = 100;
                        weights[1] = 0;

                        for (int i = 0; i < (fame / 100); i++)
                        {
                            if (weights[0] != 0)
                            {
                                weights[0] -= 5;
                                weights[1] += ((weights[1] >= 20) ? 4 : 5);
                            }
                            else
                            {
                                if (weights[1] != 0)
                                {
                                    weights[1] -= 4;
                                }
                                else if (weights[1] == 0)
                                {
                                    break;
                                }
                            }
                        }
                        weights[2] = 100 - (weights[0] + weights[1]);
                    }
                }
                break;
            case 2:
                {
                    if (fame >= 0)
                    {
                        weights[0] = 100;
                        for (int i = 0; i < (fame / 100); i++)
                        {
                            if (weights[0] == 0)
                            {
                                break;
                            }
                            weights[0] -= 1;
                        }
                        weights[1] = 100 - weights[0];
                    }
                }
                break;
            case 1:
                {
                    weights[1] = 100;
                }
                break;
        }
        // total�� 100�ǰ� ����
        int total = 0;
        for (int t = 0; t < weights.Length; ++t)
        { total += weights[t]; }

        // ��������Ʈ ����
        int pivot = Mathf.RoundToInt(total * UnityEngine.Random.Range(0.0f, 1.0f));

        // ��������Ʈ�� �ش��ϴ� �迭�� ��ġ�� ����
        for (int n = 0; n < weights.Length; ++n)
        {
            if (pivot <= weights[n])
            {
                return n; // �迭�� [n]��° ���� ��ȯ 
            }
            else
            {
                pivot -= weights[n];
            }
        }
        return 0; // �ǹ̾��� ������ b�� ��ȯ��
        // ���� ��ȯ�ϴ� �Լ��� ��� �ڵ� ��ο� return���� �־���� / ������ ����
    }
}
