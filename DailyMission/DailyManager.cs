using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyManager : MonoBehaviour
{
    public GameObject dailyView;

    public DailyContent[] dailyContents;



    List<int> missionIndexs = new List<int>();
    Dictionary<string, string> dailyMissionData = new Dictionary<string, string>();


    DailyMissionList dailyMissionList;
    PlayerDataBase playerDataBase;


    private void Awake()
    {
        if (dailyMissionList == null) dailyMissionList = Resources.Load("DailyMissionList") as DailyMissionList;
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        dailyView.SetActive(false);
    }

    public void OpenDaily()
    {
        if (!dailyView.activeSelf)
        {
            dailyView.SetActive(true);

            CheckDailyMission();
        }
        else
        {
            dailyView.SetActive(false);
        }
    }

    public void CheckDailyMission()
    {
        int number = PlayerPrefs.GetInt(System.DateTime.Today.ToString());

        if(number == 0)
        {
            Debug.Log("New DailyMission Appeard!");

            RandomDailyMission();
        }
        else
        {
            Debug.Log("Now DailyMission");

            LoadDailyMission();
        }
    }

    void RandomDailyMission()
    {
        PlayerPrefs.SetInt(System.DateTime.Today.ToString(), 1);

        UnDuplicateRandom(0, dailyMissionList.dailyMissions.Length);
    }

    void UnDuplicateRandom(int min, int max)
    {
        int currentNumber = Random.Range(min, max);
        missionIndexs.Clear();

        for (int i = 0; i < 3;)
        {
            if (missionIndexs.Contains(currentNumber))
            {
                currentNumber = Random.Range(min, max);
            }
            else
            {
                missionIndexs.Add(currentNumber);
                i++;
            }
        }

        for (int i = 0; i < missionIndexs.Count; i++)
        {
            dailyContents[i].Initialize(dailyMissionList.dailyMissions[missionIndexs[i]],this);

            //playerDataBase.SetDailyMission(dailyMissionList.dailyMissions[missionIndexs[i]]);


            //���� ����

            DailyMissionJson dailyMissionJson = new DailyMissionJson();

            dailyMissionJson.gamePlayType = dailyMissionList.dailyMissions[missionIndexs[i]].gamePlayType;
            dailyMissionJson.missionType = dailyMissionList.dailyMissions[missionIndexs[i]].missionType;
            dailyMissionJson.goal = dailyMissionList.dailyMissions[missionIndexs[i]].goal;
            dailyMissionJson.isClear = false;
 
            dailyMissionData.Clear();
            dailyMissionData.Add("DailyMission_" + i, JsonUtility.ToJson(dailyMissionJson));

            if(PlayfabManager.instance.isActive) PlayfabManager.instance.SetPlayerData(dailyMissionData);
        }
    }


    void LoadDailyMission() //�����ͺ��̽����� �ε�
    {
        for(int i = 0; i < dailyContents.Length; i ++)
        {
            //dailyContents[i].Initialize(playerDataBase.GetDailyMission(i),this);
        }

        UpdateDailyMission();
    }

    public void UpdateDailyMission() //���� ������ ���� üũ
    {
        //for(int i = 0; i < dailyContents.Length; i ++)
        //{
        //    if(int.Parse(dailyContents[i].goalText.text) >= playerDataBase.GetDailyMissionGoal(i))
        //    {
        //        if(!playerDataBase.GetCheckReardDailyMission(i))
        //        {
        //            //��ǥ ����!

        //            dailyContents[i].SuccessDaily();
        //        }
        //        else
        //        {
        //            //�̹� ����

        //            dailyContents[i].NowReceived();
        //        }
        //    }
        //}
    }

    public void Receive(DailyMission dailyMission)
    {
        for (int i = 0; i < dailyMission.rewardCount; i++)
        {
            switch (dailyMission.missionRewards[i].rewardType)
            {
                case RewardType.Coin:


                    break;
                case RewardType.Crystal:


                    break;
                case RewardType.Exp:


                    break;
            }
        }    

        //�����ͺ��̽��� �޾Ҵٰ� �����������

        //�������� �޾Ҵٰ� �����������
    }
}
