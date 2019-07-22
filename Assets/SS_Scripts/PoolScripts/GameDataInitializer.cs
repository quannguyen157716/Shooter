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
    PlayerShot RegularShot;
    PlayerGun PlayerWeapon;
    
	void Start () {
        Debug.Log(Application.persistentDataPath);
        if(!File.Exists(Application.persistentDataPath+"/RegularShot.json"))
        {
            RegularShot=new PlayerShot();
            RegularShot.shotTag="RegularShot";
            RegularShot.damage=1;
            RegularShot.speed=10;
            Debug.Log(Application.persistentDataPath+"/RegularShot.json");

            string json=JsonUtility.ToJson(RegularShot,true);        
            File.WriteAllText(Application.persistentDataPath+"/RegularShot.json",json);

        }

        if(!File.Exists(Application.persistentDataPath+"/BurstShot.json"))
        {
            RegularShot=new PlayerShot();
            RegularShot.shotTag="BurstShot";
            RegularShot.damage=3;
            RegularShot.speed=10;
            Debug.Log(Application.persistentDataPath+"/BurstShot.json");

            string json=JsonUtility.ToJson(RegularShot,true);        
            File.WriteAllText(Application.persistentDataPath+"/BurstShot.json",json);
        }

        if(!File.Exists(Application.persistentDataPath+"/TracingShot.json"))
        {
            RegularShot=new PlayerShot();
            RegularShot.shotTag="TracingShot";
            RegularShot.damage=3;
            RegularShot.speed=10;
            Debug.Log(Application.persistentDataPath+"/TracingShot.json");

            string json=JsonUtility.ToJson(RegularShot,true);        
            File.WriteAllText(Application.persistentDataPath+"/TracingShot.json",json);
        }

        if(!File.Exists(Application.persistentDataPath+"/PlayerGunConfig.json"))
        {
            PlayerWeapon= new PlayerGun();
            PlayerWeapon.fireRate=1f;
            PlayerWeapon.nextFire=0.0f;
            Debug.Log(Application.persistentDataPath+"/PlayerGunConfig.json");

            string json=JsonUtility.ToJson(RegularShot,true);        
            File.WriteAllText(Application.persistentDataPath+"/PlayerGunConfig.json",json);
        }
    }
/* 		string json=JsonUtility.ToJson(Obja,true);
		Debug.Log(json);
		Debug.Log(Application.persistentDataPath);
		File.WriteAllText(Application.persistentDataPath+"/F.json",json);
		json=File.ReadAllText(Application.persistentDataPath+"/F.json");
		TestingS myOb=JsonUtility.FromJson<TestingS>(json);
		Debug.Log(myOb.position);
		Debug.Log(myOb.health); */
		
	
}

