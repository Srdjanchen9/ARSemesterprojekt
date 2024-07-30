using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class PlaceOnPlane : MonoBehaviour
{
    // Referenz auf das zu platzierende Objekt
    public GameObject objectToPlace;
    // Referenz auf den AR Raycast Manager
    private ARRaycastManager raycastManager;
    // Liste der Trefferpunkte
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        // Initialisierung des Raycast Managers
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        // Überprüfen, ob der Bildschirm berührt wird
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Überprüfen, ob der Touch gerade begonnen hat
            if (touch.phase == TouchPhase.Began)
            {
                // Durchführung eines Raycasts
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    // Platzieren des Objekts am ersten Trefferpunkt
                    Pose hitPose = hits[0].pose;
                    Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
                }
            }
        }
    }
}
