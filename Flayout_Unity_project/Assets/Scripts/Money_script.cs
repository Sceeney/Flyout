using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Money_script : MonoBehaviour
{
    //[SerializeField] Text Gold_text;
    [SerializeField] Text Money_text;

    //public static int Gold;
    public static int Money;

    void Start()
    {
        How_much_money();
    }

    public void How_much_money()
    {
        //Gold = PlayerPrefs.GetInt("Gold_save");
        //Money = PlayerPrefs.GetInt("Money_save");
        Money = Progress.Instance.PlayerInfo.Coins;
        //Gold_text.text = Gold.ToString();
        Money_text.text = Money.ToString();
    }

    public void Add_Money_and_Gold()
    {
       // Gold = Gold + 100;
        //PlayerPrefs.SetInt("Gold_save", Gold);
        Money = Money + 10000;
        Progress.Instance.PlayerInfo.Coins = Money;
        //PlayerPrefs.SetInt("Money_save", Money);
        How_much_money();
    }

}
