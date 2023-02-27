using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    //BPM de la musique
    public float songBpm;

    //Mesure pour les beats
    public float secPerBeat;

    //Position de la musique en cours
    public float songPosition;

    //Position de la musique en beat (IMPORTANT)
    public float songPositionInBeats;

    //Nombre de secondes depuis le début de la musique
    public float dspSongTime;

    //Se qui joue la musique
    public AudioSource musicSource;

    void Start()
    {
        musicSource = GetComponent<AudioSource>();

        //Calcul du nombre de seconde dans chaque beat
        secPerBeat = 60f / songBpm;

        //Record the time when the music starts (pas compris)
        dspSongTime = (float)AudioSettings.dspTime;

        musicSource.Play();
    }

    void Update()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);

        //Determine le nombre de beat depuis le début de la chanson
        songPositionInBeats = songPosition / secPerBeat;


        if (Input.GetKeyDown("space") && (songPositionInBeats< Mathf.Round(songPositionInBeats)-0.10f || songPositionInBeats < Mathf.Round(songPositionInBeats) + 0.10f))
        {
            Debug.Log("PAN");
            //Il me faut une condition pour dire que ça s'active qu'une seule fois
        } //Il faut un truc pour quand c'est pas en rythme
    }
}