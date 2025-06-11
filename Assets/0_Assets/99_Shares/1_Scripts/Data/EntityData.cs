using UnityEngine;

namespace Attention.Data
{
    public class EntityData
    {
        public Vector3 position = new Vector3(0, -11);
        public bool direction = true;

        public bool isActivate = true;
        public bool isVisible = true;
        public bool isCollerable = false;

        public string basicImage = null;
        public int animator = -1;

        public EntityData()
        {
            position = new Vector3(0, -11);
            direction = true;

            isActivate = true;
            isVisible = true;
            isCollerable = false;

            basicImage = null;
            animator = -1;
    }
    }
}