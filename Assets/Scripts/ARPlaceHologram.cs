using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem.EnhancedTouch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

[RequireComponent(typeof(ARAnchorManager))]
[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARPlaneManager))]
public class ARPlaceHologram : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefabToPlace;

    [SerializeField]
    private GameObject coinPrefab; // Münz-Prefab

    private ARRaycastManager _raycastManager;
    private ARAnchorManager _anchorManager;
    private static readonly List<ARRaycastHit> Hits = new();

    private GameObject placedObject;
    public CarInputHandler carInputHandler;

    protected void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    protected void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    protected void Awake()
    {
        _raycastManager = GetComponent<ARRaycastManager>();
        _anchorManager = GetComponent<ARAnchorManager>();
    }

    protected void Update()
    {
        var activeTouches = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;
        if (activeTouches.Count < 1 || activeTouches[0].phase != TouchPhase.Began)
        {
            return;
        }

        const TrackableType trackableTypes = TrackableType.FeaturePoint | TrackableType.PlaneWithinPolygon;

        if (_raycastManager.Raycast(activeTouches[0].screenPosition, Hits, trackableTypes))
        {
            if (placedObject == null)
            {
                placedObject = CreateAnchor(Hits[0]);
                CarController carController = placedObject.GetComponent<CarController>();
                if (carController != null && carInputHandler != null)
                {
                    carInputHandler.carController = carController;
                }

                PlaceCoinsRandomly();
            }
        }
        else
        {
            Debug.Log("Raycast did not hit any trackable surfaces.");
        }
    }

    private GameObject CreateAnchor(in ARRaycastHit hit)
    {
        ARAnchor anchor;
        if (hit.trackable is ARPlane hitPlane)
        {
            var oldPrefab = _anchorManager.anchorPrefab;
            _anchorManager.anchorPrefab = _prefabToPlace;
            anchor = _anchorManager.AttachAnchor(hitPlane, hit.pose);
            _anchorManager.anchorPrefab = oldPrefab;

            Debug.Log($"Created anchor attachment for plane (id: {anchor.nativePtr}).");
        }
        else
        {
            placedObject = Instantiate(_prefabToPlace, hit.pose.position, hit.pose.rotation);
            if (!placedObject.TryGetComponent<ARAnchor>(out anchor))
            {
                anchor = placedObject.AddComponent<ARAnchor>();
            }

            Debug.Log($"Created regular anchor (id: {anchor.nativePtr}).");
        }

        return placedObject;
    }

    private void PlaceCoinsRandomly()
    {
        int numberOfCoins = 10; // Anzahl der Münzen
        float radius = 2.0f; // Radius um die zentrale Position

        for (int i = 0; i < numberOfCoins; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-radius, radius), 0, Random.Range(-radius, radius));
            Instantiate(coinPrefab, placedObject.transform.position + randomPosition, Quaternion.identity);
        }
    }
}
