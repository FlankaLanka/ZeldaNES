using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    Rigidbody rb;

    public float power = 0.5f;
    public float knock_time = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        GameObject collided_with = other.gameObject;

        if (collided_with.tag == "Enemy")
        {
            Knock(); //tells Link to stop getting knocked back

            Vector3 diff = transform.position - collided_with.transform.position;
            Debug.Log(diff.x);
            Debug.Log(diff.y);
            diff = diff.normalized * power;

            rb.AddForce(diff, ForceMode.Impulse);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Knock(); //tells Link to stop getting knocked back
        }
    }

    private void Knock()
    {
        StartCoroutine(getKnockTime());
    }


    private IEnumerator getKnockTime()
    {
        yield return new WaitForSeconds(knock_time);
        rb.velocity = Vector3.zero;
    }
}
