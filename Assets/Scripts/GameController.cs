using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Bounce
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        public GameObject target;
        public float xSize = 5.0f;
        public float ySize = 0.0f;
        public float zSize = 5.0f;
        public HitController player;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        public void Spawn()
        {
            Vector3 playerPosition = player.transform.position;
            Vector3 spawnPoint = new Vector3(Random.Range(-xSize, xSize), ySize, Random.Range(-zSize, zSize));

            while ((spawnPoint - playerPosition).magnitude <= 3.0f)
            {
                spawnPoint = new Vector3(Random.Range(-xSize, xSize), ySize, Random.Range(-zSize, zSize));
            }

            Instantiate(target, spawnPoint, Quaternion.identity);
        }


    }
}