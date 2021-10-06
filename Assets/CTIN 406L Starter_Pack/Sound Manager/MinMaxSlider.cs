using UnityEngine;

namespace CTIN_406L_Starter_Pack.Sound_Manager
{
    public class MinMaxSlider : PropertyAttribute
    {
    

        public float min;
        public float max;

        public MinMaxSlider(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    
    }
}