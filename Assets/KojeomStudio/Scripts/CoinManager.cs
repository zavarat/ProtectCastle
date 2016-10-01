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
    public int maxCoinTextEffect = 8;

    private readonly uint maxCoins = 9999;
    private uint curCoins = 0;

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
        uint sum = coin + curCoins;
        if(sum >= maxCoins)
        {
            curCoins = maxCoins;
            inGameUIMgr.GetCoinsLbl().text = "MAX";
        }else
        {
            curCoins += coin;
            CoinTextEffect effect = coinEffects.Dequeue();
            coinEffects.Enqueue(effect);
            effect.ActivateTextEffect();
            effect.ResetEffect(new Color(0,255,0), "+" + coin.ToString());
            inGameUIMgr.GetCoinsLbl().text = curCoins.ToString();
        }
        
    }
    public void DecreaseCoins(uint coin)
    {
        uint sum = curCoins - coin;
        if(sum <= 0)
        {
            curCoins = 0;
            inGameUIMgr.GetCoinsLbl().text = "0";
        }else
        {
            curCoins -= coin;
            CoinTextEffect effect = coinEffects.Dequeue();
            coinEffects.Enqueue(effect);
            effect.ActivateTextEffect();
            effect.ResetEffect(new Color(0, 255, 0), "-" + coin.ToString());
            inGameUIMgr.GetCoinsLbl().text = curCoins.ToString();
        }
    }
}
