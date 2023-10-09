using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MeshPainter : MonoBehaviour
{
    [SerializeField] GameObject decalPrefab;
    
    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) )
        {
            InstantiateDecal();
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Move the cube if the screen has the finger moving.
            if (touch.phase == TouchPhase.Moved)
            {
                InstantiateDecal();
            }
        }
    }

    void InstantiateDecal()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, 100f))
        {
            GameObject decal =  Instantiate(decalPrefab, hitInfo.point, Quaternion.FromToRotation(Vector3.up, hitInfo.normal));
        }
    }
}
