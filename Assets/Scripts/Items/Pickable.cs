using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{

    public string itemName;
    public string itemDescription;

    public GameObject pickedPrefab;

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
            effect.ApplyEffect();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;

            StartCoroutine(DestroyPicked(picked));
        }
    }

    IEnumerator DestroyPicked(GameObject picked)
    {
        yield return new WaitForSeconds(2);
        picked.GetComponent<Animator>().SetBool("fade", true);
        Destroy(gameObject);
        Destroy(picked, 1f);
    }
}
