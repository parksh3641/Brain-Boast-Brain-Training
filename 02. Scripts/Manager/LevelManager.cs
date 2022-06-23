using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject levelupView;

    public Text levelUpText; //���� ����
    public Text coinUpText; //���� ȹ�淮

    private int level = 0;
    private int exp = 0;

    private int defaultExp = 1000;
    private int addExp = 100;

    public PlayerDataBase playerDataBase;
    public SoundManager soundManager;


    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
    }

    public void CheckLevelUp()
    {
        //level = playerDataBase.Level;
        //exp = playerDataBase.Exp;
        //defaultExp = ValueManager.instance.GetDefaultExp();
        //addExp = ValueManager.instance.GetAddExp();

        if(defaultExp == 0 || addExp == 0)
        {
            Debug.Log("������ ����");
            return;
        }

        int needExp = defaultExp + ((level + 1) * addExp);

        if(exp >= (defaultExp * needExp))
        {
            Debug.Log("���� ��");

            OpenLevelView();

            playerDataBase.Level += 1;
            if (PlayfabManager.instance.isActive) PlayfabManager.instance.UpdatePlayerStatisticsInsert("Level", playerDataBase.Level);

            playerDataBase.Exp -= needExp;
            if (PlayfabManager.instance.isActive) PlayfabManager.instance.UpdatePlayerStatisticsInsert("Exp", playerDataBase.Exp);
        }
        else
        {
            Debug.Log("����ġ ����");

            playerDataBase.Exp += needExp;
            if (PlayfabManager.instance.isActive) PlayfabManager.instance.UpdatePlayerStatisticsInsert("Exp", playerDataBase.Exp);
        }
    }

    public void OpenLevelView()
    {
        levelupView.SetActive(true);

        levelUpText.text = level.ToString();
        coinUpText.text = "+" + level + "%";

        //soundManager.PlaySFX(SFXType.LevelUp);

    }

    public void CloseLevelView()
    {
        levelupView.SetActive(false);
    }
}
