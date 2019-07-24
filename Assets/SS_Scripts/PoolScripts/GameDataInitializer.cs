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
    PlayerWeapon gun;
	void Start () {
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

