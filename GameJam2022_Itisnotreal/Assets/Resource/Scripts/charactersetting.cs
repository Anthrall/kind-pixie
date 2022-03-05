using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class charactersetting : MonoBehaviour
{
    public CharacterName CharacterName;
    public bool isResult = false;
    public ResultSpell result;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void GetIdle()
    {
        anim.SetBool("neutral", false);
    }

    public void LaughSon()
    {
        FindObjectOfType<AudioManager>().Play("SonLaugh");
    }

    public void CatMeow()
    {
        FindObjectOfType<AudioManager>().Play("Meow");
    }
    public void CatPurr()
    {
        FindObjectOfType<AudioManager>().Play("Purr");
    }
    public void CatScream()
    {
        FindObjectOfType<AudioManager>().Play("CatScream");
    }
    public void DogRuff()
    {
        FindObjectOfType<AudioManager>().Play("Ruff");
    }
    public void MagicWind()
    {
        FindObjectOfType<AudioManager>().Play("Wind");
    }
    public void KickToTable()
    {
        FindObjectOfType<AudioManager>().Play("TableKick");
    }
    public void ThatYourDinner()
    {
        FindObjectOfType<AudioManager>().Play("KickDinner");
    }
    public void Thunder()
    {
        FindObjectOfType<AudioManager>().Play("Thunder");
    }
    public void MaleScream()
    {
        FindObjectOfType<AudioManager>().Play("MaleScream");
    }
    public void FemaleScream()
    {
        FindObjectOfType<AudioManager>().Play("FemaleScream");
    }
    public void FemaleCry()
    {
        FindObjectOfType<AudioManager>().Play("FemaleCry");
    }
    public void PlantGrow()
    {
        FindObjectOfType<AudioManager>().Play("Plant");
    }
    public void TreeUm()
    {
        FindObjectOfType<AudioManager>().Play("Tree");
    }
    public void WalkOnHouse()
    {
        FindObjectOfType<AudioManager>().Play("WalkHouse");
    }
    public void Run()
    {
        FindObjectOfType<AudioManager>().Play("Run");
    }
    public void StopRun()
    {
        FindObjectOfType<AudioManager>().Stop("Run");
    }
    public void SonScream()
    {
        FindObjectOfType<AudioManager>().Play("SonScream");
    }

    public void PlusOne()
    {
        FindObjectOfType<SpellsManager>().charascterSpelling += 1;
    }
    // Update is called once per frame
    void Update()
    {
    }

    
}
