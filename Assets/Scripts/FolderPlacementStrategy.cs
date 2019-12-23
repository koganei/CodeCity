using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderPlacementStrategy
{

    public List<Vector2Int> vertices;

    public int boundX = 0;
    public int boundY = 0;

    public List<Directory> directoriesToPlace;
    public List<Directory> directoriesPlaced;

    public FolderPlacementStrategy()
    {
        vertices = new List<Vector2Int>
        {
            new Vector2Int(0, 0)
        };
        directoriesToPlace = new List<Directory>();
        directoriesPlaced = new List<Directory>();
}

    public void PlaceDirectories(List<Directory> directories)
    {
        directoriesToPlace = new List<Directory>(directories);

        while (directoriesToPlace.Count > 0)
        {
            if (boundX > boundY)
            {
                PlaceNextDirectoryVertically();
            }
            else
            {
                PlaceNextDirectoryHorizontally();
            }
        }
    }

    public void PlaceNextDirectoryVertically()
    {
        Directory biggestY = FindDirectorywithBiggestSize(false);
        Vector2Int FirstFreeVertice = FindFreeVerticeOnXAxis(biggestY.bounds.size);
        PlaceDirectory(biggestY, FirstFreeVertice);
    }

    public void PlaceNextDirectoryHorizontally()
    {
        Directory biggestX = FindDirectorywithBiggestSize();
        Vector2Int FirstFreeVertice = FindFreeVerticeOnYAxis(biggestX.bounds.size);
        PlaceDirectory(biggestX, FirstFreeVertice);
    }


    public Directory FindDirectorywithBiggestSize(bool isWidth = true)
    {
        int max = 0;
        Directory maxDirectory = directoriesToPlace[0];
        foreach (Directory dir in directoriesToPlace)
        {
            int currentDirSize = isWidth ? dir.bounds.width : dir.bounds.height;
            if (currentDirSize > max)
            {
                max = currentDirSize;
                maxDirectory = dir;
            }
        }
        return maxDirectory;
    }

    public void PlaceDirectory(Directory directoryToPlace, Vector2Int position) {
        directoryToPlace.bounds.SetMinMax(position, new Vector2Int(position.x + directoryToPlace.bounds.width, position.y + directoryToPlace.bounds.height));
        Vector2Int[] verticesToAdd = new Vector2Int[] {
            new Vector2Int(directoryToPlace.bounds.xMin, directoryToPlace.bounds.yMin),
            new Vector2Int(directoryToPlace.bounds.xMin, directoryToPlace.bounds.yMax),
            new Vector2Int(directoryToPlace.bounds.xMax, directoryToPlace.bounds.yMin),
            new Vector2Int(directoryToPlace.bounds.xMax, directoryToPlace.bounds.yMax)
        };

        vertices.AddRange(verticesToAdd);
        directoriesToPlace.Remove(directoryToPlace);
        directoriesPlaced.Add(directoryToPlace);

        if(boundX < directoryToPlace.bounds.xMax)
        {
            boundX = directoryToPlace.bounds.xMax;
        }

        if (boundY < directoryToPlace.bounds.yMax)
        {
            boundY = directoryToPlace.bounds.yMax;
        }
    }

    public Vector2Int FindFreeVerticeOnXAxis(Vector2Int size)
    {
        List<Vector2Int> potentialVertices = new List<Vector2Int>();
        foreach(Vector2Int vi in vertices)
        {
            bool foundVerticeFurtherOut = false;
            foreach(Vector2Int vj in vertices)
            {
                if(vi.x == vj.x && vi.y < vj.y)
                {
                    foundVerticeFurtherOut = true;
                    break;
                }
            }

            if (!foundVerticeFurtherOut && HasSpaceAtVertice(vi, size))
            {
                potentialVertices.Add(vi);
            }
        }

        Vector2Int lowestVertice = potentialVertices[0];
        foreach(Vector2Int p in potentialVertices)
        {
            if(p.y < lowestVertice.y)
            {
                lowestVertice = p;
            }
        }

        return lowestVertice;
    }

    public Vector2Int FindFreeVerticeOnYAxis(Vector2Int size)
    {
        List<Vector2Int> potentialVertices = new List<Vector2Int>();
        foreach (Vector2Int vi in vertices)
        {
            bool foundVerticeFurtherOut = false;
            foreach (Vector2Int vj in vertices)
            {
                if (vi.y == vj.y && vi.x < vj.x)
                {
                    foundVerticeFurtherOut = true;
                    break;
                }
            }

            if (!foundVerticeFurtherOut && HasSpaceAtVertice(vi, size))
            {
                potentialVertices.Add(vi);
            }
        }

        Vector2Int lowestVertice = potentialVertices[0];
        foreach (Vector2Int p in potentialVertices)
        {
            if (p.x < lowestVertice.x)
            {
                lowestVertice = p;
            }
        }

        return lowestVertice;
    }

    public bool HasSpaceAtVertice(Vector2Int placementVertice, Vector2Int size)
    {
        foreach (Directory placedDirectory in directoriesPlaced)
        {

            RectInt placementRect = new RectInt(placementVertice, size);
            bool invalidPosition = false;

            foreach (var position in placementRect.allPositionsWithin)
            {
                if (placedDirectory.bounds.Contains(position))
                {
                    invalidPosition = true;
                    break;
                }
            }

            if(invalidPosition)
            {
                return false;
            }

            if (placedDirectory.bounds.Contains(placementVertice)
                || placedDirectory.bounds.Contains(new Vector2Int(placementVertice.x + size.x, placementVertice.y))
                || placedDirectory.bounds.Contains(new Vector2Int(placementVertice.x, placementVertice.y + size.y))
                || placedDirectory.bounds.Contains(new Vector2Int(placementVertice.x + size.x, placementVertice.y + size.y)))
            {
                return false;
            }
        }

        return true;
    }
}

