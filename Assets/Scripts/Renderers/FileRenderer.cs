using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileRenderer : MonoBehaviour
{
    public Directory parentDirectory;
    public File file;

    private float HEIGHT_DAMPENER = 50000f;

    // Start is called before the first frame update
    public void Start()
    {
        float height = System.Math.Max(file.info.Length / HEIGHT_DAMPENER, 1f);
        gameObject.transform.localScale = new Vector3(1, height, 1);

        if(file.info.Length > 1000000)
        {
            SetColor(Color.red);
        }
        else if(file.info.Length > 250000)
        {
            SetColor(Color.yellow);
        } else
        {
            SetColor(Color.green);
        }
    }

    public void SetColor(Color color)
    {
        transform.Find("Cube").GetComponent<Renderer>().material.color = color;
    }
}
