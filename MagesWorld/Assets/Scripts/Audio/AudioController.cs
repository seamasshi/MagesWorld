using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {
    public AudioSource[] audioSource;
    
 

   

    public void ChangeAudioTo(int index)
    {
        for (int i = 0; i < audioSource.Length; i++)
        {
            if (audioSource[i].isPlaying)
            {
                audioSource[i].Stop();
            }
        }     
        
            audioSource[index].Play();
     } 
}
