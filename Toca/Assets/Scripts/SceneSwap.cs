using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwap : MonoBehaviour
{
    public void SwapScene (string s)
    {
        SaveManager.Save();
        SceneManager.LoadScene(s);
    }
}
