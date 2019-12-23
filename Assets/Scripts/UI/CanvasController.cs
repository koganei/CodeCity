using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public List<Directory> currentLocations;
    public Directory currentLocation;

    public TextMeshProUGUI currentLocationText;

    // Start is called before the first frame update
    void Start()
    {
        currentLocations = new List<Directory>();
    }

    private void Awake()
    {
        currentLocationText = transform.Find("CurrentLocation").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void UpdateUI()
    {
        if(currentLocation == null)
        {
            currentLocationText.text = "No Location";
        } else
        {
            currentLocationText.text = $"Current Location: {currentLocation.path}";
        }
        
    }

    public void AddLocation(Directory directory)
    {
        if(!currentLocations.Contains(directory))
        {
            currentLocations.Add(directory);
            UpdateLocation();
        }

    }

    public void RemoveLocation(Directory directory)
    {
        if (currentLocations.Contains(directory))
        {
            currentLocations.Remove(directory);
            if(currentLocation == directory)
            {
                currentLocation = null;
            }
            UpdateLocation();
        }
    }

    public void UpdateLocation()
    {
        foreach(Directory d in currentLocations)
        {
            if(currentLocation == null || d.path.Length > currentLocation.path.Length)
            {
                currentLocation = d;
            }
        }
        UpdateUI();
    }
}
