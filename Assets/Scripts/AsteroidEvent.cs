using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidEvent : Event
{
    // Fields for spawning asteroids
    [SerializeField] private GameObject asteroidPrefab;
	[SerializeField] private Vector2 asteroidSpeed;
	private List<GameObject> asteroids = new List<GameObject>();
    private Vector2 spawnPosition;
    private bool isSpawning = false;
    private int numAsteroidsSpawned = 0;
    private int numAsteroidsDestroyed = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        // Initialize things for the asteroids
        base.Start();
        spawnPosition = new Vector2(Camera.main.transform.position.x + (Camera.main.aspect * Camera.main.orthographicSize) + 8, 0);
    }

	protected override void FixedUpdate()
	{
		// Play the warning
		base.FixedUpdate();
	}

	// Update is called once per frame
	private void Update()
    {
        // When the warning is done, spawn asteroids
        if (!isWarningPlaying)
        {
   //         if (Input.GetKey(KeyCode.W))
   //         {
   //             player.GetComponent<Rigidbody2D>().velocity = Vector2.up * 3f;
   //         }
   //         else if(Input.GetKey(KeyCode.S))
   //         {
			//	player.GetComponent<Rigidbody2D>().velocity = Vector2.down * 3f;
			//}
			//else
			//{
			//	player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			//}

			player.GetComponent<Rigidbody2D>().velocity = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position).normalized * 10f;
			//player.transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
			player.transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(-(Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position).x, (Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position).y));

			if (!isSpawning)
			{
				if (numAsteroidsSpawned < 20)
				{
					isSpawning = true;
					StartCoroutine(SpawnAsteroid());
				}
			}

			// Destroy old asteroids
			if (asteroids.Count > 0 && asteroids[0].transform.position.x < -Camera.main.aspect * Camera.main.orthographicSize - 8)
			{
				GameObject oldAsteroid = asteroids[0];
				asteroids.RemoveAt(0);
				Destroy(oldAsteroid);
				numAsteroidsDestroyed++;

				if (numAsteroidsDestroyed == 20)
				{
					player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					Destroy(gameObject);
				}
			}
		}
	}

    // Spawn an asteroid at a random location off-screen
    private IEnumerator SpawnAsteroid()
    {
        yield return new WaitForSeconds(1);

        spawnPosition.y = Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize);
        asteroids.Add(Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity));

		asteroidSpeed.y = Mathf.Atan2(spawnPosition.y, spawnPosition.x) * asteroidSpeed.x;

        asteroids[asteroids.Count - 1].GetComponent<Rigidbody2D>().velocity = asteroidSpeed;
        numAsteroidsSpawned++;

        isSpawning = false;

        yield return null;
    }
}
