using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidEvent : Event
{
    // Fields for spawning asteroids
    [SerializeField] private GameObject asteroidPrefab;
	[SerializeField] private Vector2 asteroidSpeed;
	[SerializeField] private GameObject indicatorPrefab;
	private List<GameObject> asteroids = new List<GameObject>();
    private Vector2 spawnPosition;
    private bool isSpawning = false;
    private int numAsteroidsSpawned = 0;
    private int numAsteroidsDestroyed = 0;
	private GameObject indicator = null;
	private Color indicatorColor;
	private float percentChanged = 0.05f;
	private bool isFading = false;

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

		if(isSpawning)
		{
			StartCoroutine(PlayWarning());
		}
	}

	// Update is called once per frame
	private void Update()
    {
        // When the warning is done, spawn asteroids
        if (!isWarningPlaying)
        {
			player.GetComponent<Rigidbody2D>().velocity = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position).normalized * 10f;
			player.transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(-(Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position).x, (Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position).y));

			if (!isSpawning)
			{
				if (numAsteroidsSpawned < 25)
				{
					isSpawning = true;
					// y = mx + b
					float m = (spawnPosition.y - player.transform.position.y) / (spawnPosition.x - player.transform.position.x);
					float b = spawnPosition.y - (m * spawnPosition.x);

					float dangerX = (Camera.main.aspect * Camera.main.orthographicSize) - 0.5f;
					float dangerY = (m * dangerX) + b;
					if (indicator != null) Destroy(indicator);
					indicator = Instantiate(indicatorPrefab, new Vector2((Camera.main.aspect * Camera.main.orthographicSize) - 0.3f, dangerY), Quaternion.identity);
					indicatorColor = indicator.GetComponent<SpriteRenderer>().color;
					indicator.GetComponent<SpriteRenderer>().color = new Color(indicatorColor.r, indicatorColor.g, indicatorColor.b, 0f);

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

				// At this point the event is over. This part should bring the player back to the narrative
				if (numAsteroidsDestroyed == 25)
				{
					player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					Destroy(gameObject);
				}
			}
		}
	}

	private IEnumerator PlayWarning()
	{
		indicator.GetComponent<SpriteRenderer>().color = new Color(indicatorColor.r, indicatorColor.g, indicatorColor.b, Mathf.Lerp(0f, 1f, percentChanged));

		percentChanged += isFading ? -0.05f : 0.05f;

		if(percentChanged >= 1f)
		{
			isFading = true;
		}
		else if(percentChanged <= 0f)
		{
			isFading = false;
		}

		yield return null;
	}

    // Spawn an asteroid at a random location off-screen
    private IEnumerator SpawnAsteroid()
    {
        yield return new WaitForSeconds(0.7f);

        spawnPosition.y = Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize);
        asteroids.Add(Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity));

		Vector2 playerAsteroidDistance = (Vector2)player.transform.position - spawnPosition;

		asteroidSpeed = playerAsteroidDistance.normalized * (8f);

		asteroids[asteroids.Count - 1].GetComponent<Rigidbody2D>().velocity = asteroidSpeed;
        numAsteroidsSpawned++;

        isSpawning = false;

        yield return null;
    }
}
