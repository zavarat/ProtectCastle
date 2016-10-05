using UnityEngine;
using System.Collections;

public class PlanetHitTextEffect : TextEffect {

    [SerializeField]
    private UILabel lbl_hitTextEffect;
    [SerializeField]
    private TweenAlpha tweenAlpha;
    [SerializeField]
    private TweenPosition tweenPos;

    public void ResetEffect()
    {
        lbl_hitTextEffect.gameObject.transform.localPosition = new Vector3(0, 0, 0);

        tweenPos.ResetToBeginning();
        tweenPos.enabled = true;
        tweenAlpha.ResetToBeginning();
        tweenAlpha.enabled = true;
    }

    public override void DeactivateTextEffect()
    {
        lbl_hitTextEffect.gameObject.SetActive(false);
    }
    public override void ActivateTextEffect(string damage)
    {
        lbl_hitTextEffect.gameObject.SetActive(true);
        lbl_hitTextEffect.text = damage;
    }
}
