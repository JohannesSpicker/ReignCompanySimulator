using System;
using UnityEngine;

namespace Reign
{
    public class ExampleMovement : MonoBehaviour
    {
       public Transform myTransform;

        private void Update()
        {
            if (Input.anyKey)
            {
                var transformPosition = transform.position;
                transformPosition.x += 1f * Time.deltaTime;
                myTransform.position = transformPosition;
            }
        }
    }
}