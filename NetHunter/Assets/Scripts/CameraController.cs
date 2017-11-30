using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


    [SerializeField]
    private float _cameraZ = 0;
    private Vector3 _newPoz;
    private float X;
    private float Y;
    [SerializeField]
    private Transform _cam;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update () {


        if (Game.Instance().GetCurrentNode() != null)
            _newPoz = Game.Instance().GetCurrentNode().transform.position;

            if (_newPoz != _cam.position)
            {
                X = Mathf.Lerp(X, _newPoz.x, Time.deltaTime*2f);
                Y = Mathf.Lerp(Y, _newPoz.y, Time.deltaTime*2f);
            }

        _cam.position = new Vector3(X, Y, _cameraZ);
             
		
	}
}
