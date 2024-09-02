using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Neoxider
{
    public class Spawner : MonoBehaviour
    {
        [Header("Spawn Setting")]
        [SerializeField] private GameObject[] prefabs;

        [Space, Header("If spawnLimit is zero then infinite spawn")]
        [SerializeField] private int spawnLimit = 0;
        [SerializeField] private float minSpawnDelay = 0.5f;
        [SerializeField] private float maxSpawnDelay = 2f;


        [Space, Header("Other Settings")]
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private bool spawnOnAwake;


        [Header("Spawn Area")]
        [SerializeField] private Collider spawnAreaCollider;
        [SerializeField] private Collider2D spawnAreaCollider2D;

        public bool isSpawning;

        private List<GameObject> spawnedObjects = new List<GameObject>();
        private int _spawnedCount = 0;

        void Start()
        {
            if (spawnOnAwake)
                StartSpawn();
        }

        public void StartSpawn()
        {
            if (!isSpawning)
            {
                StartCoroutine(SpawnObjects());
            }
        }

        private IEnumerator SpawnObjects()
        {
            _spawnedCount = 0;
            isSpawning = true;

            while (isSpawning && ((spawnLimit == 0) || _spawnedCount < spawnLimit))
            {
                SpawnRandomObject();
                _spawnedCount++;
                float randomDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
                yield return new WaitForSeconds(randomDelay);
            }

            isSpawning = false;
        }

        private void SpawnRandomObject()
        {
            if (prefabs.Length == 0) return;

            int randomIndex = Random.Range(0, prefabs.Length);
            GameObject prefabToSpawn = prefabs[randomIndex];
            GameObject spawnedObject;

            Vector3 spawnPosition = transform.position;

            if (spawnAreaCollider != null)
                spawnPosition = GetRandomPointInCollider(spawnAreaCollider);
            else if (spawnAreaCollider2D != null)
                spawnPosition = GetRandomPointInCollider2D(spawnAreaCollider2D);

            spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity, transform);

            spawnedObjects.Add(spawnedObject);
        }

        public void RemoveAllSpawnedObjects()
        {
            isSpawning=  false;
            print("Clear");

            foreach (GameObject obj in spawnedObjects)
            {
                {
                    Destroy(obj);
                }
            }

            spawnedObjects.Clear();
        }

        public int GetActiveObjectCount()
        {
            return spawnedObjects.Count(obj => obj != null);
        }

        private void OnValidate()
        {
            if (spawnPoint == null)
                spawnPoint = transform;
        }

        private Vector3 GetRandomPointInCollider2D(Collider2D collider)
        {
            if (collider is BoxCollider2D boxCollider)
            {
                Vector2 center = boxCollider.offset;
                Vector2 size = boxCollider.size;

                Vector2 randomPoint = new Vector2(
                    Random.Range(center.x - size.x / 2, center.x + size.x / 2),
                    Random.Range(center.y - size.y / 2, center.y + size.y / 2)
                );

                return boxCollider.transform.TransformPoint(randomPoint);
            }
            else if (collider is CircleCollider2D circleCollider)
            {
                Vector2 center = circleCollider.offset;
                float radius = circleCollider.radius;

                Vector2 randomPoint = Random.insideUnitCircle * radius + center;

                return circleCollider.transform.TransformPoint(randomPoint);
            }
            else
            {
                Debug.LogWarning("Unsupported collider type for spawning.");
                return spawnPoint.position;
            }
        }

        private Vector3 GetRandomPointInCollider(Collider collider)
        {
            if (collider is BoxCollider boxCollider)
            {
                Vector3 center = boxCollider.center;
                Vector3 size = boxCollider.size;

                Vector3 randomPoint = new Vector3(
                    Random.Range(center.x - size.x / 2, center.x + size.x / 2),
                    Random.Range(center.y - size.y / 2, center.y + size.y / 2),
                    Random.Range(center.z - size.z / 2, center.z + size.z / 2)
                );

                return boxCollider.transform.TransformPoint(randomPoint);
            }
            else if (collider is SphereCollider sphereCollider)
            {
                Vector3 center = sphereCollider.center;
                float radius = sphereCollider.radius;

                Vector3 randomPoint = Random.insideUnitSphere * radius + center;

                return sphereCollider.transform.TransformPoint(randomPoint);
            }
            else if (collider is CapsuleCollider capsuleCollider)
            {
                Vector3 center = capsuleCollider.center;
                float radius = capsuleCollider.radius;
                float height = capsuleCollider.height;

                Vector3 randomPoint = Random.insideUnitSphere * radius + center;
                randomPoint.y = Random.Range(center.y - height / 2, center.y + height / 2);

                return capsuleCollider.transform.TransformPoint(randomPoint);
            }
            else
            {
                Debug.LogWarning("Unsupported collider type for spawning.");
                return spawnPoint.position;
            }
        }
    }
}
