using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[Serializable]
public class File
{
    public FileInfo info;
    public RectInt bounds;
    public string name;
    public string path;

    public File()
    {}

    public File(RectInt bounds)
    {
        this.bounds = bounds;
    }

    public override string ToString()
    {
        return "File[" + this.name + "] [" + this.bounds.ToString() + "]";
    }
}
