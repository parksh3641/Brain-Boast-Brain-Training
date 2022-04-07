using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionContent : MonoBehaviour
{
    public OptionType optionType;

    public Image iconImg;
    public Sprite[] iconList;
    public Text iconText;

    public Image buttonImg;
    public Sprite[] buttonList;
    public Text buttonText;

    private void Start()
    {
        switch (optionType)
        {
            case OptionType.Music:
                if(GameStateManager.instance.Music)
                {
                    iconImg.sprite = iconList[0];
                    iconText.text = "Music";
                    buttonImg.sprite = buttonList[0];
                    buttonText.text = "ON";
                }
                else
                {
                    iconImg.sprite = iconList[1];
                    iconText.text = "Music";
                    buttonImg.sprite = buttonList[1];
                    buttonText.text = "OFF";
                }
                break;
            case OptionType.SFX:
                if (GameStateManager.instance.Sfx)
                {
                    iconImg.sprite = iconList[2];
                    iconText.text = "Sfx";
                    buttonImg.sprite = buttonList[0];
                    buttonText.text = "ON";
                }
                else
                {
                    iconImg.sprite = iconList[3];
                    iconText.text = "Sfx";
                    buttonImg.sprite = buttonList[1];
                    buttonText.text = "OFF";
                }
                break;
        }
    }

    public void OnClick()
    {
        switch (optionType)
        {
            case OptionType.Music:
                if(GameStateManager.instance.Music)
                {
                    GameStateManager.instance.Music = false;

                    iconImg.sprite = iconList[1];
                    iconText.text = "Music";
                    buttonImg.sprite = buttonList[1];
                    buttonText.text = "OFF";

                }
                else
                {
                    GameStateManager.instance.Music = true;

                    iconImg.sprite = iconList[0];
                    iconText.text = "Music";
                    buttonImg.sprite = buttonList[0];
                    buttonText.text = "ON";
                }
                break;
            case OptionType.SFX:
                if (GameStateManager.instance.Sfx)
                {
                    GameStateManager.instance.Sfx = false;

                    iconImg.sprite = iconList[3];
                    iconText.text = "Sfx";
                    buttonImg.sprite = buttonList[1];
                    buttonText.text = "OFF";
                }
                else
                {
                    GameStateManager.instance.Sfx = true;

                    iconImg.sprite = iconList[2];
                    iconText.text = "Sfx";
                    buttonImg.sprite = buttonList[0];
                    buttonText.text = "ON";
                }
                break;
        }
    }

}