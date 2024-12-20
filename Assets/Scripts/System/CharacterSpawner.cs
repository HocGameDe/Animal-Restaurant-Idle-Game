using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public static CharacterSpawner Instance;
    public float timeBetweenSpawn = 3f;
    public Transform spawnPoint;
    public Transform startPoint;
    public Transform exitPoint;
    public Character character;
    float nextSpawn;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Time.time > nextSpawn)
        {
            SpawnVisitor();
        }
    }

    public void SpawnVisitor()
    {
        nextSpawn = Time.time + timeBetweenSpawn * Random.Range(0.75f, 1.25f);
        var spawned = MyPoolManager.Instance.Get<Character>(character.gameObject);
        spawned.transform.position = spawnPoint.position;
        spawned.StartVisit();
    }
}