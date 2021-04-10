using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{

    [Header ("Item Info")]
    public string itemName;
    public string itemDescription;

    [Header("Pickup Message")]
    public GameObject pickedPrefab;
    public bool showsProvoli = true;

    [Header("Pickup Effect")]   
    public ItemEffect effect;


    public void Start()
    {
        pickedPrefab.GetComponent<OnPickedItem>().SetText(itemName, itemDescription);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject picked = Instantiate(pickedPrefab.gameObject, GameManager.Instance.canvas.transform);
            picked.GetComponent<OnPickedItem>().SetText(itemName, itemDescription);

            effect.ApplyEffect();
            
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;

            Destroy(gameObject);
        }
    }
}
