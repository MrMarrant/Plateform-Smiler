using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public new Camera camera;
    public Transform followTarget;

    // Starting position of the parallax
    Vector2 startingPosition;

    // Position Z value of the parallax
    float positionZ;

    // Distance travelled from the camera since the start position
    // => [It update itself on every frame, like it is in Update() method]
    Vector2 distanceCameraFromStart => (Vector2)camera.transform.position - startingPosition;

    float zDistanceFromTarget => transform.position.z - followTarget.position.z;

    // If object is in front of target, use near clip plane. If behind target, use farClipPlane.
    float clippingPlane => (camera.transform.position.z + (zDistanceFromTarget > 0 ? camera.farClipPlane : camera.nearClipPlane));

    // The futur the object from the player, the faster the parallax object will move. Drag it's Z value closer to the target to make it move slower.
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;


    // Start is called before the first frame update
    void Start()
    {
        startingPosition = followTarget.position;
        positionZ = followTarget.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = startingPosition + distanceCameraFromStart * parallaxFactor;
        transform.position = new Vector3(newPosition.x, newPosition.y, positionZ);
    }
}
