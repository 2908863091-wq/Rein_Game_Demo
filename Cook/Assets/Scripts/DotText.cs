using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DotText : MonoBehaviour
{
    private TextMeshProUGUI dotText;
    private float dotRate = 0.3f;

    private void Start()
    {
        dotText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(DotAnimation());
    }

    IEnumerator DotAnimation()
    {
        string[] dots = { ".", "..", "...", "....", ".....", "......"};
        int index = 0;

        while (true)
        {
            dotText.text = dots[index];
            yield return new WaitForSeconds(dotRate);
            index = (index + 1) % dots.Length;
        }
    }
}
