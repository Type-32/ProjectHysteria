var sound : AudioClip[] = new AudioClip[5];
 function Start(){
     InvokeRepeating("PlayClipAndChange",0.01f,18.0f);
 }
 
 function PlayClipAndChange(){
     GetComponent.<AudioSource>().clip = sound[Random.Range(0, 5)];
     GetComponent.<AudioSource>().Play();
 }