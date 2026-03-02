using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Slider slider;
    public float blood = 0;
    public LunaController controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        blood = (float)controller.Ima_health / (float)controller.Max_health;
        slider.value = blood;
    }

}
