using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private int Health;
    [SerializeField]
    private int Damage;

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



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
