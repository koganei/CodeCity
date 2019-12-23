using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DirectoryReader
{

    public static Directory ReadDirectory(string path, string[] ignoreFolders, string name = ".")
    {
        Directory dirInfo = new Directory(name, path);

        try
        {
            foreach (string dir in System.IO.Directory.EnumerateDirectories(path))
            {
                string subDirName = dir.Substring(dir.LastIndexOf("\\") + 1);
                bool isIgnored = false;
                if (ignoreFolders != null)
                {
                    foreach (string ignoreFolder in ignoreFolders)
                    {
                        if (subDirName == ignoreFolder)
                        {
                            isIgnored = true;
                        }
                    }
                }
                if (subDirName != ".git" && subDirName != "node_modules" && !isIgnored)
                {
                    Directory subDir = ReadDirectory(dir, ignoreFolders, subDirName);
                    dirInfo.directories.Add(subDir);
                }
            }
        } catch (System.Exception Error)
        {
            Debug.LogError("Error while Enumerating Directories:" + Error.Message);
        }
        

        foreach (string file in System.IO.Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly))
        {
            string subFileName = file.Substring(file.LastIndexOf("\\") + 1);
            dirInfo.files.Add(new File
            {
                path = file,
                name = subFileName,
                info = new FileInfo(file)
            });

        }

        return dirInfo;
    }
}