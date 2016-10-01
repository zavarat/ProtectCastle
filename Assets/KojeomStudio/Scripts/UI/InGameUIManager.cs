﻿using UnityEngine;
using System.Collections;

public class InGameUIManager : MonoBehaviour {
    [SerializeField]
    private UILabel lbl_GameTimer;
    public UILabel GetGameTimerLbl() { return lbl_GameTimer; }

    [SerializeField]
    private UILabel lbl_countDown;
    public UILabel GetCountDownLbl() { return lbl_countDown; }

    [SerializeField]
    private UILabel lbl_coins;
    public UILabel GetCoinsLbl() { return lbl_coins; }

    [SerializeField]
    private UISprite spr_coin;
    public UISprite GetCoinSpr() { return spr_coin; }
}
