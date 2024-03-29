﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class NickNameManager : MonoBehaviour
{
    public GameObject nickNameView;

    public GameObject nickNameFirstView;

    public ProfileManager profileManager;

    public Text nickNameText;
    public InputField inputField;
    public InputField inputFieldFree;

    public string[] lines;
    string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";

    public PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        nickNameView.SetActive(false);
        nickNameFirstView.SetActive(false);
    }

    public void Initialize()
    {
        if(GameStateManager.instance.NickName.Length > 10)
        {
            nickNameFirstView.SetActive(true);
        }

        TextAsset textAsset = Resources.Load<TextAsset>("BadWord");

        lines = Regex.Split(textAsset.ToString(), LINE_SPLIT_RE);

        //string source;

        //if (File.Exists(file))
        //{
        //    StreamReader word = new StreamReader(file);
        //    source = word.ReadToEnd();
        //    word.Close();

        //    lines = Regex.Split(textAsset.ToString(), LINE_SPLIT_RE);
        //}
    }

    public void OpenNickName()
    {
        if (!nickNameView.activeSelf)
        {
            inputField.text = "";

            nickNameView.SetActive(true);
        }
        else
        {
            nickNameView.SetActive(false);
        }
    }

    public void CheckNickName()
    {
        if (playerDataBase.Coin >= 100)
        {
            string Check = Regex.Replace(inputField.text, @"[^a-zA-Z0-9가-힣]", "", RegexOptions.Singleline);

            for(int i = 0; i < lines.Length; i ++)
            {
                if (inputField.text.Contains(lines[i]))
                {
                    NotionManager.instance.UseNotion(NotionType.NickNameNotion3);
                    return;
                }
            }

            if (inputField.text.Equals(Check) == true)
            {
                string newNickName = ((inputField.text.Trim()).Replace(" ", ""));
                string oldNickName = "";

                if(GameStateManager.instance.NickName != null)
                {
                    oldNickName = GameStateManager.instance.NickName.Trim().Replace(" ", "");
                }
                else
                {
                    oldNickName = "";
                }

                if (newNickName.Length > 1)
                {
                    if (!(newNickName.Equals(oldNickName)))
                    {
                        PlayfabManager.instance.UpdateDisplayName(newNickName, Success, Failure);
                    }
                    else
                    {
                        NotionManager.instance.UseNotion(NotionType.NickNameNotion1);
                    }
                }
                else
                {
                    NotionManager.instance.UseNotion(NotionType.NickNameNotion2);
                }
            }
            else
            {
                NotionManager.instance.UseNotion(NotionType.NickNameNotion3);
            }
        }
        else
        {
            NotionManager.instance.UseNotion(NotionType.NickNameNotion4);
        }
    }

    public void CheckFreeNickName()
    {
            string Check = Regex.Replace(inputFieldFree.text, @"[^a-zA-Z0-9가-힣]", "", RegexOptions.Singleline);

            for (int i = 0; i < lines.Length; i++)
            {
                if (inputFieldFree.text.Contains(lines[i]))
                {
                    NotionManager.instance.UseNotion(NotionType.NickNameNotion3);
                    return;
                }
            }

        if (inputFieldFree.text.Equals(Check) == true)
        {
            string newNickName = ((inputFieldFree.text.Trim()).Replace(" ", ""));
            string oldNickName = "";

            if (GameStateManager.instance.NickName != null)
            {
                oldNickName = GameStateManager.instance.NickName.Trim().Replace(" ", "");
            }
            else
            {
                oldNickName = "";
            }

            if (newNickName.Length > 1)
            {
                if (!(newNickName.Equals(oldNickName)))
                {
                    PlayfabManager.instance.UpdateDisplayName(newNickName, FreeSuccess, Failure);
                }
                else
                {
                    NotionManager.instance.UseNotion(NotionType.NickNameNotion1);
                }
            }
            else
            {
                NotionManager.instance.UseNotion(NotionType.NickNameNotion2);
            }
        }
        else
        {
            NotionManager.instance.UseNotion(NotionType.NickNameNotion3);
        }
    }

    public void Success()
    {
        profileManager.Initialize();

        nickNameText.text = GameStateManager.instance.NickName;

        if (PlayfabManager.instance.isActive) PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Coin, 100);

        NotionManager.instance.UseNotion(NotionType.NickNameNotion6);

        nickNameView.SetActive(false);
    }

    public void FreeSuccess()
    {
        profileManager.Initialize();

        nickNameText.text = GameStateManager.instance.NickName;

        NotionManager.instance.UseNotion(NotionType.NickNameNotion6);

        nickNameFirstView.SetActive(false);
    }

    public void Failure()
    {
        NotionManager.instance.UseNotion(NotionType.NickNameNotion5);
    }

    public void CopyId()
    {
        GUIUtility.systemCopyBuffer = nickNameText.text;

        NotionManager.instance.UseNotion(NotionType.CopyIdNotion);
    }
}
