using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DirectoryGenerator
{
    public static Directory Generate()
    {
        List<Directory> subdirs = GenerateDirectories();
        List<File> files = GenerateFiles();
        Directory dir = new Directory("a", 2, 2, subdirs, files);
        return dir;
    }

    public static List<Directory> GenerateDirectories()
    {
        return new List<Directory>();
    }

    public static List<File> GenerateFiles()
    {
        return new List<File>(new File[] {
            new File(new RectInt(0,0,1,1)),
            new File(new RectInt(0,0,1,1)),
            new File(new RectInt(0,0,1,1)),
            new File(new RectInt(0,0,1,1))
        }); 
    }
}
