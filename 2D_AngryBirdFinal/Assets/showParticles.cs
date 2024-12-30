using System.Collections;
using UnityEngine;

public class showParticles : MonoBehaviour
{
    public ParticleSystem ps { get; private set; }

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void PlayEffect()
    {
        StartCoroutine(ShowEffect());
    }
    IEnumerator ShowEffect()
    {
        yield return null;
        ps.Play();
        Destroy(gameObject, 1f);
    }

    public void ChangeEffect(ParticleSystem temp)
    {
        ps = temp;
    }
    
    
}
