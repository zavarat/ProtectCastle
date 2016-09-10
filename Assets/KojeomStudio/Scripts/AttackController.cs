using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackController : MonoBehaviour {
    [SerializeField]
    private Transform attacker;
    [SerializeField]
    private GameObject missilePrefab;
    private Queue<GameObject> missiles = new Queue<GameObject>();
    private GameObject target;
    private Transform parentTrans;
    private IEnumerator attackProcess;

    public int maxQuantity = 5;
    [Range(1.0f, 5.0f)]
    public float attackIntervalTime = 1.0f;

	public void Init () {
        attackProcess = AttackProcess();
        parentTrans = GameObject.FindGameObjectWithTag("missileBucket").transform;
        target = GameObject.FindGameObjectWithTag("target");

	    for(int idx = 0; idx < maxQuantity; idx++)
        {
            GameObject mis = Instantiate(missilePrefab, attacker.position,
                new Quaternion(0, 0, 0, 0)) as GameObject;
            mis.transform.parent = parentTrans;
            EffectSettings settings = mis.GetComponent<EffectSettings>();
            settings.Target = target;
            settings.EffectDeactivated += (n, e) =>
            {
                mis.SetActive(false);
            };

            mis.SetActive(false);
            missiles.Enqueue(mis);
        }
    }
	
    public void StartAttack()
    {
        StartCoroutine(attackProcess);
    }

    public void StopAttack()
    {
        StopCoroutine(attackProcess);
    }

    private IEnumerator AttackProcess()
    {
        while (true)
        {
            GameObject mis = missiles.Dequeue();
            if((mis != null) && (!mis.activeSelf))
            {
                mis.transform.position = attacker.transform.position;
                mis.SetActive(true);
                missiles.Enqueue(mis);
            }
            yield return new WaitForSeconds(attackIntervalTime);
        }
    }
}
