using UnityEngine;
using System.Collections;

public class MonsterManager : MonoBehaviour {

    public bool isNormalType = false;
    public bool isAdvancedType = false;
    public bool isSpeedType = false;
    private CoinManager coinMgr;

    [Range(1, 100)]
    public uint hasCoin;

    [SerializeField]
    private BT_base monsterAI;

    public delegate void AfterDestroy(MonsterManager monMgr);
    public event AfterDestroy OnAfterDestroy;

    public delegate void AfterCreate(MonsterManager monMgr);
    public event AfterCreate OnAfterCreate;

    public void Init()
    {
        monsterAI.Init();
        coinMgr = GameObject.FindGameObjectWithTag("CoinManager").GetComponent<CoinManager>();
    }

    public void StartMonsterAI()
    {
        monsterAI.StartBT();
    }
    public void StopMonsterAI()
    {
        monsterAI.StopBT();
    }

    [SerializeField]
    private GameObject monsterPrefab;
    public GameObject GetMonsterPrefab()
    {
        return monsterPrefab;
    }
    [SerializeField]
    private GameObject destroyEffect;
    public void DestroyProcess()
    {
        coinMgr.IncreaseCoins(hasCoin);

        monsterPrefab.GetComponent<MonsterController>().Revive();
        monsterPrefab.SetActive(false);
        destroyEffect.transform.position = monsterPrefab.transform.position;
        destroyEffect.SetActive(true);

        OnAfterDestroy(this);
    }
    [SerializeField]
    private GameObject createEffect;
    public GameObject GetCreateEffect()
    {
        return createEffect;
    }
    public void CreateProcess()
    {
        OnAfterCreate(this);
    }
}
