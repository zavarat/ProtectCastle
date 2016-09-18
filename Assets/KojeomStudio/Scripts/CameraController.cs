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
                startMousePos.z = Input.mousePosition.x;
            }
            else if (Input.GetMouseButton(0))
            {
                endMousePos.x = Input.mousePosition.y;
                endMousePos.z = Input.mousePosition.x;
                rotAxis = endMousePos - startMousePos;
                rotAxis.Normalize();
                Debug.Log(rotAxis);
                gameObject.transform.RotateAround(planetTrans.position, rotAxis, 100.0f * Time.deltaTime);
            }
        }
        else if(RuntimePlatform.Android == Application.platform)
        {

        }
    }
}
