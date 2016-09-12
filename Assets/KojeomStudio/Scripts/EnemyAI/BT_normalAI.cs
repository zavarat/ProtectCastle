using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class BT_normalAI : MonoBehaviour {
    private Sequence root = new Sequence();
    private Selector selector = new Selector();
    private Sequence seqMovingAttack = new Sequence();
    private Sequence seqSmoothStop = new Sequence();

    private MoveForTarget moveForTarget = new MoveForTarget();
    private StartAttack startAttack = new StartAttack();
    private StopAttack stopAttack = new StopAttack();
    private IsTooCloseTarget isTooClose = new IsTooCloseTarget();
    private StopMoving stopMoving = new StopMoving();
    private RotAroundTarget rotTarget = new RotAroundTarget();

    private IEnumerator behaviorProcess;
    private MonsterController monController;

    void Start()
    {
        monController = gameObject.GetComponent<MonsterController>();
        monController.Init();

        root.AddChild(selector);

        selector.AddChild(seqMovingAttack);

        moveForTarget.monController = monController;
        startAttack.monController = monController;
        stopAttack.monController = monController;
        isTooClose.monController = monController;
        stopMoving.monController = monController;
        rotTarget.monController = monController;

        seqSmoothStop.AddChild(stopMoving);
        seqSmoothStop.AddChild(stopAttack);

        seqMovingAttack.AddChild(seqSmoothStop);
        seqMovingAttack.AddChild(isTooClose);
        seqMovingAttack.AddChild(moveForTarget);
        seqMovingAttack.AddChild(rotTarget);
        seqMovingAttack.AddChild(startAttack);

        behaviorProcess = BehaviorProcess();
        StartBT();
    }
    public void StartBT()
    {
        StartCoroutine(behaviorProcess);
    }
    public void StopBT()
    {
        StopCoroutine(behaviorProcess);
    }

    private IEnumerator BehaviorProcess()
    {
        while (!root.Invoke())
        {
            Debug.Log("======================");
            yield return new WaitForSeconds(1.0f);
        }
        Debug.Log("behavior process exit");
    }
}
