using UnityEngine;

namespace Attention.View
{
    public class Obj_Cat : Obj_Base
    {
        public override ViewType Type => ViewType.Cat;

        public void TimeEventUpdate(float deltaTime)
        {
            //Debug.Log("wowowo : " + deltaTime);
            this.transform.Translate(new Vector3(1, 0, 0));
        }
    }
}