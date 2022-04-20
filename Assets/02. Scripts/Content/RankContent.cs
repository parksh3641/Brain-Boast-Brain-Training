using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankContent : MonoBehaviour
{
    public Text indexText;
    public Image indexRankImg; //3�� �ȿ� ���� ���������� ǥ��
    public Sprite[] rankIconList;
    public Image countryImg;
    public Text nickNameText;
    public Text scoreText;

    private void Awake()
    {
        indexText.text = "";
        nickNameText.text = "";
        scoreText.text = "";
    }

    public void InitState(int index, string country, string nickName, int score, bool checkMy)
    {
        if(index <= 3)
        {
            indexRankImg.enabled = true;
            indexRankImg.sprite = rankIconList[index - 1];
        }
        else
        {
            indexRankImg.enabled = false;
        }

        indexText.text = index.ToString();
        nickNameText.text = nickName;
        countryImg.sprite = Resources.Load<Sprite>(country);
        scoreText.text = score.ToString();


        if (index == 999)
        {
            indexText.text = "-";
        }
    }

}
