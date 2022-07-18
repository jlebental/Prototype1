using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //leave the player GameObject public... you need access to the reference to the other object!
    //without the reference alive the script will break
    public GameObject player;
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -7);

    // Start is called before the first frame update
    /*void Start()
    {
        
    }*/
    //no need for start()

    // Update is called once per frame
    void LateUpdate()   //LateUpdate is called following Update... smoothing camera! Ideal for smoothing cameras
    {
        //offsetting the camera from the player a little
        transform.position = player.transform.position + offset;
    }
}
