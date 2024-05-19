using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Multiplier : MonoBehaviour
{
    private static TMP_Text tMP_Text;
    private static int multiply = 1;
    private static Animator animator;
    // Start is called before the first frame update
    void Start() {
        tMP_Text = GetComponent<TMP_Text>();
        animator = GetComponent<Animator>();
        tMP_Text.text = multiply.ToString();
    }

    public static void addMultiplier(int value) {
        if (animator)
            animator.SetTrigger("score");
        multiply += value;
        tMP_Text.text = multiply.ToString();
    }

    public static void setMultiplier(int value) {
        multiply = value;
        tMP_Text.text = multiply.ToString();
    }

    public static int getMultiplier() {
        return multiply;
    }
}
