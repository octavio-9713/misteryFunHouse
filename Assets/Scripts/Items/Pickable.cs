using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{

    [Header ("Item Info")]
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;

    [Header("Pickup Message")]
    public GameObject pickedPrefab;
    public bool showsProvoli = true;

    [Header("Pickup Effect")]   
    public ItemEffect effect;

    public GameObject itemImage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (showsProvoli)
            {
                GameObject picked = Instantiate(pickedPrefab.gameObject, GameManager.Instance.canvas.transform);
                picked.GetComponent<OnPickedItem>().SetText(itemName, itemDescription, itemSprite);
            }

            effect.ApplyEffect();

            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            Destroy(itemImage);
        }
    }

}
