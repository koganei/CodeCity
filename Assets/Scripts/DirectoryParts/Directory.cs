using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Directory
{
    public RectInt bounds;
    public Vector2Int size;
    public List<Directory> directories;
    public List<File> files;
    public string name;
    public string path;
    public GameObject obj;

    public Directory(string name, int sizeX, int sizeY, List<Directory> directories, List<File> files)
    {
        sizeY = UnityEngine.Random.Range(sizeX, sizeX+1);
        size = new Vector2Int(sizeX, sizeY);
        this.bounds = new RectInt(new Vector2Int(0,0), size);
        this.directories = directories;
        this.files = files;
        this.name = name;
    }

    public Directory(string name, string path)
    {
        this.name = name;
        this.path = path;
        this.files = new List<File>();
        this.directories = new List<Directory>();
    }

    public override string ToString()
    {
        // string directory = "Dir["+ name +"][" + bounds.ToString() + "] [" + directories.Count + " subdirs] ["+ files.Count +" files]\n";
        string directory = "Dir[" + name + "]["+ path +"][" + bounds.ToString() + "]\n";

        //foreach (Directory d in directories)
        //{
        //    directory += "\t" + d.ToString() + "\n";
        //}

        //foreach (File f in files)
        //{
        //    directory += "\t" + f.ToString() + "\n";
        //}

        return directory;
    }
}
