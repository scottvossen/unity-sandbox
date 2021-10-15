using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public static int Lives;

    public int startMoney = 200;
    public int startLives = 10;

    private void Start()
    {
        Money = startMoney;
        Lives = startLives;
    }
}
