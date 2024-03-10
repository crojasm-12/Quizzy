using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlane : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera mainCamera = Camera.main; // Get the main camera
        float screenHeight = mainCamera.orthographicSize * 2; // Camera's vertical size
        float screenWidth = screenHeight * mainCamera.aspect; // Calculate screen width based on aspect ratio

        // Assuming the Plane's size is 10x10 Unity units by default, calculate the needed scale
        Vector3 newScale = new Vector3(screenWidth / 10f, 1, screenHeight / 10f);
        GameObject.Find("PlaneBackground").transform.localScale = newScale;

        Color newColor = Color.red; // Set to any color you want
        GameObject.Find("PlaneBackground").GetComponent<Renderer>().material.color = newColor;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
