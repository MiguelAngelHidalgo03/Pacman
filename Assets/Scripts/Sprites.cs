using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprites : MonoBehaviour
{
    // Ponemos las variables con la altura y la anchura de los personajes
    public Texture2D sheet;
    public int baseTileWidth = 16;
    public int baseTileHeight = 16;
    public int tileWidth = 16;
    public int tileHeight = 16; 

    void Start()
    {
        LoadSpritesheet();
    }

    void LoadSpritesheet()
    {
        // Carga la textura desde los recursos
        sheet = Resources.Load<Texture2D>("spritesheet");

        // Establece el color transparente (si es necesario)
        Color transcolor = sheet.GetPixel(0, 0);
        SetTransparentColor(transcolor);

        // Redimensiona la textura
        int width = sheet.width / baseTileWidth * tileWidth;
        int height = sheet.height / baseTileHeight * tileHeight;
        sheet = ScaleTexture(sheet, width, height);
    }

    void SetTransparentColor(Color transcolor)
    {
        for (int y = 0; y < sheet.height; y++)
        {
            for (int x = 0; x < sheet.width; x++)
            {
                if (sheet.GetPixel(x, y) == transcolor)
                {
                    sheet.SetPixel(x, y, new Color(0, 0, 0, 0));
                }
            }
        }
        sheet.Apply();
    }

    public Sprite GetImage(int x, int y, int width, int height)
    {
        x *= tileWidth;
        y *= tileHeight;
        Rect rect = new Rect(x, y, width, height);
        return Sprite.Create(sheet, rect, new Vector2(0.5f, 0.5f));
    }

    Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
    {
        Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, false);
        for (int y = 0; y < result.height; y++)
        {
            for (int x = 0; x < result.width; x++)
            {
                Color newColor = source.GetPixelBilinear((float)x / result.width, (float)y / result.height);
                result.SetPixel(x, y, newColor);
            }
        }
        result.Apply();
        return result;
    }


}
