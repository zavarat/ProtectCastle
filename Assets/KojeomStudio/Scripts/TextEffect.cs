using UnityEngine;
using System.Collections;

public abstract class TextEffect : MonoBehaviour {

    public abstract void DeactivateTextEffect();
    public abstract void ActivateTextEffect(string str);
}
