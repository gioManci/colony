using UnityEngine;
using System.Collections;

namespace Colony
{

    public class Aged : MonoBehaviour
    {

        public float Lifespan;
        public float Age { private set; get; }

        // Age bar management
        MaterialPropertyBlock block;
        public SpriteRenderer rend;

        void Start()
        {
            Age = Lifespan;
            rend = GetComponent<SpriteRenderer>();
            block = new MaterialPropertyBlock();
            rend.GetPropertyBlock(block);
            block.SetFloat("_Cutoff", 0f);
            rend.SetPropertyBlock(block);
        }

        void Update()
        {
            Age -= Time.deltaTime;
            if (Age < 0)
            {
                // Add code to handle destruction
                EntityManager.Instance.DestroyBee(gameObject);
            }
            else
            {
                updateAgeBar(Age / Lifespan);
            }
        }

        private void updateAgeBar(float percentage)
        {
            rend.GetPropertyBlock(block);
            block.SetFloat("_Cutoff", percentage);
            rend.SetPropertyBlock(block);
        }
    }
}