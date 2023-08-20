using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;
public class GameManager : MonoBehaviour
{
    public Vector3[] tiles;
    public static GameManager gm;
    public GameObject player;
    bool playerSpawned;
    public List<GameObject> enemySpawns = new List<GameObject>();
    public TileAutomata tileAuto;
    public List<GameObject> liveEnemies = new List<GameObject>();
    bool portalSpawned = false;
    public GameObject portal;
    public CompositeCollider2D tileMapCollider;
    public Tilemap tileMap;
    Tilemap bumTileMap;
    public List<Vector3> tileList;
    TileBase removedTile = null;
    int start = 0;
    int end = 0;
    public static int Difficulty;
    //public TilemapShadowCaster2D shadow;
    // Start is called before the first frame update
    void Start()
    {
        //bumTileMap = tileMap;
        GameManager.gm.tiles = tileAuto.GetAllValidTiles();
        tileList = tiles.ToList(); 
        Vector3Int tilePos = new Vector3Int(-50, 0, 0);
        bool tileHitYet = false;

        int guy = 0;
        while(guy < 100000) {
            if(tileMap.HasTile(tilePos) && tileHitYet == false) {
                removedTile = tileMap.GetTile(tilePos);
                tileMap.SetTile(tilePos, null);
                start = tilePos.x;
                tileHitYet = true;
            } else if(tileMap.HasTile(tilePos) && tileHitYet == true) {
                tileMap.SetTile(tilePos, null);
            } else if(tileMap.HasTile(tilePos) == false && tileHitYet ==true) {
                end = tilePos.x;
                ShadowCaster2DGenerator.GenerateTilemapShadowCasters(tileMapCollider, true);
                //Invoke("BakeShadows", 5f);
                break;
            }
            tilePos = new Vector3Int(tilePos.x += 1, tilePos.y, tilePos.z);
            if(tilePos.x > 100) {
                break;
            }
            guy ++;
        }
        //ShadowCaster2DGenerator.GenerateTilemapShadowCasters(tileMapCollider, true);
        //ShadowCaster2DGenerator.GenerateTilemapShadowCasters(tileMapCollider, true);
        foreach(Vector3 v in tiles) {
            Vector3 t = new Vector3((v.x - 50) * 2, (v.y - 50) * 2, v.z);
            t = Vector3Offset(t);
            Debug.DrawLine(t, new Vector3(t.x + 0.3f, t.y, t.z), Color.green, 100f);
        }
        gm.GameObjectSpawner(player.transform, false);
        // Random enemy spawns according to difficulty value
        /*
        foreach(GameObject e in enemySpawns) {
            SpawnEnemy(e);
        }
        */
        int enemyAmount = 3 + (2 ^ Difficulty);
        GameObject[] enemies = (GameObject[])Resources.LoadAll("Enemies/Prefab", typeof(GameObject)).Cast<GameObject>().ToArray();
        for(int i = 0; i < enemyAmount; i++) {
            
            SpawnEnemy(enemies[Random.Range(0, enemies.Length)]);
        }
        Invoke("BakeShadows", 0.02f);
    }

    // Update is called once per frame
    void Update()
    {

        //ShadowCaster2DGenerator.GenerateTilemapShadowCasters(tileMapCollider, true);
        //ShadowCaster2DGenerator.GenerateTilemapShadowCasters(tileMapCollider, true);
        //ShadowCaster2DGenerator.GenerateTilemapShadowCasters(bumTileMap.GetComponent<CompositeCollider2D>(), true);
        if(liveEnemies.Count != 0) {
            foreach(GameObject e in liveEnemies) {
                if(e == null) {
                    liveEnemies.Remove(e);
                    Debug.Log("Enemy Count: " + liveEnemies.Count);
                }
            }
        }
        
        
        if(liveEnemies.Count < 1 && portalSpawned == false) {
            portalSpawned = true;
            Instantiate(portal);
            GameObjectSpawner(portal.transform, true);
            
        }
        
        
    }

    void Awake() {
        gm = this;
        Invoke("BakeShadows", 0.02f);
       
    }
    

    public static Gun_Def NewPlayerWeapon() {
        Gun_Def[] guns = (Gun_Def[])Resources.LoadAll("Weapons/FinishedWeapons", typeof(Gun_Def)).Cast<Gun_Def>().ToArray();
        return guns[Random.Range(0, guns.Length)];
    }

    public void GameObjectSpawner(Transform t, bool isEnemy) {
        tileList = tileAuto.GetAllValidTiles().ToList(); 
        bool valid = false;
        System.Random rnd = new System.Random();

        while(valid == false) {
            
            Vector3 posOG = tileList[rnd.Next(tileList.Count - 1)];
            Vector3 pos = new Vector3((posOG.x - 50) * 2, (posOG.y - 50) * 2, posOG.z);
            pos = Vector3Offset(pos);
            t.position = pos;
            LayerMask mask = LayerMask.GetMask("Wall");
            Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(pos.x, pos.y), 8, mask);
            //Debug.Log("Collders: " + colliders.Length);
            //Debug.Log("Colliders: " + colliders[0]);
            if(colliders.Length < 1) {
                LayerMask enemyMask = LayerMask.GetMask("Player");
                if(t.gameObject.tag == "Portal") {
                    //Physics2D.OverlapCircleAll(new Vector2(pos.x, pos.y), 25, enemyMask).Length > 0
                    valid = true;
                    
                }
                
                else if(isEnemy == true) {
                    
                    
                } else {
                    
                    
                }
                
                //playerSpawned = true;
                valid = true;    
            } else {
                
            }
            //tileList.Remove(posOG);
        }
    }

    public void SpawnEnemy(GameObject g) {
        GameObject newEnemy = Instantiate(g);
        GameObjectSpawner(newEnemy.transform, true);
        liveEnemies.Add(newEnemy);
        

    }

    public Vector3 Vector3Offset(Vector3 b) {
        b = new Vector3(b.x + 2.8f, b.y + 3f, b.z);
        return b;
    }

    public void EnemyDead(GameObject e) {
        
    }
    public void BakeShadows() {
        ShadowCaster2DGenerator.GenerateTilemapShadowCasters(tileMapCollider, true);
        List<Transform> children = new List<Transform>();
        tileMap.BoxFill(new Vector3Int(start, 0, 0), removedTile, start, 0, end, 0);
        //foreach (Transform child in tileMap.gameObject.transform)
        //{
        //    children.Add(child);
        //}   
        //foreach(Transform t in children) {
            //t.parent = null;
        //}
    }
}


