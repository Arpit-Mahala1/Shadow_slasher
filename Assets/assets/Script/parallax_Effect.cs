using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax_Effect : MonoBehaviour
{
    public Camera cam;
    public Transform followPlayer;
    Vector2 startingPosition;
    float startingZ;
    Vector2 carMovedSinceStart => (Vector2)cam.transform.position-startingPosition;
    float zDistanceFromTarget => transform.position.z - followPlayer.transform.position.z;
    float clippingPlane=> (cam.transform.position.z +(zDistanceFromTarget > 0 ? cam.farClipPlane: cam.nearClipPlane));
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;
    void Start()
    {
        startingPosition = transform.position;
        startingZ= transform.position.z;
    }

    
    void Update()
    {
        Vector2 newPosition = startingPosition + carMovedSinceStart * parallaxFactor;
        transform.position =new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
