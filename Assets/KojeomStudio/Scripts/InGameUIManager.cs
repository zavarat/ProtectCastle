using UnityEngine;
using System.Collections;

public class InGameUIManager : MonoBehaviour {
    [SerializeField]
    private UILabel lbl_GameTimer;
    public UILabel GetGameTimerLbl() { return lbl_GameTimer; }

    [SerializeField]
    private UILabel lbl_countDown;
    public UILabel GetCountDownLbl() { return lbl_countDown; }
}
