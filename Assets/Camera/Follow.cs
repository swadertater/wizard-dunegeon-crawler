using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public static GameObject poi;
    private float offset = 60.0f;

    private void Awake()
    {
        poi = Wizard.S.gameObject;
    }
    private void Update()
    {
        SetCameraPositionOntoPOI();
    }

    private void SetCameraPositionOntoPOI()
    {
        if (poi)
        {
            var poiPos = poi.transform.position;
            transform.position = new Vector3(poiPos.x, offset, poiPos.z);
        }
    }
}
