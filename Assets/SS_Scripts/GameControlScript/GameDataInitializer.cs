using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Items
{
    public string[] enemy;
    public string[] gun;

}

[System.Serializable]
public class SpawnInfo
{
	public string ID;
	public string enemyTag; 
	public string typeOfSpawn;
	public int numberOfWave; 
	public int numberOfObject; 
    //Time to wait before a new spawn
	public float RandomSpawnWaitMin;  
	public float RandomSpawnWaitMax;
    //Time to wait till a new wave
	public float wavewaitMin;  
	public float wavewaitMax;
    //position to spawn
	public Vector2 position;  
	public bool spawnEnd=false;
    public float duration;
}
[System.Serializable]
public class EnemyInfo
{
    public string name;
    public float speed;						
	public int health;					
	public int ScoreValue;					
	public float fireRate;			
}


/* 
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
*/ 
public class GameDataInitializer : MonoBehaviour 
{
    PlayerWeapon gun;
    SpawnInfo info;

    EnemyInfo enemyInfo;
	void Awake () 
    {
        //Based value for object attributes
        Debug.Log(Application.persistentDataPath);
        if(!File.Exists(Application.persistentDataPath+"/Items.json"))
        {
            Items i=new Items();
            i.enemy=new string[4];
            i.enemy[0]="EnemyBlue";
            i.enemy[1]="EnemyGreen";
            i.enemy[2]="EnemyRed";
            i.enemy[3]="Asteroid";

            i.gun=new string[3];
            i.gun[0]="PlayerRegularShot";
            i.gun[1]="PlayerTracingShot";
            i.gun[2]="PlayerBurstShot";
            string json=JsonUtility.ToJson(i,true);
            File.WriteAllText(Application.persistentDataPath+"/Items.json",json);
        }

        if(!File.Exists(Application.persistentDataPath+"/EnemyBlue.json"))
        {
           enemyInfo=new EnemyInfo();
           enemyInfo.name="EnemyBlue";
           enemyInfo.speed=2;						
	       enemyInfo.health=2;						
           enemyInfo.ScoreValue=15;					
	       enemyInfo.fireRate = 4F;	
           string json=JsonUtility.ToJson(enemyInfo,true);        
           File.WriteAllText(Application.persistentDataPath+"/EnemyBlue.json",json);
        }

        if(!File.Exists(Application.persistentDataPath+"/EnemyGreen.json"))
        {
           enemyInfo=new EnemyInfo();
           enemyInfo.name="EnemyGreen";
           enemyInfo.speed=3;						
	       enemyInfo.health=4;						
           enemyInfo.ScoreValue=25;					
	       enemyInfo.fireRate = 3F;	
           string json=JsonUtility.ToJson(enemyInfo,true);        
           File.WriteAllText(Application.persistentDataPath+"/EnemyGreen.json",json);

        }

        if(!File.Exists(Application.persistentDataPath+"/EnemyRed.json"))
        {
           enemyInfo=new EnemyInfo();
           enemyInfo.name="EnemyRed";
           enemyInfo.speed=1.5f;						
	       enemyInfo.health=25;						
           enemyInfo.ScoreValue=100;					
	       enemyInfo.fireRate = 3F;	
           string json=JsonUtility.ToJson(enemyInfo,true);        
           File.WriteAllText(Application.persistentDataPath+"/EnemyRed.json",json);

        }

        if(!File.Exists(Application.persistentDataPath+"/Asteroid.json"))
        {
           enemyInfo=new EnemyInfo();
           enemyInfo.name="Asteroid";
           enemyInfo.speed=2f;						
	       enemyInfo.health=20;						
           enemyInfo.ScoreValue=10;						
           string json=JsonUtility.ToJson(enemyInfo,true);        
           File.WriteAllText(Application.persistentDataPath+"/Asteroid.json",json);

        }

        if(!File.Exists(Application.persistentDataPath+"/Boss1.json"))
        {
            BossInfo bossInfo=new BossInfo();
            bossInfo.MachineGunFireRate=0.3f;
            bossInfo.CentralGunFireRate=0.4f;
            bossInfo.health=150;
            bossInfo.speed=1.5f;
            bossInfo.ScoreValue=1000;

            string json=JsonUtility.ToJson(bossInfo,true);        
            File.WriteAllText(Application.persistentDataPath+"/Boss1.json",json);
        }

        if(!File.Exists(Application.persistentDataPath+"/PlayerRegularGun.json"))
        {
            gun=new PlayerWeapon();
            gun.shotType="PlayerRegularShot";
            gun.nextFire=0f;
            gun.fireRate=0.4f;
            gun.shotDamage=1;
            gun.shotSpeed=9f;
            gun.level=1;
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
            gun.level=1;
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
            gun.level=1;
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
            level.events[0].typeOfSpawn="EnemyRandomSpawn";
            level.events[0].numberOfWave=5;
            level.events[0].numberOfObject=4; 
        	level.events[0].RandomSpawnWaitMin=6;  
        	level.events[0].RandomSpawnWaitMax=6;
        	level.events[0].wavewaitMin=6;  
        	level.events[0].wavewaitMax=8;
        	level.events[0].position=new Vector2(3,6);   
            level.events[0].duration=85;

            level.events[1]=new SpawnInfo();
            level.events[1].ID="LV1_Event01";
            level.events[1].enemyTag="EnemyStream1";
            level.events[1].typeOfSpawn="EnemyRandomSpawn";
            level.events[1].numberOfWave=4;
            level.events[1].numberOfObject=2; 
        	level.events[1].RandomSpawnWaitMin=12;  
        	level.events[1].RandomSpawnWaitMax=12;
        	level.events[1].wavewaitMin=6;  
        	level.events[1].wavewaitMax=6;
        	level.events[1].position=new Vector2(3,6);   
            level.events[1].duration=80;
            level.timeToStartEachEvent=new float[2]{3,85};

            string json =JsonUtility.ToJson(level,true);
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
            level.events[0].typeOfSpawn="EnemyRandomSpawn";
            level.events[0].numberOfWave=4;
            level.events[0].numberOfObject=2; 
        	level.events[0].RandomSpawnWaitMin=12;  
        	level.events[0].RandomSpawnWaitMax=12;
        	level.events[0].wavewaitMin=6;  
        	level.events[0].wavewaitMax=6;
        	level.events[0].position=new Vector2(3,6);   
            level.events[0].duration=20;
            level.timeToStartEachEvent=new float[2]{3,85};
            

            level.events[3]=new SpawnInfo();
            level.events[3].ID="LV2_Event04";
            level.events[3].enemyTag="EnemyGreen";
            level.events[3].typeOfSpawn="EnemyRandomSpawn";
            level.events[3].numberOfWave=8;
            level.events[3].numberOfObject=4; 
        	level.events[3].RandomSpawnWaitMin=2;  
        	level.events[3].RandomSpawnWaitMax=5;
        	level.events[3].wavewaitMin=4;  
        	level.events[3].wavewaitMax=6;
        	level.events[3].position=new Vector2(3,6);  
            level.events[3].duration=80;

            level.events[1]=new SpawnInfo();
            level.events[1].ID="LV2_Event01";
            level.events[1].enemyTag="EnemyStream2";
            level.events[1].typeOfSpawn="EnemyHorizontalRandomSpawn";
            level.events[1].numberOfWave=5;
            level.events[1].numberOfObject=2; 
        	level.events[1].RandomSpawnWaitMin=7;  
        	level.events[1].RandomSpawnWaitMax=10;
        	level.events[1].wavewaitMin=8;  
        	level.events[1].wavewaitMax=8;
        	level.events[1].position=new Vector2(-5,4);  
            level.events[1].duration=50;

            level.events[2]=new SpawnInfo();
            level.events[2].ID="LV2_Event03";
            level.events[2].enemyTag="EnemyStream3";
            level.events[2].typeOfSpawn="EnemyHorizontalRandomSpawn";
            level.events[2].numberOfWave=5;
            level.events[2].numberOfObject=2; 
        	level.events[2].RandomSpawnWaitMin=7;  
        	level.events[2].RandomSpawnWaitMax=10;
        	level.events[2].wavewaitMin=8;  
        	level.events[2].wavewaitMax=8;
        	level.events[2].position=new Vector2(5,4);  
            level.events[2].duration=50;

            level.timeToStartEachEvent=new float[4]{3,20,20,20};

            string json =JsonUtility.ToJson(level,true);
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
            level.events[0].typeOfSpawn="EnemyRandomSpawn";
            level.events[0].numberOfWave=10;
            level.events[0].numberOfObject=6; 
        	level.events[0].RandomSpawnWaitMin=2;  
        	level.events[0].RandomSpawnWaitMax=6;
        	level.events[0].wavewaitMin=3;  
        	level.events[0].wavewaitMax=8;
        	level.events[0].position=new Vector2(3,6);  
            level.events[0].duration=45;

            level.events[1]=new SpawnInfo();
            level.events[1].ID="LV3_Event01";
            level.events[1].enemyTag="EnemyRed";
            level.events[1].typeOfSpawn="SingleSpawn";
        	level.events[1].position=new Vector2(3,6); 
            level.events[1].duration=60;  

            level.events[2]=new SpawnInfo();
            level.events[2].ID="LV3_Event02";
            level.events[2].enemyTag="EnemyRed";
            level.events[2].typeOfSpawn="EnemyRandomSpawn";
            level.events[2].numberOfWave=1;
            level.events[2].numberOfObject=2; 
        	level.events[2].RandomSpawnWaitMin=2;  
        	level.events[2].RandomSpawnWaitMax=5;
        	level.events[2].wavewaitMin=5;  
        	level.events[2].wavewaitMax=8;
        	level.events[2].position=new Vector2(3,6);   
            level.events[2].duration=60;

            level.events[3]=new SpawnInfo();
            level.events[3].ID="LV3_Event03";
            level.events[3].enemyTag="EnemyStream2";
            level.events[3].typeOfSpawn="EnemyHorizontalRandomSpawn";
            level.events[3].numberOfWave=3;
            level.events[3].numberOfObject=2; 
        	level.events[3].RandomSpawnWaitMin=7;  
        	level.events[3].RandomSpawnWaitMax=10;
        	level.events[3].wavewaitMin=8;  
        	level.events[3].wavewaitMax=8;
        	level.events[3].position=new Vector2(-5,4);  
            level.events[3].duration=20;

            level.events[4]=new SpawnInfo();
            level.events[4].ID="LV3_Event04";
            level.events[4].enemyTag="EnemyGreen";
            level.events[4].typeOfSpawn="EnemyRandomSpawn";
            level.events[4].numberOfWave=8;
            level.events[4].numberOfObject=4; 
        	level.events[4].RandomSpawnWaitMin=2;  
        	level.events[4].RandomSpawnWaitMax=5;
        	level.events[4].wavewaitMin=4;  
        	level.events[4].wavewaitMax=6;
        	level.events[4].position=new Vector2(3,6);  
            level.events[4].duration=80;

            level.events[5]=new SpawnInfo();
            level.events[5].ID="LV3_Event04";
            level.events[5].enemyTag="EnemyStream3";
            level.events[5].typeOfSpawn="EnemyHorizontalRandomSpawn";
            level.events[5].numberOfWave=3;
            level.events[5].numberOfObject=2; 
        	level.events[5].RandomSpawnWaitMin=7;  
        	level.events[5].RandomSpawnWaitMax=10;
        	level.events[5].wavewaitMin=8;  
        	level.events[5].wavewaitMax=8;
        	level.events[5].position=new Vector2(5,4);  
            level.events[5].duration=20;

            level.events[6]=new SpawnInfo();
            level.events[6].ID="LV3_Event05";
            level.events[6].enemyTag="EnemyStream4";
            level.events[6].typeOfSpawn="EnemyHorizontalRandomSpawn";
            level.events[6].numberOfWave=3;
            level.events[6].numberOfObject=2; 
        	level.events[6].RandomSpawnWaitMin=7;  
        	level.events[6].RandomSpawnWaitMax=10;
        	level.events[6].wavewaitMin=8;  
        	level.events[6].wavewaitMax=8;
        	level.events[6].position=new Vector2(-5,4);  
            level.events[6].duration=20;

            level.events[7]=new SpawnInfo();
            level.events[7].ID="LV3_Event06";
            level.events[7].enemyTag="EnemyStream5";
            level.events[7].typeOfSpawn="EnemyHorizontalRandomSpawn";
            level.events[7].numberOfWave=3;
            level.events[7].numberOfObject=2; 
        	level.events[7].RandomSpawnWaitMin=7;  
        	level.events[7].RandomSpawnWaitMax=10;
        	level.events[7].wavewaitMin=8;  
        	level.events[7].wavewaitMax=8;
        	level.events[7].position=new Vector2(5,4);  
            level.events[7].duration=20;

            level.timeToStartEachEvent=new float[8]{3,45,45,60,10,30,20,15};

            string json =JsonUtility.ToJson(level,true);
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
            level.events[0].typeOfSpawn="EnemyRandomSpawn";
            level.events[0].numberOfWave=10;
            level.events[0].numberOfObject=6; 
        	level.events[0].RandomSpawnWaitMin=2;  
        	level.events[0].RandomSpawnWaitMax=3;
        	level.events[0].wavewaitMin=5;  
        	level.events[0].wavewaitMax=8;
        	level.events[0].position=new Vector2(3,6);  
            level.events[0].duration=60;

            level.events[1]=new SpawnInfo();
            level.events[1].ID="LV4_Event01";
            level.events[1].enemyTag="EnemyRed";
            level.events[1].typeOfSpawn="SingleSpawn";
        	level.events[1].position=new Vector2(3,6);   

            level.events[2]=new SpawnInfo();
            level.events[2].ID="LV4_Event02";
            level.events[2].enemyTag="EnemyStream1";
            level.events[2].typeOfSpawn="EnemyRandomSpawn";
            level.events[2].numberOfWave=5;
            level.events[2].numberOfObject=2; 
        	level.events[2].RandomSpawnWaitMin=12;  
        	level.events[2].RandomSpawnWaitMax=12;
        	level.events[2].wavewaitMin=6;  
        	level.events[2].wavewaitMax=6;
        	level.events[2].position=new Vector2(3,6);   
            level.events[2].duration=60;

            level.events[3]=new SpawnInfo();
            level.events[3].ID="LV4_Event03";
            level.events[3].enemyTag="EnemyGreen";
            level.events[3].typeOfSpawn="EnemyRandomSpawn";
            level.events[3].numberOfWave=8;
            level.events[3].numberOfObject=4; 
        	level.events[3].RandomSpawnWaitMin=2;  
        	level.events[3].RandomSpawnWaitMax=5;
        	level.events[3].wavewaitMin=4;  
        	level.events[3].wavewaitMax=6;
        	level.events[3].position=new Vector2(3,6);  
            level.events[3].duration=80;

            level.events[4]=new SpawnInfo();
            level.events[4].ID="LV4_Event04";
            level.events[4].enemyTag="EnemyStream4";
            level.events[4].typeOfSpawn="EnemyHorizontalRandomSpawn";
            level.events[4].numberOfWave=6;
            level.events[4].numberOfObject=2; 
        	level.events[4].RandomSpawnWaitMin=7;  
        	level.events[4].RandomSpawnWaitMax=10;
        	level.events[4].wavewaitMin=8;  
        	level.events[4].wavewaitMax=8;
        	level.events[4].position=new Vector2(-5,4);  
            level.events[4].duration=80;

            level.events[5]=new SpawnInfo();
            level.events[5].ID="LV4_Event05";
            level.events[5].enemyTag="EnemyRed";
            level.events[5].typeOfSpawn="EnemyRandomSpawn";
            level.events[5].numberOfWave=2;
            level.events[5].numberOfObject=2; 
        	level.events[5].RandomSpawnWaitMin=2;  
        	level.events[5].RandomSpawnWaitMax=5;
        	level.events[5].wavewaitMin=50;  
        	level.events[5].wavewaitMax=50;
        	level.events[5].position=new Vector2(3,6);   
            level.events[5].duration=140;

            level.timeToStartEachEvent=new float[6]{3,15,30,15,20,15};

            string json =JsonUtility.ToJson(level,true);
            File.WriteAllText(Application.persistentDataPath+"//Level_4.json",json);
        }

        if(!File.Exists(Application.persistentDataPath+"/Level_5.json"))
        {
            Level level=new Level();
            level.name="Level_5";
        
            level.events=new SpawnInfo[2];

            level.events[0]=new SpawnInfo();
            level.events[0].ID="LV5_Event00";
            level.events[0].enemyTag="EnemyGreen";
            level.events[0].typeOfSpawn="EnemyRandomSpawn";
            level.events[0].numberOfWave=10;
            level.events[0].numberOfObject=5; 
        	level.events[0].RandomSpawnWaitMin=2;  
        	level.events[0].RandomSpawnWaitMax=5;
        	level.events[0].wavewaitMin=4;  
        	level.events[0].wavewaitMax=6;
        	level.events[0].position=new Vector2(3,6);  
            level.events[0].duration=80;

            level.events[1]=new SpawnInfo();
            level.events[1].ID="LV5_Event01";
            level.events[1].enemyTag="EnemyRed";
            level.events[1].typeOfSpawn="EnemyRandomSpawn";
            level.events[1].numberOfWave=3;
            level.events[1].numberOfObject=2; 
        	level.events[1].RandomSpawnWaitMin=2;  
        	level.events[1].RandomSpawnWaitMax=5;
        	level.events[1].wavewaitMin=50;  
        	level.events[1].wavewaitMax=50;
        	level.events[1].position=new Vector2(3,6);   
            level.events[1].duration=160;

            level.timeToStartEachEvent=new float[2]{3,15};

            string json =JsonUtility.ToJson(level,true);
            File.WriteAllText(Application.persistentDataPath+"//Level_5.json",json);
        }

        if(!File.Exists(Application.persistentDataPath+"/Level_6.json"))
        {
            Level level=new Level();
            level.name="Level_6";
        
            level.events=new SpawnInfo[1];
            level.events[0]=new SpawnInfo();
            level.events[0].ID="LV6_Event00";
            level.events[0].enemyTag="Boss1";
            level.events[0].typeOfSpawn="BossSpawn";
            level.events[0].duration=180;

            level.timeToStartEachEvent=new float[1]{6};
            string json =JsonUtility.ToJson(level,true);
            File.WriteAllText(Application.persistentDataPath+"//Level_6.json",json);
        }

    }
	
}

