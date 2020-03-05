using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using System;

[Serializable]
public class TilemapAdder : BaseNM {
    // Randomized tiles
    [SerializeField]
    private Tile[] m_floorTiles = null;
    [SerializeField]
    private Tilemap m_floorTileMap = null;
    [SerializeField]
    private Tile[] m_obstacleTile = null;
    [SerializeField]
    private Tilemap m_obstacleTileMap = null;
    // The spawn chance of the first object
    private int i_randObjectSpawnChance = 0;
    [SerializeField]
    // The increment of chance
    private int i_randObjectSpawnIncrement = 0;
    // Stacking up the spawn objects
    private int i_stackedObjectSpawnChance = 0;
    // The increment of stacked spawn object chance
    [SerializeField]
    private int i_stackedObjectSpawnIncrement = 0;
    // Tilemap to edit on runtime
    [SerializeField]
    private Transform m_playerTransform = null;
    // The initial tile that is at the very back
    private Vector3Int m_lastTile = new Vector3Int(0, 0, 0);
    // The first tile that is at the very front
    private Vector3Int m_firstTile = new Vector3Int(0, 0, 0);
    // The distance between the player and the last tile
    private int i_lastTileDistance = 0;
    // The distance between the play and the first tile
    private int i_firstTileDistance = 0;
    // Clear Tile from tile map
    public void ClearTile(Vector3Int pos) {
        m_floorTileMap.SetTile(pos, null);
    }
    // Awake
    public override void Awake() {
        // The initial tile that is at the very back
        m_lastTile = new Vector3Int(0, 0, 0);
        // Distance between player and last tile
        i_lastTileDistance = 0;
        // Spawn chance of first object
        i_randObjectSpawnChance = 0;
        // Stacking up chance
        i_stackedObjectSpawnChance = 0;
    }
    // Init
    public override void Init() {
        Vector3Int pos = new Vector3Int(0, 0, 0);
        // Finds the last tile
        for (int y = 0; y <= 0; --y) {
            if (!m_floorTileMap.HasTile(pos)) {
                pos.y--;
            }
            else {
                break;
            }
        }

        for (int x = 0; x <= 0; --x) {
            if (m_floorTileMap.HasTile(pos)) {
                pos.x--;
            }
            else {
                break;
            }
        }
        // Insert last tile
        m_lastTile = new Vector3Int(pos.x + 1, pos.y, pos.z);
        // Finds the first tile
        for (int x = 0; x >= 0; ++x) {
            if (m_floorTileMap.HasTile(pos + Vector3Int.right)) {
                pos.x++;
            }
            else {
                pos.x++;
                break;
            }
        }
        // Insert first tile
        m_firstTile = new Vector3Int(pos.x, pos.y, pos.z);
        // Gets the distance for back
        i_lastTileDistance = m_floorTileMap.WorldToCell(m_playerTransform.position).x - m_lastTile.x;
        // Gets the distance for front
        i_firstTileDistance = m_firstTile.x - m_floorTileMap.WorldToCell(m_playerTransform.position).x;
    }
    // Update
    public override void RunUpdate() {
        // Checks if the tile is more than the distance tile
        if (m_floorTileMap.WorldToCell(m_playerTransform.position).x - m_lastTile.x >= i_lastTileDistance + 1) {
            // Deletes the last tile
            m_floorTileMap.SetTile(m_lastTile, null);
            // Checks vertically to remove obstacles
            for (int y = 1; y <= 2; ++y) {
                if (m_obstacleTileMap.HasTile(m_lastTile + Vector3Int.up * y)) {
                    // Remove the tile
                    m_obstacleTileMap.SetTile(m_lastTile + Vector3Int.up * y, null);
                }
            }
            // Set a new last tile pos
            m_lastTile += Vector3Int.right;
        }
        // Checks if first tile is coming into the screen
        if (m_firstTile.x - m_floorTileMap.WorldToCell(m_playerTransform.position).x <= i_firstTileDistance) {
            // Set a new tile
            m_floorTileMap.SetTile(m_firstTile, m_floorTiles[UnityEngine.Random.Range(0, m_floorTiles.Length)]);
            // Move the checker forward
            m_firstTile += Vector3Int.right;
            // Randomize chance for obstacle spawning
            int randObstacle = UnityEngine.Random.Range(1, 201);
            // Checks if it is within spawn chance
            if (randObstacle <= i_randObjectSpawnChance) {
                // Resets object spawn chance
                i_randObjectSpawnChance = 0;
                // Creates obstacle tile
                m_obstacleTileMap.SetTile(m_firstTile + Vector3Int.up, m_obstacleTile[UnityEngine.Random.Range(0, m_obstacleTile.Length)]);
                // Randomize chance for stacked obstacle spawning
                int randStack = UnityEngine.Random.Range(1, 201);
                // Checks if it is within stacked spawn chance
                if (randStack <= i_stackedObjectSpawnChance) {
                    // Resets object spawn chance
                    i_stackedObjectSpawnChance = 0;
                    // Creates stacked obstacle tile
                    m_obstacleTileMap.SetTile(m_firstTile + Vector3Int.up * 2, m_obstacleTile[UnityEngine.Random.Range(0, m_obstacleTile.Length)]);
                }
                else {
                    i_stackedObjectSpawnChance += i_stackedObjectSpawnIncrement;
                }
            }
            else {
                i_randObjectSpawnChance += i_randObjectSpawnIncrement;
            }
        }
    }
    // Destroy on end
    public override void DestroyOnEnd() {
        m_floorTiles = null;
        m_obstacleTile = null;
         m_floorTileMap = null;
        m_playerTransform = null;
    }
}
