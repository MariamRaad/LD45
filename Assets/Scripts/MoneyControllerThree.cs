using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyControllerThree : MonoBehaviour
{
    [SerializeField]
    Transform[] waypoints_left;
    [SerializeField]
    Transform[] waypoints_right;

    int[] moveSpeed = new int[] { 10, 11, 12, 15, 17 };
    int random_moveSpeed;

    int waypoint_left_Index = 0;
    int waypoint_right_Index = 0;

    /*
    private bool startsLeft;
    private bool startsRight;

    public MoneyManager mm;
    public bool isDestroyed;

    */
    private bool objectSpawned = false;
    private int num = 2;

    void Start()
    {
        random_moveSpeed = RandomizeMoveSpeed();
        /*objectSpawned = false;
        isDestroyed = false;

        startsLeft = mm.startsLeft;
        startsRight = mm.startsRight;

        if (startsLeft)
        {
            RandomizeStartPositionLeft();
        }
        if (startsRight)
        {
            RandomizeStartPositionRight();
        }
        */

        /*Debug.Log("Start left " + startsLeft);
        Debug.Log("Start right " + startsRight);
        Debug.Log("Start left mm " + mm.startsLeft);
        Debug.Log("Start right mm " + mm.startsRight);
        Debug.Log("DESTROYED mm " + mm.destroyed);
        */
    }

    void Update()
    {
        //objectSpawned = false;
        Wait();

        if (!objectSpawned)
        {
            CalculateModulo();
        }
            /*
            if (startsLeft)
            {
                if (!objectSpawned)
                {
                    waypoint_right_Index = RandomizeEndPositionRight();
                    objectSpawned = true;
                    isDestroyed = true;
                }
                MoveToRight();
            }
            else if (startsRight)
            {
                if (!objectSpawned)
                {
                    waypoint_left_Index = RandomizeEndPositionLeft();
                    objectSpawned = true;
                    isDestroyed = true;
                }
                MoveToLeft();
            }
            */
        }

    void MoveToRight()
    {
        transform.position = Vector2.MoveTowards(transform.position,
                                                waypoints_right[waypoint_right_Index].transform.position,
                                                random_moveSpeed * Time.deltaTime);
        // if moneyObject is at endPos right
        if (transform.position == waypoints_right[waypoint_right_Index].transform.position)
        {
            //waypoint_right_Index += 1;
            StartCoroutine(WaitUntilDestroy());
        }

        if (waypoint_right_Index == waypoints_right.Length)
            waypoint_right_Index = 0;
    }

    void MoveToLeft()
    {
        transform.position = Vector2.MoveTowards(transform.position,
                                                waypoints_left[waypoint_left_Index].transform.position,
                                                random_moveSpeed * Time.deltaTime);
        // if moneyObject is at endPos left
        if (transform.position == waypoints_left[waypoint_left_Index].transform.position)
        {
            //waypoint_right_Index += 1;
            StartCoroutine(WaitUntilDestroy());
        }

        if (waypoint_left_Index == waypoints_left.Length)
            waypoint_left_Index = 0;
    }

    /*
    private void OnMouseDown()
    {                
        StartCoroutine(WaitUntilDestroy());
    }
    */

    IEnumerator WaitUntilDestroy()
    {
        yield return new WaitForSeconds(0.2f);
        Instantiate(gameObject);

        //isDestroyed = true;
        //mm.destroyed = isDestroyed;
        //ToggleStartPosition();
        Destroy(gameObject);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(4f);
        objectSpawned = false;
    }

    private int RandomizeMoveSpeed()
    {
        int index = Random.Range(0, moveSpeed.Length);

        return random_moveSpeed = moveSpeed[index];
    }

    private void RandomizeStartPositionLeft()
    {
        int waypoint_left_Index = Random.Range(0, waypoints_left.Length);
        transform.position = waypoints_left[waypoint_left_Index].transform.position;

        //int spawnPositionIndex = Random.Range(0, waypoints_left.Length); // the length of the array list
        //Instantiate(gameObject, waypoints_left[spawnPositionIndex].transform.position, waypoints_left[spawnPositionIndex].transform.rotation);
    }

    private void RandomizeStartPositionRight()
    {
        int waypoint_right_Index = Random.Range(0, waypoints_right.Length);
        transform.position = waypoints_right[waypoint_right_Index].transform.position;

        //int spawnPositionIndex = Random.Range(0, waypoints_left.Length); // the length of the array list
        //Instantiate(gameObject, waypoints_left[spawnPositionIndex].transform.position, waypoints_left[spawnPositionIndex].transform.rotation);
    }

    private int RandomizeEndPositionRight()
    {
        waypoint_right_Index = Random.Range(0, waypoints_right.Length);
        //Debug.Log("waypoint_right_Index " + waypoint_right_Index);
        return waypoint_right_Index;
        //transform.position = waypoints_right[waypoint_right_Index].transform.position;

        //int spawnPositionIndex = Random.Range(0, waypoints_left.Length); // the length of the array list
        //Instantiate(gameObject, waypoints_left[spawnPositionIndex].transform.position, waypoints_left[spawnPositionIndex].transform.rotation);
    }

    private int RandomizeEndPositionLeft()
    {
        waypoint_left_Index = Random.Range(0, waypoints_left.Length);
        //Debug.Log("waypoint_right_Index " + waypoint_right_Index);
        return waypoint_left_Index;
        //transform.position = waypoints_right[waypoint_right_Index].transform.position;

        //int spawnPositionIndex = Random.Range(0, waypoints_left.Length); // the length of the array list
        //Instantiate(gameObject, waypoints_left[spawnPositionIndex].transform.position, waypoints_left[spawnPositionIndex].transform.rotation);
    }

    private void ToggleStartLeft()
    {
        /*
        if (startsLeft)
        {
            startsLeft = false;
        }
        else if (startsLeft == false)
        {
            startsLeft = true;
        }
        */
    }

    private void ToggleStartRight()
    {
        /*
        if (startsRight)
        {
            startsRight = false;
        }
        else if (startsRight == false)
        {
            startsRight = true;
        }
        */
    }

    private void ToggleStartPosition()
    {
        /*
        ToggleStartLeft();
        ToggleStartRight();

        /*
        if (startsLeft)
        {
            ToggleStartLeft(); //false
            ToggleStartRight(); //true
        }
        else if (startsRight)
        {
            ToggleStartLeft(); //true
            ToggleStartRight(); //false
        }
        */
    }

    private void CalculateModulo()
    {
        num++;

        if (num % 2 == 1)
        {
            RandomizeStartPositionLeft();
            waypoint_right_Index = RandomizeEndPositionRight();
            MoveToRight();
            objectSpawned = true;
        }
        else
        {
            RandomizeStartPositionRight();
            waypoint_left_Index = RandomizeEndPositionLeft();
            MoveToLeft();
            objectSpawned = true;
        }
    }    
}