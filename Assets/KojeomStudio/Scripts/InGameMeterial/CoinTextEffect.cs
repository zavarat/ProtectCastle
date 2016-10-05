using UnityEngine;
using System.Collections;

public class CoinTextEffect : TextEffect {
    [SerializeField]
    private TweenAlpha tweenAlpha;
    [SerializeField]
    private TweenPosition tweenPos;
    [SerializeField]
    private UILabel lbl_coinTextEffect;
    
    public void ResetEffect(Color textColor)
    {
        lbl_coinTextEffect.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        lbl_coinTextEffect.color = textColor;
        
        tweenPos.ResetToBeginning();
        tweenPos.enabled = true;
        tweenAlpha.ResetToBeginning();
        tweenAlpha.enabled = true;
    }

	public override void DeactivateTextEffect()
    {
        lbl_coinTextEffect.gameObject.SetActive(false);
    }
    public override void ActivateTextEffect(string coin)
    {
        lbl_coinTextEffect.gameObject.SetActive(true);
        lbl_coinTextEffect.text = coin;
    }
}
