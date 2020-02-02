using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Analyser : MonoBehaviour {

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
       if (collision.gameObject.tag == "InterestPoint")
        {
            pointInterestDetected = true;
            Vector3Int tilePosittion = GetTilePosition(collision.transform.position);
            if (tilemap.GetTile(tilePosittion) == null)
            {
                Debug.Log("is visible");
                if (playerController.ConsumeAnalyse())
                {
                    Debug.Log("analyze done");
                    Destroy(collision.gameObject);
                }
            }
        }
        else if (collision.gameObject.tag == "Water")
        {
            Vector3Int tilePosittion = GetTilePosition(collision.gameObject.transform.position);

            if (tilemap.GetTile(tilePosittion) == null)
            {
                if (playerController.ConsumeWaterAnalyse())
                {
                    Destroy(collision.gameObject);
                    GameManager.instance.GotWater();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "InterestPoint")
        {
            pointInterestDetected = false;
        }
    }

    private Vector3Int GetTilePosition(Vector3 pos)
    {
        return new Vector3Int(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y), Mathf.FloorToInt(pos.z));
    }

    private void FixedUpdate()
    {

    }
}
