using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlanetManager : MonoBehaviour {

    [SerializeField]
    private InGameUIManager inGameUIMgr;
    [SerializeField]
    private GameObject hitTextEffect;
    [SerializeField]
    private Transform effectBucket;
    private Queue<PlanetHitTextEffect> planetHitEffects = new Queue<PlanetHitTextEffect>();
    [Range(30, 50)]
    public int maxHitTextEffect;

    [Range(5000, 8000)]
    public int planetMaxHP;
    private int planetCurHP;

    public void Init()
    {
        planetCurHP = planetMaxHP;
        inGameUIMgr.GetPlanetMaxHpLbl().text = planetMaxHP.ToString();
        inGameUIMgr.GetPlanetCurHpLbl().text = planetCurHP.ToString();

        for (int i = 0; i < maxHitTextEffect; i++)
        {
            GameObject obj = Instantiate(hitTextEffect,
                new Vector3(0, 0, 0),
                new Quaternion(0, 0, 0, 0)) as GameObject;
            obj.transform.parent = effectBucket;
            obj.transform.localPosition = new Vector3(0, 0, 0);
            obj.transform.localScale = new Vector3(1, 1, 1);
            planetHitEffects.Enqueue(obj.GetComponent<PlanetHitTextEffect>());
        }
    }

    public void InitCurHPColor()
    {
        inGameUIMgr.GetPlanetCurHpLbl().color = new Color(0, 255, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("missile"))
        {
            PlanetHitProcess(other.GetComponent<MissileInfo>().attackPoint);
        }
    }

    private void PlanetHitProcess(int ap)
    {
        int sum = planetCurHP - ap;
        if (sum <= 0)
        {
            planetCurHP = 0;
            // game over..
        }
        else
        {
            PlanetHitTextEffect effect = planetHitEffects.Dequeue();
            planetHitEffects.Enqueue(effect);
            effect.ActivateTextEffect("-10");
            effect.ResetEffect();

            inGameUIMgr.GetPlanetCurHpLbl().GetComponent<TweenColor>().ResetToBeginning();
            inGameUIMgr.GetPlanetCurHpLbl().GetComponent<TweenColor>().enabled = true;
            planetCurHP -= 10;
            inGameUIMgr.GetPlanetCurHpLbl().text = planetCurHP.ToString();
        }
    }
}
