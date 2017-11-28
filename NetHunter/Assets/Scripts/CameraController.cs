using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


    [SerializeField]
    private float _cameraZ = 0;
    private Vector3 _newPoz;
    private float X;
    private float Y;
    // Update is called once per frame
    void Update () {


        if (Game.Instance().GetCurrentNode() != null)
            _newPoz = Game.Instance().GetCurrentNode().transform.position;

            if (_newPoz != transform.position)
            {
                X = Mathf.Lerp(X, _newPoz.x, Time.deltaTime);
                Y = Mathf.Lerp(Y, _newPoz.y, Time.deltaTime);
            }

        transform.position = new Vector3(X, Y, _cameraZ);
             
		
	}
}
