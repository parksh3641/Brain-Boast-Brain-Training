using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AchievementData
{
    public GamePlayType achievementType = GamePlayType.GameChoice1;
    public List<int> achievementList = new List<int>();
}
public class AchievementManager : MonoBehaviour
{
    AchievementData achievementData = new AchievementData();

    public AchievementContent achievementContent;
    public Transform achievementTransform;







    public PlayerDataBase playerDataBase;

    private List<AchievementContent> achievementContentList = new List<AchievementContent>();


    private Dictionary<string, string> playerData = new Dictionary<string, string>();

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        for(int i =0; i < 20; i ++)
        {
            AchievementContent monster = Instantiate(achievementContent) as AchievementContent;
            monster.transform.parent = achievementTransform;
            monster.transform.localPosition = Vector3.zero;
            monster.transform.localRotation = Quaternion.identity;
            monster.transform.localScale = Vector3.one;
            monster.gameObject.SetActive(false);

            achievementContentList.Add(monster);
        }
    }

    private void Start()
    {
        OnReset();

        OnCreateContent();
    }

    public void OpenAchievement()
    {
        Social.ShowAchievementsUI();
    }

    public void AddAchieveContentData(int number) //�Ű������� ���� �־�ߵǰ�
    {
        //�����ͺ��̽����� �ε�

        //�� ����


        //�����ͺ��̽��� ����

        //save to playfab ����
    }

    public void OnReset()
    {
        achievementData.achievementType = GamePlayType.GameChoice1;

        achievementData.achievementList.Clear();
    }

    public void SaveToPlayfab()
    {
        Debug.Log("Save to Playfab");
        playerData.Add(achievementData.achievementType.ToString(), JsonUtility.ToJson(achievementData));

        if (PlayfabManager.instance.isActive) PlayfabManager.instance.SetPlayerData(playerData);
    }


    public void OnCreateContent()
    {
        GamePlayType gamePlayType = GamePlayType.GameChoice1;

        //for(int i = 0; i < System.Enum.GetValues(typeof(GamePlayType)).Length; i ++)
        //{

        //}

        for(int i = 0; i < 4; i ++)
        {
            achievementContentList[i].Initialized(gamePlayType + i);
            achievementContentList[i].SetPerfectMode(playerDataBase.GetPerfectMode(gamePlayType + i));
            achievementContentList[i].gameObject.SetActive(true);
        }
    }
}
