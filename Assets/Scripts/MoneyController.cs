//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    [SerializeField]
    Transform[] waypoints_left;
    [SerializeField]
    Transform[] waypoints_right;
    
    int[] moveSpeed = new int[] { 8, 9, 10, 12, 14 };
    int random_moveSpeed;

    int waypoint_left_Index = 0;
    int waypoint_right_Index = 0;

    void Start()
    {
        random_moveSpeed = RandomizeMoveSpeed();
        //Debug.Log(random_moveSpeed);
        RandomizeStartPosition();
        //transform.position = waypoints_left[waypoint_left_Index].transform.position;
        waypoint_right_Index = RandomizeEndPosition();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position,
                                                waypoints_right[waypoint_right_Index].transform.position,
                                                random_moveSpeed * Time.deltaTime);
        // if moneyObject is at endPos
        if (transform.position == waypoints_right[waypoint_right_Index].transform.position)
        {
            //waypoint_right_Index += 1;
            StartCoroutine(WaitUntilDestroy());
        }

        if (waypoint_right_Index == waypoints_right.Length)
            waypoint_right_Index = 0;            
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
        Destroy(gameObject);
    }

    private int RandomizeMoveSpeed()
    {
        int index = Random.Range(0, moveSpeed.Length);

        return random_moveSpeed = moveSpeed[index];
    }

    private void RandomizeStartPosition()
    {
        int waypoint_left_Index = Random.Range(0, waypoints_left.Length);
        transform.position = waypoints_left[waypoint_left_Index].transform.position;

        //int spawnPositionIndex = Random.Range(0, waypoints_left.Length); // the length of the array list
        //Instantiate(gameObject, waypoints_left[spawnPositionIndex].transform.position, waypoints_left[spawnPositionIndex].transform.rotation);
    }

    private int RandomizeEndPosition()
    {
        waypoint_right_Index = Random.Range(0, waypoints_right.Length);
        //Debug.Log("waypoint_right_Index " + waypoint_right_Index);
        return waypoint_right_Index;
        //transform.position = waypoints_right[waypoint_right_Index].transform.position;

        //int spawnPositionIndex = Random.Range(0, waypoints_left.Length); // the length of the array list
        //Instantiate(gameObject, waypoints_left[spawnPositionIndex].transform.position, waypoints_left[spawnPositionIndex].transform.rotation);
    }
}
