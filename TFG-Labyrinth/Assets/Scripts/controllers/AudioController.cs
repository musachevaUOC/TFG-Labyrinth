using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public float effectsVolume = 1f;
    
    public static AudioController inst;

    public AudioSource audioSource;

    public AudioClip coin, hit_enemy, hit_env, key, shot, shot_enemy;
    public AudioClip music;


    public void Awake() //init singleton class
    {
        if (AudioController.inst == null)
        {
            AudioController.inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    

    public void playCoinPickup()
    {
        audioSource.PlayOneShot(coin, effectsVolume);
    }

    public void playhitEnemy()
    {
        audioSource.PlayOneShot(hit_enemy, effectsVolume*0.6f);
    }

    public void playHitEnv(Vector3 loc)
    {
        AudioSource.PlayClipAtPoint(hit_env, loc);
    }

    public void playKeyPickup()
    {
        audioSource.PlayOneShot(key, effectsVolume);
    }

    public void playshot(Vector3 loc)
    {
        AudioSource.PlayClipAtPoint(shot, loc, effectsVolume*0.3f);
    }

    public void playshotEnemy(Vector3 loc)
    {
        AudioSource.PlayClipAtPoint(shot_enemy, loc);
    }
}
