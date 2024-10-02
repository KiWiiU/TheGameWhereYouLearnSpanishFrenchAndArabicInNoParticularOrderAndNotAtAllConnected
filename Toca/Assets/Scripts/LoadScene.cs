using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public void Character()
    {
        SceneManager.LoadScene("CharacterCreator");
    }

    public void Lesson()
    {
        SceneManager.LoadScene("Lessons");
    }
}
