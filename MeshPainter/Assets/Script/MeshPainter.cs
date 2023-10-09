using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MeshPainter : MonoBehaviour
{
    [SerializeField] GameObject decalPrefab;
    public float pointSpacing = 0.1f;
    bool canDraw = false;
    
    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            tempPos = GetPoint();
            canDraw = true;
        }
        else if (Input.GetMouseButton(0) )
        {
            InstantiateDecal();
        }
        else
        {
            canDraw = false;
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Move the cube if the screen has the finger moving.
            if (touch.phase == TouchPhase.Moved)
            {
                InstantiateDecal();
            }
            else
            {
                tempPos = GetPoint();
                canDraw = true;
            }
        }else
        {
            canDraw = false;
        }
        
    }

    Vector3 GetPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, 100f))
        {
            return hitInfo.point;
        }
        return Vector3.zero;
    }

    private Vector3 tempPos;
    void InstantiateDecal()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, 100f))
        {
            if(tempPos == hitInfo.point)
                return;
            
            if(tempPos == Vector3.zero)
                tempPos = hitInfo.point;

            foreach (var point in GeneratePoints(tempPos,hitInfo.point))
            {
                Instantiate(decalPrefab, point, Quaternion.FromToRotation(Vector3.back, hitInfo.normal));
            }
            tempPos = hitInfo.point;
        }
    }
    
    List<Vector3> GeneratePoints(Vector3 pointA, Vector3 pointB)
    {
        List<Vector3> pointsList = new List<Vector3>();
        float distance = Vector3.Distance(pointA, pointB);
        int numPoints = Mathf.FloorToInt(distance / pointSpacing);
    
        for (int i = 0; i <= numPoints; i++)
        {
            float t = i / (float)numPoints;
            Vector3 point = Vector3.Lerp(pointA, pointB, t);
            pointsList.Add(point);
        }

        return pointsList;
    }
    
}
