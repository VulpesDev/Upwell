using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Multiplier : MonoBehaviour
{
    private static TMP_Text tMP_Text;
    private static int multiply = 1;
    // Start is called before the first frame update
    void Start() {
        tMP_Text = GetComponent<TMP_Text>();
        tMP_Text.text = multiply.ToString();
    }

    public static void addMultiplier(int value) {
        multiply += value;
        tMP_Text.text = multiply.ToString();
    }

    public static void setMultiplier(int value) {
        multiply = value;
        tMP_Text.text = multiply.ToString();
    }
}
