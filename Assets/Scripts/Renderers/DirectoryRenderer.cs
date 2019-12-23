using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectoryRenderer
{
    private readonly int FILE_BUFFER = 2;

    public void RenderDirectory(Directory dir, GameObject parent, GameObject directoryPrefab, GameObject filePrefab)
    {
        PlaceDirectory(dir);
        InstantiateDirectory(dir, parent, directoryPrefab, filePrefab);
    }

    public void PlaceDirectory(Directory topDirectory)
    {
        if(topDirectory.directories.Count > 0 && topDirectory.files.Count > 0)
        {
            Directory fileDirectory = new Directory("[Directory Files]", topDirectory.path)
            {
                files = topDirectory.files
            };

            topDirectory.directories.Add(
                fileDirectory
            );
        }

        foreach (Directory subDirectory in topDirectory.directories)
        {
            if (subDirectory.directories.Count > 0)
            {
                PlaceDirectory(subDirectory);
            }
            else
            {
                if(subDirectory.files.Count > 0)
                {
                    subDirectory.bounds.SetMinMax(new Vector2Int(0, 0), GetFileDirectorySize(subDirectory.files));
                }
            }
        }

        FolderPlacementStrategy fps = new FolderPlacementStrategy();
        fps.PlaceDirectories(topDirectory.directories);
        topDirectory.bounds.SetMinMax(new Vector2Int(0, 0), new Vector2Int(fps.boundX, fps.boundY));
    }

    public void InstantiateDirectory(Directory topDirectory, GameObject directoryObject, GameObject directoryPrefab, GameObject filePrefab)
    {
        foreach (Directory subDirectory in topDirectory.directories)
        {
            GameObject subDirObj = GameObject.Instantiate(directoryPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            subDirObj.name = "[Dir] " + subDirectory.name + " :" + subDirectory.bounds.ToString();
            subDirObj.transform.parent = directoryObject.transform;
            subDirObj.transform.localPosition = new Vector3(subDirectory.bounds.xMin, 0.3f, subDirectory.bounds.yMin);
            subDirObj.transform.Find("DirectoryAnchor").localScale = new Vector3(subDirectory.bounds.width, 0.3f, subDirectory.bounds.height);
            subDirObj.GetComponent<DirectoryController>().directory = subDirectory;

            if (subDirectory.directories.Count > 0)
            {
                InstantiateDirectory(subDirectory, subDirObj, directoryPrefab, filePrefab);
                subDirObj.transform.Find("DirectoryAnchor/Cube").gameObject.SetActive(true);
                // SetDirectoryColor(subDirObj, Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
            }
            else
            {
                InstantiateFiles(subDirectory.files, subDirectory, subDirObj, filePrefab);
                subDirObj.transform.Find("DirectoryAnchor/Cube").gameObject.SetActive(true);

                //SetDirectoryColor(subDirObj, Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
            }

            SetDirectoryColor(subDirObj, Color.grey);
        }
    }

    public void SetDirectoryColor(GameObject directory, Color color)
    {
        directory.transform.Find("DirectoryAnchor/Cube").GetComponent<Renderer>().material.color = color;
    }

    public Vector2Int GetFileDirectorySize(List<File> files)
    {
        int width = (int)System.Math.Ceiling(System.Math.Sqrt(files.Count));
        int height = (files.Count % width == 0) ? width : width + 1;
        return new Vector2Int(width + (FILE_BUFFER * width), height + (FILE_BUFFER * height));
    }

    public void InstantiateFiles(List<File> files, Directory parentDirectory, GameObject directoryObject, GameObject filePrefab)
    {
        int i = 0;
        foreach(File file in files)
        {
            int width = (int)System.Math.Ceiling(System.Math.Sqrt(files.Count));
            int x = (i % width);
            int z = (int)System.Math.Floor((double)(i / width));
            GameObject fileObj = GameObject.Instantiate(filePrefab, directoryObject.transform);
            fileObj.name = "[File] " + file.name + "["+ (file.info.Length / 1000) +"kb]";
            FileRenderer fr = fileObj.GetComponent<FileRenderer>();
            fr.parentDirectory = parentDirectory;
            fr.file = file;
            fileObj.transform.localPosition = new Vector3(x + (FILE_BUFFER * x), 0.2f, z + (FILE_BUFFER * z));
            fileObj.SetActive(true);
            i++;
        }
    }
}
