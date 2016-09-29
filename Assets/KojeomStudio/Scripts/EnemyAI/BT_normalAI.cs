using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class BT_normalAI : BT_base {
    private Sequence root = new Sequence();
    private Selector selector = new Selector();
    private Sequence seqMovingAttack = new Sequence();
    private Sequence seqDead = new Sequence();

    private MoveForTarget moveForTarget = new MoveForTarget();
    private StartAttack startAttack = new StartAttack();
    private StopAttack stopAttack = new StopAttack();
    private IsTooCloseTarget isTooClose = new IsTooCloseTarget();
    private RotAroundTarget rotTarget = new RotAroundTarget();
    private IsDead isDead = new IsDead();
    private DeadProcess deadProcess = new DeadProcess();

    private IEnumerator behaviorProcess;
    private MonsterController monController;

    public override void Init()
    {
        monController = gameObject.GetComponent<MonsterController>();
        monController.Init();

        root.AddChild(selector);

        selector.AddChild(seqMovingAttack);
        selector.AddChild(seqDead);

        moveForTarget.monController = monController;
        startAttack.monController = monController;
        stopAttack.monController = monController;
        isTooClose.monController = monController;
        rotTarget.monController = monController;
        isDead.monController = monController;
        deadProcess.monController = monController;

        seqMovingAttack.AddChild(isTooClose);
        seqMovingAttack.AddChild(moveForTarget);
        seqMovingAttack.AddChild(rotTarget);
        seqMovingAttack.AddChild(startAttack);

        seqDead.AddChild(deadProcess);
        seqDead.AddChild(stopAttack);
        seqDead.AddChild(isDead);

        behaviorProcess = BehaviorProcess();
    }
    public override void StartBT()
    {
        StartCoroutine(behaviorProcess);
    }
    public override void StopBT()
    {
        StopCoroutine(behaviorProcess);
    }

    public override IEnumerator BehaviorProcess()
    {
        while (!root.Invoke())
        {
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("behavior process exit");
    }
}
