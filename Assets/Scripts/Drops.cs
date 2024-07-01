using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Drops : MonoBehaviour
{
    //components
    public Rigidbody rb;
    public Vector3 velocity;

    //targets
    public GameObject target;
    public bool isPicked;

    [SerializeField] GameObject effect;
    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.drops.Add(gameObject);
        isPicked = false;
    }

    private void FixedUpdate()
    {
        if (isPicked && target)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position + Vector3.up, 0.08f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LootRange")
        {
            target = other.transform.parent.gameObject;
            rb.AddForce(Vector3.up * 2, ForceMode.Impulse);
            isPicked = true;
        }
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            EffectController.Instance.PlayEffect(effect, other.gameObject.transform);
            other.gameObject.GetComponent<LevelManager>().AddLevel(other.gameObject.GetComponent<Character>().foodPerDrop);
        }
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            EffectController.Instance.PlayEffect(effect, other.gameObject.transform);
            other.gameObject.GetComponent<LevelManager>().AddLevel(other.gameObject.GetComponent<Character>().foodPerDrop);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "LootRange")
        {
            isPicked = false;
            target = null;
        }
    }
}
