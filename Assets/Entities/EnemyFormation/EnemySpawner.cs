using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    public float padding;
    public float movementSpeed = 5f;
    public float spawnDelay = 0.5f;

    public Vector3 leftMost;
    public Vector3 rightMost;

    public bool movingRight = false;
    private float xMin;
    private float xMax;

    Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount == 0)
            {
                return childPositionGameObject;
            }
        }
        return null;
    }

    //Checks if all gameobject are destroyed
    bool AllMembersDead()
    {
        foreach(Transform childPositionGameObject in transform)
        {
            if(childPositionGameObject.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }

    //----------------------------------------------------------FUNCTIONS------------------------------------------------

    void Start () {
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        //Lets you act on main camera
        leftMost = Camera.main.ViewportToWorldPoint(new Vector3(-0.1f, 0, distanceToCamera));
        rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1.2f, 0, distanceToCamera));
        xMin = leftMost.x + padding;
        xMax = rightMost.x - padding;
        SpawnUntilFull();
        
    }

    // Update is called once per frame
    void Update () {
        if (AllMembersDead())
        {
            SpawnUntilFull();
            Debug.Log("Next wave");
            EnemyBehavior enemyBehavior = enemyPrefab.GetComponent<EnemyBehavior>();
            enemyBehavior.AddDifficulty();
            movementSpeed *= 1.1f;
            PlayerController player = GameObject.Find("player").GetComponent<PlayerController>();
            player.projectileSpeed *= 1.2f;
        }

    }

    private void FixedUpdate()
    {
        MoveEnemy();
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if (freePosition) { 
            //GameObject enemy is needed to create GameObject. Otherwise it appears as just Object. (child.transform.position) makes prefabs appear at different spots.
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            //Makes the enemy instance child of the Spawner(with transform)
            enemy.transform.parent = freePosition;
        }
        //Checks if there are any free positions and then spawns enemies
        if (NextFreePosition()) { 
            Invoke("SpawnUntilFull", spawnDelay);
        }
    }

    void SpawnEnemies()
    {
        foreach (Transform child in transform)
        {
            //GameObject enemy is needed to create GameObject. Otherwise it appears as just Object. (child.transform.position) makes prefabs appear at different spots.
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            //Makes the enemy instance child of the Spawner(with transform)
            enemy.transform.parent = child;

        }
        
    }

    public void MoveEnemy()
    {
        if (movingRight) { 
            //transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
            transform.position += new Vector3(movementSpeed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.position += new Vector3(-movementSpeed * Time.deltaTime, 0, 0);  
        }

        //variable floats of the border of the formation
        float rightEdgeOfFormation = transform.position.x +(0.4f*width);
        float leftEdgeOfFormation = transform.position.x -(0.4f * width);

        //Updates the BOOLEAN of the condition when hitting the edge
        if (leftEdgeOfFormation < xMin)
        {
            movingRight = true;
            //movingRight = !movingRight; 
        }else if(rightEdgeOfFormation > xMax)
        {
            movingRight = false;
        }
        
    }
}
