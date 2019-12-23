using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectoryTrigger : MonoBehaviour
{

    public CanvasController canvasController;
    private Directory dir = null;

    // Start is called before the first frame update
    void Awake()
    {
        canvasController = Transform.FindObjectOfType<CanvasController>();
    }

    void GetDir()
    {
        dir = transform.GetComponentInParent<DirectoryController>().directory;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(dir == null) GetDir();
        
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player entered directory:" + dir.ToString());
            canvasController.AddLocation(dir);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (dir == null) GetDir();

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered directory:" + dir.ToString());
            canvasController.RemoveLocation(dir);
        }
    }
}
