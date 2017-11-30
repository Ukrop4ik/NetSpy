using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private int Health;
    [SerializeField]
    private int Damage;
    [SerializeField]
    private int Data;

    private static Player instance;
    public static Player Instance() { return instance; }

    public int GetHealth()
    {
        return Health;
    }
    public void SetHealth(int value)
    {
        Health += value;
    }
    public int GetDamage()
    {
        return Damage;
    }
    public void SetDamage(int value)
    {
        Damage += value;
    }
    public int GetData()
    {
        return Data;
    }
    public void SetData(int value)
    {
        Data += value;
    }



    // Use this for initialization
    void Start ()
    {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
