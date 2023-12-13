using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamingTools
{
    public static class UnityAddOn
    {
        public static void RotateX(Transform t, float amount)
        {
            t.Rotate(new Vector3(amount * Time.deltaTime, 0, 0));
        }
        public static void RotateY(Transform t, float amount)
        {
            t.Rotate(new Vector3(0, amount * Time.deltaTime, 0));
        }
        public static void RotateZ(Transform t, float amount)
        {
            t.Rotate(new Vector3(0, 0, amount * Time.deltaTime));
        }
    }
}