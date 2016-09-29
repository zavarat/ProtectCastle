using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour {
    [Range(0, 5)]
    public int hp;
    private Transform trans;
    private bool isDead;
    private Transform target;
    private AttackController attackController;

    public delegate void DeadEffect();
    public event DeadEffect OnDeadEffect;
    [SerializeField]
    private MonsterManager monMgr;

    public void Init()
    {
        hp = 1;
        isDead = false;
        trans = gameObject.transform;
        target = GameObject.FindGameObjectWithTag("target").transform;
        attackController = gameObject.GetComponent<AttackController>();
        attackController.Init();

        OnDeadEffect += monMgr.DestroyProcess;
    }

    public void BeHit()
    {
        if(hp > 0) hp--;
        else isDead = true;
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void DeadPrcoess()
    {
        OnDeadEffect();
    }
    
    public void MoveForTarget()
    {
        Vector3 dir = target.position - trans.position;
        dir.Normalize();
        trans.position += dir * Time.deltaTime;
    }
    
    public void RotAroundTarget()
    {
        trans.RotateAround(target.position, target.up, 1.0f);
    }
    
    public bool IsTooCloseTarget()
    {
        float dist = Vector3.Distance(trans.position, target.position);
        if (dist <= 5.0f) return true;
        else return false;
    }

    public void StartAttack()
    {
        attackController.StartAttack();
    }
    public void StopAttack()
    {
        attackController.StopAttack();
    }
    
}
