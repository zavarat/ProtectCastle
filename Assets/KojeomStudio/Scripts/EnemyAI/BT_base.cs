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

public class BT_base : MonoBehaviour {

    private Sequence root = new Sequence();
    private Selector selector = new Selector();
    
    [SerializeField]
    private NavMeshAgent enemyAgent;
    [SerializeField]
    private Transform target;

    private IEnumerator behaviorProcess;

    // test start func
	void Start () {
        root.AddChild(selector);

        behaviorProcess = BehaviorProcess();
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
