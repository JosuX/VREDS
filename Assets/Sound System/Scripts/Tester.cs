using UnityEngine;

public class Tester : MonoBehaviour
{

    /// THIS IS ONLY A SAMPLE SCRIPT ON HOW TO REFERENCE AND CALL THE FUNCTIONS ///
    /// Ang kelangan lang na script para gumana ung sounds is yung SoundPlayer and SoundManager ///
    /// You can safely delete this script pag naintindihan nyo na kung paano paganahin ung sounds ///
    
    /// IMPORTANT : Dapat magkasama sa isang GameObject yung Soundplayer at SoundManager para gumana yung reference ///


    SoundPlayer sp;             // DECLARATION
    void Start()
    {
        sp = GetComponent<SoundPlayer>();   // SET REFERENCE
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) //// Pressing 1 on keyboard will play the Element 0 of sounds in soundmanager 
        {
            sp.PlaySound(0); // PLAY FUNCTION
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)) //// Pressing 2 on keyboard will play the Element 1 of sounds in soundmanager
        {
            sp.PlaySound(1); // PLAY FUNCTION
        }
    }
}
