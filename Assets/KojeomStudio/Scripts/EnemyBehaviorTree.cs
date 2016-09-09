using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public abstract class Node
{
    public abstract bool Invoke();
}

public class CompositeNode : Node
{
    public override bool Invoke()
    {
        throw new NotImplementedException();
    }

    public void AddChild(Node node)
    {
        childrens.Push(node);
    }

    public Stack<Node> GetChildrens()
    {
        return childrens;
    }
    private Stack<Node> childrens = new Stack<Node>();
}

public class Selector : CompositeNode
{
    public override bool Invoke()
    {
        foreach (var node in GetChildrens())
        {
            if (node.Invoke())
            {
                return true;
            }
        }
        return false;
    }
}

public class Sequence : CompositeNode
{
    public override bool Invoke()
    {
        foreach (var node in GetChildrens())
        {
            if (!node.Invoke())
            {
                return false;
            }
        }
        return true;
    }
}

public class MoveForTarget : Node
{
    public NavMeshAgent agent
    {
        set { _agent = value; }
    }
    private NavMeshAgent _agent;
    public Transform target
    {
        set { _target = value; }
    }
    private Transform _target;
    public override bool Invoke()
    {
        Debug.Log("Move for Target!");
        _agent.destination = _target.position;
        return true;
    }
    
}

public class Waiting : Node
{
    public NavMeshAgent agent
    {
        set { _agent = value; }
    }
    private NavMeshAgent _agent;
    public override bool Invoke()
    {
        Debug.Log("stop");
        _agent.Stop();
        return true;
    }
}

public class EnemyBehaviorTree : MonoBehaviour {

    private Sequence root = new Sequence();
    private Selector selector = new Selector();
    private Sequence seqMove = new Sequence();
    private Sequence seqStop = new Sequence();

    private Waiting waiting = new Waiting();
    private MoveForTarget moveForTarget = new MoveForTarget();

    [SerializeField]
    private NavMeshAgent enemyAgent;
    [SerializeField]
    private Transform target;

	// Use this for initialization
	void Start () {

        root.AddChild(selector);

        selector.AddChild(seqStop);
        selector.AddChild(seqMove);

        moveForTarget.agent = enemyAgent;
        moveForTarget.target = target;
        waiting.agent = enemyAgent;
        seqStop.AddChild(waiting);
        seqMove.AddChild(moveForTarget);

        StartCoroutine(BehaviorProcess());
	}

    IEnumerator BehaviorProcess()
    {
        while (!root.Invoke())
        {
            Debug.Log("======================");
            yield return new WaitForSeconds(1.0f);
        }
        Debug.Log("behavior process exit");
    }
	
	
}
