using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpManager : MonoBehaviour
{
    public GameObject helpView;

    GamePlayType gamePlayType = GamePlayType.GameChoice1;
    ItemType itemType = ItemType.Clock;

    [Title("TopMenu")]
    public Image[] topMenuImgArray;
    public Sprite[] topMenuSpriteArray;
    public GameObject[] scrollView;

    private int topNumber = 0;

    public HelpContent helpContent;

    public RectTransform helpGameTransform;
    public RectTransform helpItemTransform;


    List<HelpContent> helpGameList = new List<HelpContent>();
    List<HelpContent> helpUseItemList = new List<HelpContent>();

    ImageDataBase imageDataBase;

    private void Awake()
    {
        if (imageDataBase == null) imageDataBase = Resources.Load("ImageDataBase") as ImageDataBase;

        helpView.SetActive(false);

        helpGameTransform.anchoredPosition = new Vector2(0, -999);
        helpItemTransform.anchoredPosition = new Vector2(0, -999);

        topNumber = -1;
    }

    public void Initialize()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(GamePlayType)).Length; i++)
        {
            HelpContent content = Instantiate(helpContent);
            content.transform.SetParent(helpGameTransform);
            content.transform.position = Vector3.zero;
            content.transform.rotation = Quaternion.identity;
            content.transform.localScale = Vector3.one;

            content.InitializeGame(gamePlayType + i);

            content.gameObject.SetActive(true);
            helpGameList.Add(content);
        }

        for (int i = 0; i < System.Enum.GetValues(typeof(ItemType)).Length; i++)
        {
            HelpContent content = Instantiate(helpContent);
            content.transform.SetParent(helpItemTransform);
            content.transform.position = Vector3.zero;
            content.transform.rotation = Quaternion.identity;
            content.transform.localScale = Vector3.one;

            content.InitalizeItem(itemType + i);

            content.gameObject.SetActive(true);
            helpUseItemList.Add(content);
        }
    }

    public void OpenHelp()
    {
        if (!helpView.activeSelf)
        {
            helpView.SetActive(true);

            if (topNumber == -1)
            {
                ChangeTopMenu(0);
            }
        }
        else
        {
            helpView.SetActive(false);
        }
    }

    public void ChangeTopMenu(int number)
    {
        if (topNumber != number)
        {
            topNumber = number;

            for (int i = 0; i < topMenuImgArray.Length; i++)
            {
                topMenuImgArray[i].sprite = topMenuSpriteArray[0];
                scrollView[i].SetActive(false);
            }
            topMenuImgArray[number].sprite = topMenuSpriteArray[1];
            scrollView[number].SetActive(true);
        }
    }

    public void BugReport()
    {
        switch(GameStateManager.instance.Language)
        {
            case LanguageType.Korean:
                Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSek7nC_FNrk3oIPWbAb8CBWSC4c7to_cKeQt9QQwCTdGcavtQ/viewform?usp=sf_link");
                break;
            default:
                Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLScF2Gz--Kuu5kExfRtMFoW_24sv8_0mys4zcoRHLr1l0YgA2A/viewform?usp=sf_link");
                break;
        }
    }
}
