using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Babel.Db.Base;

namespace Babel.Api.Graph
{
    public class Node
    {
        public static int NODE_SIZE = 16;
        public Node Parent;
        public Vector Position;
        public Vector Center
        {
            get
            {
                return new Vector(Position.X + NODE_SIZE / 2, Position.Y + NODE_SIZE / 2);
            }
        }
        public float DistanceToTarget;
        public float Cost;
        public float Weight;
        public float F
        {
            get
            {
                if (DistanceToTarget != -1 && Cost != -1)
                    return DistanceToTarget + Cost;
                else
                    return -1;
            }
        }
        public bool Walkable;

        public Node(Vector pos, bool walkable, float weight = 1)
        {
            Parent = null;
            Position = pos;
            DistanceToTarget = -1;
            Cost = 1;
            Weight = weight;
            Walkable = walkable;
        }
    }
}
