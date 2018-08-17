using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetection : MonoBehaviour {
  //public GameObject shark;
  //private ShadowShark sharkscript;
    public float groundDistance=2f;
    public bool obstacle = false;
    public LayerMask Platform;
   
    

    // Use this for initialization
    void Start () {
    
	}
	
	// Update is called once per frame
	void Update () {
        //verifica com os Raycast se pode continuar o caminho.
        RaycastHit2D groundInfo = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, Platform);
        RaycastHit2D wallInfo = Physics2D.Raycast(transform.position, Vector2.right, groundDistance, Platform);

        if (groundInfo.collider == false||wallInfo.collider == true)
        {
            obstacle = true;
        } else
        {
            obstacle = false;
        }
  

    }
  
}
