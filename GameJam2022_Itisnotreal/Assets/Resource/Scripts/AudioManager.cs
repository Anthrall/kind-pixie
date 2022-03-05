//using UnityEditor.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public float maxDistance=60, minDistance = 20;
    public Sounds[] sounds;
    private float volume, currentdist=100;
    public bool CutsceneIsEnd = false;



    private AudioSource OtherSounds;
    public static AudioManager instance;

    public GameObject Player;

    void Start()
    {
        OtherSounds = gameObject.AddComponent<AudioSource>();

        Play("Theme");
        Play("Effect");

        foreach (Sounds s in sounds)
        {
            
                s.originalVolume = s.volume;
            
                
        }
        //PlayBackground();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 && CutsceneIsEnd)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            foreach (Sounds audio in sounds)
            {
                if (audio.Characters.Length != 0)
                {

                    foreach (GameObject ch in audio.Characters)
                    {
                        if(audio.originalVolume == 0)
                        {
                            audio.originalVolume = 0.5f;
                        }

                        float distance = GetDistance(ch);
                        if (audio.Characters.Length > 1)
                        {
                            if (currentdist >= distance)
                            {
                                currentdist = distance;
                            }
                        }
                        else
                        {
                            currentdist = distance;
                        }
                    }
                    if ((!Player.GetComponent<player_movement>().IsOutside && audio.outside && !audio.IsTheme) ||
                        (Player.GetComponent<player_movement>().IsOutside && !audio.outside && !audio.IsTheme))
                    {
                        currentdist = maxDistance;
                    }



                    if (currentdist <= maxDistance)
                    {
                        volume = Mathf.Abs(GetDistanceToVolume(currentdist, audio.originalVolume));
                    }
                    else
                        volume = 0;

                    //Debug.Log("For " + audio.name.ToUpper() + " volume = " + volume + "  Distance to Player => " + currentdist);
                    audio.source.volume = volume;
                    audio.volume = volume;

                }

                audio.source.pitch = audio.pitch;
            }
        }

    }

    public float GetDistanceToVolume(float distance, float max)
    {
        float From1 = 0, From2 = maxDistance, To1 = max, To2 = 0;
        To1 *= -1;
        return (distance - From1) / (From2 - From1)*(To2-To1)+To1;
    }

    public float GetDistance(GameObject character)
    {
        float dist = Vector3.Distance(Player.transform.position, character.transform.position);
        return dist;
        //Debug.Log("Distance Player --> " + character.name.ToUpper() + " = " + dist);
       
    }

    void Awake()
    {
        

       /* if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }*/

         foreach (Sounds s in sounds)
         {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spatialBlend;
            s.source.rolloffMode = s.rollof;
            //s.originalVolume = s.volume;
         }
        CutsceneIsEnd = false;
        Play("Theme");
        Play("Effect");

    }

    public void StopEveryone()
    {
        foreach (Sounds s in sounds)
        {
            s.source.Stop();
        }
    }

    public void Stop(string name)
    {
        foreach (Sounds s in sounds)
        {
            if (s.name == name)
            {
                s.source.Stop();
                break;
            }
        }
    }

    public void Play(string name)
    {
        foreach(Sounds s in sounds)
        {
            if(s.name == name)
            {
                if (!s.source.isPlaying)
                {
                    if (s.source.loop)
                    {
                        s.source.Play();
                    }
                    else
                    {
                        s.source.PlayOneShot(s.source.clip);
                    }
                }
                break;
            }
        }
    }
}
