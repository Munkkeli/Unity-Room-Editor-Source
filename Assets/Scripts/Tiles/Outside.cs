using UnityEngine;
using System.Collections;

namespace Tiles {
    public class Outside : Tile {
        public override void Create(int x, int y, Geometry geometry, Tile[] sides) {
            geometry.CreateFace(x, 2, y, Geometry.Direction.up, null, 1);
        }
    }
}
