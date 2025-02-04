using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TrackImage : MonoBehaviour
{
    [SerializeField]
    ARTrackedImageManager m_TrackedImageManager; 
    public GameObject shipPrefab; 
    public GameObject yippePrefab; 
    public GameObject logoPrefab;   
    public GameObject riotPrefab;  
    public GameObject spiralPrefab; 
    public GameObject timePrefab;   
    public AudioClip shipSound;   
    public AudioClip yippeSound;   
    private AudioSource source;
    
    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>();

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;

    void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
       
        foreach (ARTrackedImage newImage in eventArgs.added)
        {
            if (newImage.referenceImage.name == "ship")
            {
                GameObject newObject = GameObject.Instantiate(shipPrefab);
                newObject.transform.SetParent(newImage.transform, false);
                spawnedObjects[newImage.referenceImage.name] = newObject;
                if (source != null && shipSound != null)
                {
                    source.PlayOneShot(shipSound);
                }
            }
            else if (newImage.referenceImage.name == "yippe")
            {
                GameObject newObject = GameObject.Instantiate(yippePrefab);
                newObject.transform.SetParent(newImage.transform, false);
                spawnedObjects[newImage.referenceImage.name] = newObject;
                if (source != null && yippeSound != null)
                {
                    source.PlayOneShot(yippeSound);
                }
            }
            else if (newImage.referenceImage.name == "logo")
            {
                GameObject newObject = GameObject.Instantiate(logoPrefab);
                newObject.transform.SetParent(newImage.transform, false);
                spawnedObjects[newImage.referenceImage.name] = newObject;
            }
            else if (newImage.referenceImage.name == "riot")
            {
                GameObject newObject = GameObject.Instantiate(riotPrefab);
                newObject.transform.SetParent(newImage.transform, false);
                spawnedObjects[newImage.referenceImage.name] = newObject;
            }
            else if (newImage.referenceImage.name == "spiral")
            {
                GameObject newObject = GameObject.Instantiate(spiralPrefab);
                newObject.transform.SetParent(newImage.transform, false);
                spawnedObjects[newImage.referenceImage.name] = newObject;
            }
            else if (newImage.referenceImage.name == "time")
            {
                GameObject newObject = GameObject.Instantiate(timePrefab);
                newObject.transform.SetParent(newImage.transform, false);
                spawnedObjects[newImage.referenceImage.name] = newObject;
            }
        }

 
        foreach (ARTrackedImage updatedImage in eventArgs.updated)
        {
            if (spawnedObjects.ContainsKey(updatedImage.referenceImage.name))
            {
                spawnedObjects[updatedImage.referenceImage.name].SetActive(updatedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking);
            }
        }

        foreach (ARTrackedImage removedImage in eventArgs.removed)
        {
            if (spawnedObjects.ContainsKey(removedImage.referenceImage.name))
            {
                Destroy(spawnedObjects[removedImage.referenceImage.name]);
                spawnedObjects.Remove(removedImage.referenceImage.name);
            }
        }
    }
}
