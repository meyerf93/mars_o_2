using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class HoleMaker : MonoBehaviour
{

    private bool holeDetected;
    private bool pointInterestDetected;
    private GameObject interestPoint;
    private Collider2D actualCollision;
    public GameObject tilemapGameObject;

    private PlayerController playerController;
    private Tilemap tilemap;

    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        if (tilemapGameObject != null)
        {
            tilemap = tilemapGameObject.GetComponent<Tilemap>();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision " + collision.gameObject.name);
        Debug.Log("collision tar" + collision.gameObject.tag);
        if (collision.gameObject.tag == "destroyable")
        {
            holeDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "destroyable")
        {
            holeDetected = false;
            //Debug.Log("Exit Forrage");
        }
    }

    private Vector3Int GetTilePosition(Vector3 pos)
    {
        return new Vector3Int(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y), Mathf.FloorToInt(pos.z));
    }

    private void FixedUpdate()
    {
        if (holeDetected)
        {
            Vector3Int position = GetTilePosition(transform.position);
            if (tilemap.GetTile(position) != null)
            {
                if (playerController.ConsumeForrage())
                {
                    tilemap.SetTile(position, null);
                }
            }
        }
    }
}