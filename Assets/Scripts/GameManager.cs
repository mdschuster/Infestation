using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        //Cursor.lockState = CursorLockMode.Confined; // keep confined in the game window
        Cursor.lockState = CursorLockMode.Locked;   // keep confined to center of screen
        //Cursor.lockState = CursorLockMode.None;     // set to default default
    }

    public static GameManager Instance()
    {
        return _instance;
    }


    public GameObject player;
    public Image mainLogo;
    public Image logo3D;
    public Image instructions;
    public Image insImage;
    public Canvas UICanvas;
    private bool controlLocked;




    // Start is called before the first frame update
    void Start()
    {
        controlLocked = true;
        UICanvas.gameObject.SetActive(true);

        StartCoroutine(fadeUIImage());

    }

    public bool isControlLocked()
    {
        return controlLocked;
    }

    public IEnumerator fadeUIImage()
    {

        float alpha = mainLogo.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 2f)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, 1f, t));
            mainLogo.color = newColor;
            yield return null;
        }

        alpha = logo3D.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 2f)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, 1f, t));
            logo3D.color = newColor;
            yield return null;
        }
        alpha = instructions.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 1f)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, 1f, t));
            instructions.color = newColor;
            insImage.color = newColor;
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        UICanvas.gameObject.SetActive(false);
        controlLocked = false;
    }

    public void fadeImage(Image image)
    {
        image.CrossFadeAlpha(1f, 2f, false);
    }

}
