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


public class GameDataInitializer : MonoBehaviour {
    PlayerWeapon RegularShot;
    PlayerGun PlayerWeapon;
    
	void Start () {
        //Based value for object attributes
        Debug.Log(Application.persistentDataPath);
        //Gun Type 
        //Gun type and round type are different
        if(!File.Exists(Application.persistentDataPath+"/PlayerRegularShot.json"))
        {
            RegularShot=new PlayerWeapon();
            RegularShot.shotTag="PlayerRegularShot";
            RegularShot.nextFire=0f;
            RegularShot.fireRate=0.4f;
            Debug.Log(Application.persistentDataPath+"/PlayerRegularShot.json");

            string json=JsonUtility.ToJson(RegularShot,true);        
            File.WriteAllText(Application.persistentDataPath+"/PlayerRegularShot.json",json);

        }
        //
        if(!File.Exists(Application.persistentDataPath+"/PlayerBurstShot.json"))
        {
            RegularShot=new PlayerWeapon();
            RegularShot.shotTag="PlayerBurstShot";
            RegularShot.nextFire=0f;
            RegularShot.fireRate=1.5f;
            Debug.Log(Application.persistentDataPath+"/PlayerBurstShot.json");

            string json=JsonUtility.ToJson(RegularShot,true);        
            File.WriteAllText(Application.persistentDataPath+"/PlayerBurstShot.json",json);
        }

        if(!File.Exists(Application.persistentDataPath+"/PlayerTracingShot.json"))
        {
            RegularShot=new PlayerWeapon();
            RegularShot.shotTag="PlayerTracingShot";
            RegularShot.nextFire=0f;
            RegularShot.fireRate=1f;
            Debug.Log(Application.persistentDataPath+"/PlayerTracingShot.json");

            string json=JsonUtility.ToJson(RegularShot,true);        
            File.WriteAllText(Application.persistentDataPath+"//PlayerTracingShot.json",json);
        }
        //Round type
        if(!File.Exists(Application.persistentDataPath+"/PlayerRegularRound.json"))
        {
            Player_RegularShot RegularRound=new Player_RegularShot();
            RegularRound.damage=1;
            RegularRound.speed=10;
            Debug.Log(Application.persistentDataPath+"/PlayerRegularRound.json");

            string json=JsonUtility.ToJson(RegularShot,true);        
            File.WriteAllText(Application.persistentDataPath+"//PlayerRegularRound.json",json);
        }

        if(!File.Exists(Application.persistentDataPath+"/PlayerBurstRound.json"))
        {
            Player_BurstShot BurstRound=new Player_BurstShot();
            BurstRound.damage=5;
            BurstRound.speed=8;
            Debug.Log(Application.persistentDataPath+"/PlayerRegularRound.json");

            string json=JsonUtility.ToJson(RegularShot,true);        
            File.WriteAllText(Application.persistentDataPath+"//PlayerRegularRound.json",json);
        }
        
        if(!File.Exists(Application.persistentDataPath+"/PlayerTracingRound.json"))
        {
            Player_TracingShot TracingRound=new Player_TracingShot();
            TracingRound.damage=3;
            TracingRound.speed=3;
            Debug.Log(Application.persistentDataPath+"/PlayerRegularRound.json");

            string json=JsonUtility.ToJson(RegularShot,true);        
            File.WriteAllText(Application.persistentDataPath+"//PlayerRegularRound.json",json);
        }
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

