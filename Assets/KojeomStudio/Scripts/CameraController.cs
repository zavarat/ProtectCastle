using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    [SerializeField]
    private Transform planetTrans;
    private Vector3 rotAxis;

    private Vector3 startMousePos;
    private Vector3 endMousePos;
	
	void Update () {
        if(RuntimePlatform.WindowsEditor == Application.platform)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startMousePos.x = Input.mousePosition.y;
                startMousePos.y = Input.mousePosition.x;
            }
            else if (Input.GetMouseButton(0))
            {
                endMousePos.x = Input.mousePosition.y;
                endMousePos.y = Input.mousePosition.x;
                rotAxis = endMousePos - startMousePos;
                rotAxis.Normalize();
                gameObject.transform.RotateAround(planetTrans.transform.position, rotAxis, 50.0f * Time.deltaTime);
            }
        }
        else if(RuntimePlatform.Android == Application.platform)
        {
            // to do 
        }
    }
}
