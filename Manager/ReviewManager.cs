//using Google.Play.Review;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviewManager : MonoBehaviour
{
    public GameObject appReviewView;

    private void Awake()
    {
        appReviewView.SetActive(false);
    }

    public void OpenReview()
    {
        appReviewView.SetActive(true);

        GameStateManager.instance.InAppReview = true;
    }

    public void CloseReview()
    {
        appReviewView.SetActive(false);
    }

    public void OpenURL()
    {
        appReviewView.SetActive(false);

        if (PlayfabManager.instance.isActive) PlayfabManager.instance.UpdateAddCurrency(MoneyType.Crystal, 100);

#if UNITY_ANDROID || UNITY_EDITOR
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.unity3d.toucharcade");
#else
        Application.OpenURL("https://apps.apple.com/us/app/gosu-of-touch-tap-arcade/id1637056029");
#endif
    }
}
