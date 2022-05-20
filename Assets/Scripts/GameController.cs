using UnityEngine;
using Random = UnityEngine.Random;

namespace Bounce
{
    public enum GameMode
    {
        Speed,
        Precision
    }

    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        public GameObject target;
        public float xSize = 18.0f;
        public float ySize = -0.75f;
        public float zSize = 8.0f;
        public HitController player;
        public GameMode mode;
        
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