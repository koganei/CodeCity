using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeCityDemo : MonoBehaviour
{
    public GameObject directoryPrefab;
    public GameObject filePrefab;
    public GameObject player;
    public GameObject car;

    void Start()
    {
        string path = "F:/www/brick";
        string commandLinePath = GetArg("codepath");
        if (commandLinePath != null)
        {
            path = commandLinePath;
        }

        print("Loading code path:" + path);

        string ignoreFolders = GetArg("ignore");
        string[] ignoredFoldersArray = ignoreFolders != null ? ignoreFolders.Split(",".ToCharArray()) : null;
        Directory fullD = DirectoryReader.ReadDirectory(path, ignoredFoldersArray);
        DirectoryRenderer dr = new DirectoryRenderer();
        dr.RenderDirectory(fullD, gameObject, directoryPrefab, filePrefab);
        // FileWatcher.Watch(path);
        player.transform.position = new Vector3(25, 10, 25);
    }

    private void Update()
    {
        if(Input.GetKeyDown("c"))
        {
            SwitchTransportationMode();
        }
    }

    // Helper function for getting the command line arguments
    private static string GetArg(string name)
    {
        var args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == name && args.Length > i + 1)
            {
                return args[i + 1];
            }
        }
        return null;
    }

    private void SwitchTransportationMode()
    {
        if(player.activeSelf)
        {
            car.transform.position = new Vector3(player.transform.position.x, 10, player.transform.position.z);
            player.SetActive(false);
            car.SetActive(true);
        } else
        {
            player.transform.position = new Vector3(car.transform.position.x, 10, car.transform.position.z);
            player.SetActive(true);
            car.SetActive(false);
        }
    }


}
