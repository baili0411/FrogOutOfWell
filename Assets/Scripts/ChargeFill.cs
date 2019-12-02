using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeFill : MonoBehaviour
{
    public Image bar;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCharge();
    }

    public void UpdateCharge(){
        bar.fillAmount = player.GetComponent<PlayerMovement>().charge;
    }
}
