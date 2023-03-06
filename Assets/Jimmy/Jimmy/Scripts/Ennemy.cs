using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : Object
{
    List<GameObject> ennemyList;

    public string type;
    public int health, moveSpeed;
    public float rangeToAttack;
    public int delayToAttack;

    public Ennemy(string type)
    {
        this.type = type;

        switch (type)
        {
            case "Ghost":
                this.health = 75;
                this.moveSpeed = 30;
                this.rangeToAttack = 10;
                this.delayToAttack = 2;
                break;

            case "Witch":
                this.health = 100;
                this.moveSpeed = 25;
                this.rangeToAttack = 15;
                this.delayToAttack = 3;
                break;

            case "Caretaker":
                this.health = 125;
                this.moveSpeed = 20;
                this.rangeToAttack = 5;
                this.delayToAttack = 1;
                break;
        }
    }

    public GameObject InstantiateEnnemy(GameObject prefab, GameObject spawner)
    {
        System.Random rnd = new System.Random();

        //Get the world position of the specified spawner
        Vector3 spawnerPosition = spawner.transform.position;

        //Get two random float within a range of radius
        double minRadius = 10;
        double maxRadius = 20;
        double range = maxRadius - minRadius;
        float randX = (float)((rnd.NextDouble() * range) + minRadius);
        float randz = (float)((rnd.NextDouble() * range) + minRadius);

        //Get a random float between 0 and 1, if round to 1 spawningPosition = spawnerPosition + radius, if round to 0 spawningPosition = spawnerPosition - radius
        //The instance of the gameObject is set as child of the spwaner : in spwanerScript, 
        if ((int)Mathf.Round((float)rnd.NextDouble()) == 1)
        {
            if ((int)Mathf.Round((float)rnd.NextDouble()) == 1)
            {
                GameObject character = Instantiate(prefab, new Vector3((spawnerPosition.x + randX), spawnerPosition.y + 1, (spawnerPosition.z + randz)), Quaternion.identity, spawner.transform);
                return character;
            }
            else
            {
                GameObject character = Instantiate(prefab, new Vector3((spawnerPosition.x + randX), spawnerPosition.y + 1, (spawnerPosition.z - randz)), Quaternion.identity, spawner.transform);
                return character;
            }
        }
        else
        {
            if ((int)Mathf.Round((float)rnd.NextDouble()) == 1)
            {
                GameObject character = Instantiate(prefab, new Vector3((spawnerPosition.x - randX), spawnerPosition.y + 1, (spawnerPosition.z + randz)), Quaternion.identity, spawner.transform);
                return character;
            }
            else
            {
                GameObject character = Instantiate(prefab, new Vector3((spawnerPosition.x - randX), spawnerPosition.y + 1, (spawnerPosition.z - randz)), Quaternion.identity, spawner.transform);
                return character;
            }
        }
    }
}
