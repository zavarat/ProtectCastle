using UnityEngine;
using System.Collections;

public class MonsterEffectController : MonoBehaviour {

    [SerializeField]
    private GameObject destroyEffect;
    [SerializeField]
    private GameObject createEffect;

    public void DestroyEffectOn(Vector3 initPos)
    {
        destroyEffect.transform.position = initPos;
        destroyEffect.SetActive(true);
    }
    public void CreateEffectOn(Vector3 initPos)
    {
        createEffect.transform.position = initPos;
        createEffect.SetActive(true);
    }
}
