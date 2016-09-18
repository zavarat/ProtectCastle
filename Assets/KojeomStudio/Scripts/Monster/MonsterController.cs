using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour {
    [Range(0, 5)]
    public int hp;
    private Transform trans;
    private bool isDead;
    private Transform target;
    private AttackController attackController;

    private IEnumerator moveForTarget;
    private bool isMovingForTarget = false;
    private IEnumerator rotAroundTarget;
    private bool isRotAroundTarget = false;

    public void Init()
    {
        hp = 1;
        isDead = false;
        trans = gameObject.transform;
        target = GameObject.FindGameObjectWithTag("target").transform;
        attackController = gameObject.GetComponent<AttackController>();
        attackController.Init();

        moveForTarget = MoveForTargetProcess();
        rotAroundTarget = RotAroundTargetProcess();
    }

    public void MoveForTarget()
    {
        if(isMovingForTarget == false)
        {
            StartCoroutine(moveForTarget);
            isMovingForTarget = true;
        }
    }
    public void StopMoveForTarget()
    {
        StopCoroutine(moveForTarget);
        isMovingForTarget = false;
    }

    private IEnumerator MoveForTargetProcess()
    {
        while (true)
        {
            Vector3 dir = target.position - trans.position;
            dir.Normalize();
            trans.position += dir * Time.deltaTime;
            yield return null;
        }
    }

    public void RotAroundTarget()
    {
        if(isRotAroundTarget == false)
        {
            StartCoroutine(rotAroundTarget);
            isRotAroundTarget = true;
        }
    }
    public void StopRotAroundTarget()
    {
        StopCoroutine(rotAroundTarget);
        isRotAroundTarget = false;
    }

    private IEnumerator RotAroundTargetProcess()
    {
        while(true)
        {
            trans.RotateAround(target.position, target.up, 1.0f);
            yield return null;
        }
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
