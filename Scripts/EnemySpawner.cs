using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	//ATTRIBUTE
	private static float speed = 5;
	//GAMEOBJECTS
	public GameObject[] enemyPrefab;
	private ScoreKeeper scoreKeeper;
	//SPAWN
	private static float spawnDelay = 0.5f;
	//BORDERS
	private static bool movingRight = false;
	private static float xmin;
	private static float xmax;
	private static float width = 10;
	private static float height = 5;

	void Start () {
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distanceToCamera));	
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, distanceToCamera));	
		
		xmax = rightBoundary.x;
		xmin = leftBoundary.x;

		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();

		if (AllAreDead()) {
			InvokeRepeating ("SpawnUntilFull", 0.001f, spawnDelay);
		}
	}

	public void OnDrawGizmos(){
		Gizmos.DrawWireCube (transform.position, new Vector3 (width, height));
	}
	
	void Update () {

		if (movingRight) {
			transform.position += Vector3.right* speed*Time.deltaTime;
		} else{
			transform.position += Vector3.left* speed*Time.deltaTime;
		}

		float rightBoundaryOfFormation = transform.position.x + (0.5f*width);
		float leftBoundaryOfFormation = transform.position.x - (0.5f*width);

		if (leftBoundaryOfFormation < xmin) {
			movingRight = true;
		}

		if (rightBoundaryOfFormation > xmax) {
			movingRight = false;
		}
	}

	bool AllAreDead(){
		foreach (Transform childPositionGameObject in transform) {
			if(childPositionGameObject.childCount > 0){
				return false;
			}
		}
		return true;
	}

	Transform NextFreePosition(){
		foreach (Transform childPositionGameObject in transform) {
			if(childPositionGameObject.childCount == 0){
				return childPositionGameObject;
			}
		}
		return null;
	}

	void SpawnUntilFull(){
		Transform freePosition = NextFreePosition ();
		if (freePosition) {
			int score = scoreKeeper.GetScore();
			if(score < 12000){
				GameObject enemy = Instantiate (enemyPrefab[0], freePosition.transform.position, Quaternion.identity) as GameObject;
				enemy.transform.parent = freePosition;
			}else if(score < 35000){
				GameObject enemy = Instantiate (enemyPrefab[1], freePosition.transform.position, Quaternion.identity) as GameObject;
				enemy.transform.parent = freePosition;
			}else{
				GameObject enemy = Instantiate (enemyPrefab[2], freePosition.transform.position, Quaternion.identity) as GameObject;
				enemy.transform.parent = freePosition;
			}
		}
		if (NextFreePosition()) {
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}

}
