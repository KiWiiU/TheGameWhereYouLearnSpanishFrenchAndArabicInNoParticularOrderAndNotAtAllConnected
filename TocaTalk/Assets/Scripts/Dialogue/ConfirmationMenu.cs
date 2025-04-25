using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmationMenu : MonoBehaviour
{
    public void onAnimationEnd() {
        gameObject.GetComponent<Canvas>().enabled = false;
    }
}
