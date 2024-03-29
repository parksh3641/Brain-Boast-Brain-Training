using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemManager : MonoBehaviour
{
    public GoogleSheetDownloader googleSheetDownloader;


    void Start()
    {
        if (GameStateManager.instance.Language == LanguageType.Default)
        {
            if (Application.systemLanguage == SystemLanguage.Korean)
            {
                GameStateManager.instance.Language = LanguageType.Korean;
            }
            else if (Application.systemLanguage == SystemLanguage.Japanese)
            {
                GameStateManager.instance.Language = LanguageType.Japanese;
            }
            else if (Application.systemLanguage == SystemLanguage.Chinese)
            {
                GameStateManager.instance.Language = LanguageType.Chinese;
            }
            else if (Application.systemLanguage == SystemLanguage.Portuguese)
            {
                GameStateManager.instance.Language = LanguageType.Portuguese;
            }
            else if (Application.systemLanguage == SystemLanguage.Russian)
            {
                GameStateManager.instance.Language = LanguageType.Russian;
            }
            else if (Application.systemLanguage == SystemLanguage.German)
            {
                GameStateManager.instance.Language = LanguageType.German;
            }
            else if (Application.systemLanguage == SystemLanguage.Spanish)
            {
                GameStateManager.instance.Language = LanguageType.Spanish;
            }
            else if (Application.systemLanguage == SystemLanguage.Arabic)
            {
                GameStateManager.instance.Language = LanguageType.Arabic;
            }
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
            {
                GameStateManager.instance.Language = LanguageType.Indonesian;
            }
            else if (Application.systemLanguage == SystemLanguage.Italian)
            {
                GameStateManager.instance.Language = LanguageType.Italian;
            }
            else if (Application.systemLanguage == SystemLanguage.Dutch)
            {
                GameStateManager.instance.Language = LanguageType.Dutch;
            }
            else if (Application.systemLanguage.ToString() == "Hindi")
            {
                GameStateManager.instance.Language = LanguageType.Indian;
            }
            else
            {
                GameStateManager.instance.Language = LanguageType.English;
            }
        }

        StartCoroutine(WaitCorution());
    }

    IEnumerator WaitCorution()
    {
        while(!googleSheetDownloader.isActive)
        {
            yield return new WaitForSeconds(0.01f);
        }

        SceneManager.LoadScene("MainScene");
    }
}
