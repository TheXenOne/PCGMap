using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnValidate()
    {
        Initialize();
        UpdateTexture();
    }
}