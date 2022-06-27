using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Text profileLevelText;
    public Image profileFillamount;

    public GameObject levelupView;

    public Text levelUpText; //���� ����
    public Text coinUpText; //���� ȹ�淮

    private int level = 0;
    private int exp = 0;

    private float defaultExp = 1000;
    private float addExp = 100;

    public PlayerDataBase playerDataBase;
    public SoundManager soundManager;


    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        levelupView.SetActive(false);
    }

    public void Initialize()
    {
        profileLevelText.text = "Lv." + (playerDataBase.Level + 1);

        profileFillamount.fillAmount = playerDataBase.Exp / CheckNeedExp();
    }

    public void CheckLevelUp(float getExp)
    {
        level = playerDataBase.Level;
        exp = playerDataBase.Exp;
        int plusExp = (int)getExp;
        if (defaultExp == 0 || addExp == 0)
        {
            Debug.Log("������ ����");
            return;
        }

        if(exp + plusExp >= CheckNeedExp())
        {
            Debug.Log("���� ��");

            OpenLevelView();

            playerDataBase.Level += 1;
            if (PlayfabManager.instance.isActive) PlayfabManager.instance.UpdatePlayerStatisticsInsert("Level", playerDataBase.Level);

            playerDataBase.Exp -= ((int)CheckNeedExp() - plusExp);
            if (PlayfabManager.instance.isActive) PlayfabManager.instance.UpdatePlayerStatisticsInsert("Exp", playerDataBase.Exp);
        }
        else
        {
            Debug.Log("����ġ ����");

            playerDataBase.Exp += plusExp;
            if (PlayfabManager.instance.isActive) PlayfabManager.instance.UpdatePlayerStatisticsInsert("Exp", playerDataBase.Exp);
        }

        Initialize();
    }

    float CheckNeedExp()
    {
        defaultExp = ValueManager.instance.GetDefaultExp();
        addExp = ValueManager.instance.GetAddExp();

        float needExp = defaultExp + (level * addExp);

        return needExp;
    }

    [Button]
    public void OpenLevelView()
    {
        levelupView.SetActive(true);

        levelUpText.text = (level + 1).ToString();
        coinUpText.text = LocalizationManager.instance.GetString("Bouns") + " + " + (level + 1) + "%";

        soundManager.PlaySFX(GameSfxType.LevelUp);

    }

    public void CloseLevelView()
    {
        levelupView.SetActive(false);
    }
}
