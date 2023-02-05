using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    private AudioSource success;
    private AudioSource damage;
    private AudioSource ambient;
    private AudioSource crackingEarth;
    private AudioSource crackingGround;
    private bool hasPlayedSound;

    private void Awake() {
        var soundObjects = GetComponentsInChildren<Transform>()[0].gameObject.GetComponentsInChildren<Transform>();
        foreach (var item in soundObjects)
        {
            var currAudioSource = item.gameObject.GetComponent<AudioSource>();
            if (item.name.Equals("Success")) {
                success = currAudioSource;
            } else if (item.name.Equals("Damage")) {
                damage = currAudioSource;
            } else if(item.name.Equals("Ambient")) {
                ambient = currAudioSource;
            } else if(item.name.Equals("CrackingEarth")) {
                crackingEarth = currAudioSource;
            } else if(item.name.Equals("CrackingGround")) {
                crackingGround = currAudioSource;
            } else if (!item.name.Equals("GameManagers") && !item.name.Equals("Sounds")){
                Debug.LogError("(Petros) Sound Object doesn't have a recognisable name. Name: "+item.name);
            }
        }
    }

    void Start()
    {
        // TODO: if !gameManager.isPlayingGroundLevel
        crackingGround?.Play();
        ambient?.Play();
    }

    public void playSuccess() 
    {
        success.Play();
    }
    
    public void playDamage() 
    {
        damage.Play();
    }
    
    public void ToggleAmbient() 
    {
        if (ambient.isPlaying) {
            ambient.Stop();
        } else {
            ambient.Play();
        }
    }

    public void ToggleCracking() 
    {
        // TODO: if gameManager.isPlayingGroundLevel
        crackingGround.Stop();
        crackingEarth.Play();
        // if (crackingEarth.isPlaying) {
        // } else {
        //     crackingEarth.Play();
        // }
    }
}
