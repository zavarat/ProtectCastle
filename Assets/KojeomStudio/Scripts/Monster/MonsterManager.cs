using UnityEngine;
using System.Collections;

public class MonsterManager : MonoBehaviour {

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
        monsterPrefab.SetActive(false);
        destroyEffect.transform.position = monsterPrefab.transform.position;
        destroyEffect.SetActive(true);
    }
    [SerializeField]
    private GameObject createEffect;
    public GameObject GetCreateEffect()
    {
        return createEffect;
    }
}
