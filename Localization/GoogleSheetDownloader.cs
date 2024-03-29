using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GoogleSheetDownloader : MonoBehaviour
{
    const string LocalizationURL = "https://docs.google.com/spreadsheets/d/1nTQjgAQ631ayvzsWQeXt0PwTpneVV5sPs173vgpg05w/export?format=tsv&gid=0";
    const string ValueURL = "https://docs.google.com/spreadsheets/d/1nTQjgAQ631ayvzsWQeXt0PwTpneVV5sPs173vgpg05w/export?format=tsv&gid=1957583039";
    const string BadWordURL = "https://docs.google.com/spreadsheets/d/1nTQjgAQ631ayvzsWQeXt0PwTpneVV5sPs173vgpg05w/export?format=tsv&gid=582114712";
    const string UpgradeURL = "https://docs.google.com/spreadsheets/d/1nTQjgAQ631ayvzsWQeXt0PwTpneVV5sPs173vgpg05w/export?format=tsv&gid=374819877";

    public bool isActive = false;
    public bool isLocalization = false;
    public bool isDownload = false;

    public float percent = 0;

    public Text messageText;
    public Text barPercentText;

    public GameObject bar;
    public Image barFillAmount;

    public LoginManager loginManager;

    LocalizationDataBase localizationDataBase;
    ValueDataBase valueDataBase;
    UpgradeDataBase upgradeDataBase;

    private void Awake()
    {
        Time.timeScale = 1;

        messageText.text = "";

        bar.SetActive(true);
        barFillAmount.fillAmount = 0f;
        barPercentText.text = "0%";

        if (localizationDataBase == null) localizationDataBase = Resources.Load("LocalizationDataBase") as LocalizationDataBase;
        if (valueDataBase == null) valueDataBase = Resources.Load("ValueDataBase") as ValueDataBase;
        if (upgradeDataBase == null) upgradeDataBase = Resources.Load("UpgradeDataBase") as UpgradeDataBase;

        if (!Directory.Exists(SystemPath.GetPath()))
        {
            Directory.CreateDirectory(SystemPath.GetPath());
        }

        StartCoroutine(LoadingCoroution());

        SyncFile();
    }

    [Button]
    void GoogleSheetDownload()
    {
        PlayerPrefs.SetInt("Repair", 1);
    }

    IEnumerator LoadingCoroution()
    {
        if(!isLocalization)
        {
            if(percent <= 0.5f)
            {
                percent += 0.01f;
                barFillAmount.fillAmount = percent;
                barPercentText.text = ((int)(percent * 100)).ToString() + "%";
                messageText.text = "Downloading...";
            }
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(LoadingCoroution());
        }
        else
        {
            messageText.text = LocalizationManager.instance.GetString("Downloading");
            yield return new WaitForSeconds(0.5f);
            messageText.text = LocalizationManager.instance.GetString("Downloading") + ".";
            yield return new WaitForSeconds(0.5f);
            messageText.text = LocalizationManager.instance.GetString("Downloading") + "..";
            yield return new WaitForSeconds(0.5f);
            messageText.text = LocalizationManager.instance.GetString("Downloading") + "...";
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(LoadingCoroution());
        }
    }

    void CheckFile()
    {
        //int repair = PlayerPrefs.GetInt("Repair");

        //if (!File.Exists(SystemPath.GetPath() + "Localization.txt") || repair == 1)
        //{
        //    Debug.Log("Localization File Downloading...");

        //    UnityWebRequest www = UnityWebRequest.Get(LocalizationURL);
        //    yield return www.SendWebRequest();

        //    File.WriteAllText(SystemPath.GetPath() + "Localization.txt", www.downloadHandler.text);

        //    SetLocalization(www.downloadHandler.text);

        //    Debug.Log("Localization File Download Complete!");

        //    CheckPercent(50);
        //}
        //else
        //{
        //    StreamReader reader = new StreamReader(SystemPath.GetPath() + "Localization.txt");
        //    string value = reader.ReadToEnd();
        //    reader.Close();
        //    SetLocalization(value);
        //    Debug.Log("Localization File is exists");

        //    CheckPercent(50);
        //}

        //    isLocalization = true;

        //if (!File.Exists(SystemPath.GetPath() + "Value.txt") || repair == 1)
        //{
        //    Debug.Log("Value File Downloading...");
        //    UnityWebRequest www2 = UnityWebRequest.Get(ValueURL);
        //    yield return www2.SendWebRequest();

        //    File.WriteAllText(SystemPath.GetPath() + "Value.txt", www2.downloadHandler.text);

        //    SetValue(www2.downloadHandler.text);
        //    Debug.Log("Value File Download Complete!");

        //    CheckPercent(75);
        //}
        //else
        //{
        //    StreamReader reader = new StreamReader(SystemPath.GetPath() + "Value.txt");
        //    string value = reader.ReadToEnd();
        //    reader.Close();
        //    SetValue(value);
        //    Debug.Log("Value File is exists");

        //    CheckPercent(75);
        //}

        //if (!File.Exists(SystemPath.GetPath() + "Upgrade.txt") || repair == 1)
        //{
        //    Debug.Log("Upgrade File Downloading...");
        //    UnityWebRequest www3 = UnityWebRequest.Get(UpgradeURL);
        //    yield return www3.SendWebRequest();

        //    File.WriteAllText(SystemPath.GetPath() + "Upgrade.txt", www3.downloadHandler.text);

        //    SetUpgrade(www3.downloadHandler.text);
        //    Debug.Log("Upgrade File Download Complete!");

        //    CheckPercent(100);
        //}
        //else
        //{
        //    StreamReader reader = new StreamReader(SystemPath.GetPath() + "Upgrade.txt");
        //    string value = reader.ReadToEnd();
        //    reader.Close();
        //    SetUpgrade(value);
        //    Debug.Log("Upgrade File is exists");

        //    CheckPercent(100);
        //}

        //if (!File.Exists(SystemPath.GetPath() + "BadWord.txt"))
        //{
        //    Debug.Log("BadWord File Downloading...");
        //    UnityWebRequest www3 = UnityWebRequest.Get(BadWordURL);
        //    yield return www3.SendWebRequest();
        //    File.WriteAllText(SystemPath.GetPath() + "BadWord.txt", www3.downloadHandler.text);
        //    Debug.Log("BadWord File Download Complete!");
        //}
        //else
        //{
        //    Debug.Log("BadWord File is exists");
        //}

        //PlayerPrefs.SetInt("Repair", 0);

        TextAsset textAsset = Resources.Load<TextAsset>("Localization");
        SetLocalization(textAsset.ToString());

        TextAsset textAsset2 = Resources.Load<TextAsset>("Value");
        SetValue(textAsset2.ToString());

        TextAsset textAsset3 = Resources.Load<TextAsset>("Upgrade");
        SetUpgrade(textAsset3.ToString());

        bar.SetActive(false);

        loginManager.NowLoaded();

        isActive = true;
    }

    void SetLocalization(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;
        //int columnSize = row[0].Split('\t').Length;

        for (int i = 1; i < rowSize; i ++)
        {
            string[] column = row[i].Split('\t');
            LocalizationData content = new LocalizationData();

            content.key = column[0];
            content.korean = column[1].Replace('#','\n');
            content.english = column[2].Replace('#', '\n');
            content.japanese = column[3].Replace('#', '\n');
            content.chinese = column[4].Replace('#', '\n');
            content.indian = column[5].Replace('#', '\n');
            content.portuguese = column[6].Replace('#', '\n');
            content.russian = column[7].Replace('#', '\n');
            content.german = column[8].Replace('#', '\n');
            content.spanish = column[9].Replace('#', '\n');
            content.arabic = column[10].Replace('#', '\n');
            content.bengali = column[11].Replace('#', '\n');
            content.indonesian = column[12].Replace('#', '\n');
            content.italian = column[13].Replace('#', '\n');
            content.dutch = column[14].Replace('#', '\n');

            localizationDataBase.SetLocalization(content);
        }
    }

    void SetValue(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;

        for (int i = 1; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');

            float value = float.Parse(column[1]);

            switch (column[0])
            {
                case "AdCoolTime":
                    valueDataBase.AdCoolTime = value;
                    break;
                case "ReadyTime":
                    valueDataBase.ReadyTime = value;
                    break;
                case "GamePlayTime":
                    valueDataBase.GamePlayTime = value;
                    break;
                case "ComboTime":
                    valueDataBase.ComboTime = value;
                    break;
                case "MoleNextTime":
                    valueDataBase.MoleNextTime = value;
                    break;
                case "MoleCatchTime":
                    valueDataBase.MoleCatchTime = value;
                    break;
                case "FilpCardRememberTime":
                    valueDataBase.FilpCardRememberTime = value;
                    break;
                case "ClockAddTime":
                    valueDataBase.ClockAddTime = value;
                    break;
                case "ComboAddTime":
                    valueDataBase.ComboAddTime = value;
                    break;
                case "DefaultExp":
                    valueDataBase.DefaultExp = value;
                    break;
                case "AddExp":
                    valueDataBase.AddExp = value;
                    break;
                case "GameChoice1Perfect":
                    valueDataBase.GameChoice1Perfect = value;
                    break;
                case "GameChoice1Normal":
                    valueDataBase.GameChoice1Normal = value;
                    break;
                case "GameChoice1Hard":
                    valueDataBase.GameChoice1Hard = value;
                    break;

                case "GameChoice2Perfect":
                    valueDataBase.GameChoice2Perfect = value;
                    break;
                case "GameChoice2Normal":
                    valueDataBase.GameChoice2Normal = value;
                    break;
                case "GameChoice2Hard":
                    valueDataBase.GameChoice2Hard = value;
                    break;

                case "GameChoice3Perfect":
                    valueDataBase.GameChoice3Perfect = value;
                    break;
                case "GameChoice3Normal":
                    valueDataBase.GameChoice3Normal = value;
                    break;
                case "GameChoice3Hard":
                    valueDataBase.GameChoice3Hard = value;
                    break;

                case "GameChoice4Perfect":
                    valueDataBase.GameChoice4Perfect = value;
                    break;
                case "GameChoice4Normal":
                    valueDataBase.GameChoice4Normal = value;
                    break;
                case "GameChoice4Hard":
                    valueDataBase.GameChoice4Hard = value;
                    break;

                case "GameChoice5Perfect":
                    valueDataBase.GameChoice5Perfect = value;
                    break;
                case "GameChoice5Normal":
                    valueDataBase.GameChoice5Normal = value;
                    break;
                case "GameChoice5Hard":
                    valueDataBase.GameChoice5Hard = value;
                    break;

                case "GameChoice6Perfect":
                    valueDataBase.GameChoice6Perfect = value;
                    break;
                case "GameChoice6Normal":
                    valueDataBase.GameChoice6Normal = value;
                    break;
                case "GameChoice6Hard":
                    valueDataBase.GameChoice6Hard = value;
                    break;

                case "GameChoice7Perfect":
                    valueDataBase.GameChoice7Perfect = value;
                    break;
                case "GameChoice7Normal":
                    valueDataBase.GameChoice7Normal = value;
                    break;
                case "GameChoice7Hard":
                    valueDataBase.GameChoice7Hard = value;
                    break;

                case "GameChoice8Perfect":
                    valueDataBase.GameChoice8Perfect = value;
                    break;
                case "GameChoice8Normal":
                    valueDataBase.GameChoice8Normal = value;
                    break;
                case "GameChoice8Hard":
                    valueDataBase.GameChoice8Hard = value;
                    break;

            }
        }
    }

    void SetUpgrade(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;

        for (int i = 1; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');

            UpgradeInformation upgradeInformation = new UpgradeInformation();

            upgradeInformation.price = int.Parse(column[1]);
            upgradeInformation.addPrice = int.Parse(column[2]);
            upgradeInformation.value = float.Parse(column[3]);
            upgradeInformation.addValue = float.Parse(column[4]);

            switch (column[0])
            {
                case "StartTime":
                    upgradeDataBase.StartTime = upgradeInformation;
                    break;
                case "Critical":
                    upgradeDataBase.Critical = upgradeInformation;
                    break;
                case "Burning":
                    upgradeDataBase.Burning = upgradeInformation;
                    break;
                case "AddExp":
                    upgradeDataBase.AddExp = upgradeInformation;
                    break;
                case "AddGold":
                    upgradeDataBase.AddGold = upgradeInformation;
                    break;
                case "ComboTime":
                    upgradeDataBase.ComboTime = upgradeInformation;
                    break;
                case "ComboCritical":
                    upgradeDataBase.ComboCritical = upgradeInformation;
                    break;
                case "AddScore":
                    upgradeDataBase.AddScore = upgradeInformation;
                    break;
            }
        }
    }

    void SyncFile()
    {
        if(NetworkConnect.instance.CheckConnectInternet())
        {
            isActive = false;

            localizationDataBase.Initialize();
            valueDataBase.Initialize();

            CheckFile();
        }
        else
        {
            StopAllCoroutines();
            messageText.text = LocalizationManager.instance.GetString("NetworkConnectNotion");
            StartCoroutine(DelayCorution());
        }
    }

    IEnumerator DelayCorution()
    {
        yield return new WaitForSeconds(3f);
        SyncFile();
    }

    void CheckPercent(float number)
    {
        barFillAmount.fillAmount = number / 100.0f;
        barPercentText.text = number.ToString() + "%";
    }
}
