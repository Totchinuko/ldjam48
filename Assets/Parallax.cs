using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constantine
{
    public class Parallax : MonoBehaviour
    {
        public float _speed;
        public Renderer _renderer;
        private void Update()
        {
            Vector2 offset = new Vector2(0, Time.time * -_speed);
            _renderer.material.mainTextureOffset = offset;
        }
    }

}

