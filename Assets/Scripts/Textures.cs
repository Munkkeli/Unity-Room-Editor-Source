using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Textures : MonoBehaviour {

    // The tile class
    public class Tile {
        public int x, y, width, height;
        public Texture2D texture;
        public Vector2[] uv;

        public Tile(int x_, int y_, int width_, int height_, Texture2D texture_, Vector2[] uv_) {
            x = x_;
            y = y_;
            width = width_;
            height = height_;
            texture = texture_;
            uv = uv_;
        }
    }

    public static Dictionary<string, Tile> TEXTURES = new Dictionary<string, Tile>();
    public static Texture2D TILEMAP;

    public static void Create() {
        // Get all the default textures
        Texture2D[] textures = Resources.LoadAll<Texture2D>("Textures");

        // Create the TEXTURES dictionary
        int width = 0, height = 0;
        for (int i = 0; i < textures.Length; i++) {
            TEXTURES.Add(textures[i].name, new Tile(width, 0, textures[i].width, textures[i].height, textures[i], null));
            width += textures[i].width;
            if (textures[i].height > height)
                height = textures[i].height;
        }

        // Create the tilemap
        TILEMAP = new Texture2D(width, height, TextureFormat.ARGB32, false);
        foreach (KeyValuePair<string, Tile> texture in TEXTURES) {
            for (int x = 0; x < texture.Value.width; x++) {
                for (int y = 0; y < texture.Value.height; y++) {
                    TILEMAP.SetPixel(texture.Value.x + x, texture.Value.y + y, texture.Value.texture.GetPixel(y, x));
                }
            }

            // Generate the uvs for this tile
            float uvX = ((float)texture.Value.x) / (float)width;
            float uvY = (((float)texture.Value.y + 1) * texture.Value.height) / (float)height;
            float uvWidth = ((texture.Value.width) / (float)width);
            float uvHeight = ((texture.Value.height) / (float)height);
            texture.Value.uv = new Vector2[] {
                new Vector2(uvX, uvY),
                new Vector2(uvX, uvY + uvHeight),
                new Vector2(uvX + uvWidth, uvY + uvHeight),
                new Vector2(uvX + uvWidth, uvY),
            };

        }
        TILEMAP.filterMode = FilterMode.Point;
        TILEMAP.Apply();
    }
}
