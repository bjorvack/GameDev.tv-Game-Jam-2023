using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Prefab;

    [SerializeField]
    private float m_SpawnRate = 1.0f;

    float m_SpawnTimer = 0.0f;

    float lastSpawn = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lastSpawn += Time.deltaTime;

        if (lastSpawn > m_SpawnTimer)
        {
            m_SpawnTimer = m_SpawnRate + Random.Range(0.1f, 2f);
            lastSpawn = 0.0f;
            GameObject go = Instantiate(m_Prefab, transform.position, Quaternion.identity);
            go.transform.parent = transform;
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1);
    }
}
