using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager instance;

    public bool isInit = false;

    public ResetManager resetManager;
    public ProfileManager profileManager;
    public NickNameManager nickNameManager;
    public ShopManager shopManager;
    public ItemManager itemManager;
    public IconManager iconManager;
    public NewsManager newsManager;
    public LevelManager levelManager;
    public TrophyManager trophyManager;
    public HelpManager helpManager;
    public MailBoxManager mailBoxManager;
    public UpgradeManager upgradeManager;
    public IconBoxManager iconBoxManager;
    public BannerManager bannerManager;
    public ProgressManager progressManager;
    public LockManager lockManager;
    public EventManager eventManager;
    public CastleManager castleManager;

    public delegate void PurchasEvent();
    public static event PurchasEvent eChangeNumber;

    void Awake()
    {
        instance = this;
    }

    public void Initialize()
    {
        if(!isInit)
        {
            isInit = true;

            resetManager.Initialize();
            profileManager.Initialize();
            nickNameManager.Initialize();
            shopManager.Initialize();
            //itemManager.Initialize();
            iconManager.Initialize();
            newsManager.Initialize();
            levelManager.Initialize();
            trophyManager.Initialize();
            helpManager.Initialize();
            mailBoxManager.Initialize();
            upgradeManager.Initialize();
            iconBoxManager.Initialize();
            bannerManager.Initialize();
            progressManager.Initialize();
            lockManager.Initialize();
            eventManager.Initialize();
            castleManager.Initialize();
        }

        GameStateManager.instance.PlayGame = false;

        GameStateManager.instance.Clock = false;
        GameStateManager.instance.Shield = false;
        GameStateManager.instance.Combo = false;
        GameStateManager.instance.Exp = false;
        GameStateManager.instance.Slow = false;
    }
}
