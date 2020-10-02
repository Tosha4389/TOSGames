using UnityEngine;
using System.IO;

public class Test : MonoBehaviour
{
    // Source textures.
    public Texture2D[] atlasTextures;

    // Rectangles for individual atlas textures.
    Rect[] rects;

    void Start()
    {
        // Pack the individual textures into the smallest possible space,
        // while leaving a two pixel gap between their edges.
        Texture2D atlas = new Texture2D(8192, 8192);
        rects = atlas.PackTextures(atlasTextures, 2, 8192);

        byte[] bytes = atlas.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/../Assets/Atlases/SavedScreen.png", bytes);
        Debug.Log("FileSaved!");
    }
}