using UnityEngine;
using System.Collections;

public class DestroyExplosions : MonoBehaviour
{
	void Start ()
    {
        StartCoroutine(Collapse());
	}
	
	IEnumerator Collapse()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
