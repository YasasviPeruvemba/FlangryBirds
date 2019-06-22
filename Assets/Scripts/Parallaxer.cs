using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxer : MonoBehaviour
{
    public class PoolObject
    {
        public Transform transform;
        public bool inuse;
        public PoolObject(Transform t) { transform = t; }
        public void Use() { inuse = true; }
        public void Dispose() { inuse = false; }
    }

    [System.Serializable]
    public struct YspawnRange
    {
        public float min;
        public float max;
    }

    public GameObject Prefab;
    public int poolSize;
    public float shiftspeed;
    public float spawnrate;
    //public Transform t;

    public YspawnRange Yspawn;
    public Vector3 defaultSpawnPos;
    public bool spawnImmediate; //Prewarn?? Find out
    public Vector3 ImmediateSpawnPos;
    public Vector2 targetAspectRatio;

    Vector3 v;
  
    float spawnTimer;
    float targetAspect;
    PoolObject[] poolobjects;

    GameManager game;

    void Awake()
    {
        Configure();
    }

    void Start()
    {
        game = GameManager.Instance;    
    }

    void OnEnable()
    {
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    void OnDisable()
    {
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    void OnGameOverConfirmed()
    {
        for(int i = 0; i < poolobjects.Length; i++)
        {
            poolobjects[i].Dispose();
            poolobjects[i].transform.position = Vector3.one * 100;
        }
        if (spawnImmediate) { SpawnImmediate(); }
    }

    void Update()
    {
        if (game.GameOver) { return; }
        Shift();
        spawnTimer += Time.deltaTime;
        if(spawnTimer > spawnrate)
        {
            Spawn();
            spawnTimer = 0;
        }
    }
    
    void Configure()
    {
        targetAspect = targetAspectRatio.x / targetAspectRatio.y;
        poolobjects = new PoolObject[poolSize];
        for(int i = 0; i < poolobjects.Length; i++)
        {
            GameObject go = Instantiate(Prefab) as GameObject;
            Transform t = go.transform;
            t.SetParent(transform);
            t.position = Vector3.one*100;
            poolobjects[i] = new PoolObject(t);
            //Debug.Log("1 ");
            //Debug.Log(poolobjects[i].transform.position.z);
        }
        if (spawnImmediate) { SpawnImmediate(); }
    }

    void Spawn()
    {
        int ind = 0;
        for (int i = 0; i < poolobjects.Length; i++)
        {
            if (!poolobjects[i].inuse)
            {
                ind = i;
                poolobjects[i].Use();
                break;
                //poolobjects[i].transform;
            }
        }
        //Transform t = GetPoolObject();
        if (poolobjects[ind].transform == null) { return; }
        Vector3 pos = Vector3.zero;
        pos.x += ((defaultSpawnPos.x) * (Camera.main.aspect) / targetAspect);
        pos.y += Random.Range(Yspawn.min,Yspawn.max);
        Debug.Log("2 This has been spawned");
        poolobjects[ind].transform.position = pos;
        Debug.Log("aaaa  "+ poolobjects[ind].transform.position.z);

    }

    void SpawnImmediate()
    {
        int ind = 0;
        for (int i = 0; i < poolobjects.Length; i++)
        {
            if (!poolobjects[i].inuse)
            {
                ind = i;
                poolobjects[i].Use();
                break;
                //poolobjects[i].transform;
            }
        }
        //Transform t = GetPoolObject();
        if (poolobjects[ind].transform == null) { return; }
        Vector3 pos = Vector3.zero;
        pos.x += ((ImmediateSpawnPos.x*(Camera.main.aspect)) / targetAspect);
        pos.y += Random.Range(Yspawn.min, Yspawn.max);
        poolobjects[ind].transform.position = pos;
        Spawn();
    }

    void Shift()
    {
        for(int i = 0; i < poolobjects.Length; i++)
        {
            poolobjects[i].transform.position += -Vector3.right * shiftspeed*Time.deltaTime;
            checkDisposeObject(poolobjects[i]);
        }
    }

    void checkDisposeObject(PoolObject poolObject)
    {
        if(poolObject.transform.position.x < (-defaultSpawnPos.x)*(Camera.main.aspect)/targetAspect)
        {
            poolObject.Dispose();
            poolObject.transform.position = Vector3.one * 100;
        }
    }

    /*Transform GetPoolObject()
    {
        for (int i = 0; i< poolobjects.Length; i++)
        {
            if (!poolobjects[i].inuse) {
                poolobjects[i].Use();           
                return poolobjects[i].transform; 
            }
        }
        return null;
    }*/



}
