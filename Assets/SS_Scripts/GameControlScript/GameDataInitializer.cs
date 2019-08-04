using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SpawnInfo
{
	public string ID;
	public string enemyTag; //tag of enemy to spawn
	public string typeOfSpawn;
	public int numberOfWave; //number of wave =0 it is individual spawn
	public int numberOfObject; //number of object in one wave
	//public float start_Wait; //Time to Start spawning
	public float RandomSpawnWaitMin;  //Time to wait before a new spawn
	public float RandomSpawnWaitMax;
	public float wavewaitMin;  //Time to wait till a new wave
	public float wavewaitMax;
	public Vector2 position;  //position to spawn 
	public bool spawnEnd=false;
    public float duration;
}

public class EnemyInfo
{
    public string name;
    public float speed;						//Enemy Ship Speed
	public int health;						//Enemy Ship Health
	public int ScoreValue;					//How much the Enemy Ship give score after explosion
	public float fireRate;			//Fire Rate between Shots
}

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

    EnemyInfo enemyInfo;
	void Awake () {
        //Based value for object attributes
        Debug.Log(Application.persistentDataPath);
        //Gun Type 
        //Gun type and round type are different
        if(!File.Exists(Application.persistentDataPath+"/EnemyBlue.json"))
        {
           enemyInfo=new EnemyInfo();
           enemyInfo.name="EnemyBlue";
           enemyInfo.speed=2;						//Enemy Ship Speed
	       enemyInfo.health=2;						//Enemy Ship Health
           enemyInfo.ScoreValue=15;					//How much the Enemy Ship give score after explosion
	       enemyInfo.fireRate = 4F;	
           string json=JsonUtility.ToJson(enemyInfo,true);        
           File.WriteAllText(Application.persistentDataPath+"/EnemyBlue.json",json);
        }

        if(!File.Exists(Application.persistentDataPath+"/EnemyGreen.json"))
        {
           enemyInfo=new EnemyInfo();
           enemyInfo.name="EnemyGreen";
           enemyInfo.speed=3;						//Enemy Ship Speed
	       enemyInfo.health=4;						//Enemy Ship Health
           enemyInfo.ScoreValue=25;					//How much the Enemy Ship give score after explosion
	       enemyInfo.fireRate = 3F;	
           string json=JsonUtility.ToJson(enemyInfo,true);        
           File.WriteAllText(Application.persistentDataPath+"/EnemyGreen.json",json);

        }

        if(!File.Exists(Application.persistentDataPath+"/EnemyRed.json"))
        {
           enemyInfo=new EnemyInfo();
           enemyInfo.name="EnemyRed";
           enemyInfo.speed=1.5f;						//Enemy Ship Speed
	       enemyInfo.health=25;						//Enemy Ship Health
           enemyInfo.ScoreValue=100;					//How much the Enemy Ship give score after explosion
	       enemyInfo.fireRate = 3F;	
           string json=JsonUtility.ToJson(enemyInfo,true);        
           File.WriteAllText(Application.persistentDataPath+"/EnemyRed.json",json);

        }

        if(!File.Exists(Application.persistentDataPath+"/Asteroid.json"))
        {
           enemyInfo=new EnemyInfo();
           enemyInfo.name="Asteroid";
           enemyInfo.speed=2f;						//Enemy Ship Speed
	       enemyInfo.health=20;						//Enemy Ship Health
           enemyInfo.ScoreValue=10;					//How much the Enemy Ship give score after explosion	
           string json=JsonUtility.ToJson(enemyInfo,true);        
           File.WriteAllText(Application.persistentDataPath+"/Asteroid.json",json);

        }

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
   
        if(!File.Exists(Application.persistentDataPath+"/Level_1.json"))
        {
            Level level=new Level();
            level.name="Level_1";
        
            level.events=new SpawnInfo[2];

            level.events[0]=new SpawnInfo();
            level.events[0].ID="LV1_Event00";
            level.events[0].enemyTag="EnemyBlue";
            level.events[0].typeOfSpawn="enemyRandomSpawn";
            level.events[0].numberOfWave=5;
            level.events[0].numberOfObject=4; //number of object in one wave
        	level.events[0].RandomSpawnWaitMin=6;  //Time to wait before a new spawn
        	level.events[0].RandomSpawnWaitMax=6;
        	level.events[0].wavewaitMin=6;  //Time to wait till a new wave
        	level.events[0].wavewaitMax=8;
        	level.events[0].position=new Vector2(3,6);  //position to spawn 
            level.events[0].duration=85;

            level.events[1]=new SpawnInfo();
            level.events[1].ID="LV1_Event01";
            level.events[1].enemyTag="EnemyStream1";
            level.events[1].typeOfSpawn="enemyRandomSpawn";
            level.events[1].numberOfWave=4;
            level.events[1].numberOfObject=2; //number of object in one wave
        	level.events[1].RandomSpawnWaitMin=12;  //Time to wait before a new spawn
        	level.events[1].RandomSpawnWaitMax=12;
        	level.events[1].wavewaitMin=6;  //Time to wait till a new wave
        	level.events[1].wavewaitMax=6;
        	level.events[1].position=new Vector2(3,6);  //position to spawn 
            level.events[1].duration=80;
            level.timeToStartEachEvent=new float[2]{3,85};

            string json =JsonUtility.ToJson(level,true);
       
            //string json=JsonHelper.ToJson(level);
            File.WriteAllText(Application.persistentDataPath+"//Level_1.json",json);
        }

        if(!File.Exists(Application.persistentDataPath+"/Level_2.json"))
        {
            Level level=new Level();
            level.name="Level_2";

            level.events=new SpawnInfo[4];

            level.events[0]=new SpawnInfo();
            level.events[0].ID="LV2_Event00";
            level.events[0].enemyTag="EnemyStream1";
            level.events[0].typeOfSpawn="enemyRandomSpawn";
            level.events[0].numberOfWave=4;
            level.events[0].numberOfObject=2; //number of object in one wave
        	level.events[0].RandomSpawnWaitMin=12;  //Time to wait before a new spawn
        	level.events[0].RandomSpawnWaitMax=12;
        	level.events[0].wavewaitMin=6;  //Time to wait till a new wave
        	level.events[0].wavewaitMax=6;
        	level.events[0].position=new Vector2(3,6);  //position to spawn 
            level.events[0].duration=20;
            level.timeToStartEachEvent=new float[2]{3,85};
            

            level.events[3]=new SpawnInfo();
            level.events[3].ID="LV2_Event04";
            level.events[3].enemyTag="EnemyGreen";
            level.events[3].typeOfSpawn="enemyRandomSpawn";
            level.events[3].numberOfWave=8;
            level.events[3].numberOfObject=4; //number of object in one wave
        	level.events[3].RandomSpawnWaitMin=2;  //Time to wait before a new spawn
        	level.events[3].RandomSpawnWaitMax=5;
        	level.events[3].wavewaitMin=4;  //Time to wait till a new wave
        	level.events[3].wavewaitMax=6;
        	level.events[3].position=new Vector2(3,6);  //position to spawn */
            level.events[3].duration=80;

            level.events[1]=new SpawnInfo();
            level.events[1].ID="LV2_Event01";
            level.events[1].enemyTag="EnemyStream2";
            level.events[1].typeOfSpawn="enemyHorizontalRandomSpawn";
            level.events[1].numberOfWave=5;
            level.events[1].numberOfObject=2; //number of object in one wave
        	level.events[1].RandomSpawnWaitMin=7;  //Time to wait before a new spawn
        	level.events[1].RandomSpawnWaitMax=10;
        	level.events[1].wavewaitMin=8;  //Time to wait till a new wave
        	level.events[1].wavewaitMax=8;
        	level.events[1].position=new Vector2(-5,4);  //position to spawn */
            level.events[1].duration=50;

            level.events[2]=new SpawnInfo();
            level.events[2].ID="LV2_Event03";
            level.events[2].enemyTag="EnemyStream3";
            level.events[2].typeOfSpawn="enemyHorizontalRandomSpawn";
            level.events[2].numberOfWave=5;
            level.events[2].numberOfObject=2; //number of object in one wave
        	level.events[2].RandomSpawnWaitMin=7;  //Time to wait before a new spawn
        	level.events[2].RandomSpawnWaitMax=10;
        	level.events[2].wavewaitMin=8;  //Time to wait till a new wave
        	level.events[2].wavewaitMax=8;
        	level.events[2].position=new Vector2(5,4);  //position to spawn */
            level.events[2].duration=50;

            level.timeToStartEachEvent=new float[4]{3,20,20,20};
            string json =JsonUtility.ToJson(level,true);
           
            //string json=JsonHelper.ToJson(level);
            File.WriteAllText(Application.persistentDataPath+"//Level_2.json",json);
        }

        if(!File.Exists(Application.persistentDataPath+"/Level_3.json"))
        {
            Level level=new Level();
            level.name="Level_3";

            
            level.events=new SpawnInfo[8];

            level.events[0]=new SpawnInfo();
            level.events[0].ID="LV3_Event00";
            level.events[0].enemyTag="Asteroid";
            level.events[0].typeOfSpawn="enemyRandomSpawn";
            level.events[0].numberOfWave=10;
            level.events[0].numberOfObject=6; //number of object in one wave
        	level.events[0].RandomSpawnWaitMin=2;  //Time to wait before a new spawn//
        	level.events[0].RandomSpawnWaitMax=6;
        	level.events[0].wavewaitMin=3;  //Time to wait till a new wave
        	level.events[0].wavewaitMax=8;
        	level.events[0].position=new Vector2(3,6);  //position to spawn 
            level.events[0].duration=45;

            level.events[1]=new SpawnInfo();
            level.events[1].ID="LV3_Event01";
            level.events[1].enemyTag="EnemyRed";
            level.events[1].typeOfSpawn="singleSpawn";
        	level.events[1].position=new Vector2(3,6);  //position to spawn 

            level.events[2]=new SpawnInfo();
            level.events[2].ID="LV3_Event02";
            level.events[2].enemyTag="EnemyRed";
            level.events[2].typeOfSpawn="enemyRandomSpawn";
            level.events[2].numberOfWave=1;
            level.events[2].numberOfObject=2; //number of object in one wave
        	level.events[2].RandomSpawnWaitMin=2;  //Time to wait before a new spawn
        	level.events[2].RandomSpawnWaitMax=5;
        	level.events[2].wavewaitMin=5;  //Time to wait till a new wave
        	level.events[2].wavewaitMax=8;
        	level.events[2].position=new Vector2(3,6);  //position to spawn 
            level.events[2].duration=60;

            level.events[3]=new SpawnInfo();
            level.events[3].ID="LV2_Event03";
            level.events[3].enemyTag="EnemyStream2";
            level.events[3].typeOfSpawn="enemyHorizontalRandomSpawn";
            level.events[3].numberOfWave=3;
            level.events[3].numberOfObject=2; //number of object in one wave
        	level.events[3].RandomSpawnWaitMin=7;  //Time to wait before a new spawn
        	level.events[3].RandomSpawnWaitMax=10;
        	level.events[3].wavewaitMin=8;  //Time to wait till a new wave
        	level.events[3].wavewaitMax=8;
        	level.events[3].position=new Vector2(-5,4);  //position to spawn */
            level.events[3].duration=20;

            level.events[4]=new SpawnInfo();
            level.events[4].ID="LV2_Event04";
            level.events[4].enemyTag="EnemyGreen";
            level.events[4].typeOfSpawn="enemyRandomSpawn";
            level.events[4].numberOfWave=8;
            level.events[4].numberOfObject=4; //number of object in one wave
        	level.events[4].RandomSpawnWaitMin=2;  //Time to wait before a new spawn
        	level.events[4].RandomSpawnWaitMax=5;
        	level.events[4].wavewaitMin=4;  //Time to wait till a new wave
        	level.events[4].wavewaitMax=6;
        	level.events[4].position=new Vector2(3,6);  //position to spawn */
            level.events[4].duration=80;

            level.events[5]=new SpawnInfo();
            level.events[5].ID="LV2_Event04";
            level.events[5].enemyTag="EnemyStream3";
            level.events[5].typeOfSpawn="enemyHorizontalRandomSpawn";
            level.events[5].numberOfWave=3;
            level.events[5].numberOfObject=2; //number of object in one wave
        	level.events[5].RandomSpawnWaitMin=7;  //Time to wait before a new spawn
        	level.events[5].RandomSpawnWaitMax=10;
        	level.events[5].wavewaitMin=8;  //Time to wait till a new wave
        	level.events[5].wavewaitMax=8;
        	level.events[5].position=new Vector2(5,4);  //position to spawn */
            level.events[5].duration=20;

            level.events[6]=new SpawnInfo();
            level.events[6].ID="LV2_Event05";
            level.events[6].enemyTag="EnemyStream4";
            level.events[6].typeOfSpawn="enemyHorizontalRandomSpawn";
            level.events[6].numberOfWave=3;
            level.events[6].numberOfObject=2; //number of object in one wave
        	level.events[6].RandomSpawnWaitMin=7;  //Time to wait before a new spawn
        	level.events[6].RandomSpawnWaitMax=10;
        	level.events[6].wavewaitMin=8;  //Time to wait till a new wave
        	level.events[6].wavewaitMax=8;
        	level.events[6].position=new Vector2(-5,4);  //position to spawn */
            level.events[6].duration=20;

            level.events[7]=new SpawnInfo();
            level.events[7].ID="LV2_Event06";
            level.events[7].enemyTag="EnemyStream5";
            level.events[7].typeOfSpawn="enemyHorizontalRandomSpawn";
            level.events[7].numberOfWave=3;
            level.events[7].numberOfObject=2; //number of object in one wave
        	level.events[7].RandomSpawnWaitMin=7;  //Time to wait before a new spawn
        	level.events[7].RandomSpawnWaitMax=10;
        	level.events[7].wavewaitMin=8;  //Time to wait till a new wave
        	level.events[7].wavewaitMax=8;
        	level.events[7].position=new Vector2(5,4);  //position to spawn */
            level.events[7].duration=20;

            level.timeToStartEachEvent=new float[7]{3,45,45,60,10,30,20};
            string json =JsonUtility.ToJson(level,true);
            Debug.Log(json.Length);
            //string json=JsonHelper.ToJson(level);
            File.WriteAllText(Application.persistentDataPath+"//Level_3.json",json);
        }

        if(!File.Exists(Application.persistentDataPath+"/Level_4.json"))
        {
            Level level=new Level();
            level.name="Level_4";
        
            level.events=new SpawnInfo[6];

            level.events[0]=new SpawnInfo();
            level.events[0].ID="LV4_Event00";
            level.events[0].enemyTag="Asteroid";
            level.events[0].typeOfSpawn="enemyRandomSpawn";
            level.events[0].numberOfWave=10;
            level.events[0].numberOfObject=6; //number of object in one wave
        	level.events[0].RandomSpawnWaitMin=2;  //Time to wait before a new spawn//
        	level.events[0].RandomSpawnWaitMax=3;
        	level.events[0].wavewaitMin=5;  //Time to wait till a new wave
        	level.events[0].wavewaitMax=8;
        	level.events[0].position=new Vector2(3,6);  //position to spawn 
            level.events[0].duration=60;

            level.events[1]=new SpawnInfo();
            level.events[1].ID="LV4_Event01";
            level.events[1].enemyTag="EnemyRed";
            level.events[1].typeOfSpawn="singleSpawn";
        	level.events[1].position=new Vector2(3,6);  //position to spawn 

            level.events[2]=new SpawnInfo();
            level.events[2].ID="LV4_Event02";
            level.events[2].enemyTag="EnemyStream1";
            level.events[2].typeOfSpawn="enemyRandomSpawn";
            level.events[2].numberOfWave=5;
            level.events[2].numberOfObject=2; //number of object in one wave
        	level.events[2].RandomSpawnWaitMin=12;  //Time to wait before a new spawn
        	level.events[2].RandomSpawnWaitMax=12;
        	level.events[2].wavewaitMin=6;  //Time to wait till a new wave
        	level.events[2].wavewaitMax=6;
        	level.events[2].position=new Vector2(3,6);  //position to spawn 
            level.events[2].duration=60;

            level.timeToStartEachEvent=new float[6]{3,15,30,15,20,15};

            level.events[3]=new SpawnInfo();
            level.events[3].ID="LV2_Event03";
            level.events[3].enemyTag="EnemyGreen";
            level.events[3].typeOfSpawn="enemyRandomSpawn";
            level.events[3].numberOfWave=8;
            level.events[3].numberOfObject=4; //number of object in one wave
        	level.events[3].RandomSpawnWaitMin=2;  //Time to wait before a new spawn
        	level.events[3].RandomSpawnWaitMax=5;
        	level.events[3].wavewaitMin=4;  //Time to wait till a new wave
        	level.events[3].wavewaitMax=6;
        	level.events[3].position=new Vector2(3,6);  //position to spawn */
            level.events[3].duration=80;

            level.events[4]=new SpawnInfo();
            level.events[4].ID="LV4_Event04";
            level.events[4].enemyTag="EnemyStream4";
            level.events[4].typeOfSpawn="enemyHorizontalRandomSpawn";
            level.events[4].numberOfWave=6;
            level.events[4].numberOfObject=2; //number of object in one wave
        	level.events[4].RandomSpawnWaitMin=7;  //Time to wait before a new spawn
        	level.events[4].RandomSpawnWaitMax=10;
        	level.events[4].wavewaitMin=8;  //Time to wait till a new wave
        	level.events[4].wavewaitMax=8;
        	level.events[4].position=new Vector2(-5,4);  //position to spawn */
            level.events[4].duration=80;

            level.events[5]=new SpawnInfo();
            level.events[5].ID="LV4_Event05";
            level.events[5].enemyTag="EnemyRed";
            level.events[5].typeOfSpawn="enemyRandomSpawn";
            level.events[5].numberOfWave=2;
            level.events[5].numberOfObject=2; //number of object in one wave
        	level.events[5].RandomSpawnWaitMin=2;  //Time to wait before a new spawn
        	level.events[5].RandomSpawnWaitMax=5;
        	level.events[5].wavewaitMin=50;  //Time to wait till a new wave
        	level.events[5].wavewaitMax=50;
        	level.events[5].position=new Vector2(3,6);  //position to spawn 
            level.events[5].duration=80;

            string json =JsonUtility.ToJson(level,true);
       
            //string json=JsonHelper.ToJson(level);
            File.WriteAllText(Application.persistentDataPath+"//Level_4.json",json);
        }


/* 		string json=JsonUtility.ToJson(Obja,true);
		Debug.Log(json);//
		Debug.Log(Application.persistentDataPath);
		File.WriteAllText(Application.persistentDataPath+"/F.json",json);
		json=File.ReadAllText(Application.persistentDataPath+"/F.json");
		TestingS myOb=JsonUtility.FromJson<TestingS>(json);
		Debug.Log(myOb.position);
		Debug.Log(myOb.health); */ //
    }
	
}

