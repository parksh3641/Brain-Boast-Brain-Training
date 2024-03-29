using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class ComboManager : MonoBehaviour
{
    GamePlayType gamePlayType = GamePlayType.GameChoice1;

    [Title("Main")]
    public GameObject comboObject;
    public Image fillamount;
    public Text comboText;
    public Notion notion;

    [Title("WaitUI")]
    public GameObject waitObject;
    public Text waitNotionText;
    public Image waitFillAmount;

    [Title("Animation")]
    public BarAnimation barAnimation;

    private int combo = 0;
    private int bestCombo = 0;

    private float timer = 0;
    private float comboTimer = 0;

    private float waitTimer = 0;
    private float waitSaveTimer = 0;

    private float critical = 0;

    bool pause = false;

    PlayerDataBase playerDataBase;
    UpgradeDataBase upgradeDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
        if (upgradeDataBase == null) upgradeDataBase = Resources.Load("UpgradeDataBase") as UpgradeDataBase;

        comboText.text = "";
        fillamount.fillAmount = 0;

        combo = 0;

        notion.gameObject.SetActive(false);
        comboObject.SetActive(false);

        GameManager.eGamePause += GamePause;
    }

    private void Start()
    {
        waitObject.SetActive(false);

        comboTimer = ValueManager.instance.GetComboTime();

        StartCoroutine(TimerCoroutine());
    }

    public void SetBestCombo(int number)
    {
        bestCombo = number;

        comboTimer = ValueManager.instance.GetComboTime();

        if (GameStateManager.instance.Combo) comboTimer += 0.5f;

        float addComboTime = playerDataBase.ComboTimeLevel * 0.02f;

        comboTimer += addComboTime;

        critical = upgradeDataBase.GetValue(UpgradeType.ComboCritical, playerDataBase.ComboCriticalLevel);
    }

    public void OnStartCombo()
    {
        comboObject.SetActive(true);

        if (timer == 0)
        {
            if (GameStateManager.instance.PlayGame) combo = 0;
        }

        CheckPlusCombo();

        timer = comboTimer;
        fillamount.fillAmount = 1;
        comboText.text = LocalizationManager.instance.GetString("Combo") + " : " + combo.ToString();

        if(combo % 50 == 0)
        {
            barAnimation.PlayAnimation();
        }

        if (bestCombo != 0)
        {
            if (combo > bestCombo)
            {
                comboText.resizeTextMaxSize = 80;
                comboText.color = new Color(1, 0, 0);
            }
            else
            {
                comboText.resizeTextMaxSize = 60;
                comboText.color = new Color(1, 150 / 255f, 0);
            }
        }

        notion.gameObject.SetActive(false);
        notion.txt.text = "+" + 1.ToString();
        notion.gameObject.SetActive(true);
    }

    void CheckPlusCombo()
    {
        float random = Random.Range(0, 100f);

        if (random <= critical)
        {
            combo += 2;
        }
        else
        {
            combo += 1;
        }
    }

    public void OnStopCombo()
    {
        StopAllCoroutines();
        StartCoroutine(TimerCoroutine());

        pause = false;

        comboObject.SetActive(false);
        waitObject.SetActive(false);

        timer = 0;
        fillamount.fillAmount = 0;
    }

    public int GetCombo()
    {
        comboText.text = "";

        return combo;
    }

    void GamePause()
    {
        if(pause)
        {
            pause = false;
        }
        else
        {
            pause = true;
        }
    }

    public void WaitNotionUI(GamePlayType type)
    {
        gamePlayType = type;

        StartCoroutine(WaitNotionUICorution());
    }


    #region Coroution
    IEnumerator TimerCoroutine()
    {
        if (!pause)
        {
            if (timer > 0)
            {
                timer -= 0.01f;

                if(GameStateManager.instance.PlayGame) fillamount.fillAmount = timer / comboTimer;
            }
            else
            {
                if (GameStateManager.instance.PlayGame)
                {
                    combo = 0;

                    if (comboObject.activeInHierarchy)
                    {
                        comboText.text = LocalizationManager.instance.GetString("Combo") + " : 0";
                    }
                }
            }
        }

        yield return new WaitForSeconds(0.01f);
        StartCoroutine(TimerCoroutine());
    }

    IEnumerator WaitNotionUICorution()
    {
        switch (GameStateManager.instance.GameModeType)
        {
            case GameModeType.Easy:
                waitTimer = ValueManager.instance.GetFilpCardRememberTime();
                break;
            case GameModeType.Normal:
                waitTimer = ValueManager.instance.GetFilpCardRememberTime() * 1.5f;
                break;
            case GameModeType.Hard:
                waitTimer = ValueManager.instance.GetFilpCardRememberTime() * 2f;
                break;
            case GameModeType.Perfect:
                waitTimer = ValueManager.instance.GetFilpCardRememberTime();
                break;
        }
        //waitTimer = ValueManager.instance.GetFilpCardRememberTime();
        waitSaveTimer =  waitTimer;

        waitFillAmount.fillAmount = 1;
        waitObject.SetActive(true);
        waitNotionText.text = LocalizationManager.instance.GetString("WaitNotion_" + gamePlayType);

        while (waitFillAmount.fillAmount > 0)
        {
            waitFillAmount.fillAmount -= 1 * Time.smoothDeltaTime / waitTimer;

            yield return null;
        }

        waitObject.SetActive(false);
    }


    #endregion
}
