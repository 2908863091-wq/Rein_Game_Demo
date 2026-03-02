using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause_UI : MonoBehaviour
{
    public Button pause_button;
    public Button restart_button;
    public Button list_button;
    public Button contiue_button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pasue()
    {
        Debug.Log("pasue");
    }
    public void Contiue()
    {
        Debug.Log("contiue");
    }
    public void List()
    {
        Debug.Log("list");
    }
    public void Restart()
    {
        Debug.Log("restart");
    }
}
