using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeInstructions : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(9);
        gameObject.SetActive(false);
    }
}
