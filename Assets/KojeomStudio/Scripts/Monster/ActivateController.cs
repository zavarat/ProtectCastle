using UnityEngine;
using System.Collections;
[RequireComponent(typeof(ParticleSystem))]
public class ActivateController : MonoBehaviour {
    // If true, deactivate the object instead of destroying it
    public bool OnlyDeactivate;
    public bool isCreateEffect;
    private MonsterGenerator monGenerator;

    void Start()
    {
        monGenerator = GameObject.FindGameObjectWithTag("monsterGenerator").GetComponent<MonsterGenerator>();
    }

    void OnEnable()
    {
        StartCoroutine("CheckIfAlive");
    }

    IEnumerator CheckIfAlive()
    {
        ParticleSystem ps = this.GetComponent<ParticleSystem>();
        while (true && ps != null)
        {
            yield return new WaitForSeconds(0.5f);
            if (!ps.IsAlive(true))
            {
                if (OnlyDeactivate)
                {
#if UNITY_3_5
						this.gameObject.SetActiveRecursively(false);
#else
                    {
                        if (isCreateEffect)
                        {
                            monGenerator.Sailing();
                        }
                        this.gameObject.SetActive(false);

                    }

#endif
                }
                else
                    GameObject.Destroy(this.gameObject);
                break;
            }
        }
    }
}
