using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    [SerializeField]
    private float _cameraZ = 0;
    private Vector3 _newPoz;
    private float X;
    private float Y;
    [SerializeField]
    private Transform _cam;
    public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.
    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (Game.Instance().GetCurrentNode() != null)
            _newPoz = Game.Instance().GetCurrentNode().transform.position;

        if (_newPoz != _cam.position)
        {
            X = Mathf.Lerp(X, _newPoz.x, Time.deltaTime * 2f);
            Y = Mathf.Lerp(Y, _newPoz.y, Time.deltaTime * 2f);
        }

        _cam.position = new Vector3(X, Y, _cameraZ);

        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // If the camera is orthographic...
            if (Camera.main.orthographic)
            {
                // ... change the orthographic size based on the change in distance between the touches.
                Camera.main.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                // Make sure the orthographic size never drops below zero.
                Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 0.1f);
            }
            else
            {
                // Otherwise change the field of view based on the change in distance between the touches.
                Camera.main.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // Clamp the field of view to make sure it's between 0 and 180.
                Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 40f, 120f);
            }
        }
    }
}
