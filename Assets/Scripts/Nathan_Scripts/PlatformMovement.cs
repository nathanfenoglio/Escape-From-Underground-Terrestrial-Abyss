using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    //adding variable for an offset amount of posA if you wanted to make it higher than the starting point
    private Vector3 posAOffset = new Vector3(0, 0, 0);

    //get the start position of the platform and the position B where you are sending the platform to
    private Vector3 posA;
    private Vector3 posB;

    private Vector3 nextPos; //you will use this variable to swap between destination points A and B

    [SerializeField]
    public float speed;

    [SerializeField]
    private Transform childTransform; //child of MovingPlatform is the platform that we want to move

    [SerializeField]
    private Transform transformB; //reference to the 1st move toward spot
    
    // Start is called before the first frame update
    void Start()
    {
        posA = childTransform.localPosition; //get position A
        posB = transformB.localPosition; //get position B
        nextPos = posB; //initially start heading to position B.
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move() {
        childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nextPos, speed * Time.deltaTime);
        
        //checking for if close enough to destination point, so head other direction
        if (Vector3.Distance(childTransform.localPosition, nextPos) <= 0.1f) {
            changeDestination();
        }
    }

    private void changeDestination() {

        if (nextPos != posA)
        {
            nextPos = posA;
        }
        else {
            nextPos = posB;
        }
    }

}
