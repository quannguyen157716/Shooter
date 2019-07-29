using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
    
[System.Serializable]
public class test
{
    public int i=1;
}
public class GameDataInitializer : MonoBehaviour {
    PlayerWeapon gun;
    SpawnInfo info;
	void Awake () {
        //Based value for object attributes
        Debug.Log(Application.persistentDataPath);
        //Gun Type 
        //Gun type and round type are different
        if(!File.Exists(Application.persistentDataPath+"/PlayerRegularGun.json"))
        {
            gun=new PlayerWeapon();
            gun.shotType="PlayerRegularShot";
            gun.nextFire=0f;
            gun.fireRate=0.4f;
            gun.shotDamage=1;
            gun.shotSpeed=9f;
            Debug.Log(Application.persistentDataPath+"/PlayerRegularGun.json");

            string json=JsonUtility.ToJson(gun,true);        
            File.WriteAllText(Application.persistentDataPath+"/PlayerRegularGun.json",json);

        }
        //
        if(!File.Exists(Application.persistentDataPath+"/PlayerBurstGun.json"))
        {
            gun=new PlayerWeapon();
            gun.shotType="PlayerBurstShot";
            gun.nextFire=0f;
            gun.fireRate=1.5f;
            gun.shotDamage=5;
            gun.shotSpeed=8f;
            Debug.Log(Application.persistentDataPath+"/PlayerBurstGun.json");

            string json=JsonUtility.ToJson(gun,true);        
            File.WriteAllText(Application.persistentDataPath+"/PlayerBurstGun.json",json);
        }

        if(!File.Exists(Application.persistentDataPath+"/PlayerTracingGun.json"))
        {
            gun=new PlayerWeapon();
            gun.shotType="PlayerTracingShot";
            gun.nextFire=0f;
            gun.fireRate=3f;
            gun.shotDamage=3;
            gun.shotSpeed=3f;
            Debug.Log(Application.persistentDataPath+"/PlayerTracingGun.json");

            string json=JsonUtility.ToJson(gun,true);        
            File.WriteAllText(Application.persistentDataPath+"//PlayerTracingGun.json",json);
        }
    
        if(!File.Exists(Application.persistentDataPath+"/S01.json"))
        {
            info=new SpawnInfo();
            info.ID="01";
            info.enemyTag="EnemyGreen";
            info.numberOfWave=3;
            info.numberOfObject=5; //number of object in one wave
	        info.start_Wait=3; //Time to Start spawning
        	info.RandomSpawnWaitMin=2;  //Time to wait before a new spawn
        	info.RandomSpawnWaitMax=5;
        	info.wavewaitMin=2;  //Time to wait till a new wave
        	info.wavewaitMin=4;
        	info.position=new Vector2(3,6);  //position to spawn
            string json=JsonUtility.ToJson(info,true);        
            File.WriteAllText(Application.persistentDataPath+"//S01.json",json);
        }

        if(!File.Exists(Application.persistentDataPath+"/S00.json"))
        {
            info=new SpawnInfo();
            info.ID="00";
            info.enemyTag="EnemyBlue";
            info.numberOfWave=15;
            info.numberOfObject=6; //number of object in one wave
	        info.start_Wait=3; //Time to Start spawning
        	info.RandomSpawnWaitMin=1;  //Time to wait before a new spawn
        	info.RandomSpawnWaitMax=4;
        	info.wavewaitMin=1;  //Time to wait till a new wave
        	info.wavewaitMin=4;
        	info.position=new Vector2(3,6);  //position to spawn
            string json=JsonUtility.ToJson(info,true);        
            File.WriteAllText(Application.persistentDataPath+"//S00.json",json);
        }

        if(!File.Exists(Application.persistentDataPath+"/Level1.json"))
        {
            Level level=new Level();
            level.name="Level 1";

            
            level.events=new SpawnInfo[2];

            level.events[0]=new SpawnInfo();
            level.events[0].ID="LV_E00";
            level.events[0].enemyTag="EnemyBlue";
            level.events[0].typeOfSpawn="enemyRandomSpawn";
            level.events[0].numberOfWave=15;
            level.events[0].numberOfObject=6; //number of object in one wave
	        level.events[0].start_Wait=3; //Time to Start spawning
        	level.events[0].RandomSpawnWaitMin=1;  //Time to wait before a new spawn
        	level.events[0].RandomSpawnWaitMax=4;
        	level.events[0].wavewaitMin=1;  //Time to wait till a new wave
        	level.events[0].wavewaitMin=4;
        	level.events[0].position=new Vector2(3,6);  //position to spawn */

            level.events[1]=new SpawnInfo();
            level.events[1].ID="LV_E01";
            level.events[1].enemyTag="EnemyGreen";
            level.events[1].typeOfSpawn="enemyRandomSpawn";
            level.events[1].numberOfWave=5;
            level.events[1].numberOfObject=3; //number of object in one wave
	        level.events[1].start_Wait=3; //Time to Start spawning
        	level.events[1].RandomSpawnWaitMin=2;  //Time to wait before a new spawn
        	level.events[1].RandomSpawnWaitMax=5;
        	level.events[1].wavewaitMin=2;  //Time to wait till a new wave
        	level.events[1].wavewaitMin=5;
        	level.events[1].position=new Vector2(3,6);  //position to spawn */

            level.timeToStartEachEvent=new float[4]{3,300,3,3};
            string json =JsonUtility.ToJson(level,true);
            Debug.Log(json);
            //string json=JsonHelper.ToJson(level);
            File.WriteAllText(Application.persistentDataPath+"//Level1.json",json);
        }

/* 		string json=JsonUtility.ToJson(Obja,true);
		Debug.Log(json);//
		Debug.Log(Application.persistentDataPath);
		File.WriteAllText(Application.persistentDataPath+"/F.json",json);
		json=File.ReadAllText(Application.persistentDataPath+"/F.json");
		TestingS myOb=JsonUtility.FromJson<TestingS>(json);
		Debug.Log(myOb.position);
		Debug.Log(myOb.health); */
    }
	
}

