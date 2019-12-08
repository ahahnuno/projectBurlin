using UnityEngine;

public class interfaceStart : MonoBehaviour
{
    bool pop = false;

    void Start()
    {
        //locking the frame rate. deltaTime does not seem to work for my animation state machines
        Application.targetFrameRate = 30;

        //fixing the timeScale if loaded from fightScene
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.anyKey)
        {
            //when the user presses any key, the game begins
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<characterMovement>().enabled = true;
            pop = true;
        }

        if(pop)
        {
            //the menu context iterates when the game officially starts
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
