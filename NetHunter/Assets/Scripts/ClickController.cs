using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : MonoBehaviour {

    // Update is called once per frame
    void Update()
    {
        

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0) && !Game.Instance().isStop)
            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.DrawLine(ray.origin, hit.point);
                if (hit.collider.gameObject.tag == "Node")
                {
                    GameNode _node = hit.collider.gameObject.GetComponent<GameNode>();
                    Game.Instance().JumpToNode(_node);
                }
            } 

    }
}
