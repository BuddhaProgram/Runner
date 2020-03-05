using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestRun : MonoBehaviour
{
    public Tile tile;
    public Tilemap tilemap;
    public Transform GO;
    public Vector2 speed;
    public Vector3Int pos;
    // Start is called before the first frame update
    void Start()
    {

        tilemap.SetTile(pos, null);
        Debug.Log(tilemap.CellToWorld(pos));
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(tilemap.WorldToCell(GO.position));
        //if (Input.GetButton("Jump")) {
        //    GO.GetComponent<Rigidbody2D>().velocity = speed;
        //}
    }
    private void FixedUpdate()
    {
        //if (Input.GetButton("Jump"))
        //    GO.GetComponent<Rigidbody2D>().AddForce(speed, ForceMode2D.Force);
    }
}
