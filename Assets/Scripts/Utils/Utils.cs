using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class Utils
    {
        public static Vector3 getRandomVector3()
        {
            var rand = UnityEngine.Random.Range(-9f, 9f);
            return new Vector3(rand, 6, 0);
        }
    }
}
