using UnityEngine;
using System.Collections;

public class CoinTextEffect : MonoBehaviour {
    [SerializeField]
    private TweenAlpha tweenAlpha;
    [SerializeField]
    private TweenPosition tweenPos;
    [SerializeField]
    private UILabel lbl_coinTextEffect;
    
    public void ResetEffect(Color textColor, string coin)
    {
        lbl_coinTextEffect.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        lbl_coinTextEffect.color = textColor;
        lbl_coinTextEffect.text = coin;

        tweenPos.ResetToBeginning();
        tweenPos.enabled = true;
        tweenAlpha.ResetToBeginning();
        tweenAlpha.enabled = true;
    }

	public void DeactivateTextEffect()
    {
        lbl_coinTextEffect.gameObject.SetActive(false);
    }
    public void ActivateTextEffect()
    {
        lbl_coinTextEffect.gameObject.SetActive(true);
    }
}
