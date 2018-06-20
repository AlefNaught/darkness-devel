using UnityEngine;
using System.Collections;

public class soundLightCreator : MonoBehaviour {

    //private int lastSampleIndex = 0;

    System.Collections.Generic.Dictionary<AudioClip, float[]> samplesCache = null;

    // Use this for initialization
    void Start () {
        samplesCache = new System.Collections.Generic.Dictionary<AudioClip, float[]>();
    }
	
	// Update is called once per frame
	void Update () {
        AudioSource[] aSs = FindObjectsOfType<AudioSource>();
        foreach (AudioSource aS in aSs)
        {
            Light l = aS.GetComponent<Light>();
            if (l == null)
                l = aS.gameObject.AddComponent<Light>();
            l.type = LightType.Point;
            l.color = Color.white;
            l.range = aS.volume * 10;
            l.intensity = 1.0F;
            l.enabled = aS.isPlaying;

            float[] samples;
            if (samplesCache.ContainsKey(aS.clip))
            {
                samples = samplesCache[aS.clip];
            }
            else
            {
                samples = new float[aS.clip.samples];
                aS.clip.GetData(samples, 0);
                samplesCache.Add(aS.clip, samples);
            }

            /*
            float avg = 0.0F;
            for (int i = lastSampleIndex; i <= aS.timeSamples; i++)
            {
                avg += samples[i];
            }
            avg /= (aS.timeSamples - lastSampleIndex + 1);
            l.range = aS.volume * 10 * Mathf.Abs( avg );
            lastSampleIndex = aS.timeSamples;
            */

            float value = samples[aS.timeSamples];
            if (value < 0.0F)
                value = 0.0F;
            l.range = aS.volume * 5 * value;
        }
    }
}
