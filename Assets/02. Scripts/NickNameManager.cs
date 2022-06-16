using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class NickNameManager : MonoBehaviour
{
    public GameObject nickNameView;

    public Text nickNameText;
    public InputField inputField;

    public PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        nickNameView.SetActive(false);
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
            string Check = Regex.Replace(inputField.text, @"[^a-zA-Z0-9��-�R]", "", RegexOptions.Singleline);
            Check = Regex.Replace(inputField.text, @"[^\w\.@-]", "", RegexOptions.Singleline);

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

                if (newNickName.Length > 2)
                {
                    if (!(newNickName.Equals(oldNickName)))
                    {
                        PlayfabManager.instance.UpdateDisplayName(newNickName, Success, Failure);
                    }
                    else
                    {
                        NotionManager.instance.UseNotion(NotionType.NickNameNotion1);
                        Debug.Log("�ߺ��� �г��� �Դϴ�.");
                    }
                }
                else
                {
                    NotionManager.instance.UseNotion(NotionType.NickNameNotion2);
                    Debug.Log("2���� �̻��̾�� �մϴ�.");
                }
            }
            else
            {
                NotionManager.instance.UseNotion(NotionType.NickNameNotion3);
                Debug.Log("Ư�����ڴ� ����� �� �����ϴ�.");
            }
        }
        else
        {
            NotionManager.instance.UseNotion(NotionType.NickNameNotion4);
            Debug.Log("��尡 �����մϴ�.");
        }
    }

    public void Success()
    {
        Debug.Log("�г��� ���� ����!");

        NotionManager.instance.UseNotion(NotionType.NickNameNotion6);

        nickNameText.text = GameStateManager.instance.NickName;

        playerDataBase.Coin -= 100;

        if (PlayfabManager.instance.isActive) PlayfabManager.instance.UpdateSubtractCurrency(MoneyType.Coin, 100);

        nickNameView.SetActive(false);
    }

    public void Failure()
    {
        NotionManager.instance.UseNotion(NotionType.NickNameNotion5);
        Debug.Log("�̹� �����ϴ� �г��� �Դϴ�.");
    }
}
