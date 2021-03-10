using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private TerrainGenerator terrainGenerator;
    [SerializeField] private Text scoreText;
   
    private Animator animator;
    private bool isHopping;
    private int score;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
        if (Input.GetKeyDown(KeyCode.W) && !isHopping)
        {
            score+=10;
            float zDifference = 0;
            if (transform.position.z % 1 != 0)
            {
                zDifference = Mathf.Round(transform.position.z) - transform.position.z;
                
            }
            MoveCharacter(new Vector3(1, 0, zDifference));

        }
        else if (Input.GetKeyDown(KeyCode.A) && !isHopping)
        {

            MoveCharacter(new Vector3(0, 0, 1));
        }
        else if (Input.GetKeyDown(KeyCode.D) && !isHopping)
        {

            MoveCharacter(new Vector3(0, 0, -1));
        }
    }

    private void MoveCharacter(Vector3 difference)
    {
        animator.SetTrigger("hop");
        isHopping = true;
        transform.position = (transform.position + difference);

        terrainGenerator.SpawnTerrain(false, transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.GetComponent<MovingObject>() != null)
        {
            if (collision.collider.GetComponent<MovingObject>().isLog)
            {
                transform.parent = collision.collider.transform;
            }
        }
        else
        {
            transform.parent = null;
        }
    }
    public void FinishHop()
    {
        isHopping = false;
    }
}
