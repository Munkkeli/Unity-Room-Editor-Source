using UnityEngine;
using System.Collections;

public class Tile {
    public bool filled = true;

    public virtual void Create(int x, int y, Geometry geometry, Tile[] sides) {
        geometry.CreateFace(x, 0, y, Geometry.Direction.east);
        geometry.CreateFace(x, 0, y, Geometry.Direction.west);
        geometry.CreateFace(x, 0, y, Geometry.Direction.up);
        geometry.CreateFace(x, 0, y, Geometry.Direction.down);
    }
}
