using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Helpers
{
    public static class DirectionsHelper
    {
        public static Dictionary<Directions, Vector3Int> directionVectors = new Dictionary<Directions, Vector3Int>()
        {
            {Directions.None    , new Vector3Int(0,  0,  0)  },
            {Directions.Right   , new Vector3Int(1,  0,  0)  },
            {Directions.Left    , new Vector3Int(-1, 0,  0)  },
            {Directions.Stand   , new Vector3Int(0,  1,  0)  },
            {Directions.Crouch  , new Vector3Int(0, -1,  0)  },
            {Directions.Up      , new Vector3Int(0,  0,  1)  },
            {Directions.Down    , new Vector3Int(0,  0, -1)  }
        };

        [Flags]
        public enum Directions
        {
            None = 0,
            Right = 1,
            Left = 2,
            Stand = 4,
            Crouch = 8,
            Up = 16,
            Down = 32
        }
    }
}
