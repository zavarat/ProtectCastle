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
    public GameObject GetDestroyEffect()
    {
        return destroyEffect;
    }
    [SerializeField]
    private GameObject createEffect;
    public GameObject GetCreateEffect()
    {
        return createEffect;
    }
}
