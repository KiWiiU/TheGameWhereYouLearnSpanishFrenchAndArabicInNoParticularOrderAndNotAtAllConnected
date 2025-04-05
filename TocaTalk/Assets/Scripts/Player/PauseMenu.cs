
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    Canvas canvas;

    public GameObject sure;
    public SceneSwap sceneSwap;
    void Start()
    {
        canvas = GetComponent<Canvas>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(canvas.enabled) {
                Resume();
            } else {
                canvas.enabled = true;
                Time.timeScale = 0;
            }
        }
    }

    public void Resume() {
        canvas.enabled = false;
        Time.timeScale = 1;
    }

    public void Quit() {
        sure.gameObject.SetActive(true);
        transform.Find("PAUSE").gameObject.SetActive(false);
        transform.Find("Resume").gameObject.SetActive(false);
        transform.Find("Quit").gameObject.SetActive(false);
    }

    public void QuitSure(int type) {
        if(!gameObject.activeSelf)
            return;
        if(type == 0) {// if user presses 'yes'
            sceneSwap.SwapScene("Lessons");
        } else { // if user presses 'no'
            sure.gameObject.SetActive(false);
            transform.Find("PAUSE").gameObject.SetActive(true);
            transform.Find("Resume").gameObject.SetActive(true);
            transform.Find("Quit").gameObject.SetActive(true);
        }
    }
}
