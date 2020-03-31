using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlacement : MonoBehaviour
{
    public GameObject placeableObjectPrefab;

    private KeyCode newObjectHotKey = KeyCode.Alpha1;

    private GameObject currentPlaceableObject;

    public LayerMask layer;

    GameController gameController;

    private void Start()
    {
        gameController = GameObject.Find("Player").GetComponent<GameController>();
    }

    private void Update()
    {
        HandleNewObjectHotkey();

        if (currentPlaceableObject != null)
        {
            MoveCurrentPlaceableObjectToMouse();
            ReleaseIfClicked();

        }
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentPlaceableObject = null;
            gameController.points -= gameController.turretCost;
        }
    }

    private void MoveCurrentPlaceableObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 200f, layer))
        {
            currentPlaceableObject.transform.position = hitInfo.point;
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }

    private void HandleNewObjectHotkey()
    {
        if (Input.GetKeyDown(newObjectHotKey) && gameController.points >= gameController.turretCost)
        {
           
            if (currentPlaceableObject == null)
            {
                currentPlaceableObject = Instantiate(placeableObjectPrefab);
            }
            else
            {
                Destroy(currentPlaceableObject);
            }
        }
    }
}
