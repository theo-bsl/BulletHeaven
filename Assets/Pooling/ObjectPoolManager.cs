using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();

    private GameObject ObjectPoolEmptyHolder;

    private static GameObject PlayerBulletsEmpty;

    private static GameObject DemonLowEmpty;
    private static GameObject DemonMidEmpty;
    private static GameObject DemonHighEmpty;
    private static GameObject DemonHighestEmpty;

    private static GameObject DemonBulletLowEmpty;
    private static GameObject DemonBulletMidEmpty;
    private static GameObject DemonBulletHighEmpty;
    private static GameObject DemonBulletHighestEmpty;

    private static GameObject BonusEmpty;

    public enum PoolType
    {
        PlayerBullet,

        DemonLow,
        DemonMid,
        DemonHigh,
        DemonHighest,

        DemonBulletLow,
        DemonBulletMid,
        DemonBulletHigh,
        DemonBulletHighest,

        Bonus,

        None
    }

    private void Awake()
    {
        SetupEmpties();
    }

    private void SetupEmpties()
    {
        ObjectPoolEmptyHolder = new GameObject("Pooled Objects");

        PlayerBulletsEmpty = new GameObject("PlayerBullets");
        PlayerBulletsEmpty.transform.SetParent(ObjectPoolEmptyHolder.transform);


        DemonLowEmpty = new GameObject("DemonLow");
        DemonLowEmpty.transform.SetParent(ObjectPoolEmptyHolder.transform);
        
        DemonMidEmpty = new GameObject("DemonMid");
        DemonMidEmpty.transform.SetParent(ObjectPoolEmptyHolder.transform);
        
        DemonHighEmpty = new GameObject("DemonHigh");
        DemonHighEmpty.transform.SetParent(ObjectPoolEmptyHolder.transform);
        
        DemonHighestEmpty = new GameObject("DemonHighest");
        DemonHighestEmpty.transform.SetParent(ObjectPoolEmptyHolder.transform);


        DemonBulletLowEmpty = new GameObject("DemonBulletLow");
        DemonBulletLowEmpty.transform.SetParent(ObjectPoolEmptyHolder.transform);

        DemonBulletMidEmpty = new GameObject("DemonBulletMid");
        DemonBulletMidEmpty.transform.SetParent(ObjectPoolEmptyHolder.transform);

        DemonBulletHighEmpty = new GameObject("DemonBulletHigh");
        DemonBulletHighEmpty.transform.SetParent(ObjectPoolEmptyHolder.transform);

        DemonBulletHighestEmpty = new GameObject("DemonBulletHighest");
        DemonBulletHighestEmpty.transform.SetParent(ObjectPoolEmptyHolder.transform);

        BonusEmpty = new GameObject("Bonus");
        BonusEmpty.transform.SetParent(ObjectPoolEmptyHolder.transform);

    }

    public static GameObject SpawnObject(GameObject ObjectToSpawn, Vector2 SpawnPosition, PoolType poolType = PoolType.None)
    {
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == ObjectToSpawn.name);
        
        // if the pool doesn't exist, create it
        if (pool == null)
        {
            pool = new PooledObjectInfo() { LookupString = ObjectToSpawn.name };
            ObjectPools.Add(pool);
        }

        //check if there are any inactive objects in the pool
        GameObject spawnableObject = pool.InactiveObjects.FirstOrDefault();

        if (spawnableObject == null)
        {
            //find the parent of the empty object 
            GameObject parentObject = SetParentObject(poolType);

            //if there are no inactive objects, create a new one 
            spawnableObject = Instantiate(ObjectToSpawn, SpawnPosition, Quaternion.identity);
            
            if (parentObject != null)
            {
                spawnableObject.transform.SetParent(parentObject.transform);
            }
        }
        else
        {
            //if there is an inactive object, reactive it 
            spawnableObject.transform.position = SpawnPosition;
            spawnableObject.transform.rotation = Quaternion.identity;
            pool.InactiveObjects.Remove(spawnableObject);
            spawnableObject.SetActive(true);
        }

        return spawnableObject;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        string goName = obj.name.Substring(0, obj.name.Length - 7); // by taking off 7, we are removing the (Clone) from the name of the passed in obj

        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == goName);

        if ( pool == null )
        {
            Debug.LogWarning("Trying to release an object that is not pooled : " + obj.name);
        }
        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }

    private static GameObject SetParentObject(PoolType poolType)
    {
        switch (poolType)
        {
            case PoolType.PlayerBullet: return PlayerBulletsEmpty;


            case PoolType.DemonLow: return DemonLowEmpty;

            case PoolType.DemonMid: return DemonMidEmpty;

            case PoolType.DemonHigh: return DemonHighEmpty;

            case PoolType.DemonHighest: return DemonHighestEmpty;


            case PoolType.DemonBulletLow: return DemonBulletLowEmpty;

            case PoolType.DemonBulletMid: return DemonBulletMidEmpty;

            case PoolType.DemonBulletHigh: return DemonBulletHighEmpty;

            case PoolType.DemonBulletHighest: return DemonBulletHighestEmpty;


            case PoolType.Bonus: return BonusEmpty;


            case PoolType.None : return null;

            default: return null;
        }
    }
}

public class PooledObjectInfo
{
    public string LookupString;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}