using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class Testing : MonoBehaviour
{
    [System.Serializable]
    public enum SortTestType
    {
        INT,
        VECTOR2
    }

    public int seed = 0;
    [ReadOnly]
    public int numPoints;
    public int textureWidth = 128;
    public int textureHeight = 128;
    [Range(0f, 10f)]
    public float scale = 1f;
    public Vector2 offset;
    public bool perlin = false;
    public SortTestType sortTestType;
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
        Stopwatch stopWatch = new Stopwatch();

        switch (sortTestType)
        {
            case SortTestType.INT:
                {
                    Mathf.Clamp(sortSize, 0, Int32.MaxValue / 2);
                    int[] sortedArray = new int[sortSize];
                    for (int i = 0; i < sortSize; i++)
                    {
                        sortedArray[i] = Random.Range(0, (int)sortSize * 2);
                    }

                    stopWatch.Start();

                    bool success = Sort<int, int>.MergeSort(sortedArray);

                    stopWatch.Stop();

                    if (success)
                    {
                        // Get the elapsed time as a TimeSpan value.
                        System.TimeSpan ts = stopWatch.Elapsed;

                        // Format and display the TimeSpan value.
                        string elapsedTime = (ts.TotalMilliseconds / 1000).ToString();

                        Debug.Log("Run time: " + elapsedTime + " seconds.");

                        int displayLength = (int)sortSize < 64 ? (int)sortSize : 64;
                        if (displayLength > 63)
                        {
                            Debug.Log("Displaying first 64 values only.");
                        }

                        string arrayString = "Sorted array: ";
                        for (int i = 0; i < displayLength; i++)
                        {
                            arrayString += sortedArray[i].ToString();
                            arrayString += " ";
                        }

                        Debug.Log(arrayString);
                    }
                }
                break;
            case SortTestType.VECTOR2:
                {
                    Mathf.Clamp(sortSize, 0, float.MaxValue / 2f);
                    Vector2[] sortedArray = new Vector2[sortSize];
                    Vector2 temp = new Vector2();
                    for (int i = 0; i < sortSize; i++)
                    {
                        temp.x = Random.Range(0, (int)sortSize * 2);
                        temp.y = Random.Range(0, (int)sortSize * 2);
                        sortedArray[i] = temp;
                    }
                    Func<Vector2[], int, float> accessor = (Vector2[] array, int index) => array[index].x;

                    stopWatch.Start();

                    Sort<Vector2, float>.MergeSort(sortedArray, accessor);

                    stopWatch.Stop();
                    // Get the elapsed time as a TimeSpan value.
                    System.TimeSpan ts = stopWatch.Elapsed;

                    // Format and display the TimeSpan value.
                    string elapsedTime = (ts.TotalMilliseconds / 1000).ToString();

                    Debug.Log("Run time: " + elapsedTime + " seconds.");

                    int displayLength = (int)sortSize < 64 ? (int)sortSize : 64;
                    if (displayLength > 63)
                    {
                        Debug.Log("Displaying first 64 values only.");
                    }

                    string arrayString = "Sorted array: ";
                    for (int i = 0; i < displayLength; i++)
                    {
                        arrayString += sortedArray[i].ToString();
                        arrayString += " ";
                    }

                    Debug.Log(arrayString);
                }
                break;
        }
    }

    private void OnValidate()
    {
        Initialize();
        UpdateTexture();
    }
}