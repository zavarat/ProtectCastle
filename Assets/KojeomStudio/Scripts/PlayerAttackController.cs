using UnityEngine;
using System.Collections;

public class PlayerAttackController : MonoBehaviour {

    private RaycastHit rayHit;
    private Ray clickRay;
	void Update () {
        if ((RuntimePlatform.WindowsEditor == Application.platform) && 
            (Input.GetMouseButton(0)) )
        {
            clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if ((Physics.Raycast(clickRay, out rayHit)) && 
                (rayHit.transform.CompareTag("Droid")))
            {
                rayHit.transform.gameObject.GetComponent<MonsterController>().BeHit();
            }
        }
        else if (RuntimePlatform.Android == Application.platform)
        {
            // to do 
        }
    }
}
