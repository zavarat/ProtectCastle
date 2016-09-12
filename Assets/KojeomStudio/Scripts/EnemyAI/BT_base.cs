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
    public MonsterController monController
    {
        set { _monController = value; }
    }
    private MonsterController _monController;
    public override bool Invoke()
    {
        _monController.MoveForTarget();
        return true;
    }
}

public class RotAroundTarget : Node
{
    public MonsterController monController
    {
        set { _monController = value; }
    }
    private MonsterController _monController;
    public override bool Invoke()
    {
        _monController.RotAroundTarget();
        return true;
    }

}

public class StartAttack : Node
{
    public MonsterController monController
    {
        set { _monController = value; }
    }
    private MonsterController _monController;
    public override bool Invoke()
    {
        _monController.StartAttack();
        return true;
    }
}
public class StopAttack : Node
{
    public MonsterController monController
    {
        set { _monController = value; }
    }
    private MonsterController _monController;
    public override bool Invoke()
    {
        _monController.StopAttack();
        return true;
    }
}
public class StopMoving : Node
{
    public MonsterController monController
    {
        set { _monController = value; }
    }
    private MonsterController _monController;
    public override bool Invoke()
    {
        _monController.StopMoveForTarget();
        return true;
    }
}

public class IsTooCloseTarget : Node
{
    public MonsterController monController
    {
        set { _monController = value; }
    }
    private MonsterController _monController;
    public override bool Invoke()
    {
        if (_monController.IsTooCloseTarget()) return true;
        else return false;
    }
}

public class IsDead : Node
{
    public override bool Invoke()
    {
        return false;
    }
}
