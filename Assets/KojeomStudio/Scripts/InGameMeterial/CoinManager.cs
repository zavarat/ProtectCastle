using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoinManager : MonoBehaviour {
    [SerializeField]
    private InGameUIManager inGameUIMgr;
    [SerializeField]
    private GameObject coinTextEffect;
    [SerializeField]
    private Transform effectBucket;
    private Queue<CoinTextEffect> coinEffects = new Queue<CoinTextEffect>();
    [Range(8, 20)]
    public int maxCoinTextEffect;

    private readonly uint maxCoins = 9999;
    private readonly uint maxCoinsDigit = 4;
    private uint _curCoins = 0;
    public uint curCoins
    {
        get { return _curCoins; }
    }

    public void Init()
    {
        for (int i = 0; i < maxCoinTextEffect; i++)
        {
            GameObject obj = Instantiate(coinTextEffect,
                new Vector3(0,0,0),
                new Quaternion(0, 0, 0, 0)) as GameObject;
            obj.transform.parent = effectBucket;
            obj.transform.localPosition = new Vector3(0, 0, 0);
            obj.transform.localScale = new Vector3(1, 1, 1);
            coinEffects.Enqueue(obj.GetComponent<CoinTextEffect>());
        }
    }

    public void IncreaseCoins(uint coin)
    {
        uint sum = coin + _curCoins;
        if(sum >= maxCoins)
        {
            _curCoins = maxCoins;
            inGameUIMgr.GetCoinsLbl().text = "MAX";
        }else
        {
            _curCoins += coin;
            CoinTextEffect effect = coinEffects.Dequeue();
            coinEffects.Enqueue(effect);
            effect.ActivateTextEffect("+" + coin.ToString());
            effect.ResetEffect(new Color(0,255,0));
            inGameUIMgr.GetCoinsLbl().text = ToCoinString(_curCoins.ToString());
        }
    }
    private string ToCoinString(string coin)
    {
        string coinStr = coin;
        uint coinDigit = (uint)coin.Length;
        int zeroNum = (int)(maxCoinsDigit - coinDigit);
        if (zeroNum < 0) zeroNum = 0;
        for (int i = 0; i < zeroNum; i++)
        {
            coinStr = coinStr.Insert(0, "0");
        }
        return coinStr;
    }
    public void DecreaseCoins(uint coin)
    {
        uint sum = _curCoins - coin;
        if(sum <= 0)
        {
            _curCoins = 0;
            inGameUIMgr.GetCoinsLbl().text = "0000";
        }else
        {
            _curCoins -= coin;
            CoinTextEffect effect = coinEffects.Dequeue();
            coinEffects.Enqueue(effect);
            effect.ActivateTextEffect("-" + coin.ToString());
            effect.ResetEffect(new Color(0, 255, 0));
            inGameUIMgr.GetCoinsLbl().text = ToCoinString(_curCoins.ToString());
        }
    }
}
