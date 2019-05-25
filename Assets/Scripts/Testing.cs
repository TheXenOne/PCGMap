using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class Testing : MonoBehaviour
{
    public int seed = 0;
    [ReadOnly]
    public int numPoints;
    public int textureWidth = 128;
    public int textureHeight = 128;
    [Range(0f, 10f)]
    public float scale = 1f;
    public Vector2 offset;
    public bool perlin = false;
    public uint sortSize = 16;

    private System.Random randomGenerator;
    
    Testing()
    {
        Initialize();
    }

    private void Initialize()
    {
        ResetGenerator();
        numPoints = (int)(textureWidth * scale);
    }

    public void ResetGenerator()
    {
        randomGenerator = new System.Random(seed);
    }

    public void UpdateTexture()
    {
        Texture2D texture = new Texture2D(textureWidth, textureHeight);
        GetComponent<Renderer>().sharedMaterial.mainTexture = texture;

        Color pixelColour = new Color();

        for (int y = 0; y < textureHeight; y++)
        {
            for (int x = 0; x < textureWidth; x++)
            {
                texture.SetPixel(x, y, Color.white);
            }
        }

        if (perlin)
        {
            for (int y = 0; y < texture.height; y++)
            {
                for (int x = 0; x < texture.width; x++)
                {
                    float colour = colour = Mathf.PerlinNoise((float)x / texture.width * scale * 10f + offset.x, (float)y / texture.height * scale * 10f + offset.y);
                    pixelColour.r = colour;
                    pixelColour.g = colour;
                    pixelColour.b = colour;
                    texture.SetPixel(x, y, pixelColour);
                }
            }
        }
        else
        {
            pixelColour.r = 0f;
            pixelColour.r = 0f;
            pixelColour.r = 0f;

            for (int i = 0; i < numPoints; i++)
            {
                texture.SetPixel(randomGenerator.Next(0, textureWidth) + (int)offset.x, randomGenerator.Next(0, textureHeight) + (int)offset.y, pixelColour);
            }
        }

        texture.Apply();
    }

    public void TestMergeSort()
    {
        int[] sortedArray = new int[sortSize];
        
        for (int i = 0; i < sortSize; i++)
        {
            sortedArray[i] = Random.Range(0, (int)sortSize * 2);
        }

        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        MergeSort(sortedArray, 0, sortedArray.Length - 1);

        stopWatch.Stop();
        // Get the elapsed time as a TimeSpan value.
        System.TimeSpan ts = stopWatch.Elapsed;

        // Format and display the TimeSpan value.
        string elapsedTime = (ts.TotalMilliseconds / 1000).ToString();

        Debug.Log("Run time: " + elapsedTime + " seconds.");
        
        int displayLength = (int)sortSize < 64 ? (int)sortSize : 64;
        if (displayLength > 63)
        {
            Debug.Log("Displaying first 64 integers only.");
        }

        string arrayString = "Sorted array: ";
        for (int i = 0; i < displayLength; i++)
        {
            arrayString += sortedArray[i].ToString();
            arrayString += " ";
        }

        Debug.Log(arrayString);
    }

    void MergeSort(int[] array, int left, int right)
    {
        if (left < right)
        {
            int middle = left + (right - left) / 2;
            MergeSort(array, left, middle);
            MergeSort(array, middle + 1, right);
            Merge(array, left, middle, right);
        }
    }

    void Merge(int[] array, int left, int middle, int right)
    {
        int[] sortedArray = new int[right - left + 1];
        int currentIndex = 0;
        int indexLeft = left;
        int indexRight = middle + 1;

        while (indexLeft < middle + 1 && indexRight < right + 1)
        {
            if (array[indexLeft] < array[indexRight])
            {
                sortedArray[currentIndex++] = array[indexLeft++];
            }
            else if (array[indexRight] < array[indexLeft])
            {
                sortedArray[currentIndex++] = array[indexRight++];
            }
            else if (array[indexRight] == array[indexLeft])
            {
                sortedArray[currentIndex++] = array[indexLeft++];
            }
            else
            {
                //error
            }
        }

        for (int i = indexLeft; i < middle + 1; ++i)
        {
            sortedArray[currentIndex++] = array[i];
        }

        for (int i = indexRight; i < right + 1; ++i)
        {
            sortedArray[currentIndex++] = array[i];
        }

        for (int i = 0; i < right - left + 1; ++i)
        {
            array[left + i] = sortedArray[i];
        }
    }

    private void OnValidate()
    {
        Initialize();
        UpdateTexture();
    }
}