using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AchievementPanel : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text description;
    Animator anim;


    public static AchievementPanel Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        if (Instance != this)
        {
            Debug.Log("More than one instance of " + name);
            Destroy(this);
        }

        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator ShowAchievement(Achievement achievement)
    {
        title.text = achievement.title;
        description.text = achievement.description;
        anim.SetBool("panelOpen", true);
        yield return new WaitForSeconds(5f);
        anim.SetBool("panelOpen", false);
    }
}
