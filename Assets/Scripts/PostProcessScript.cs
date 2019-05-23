using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessScript : MonoBehaviour {

    private PostProcessVolume m_Volume;
    private Vignette m_Vignette;

    public Rigidbody PlayerRigidBody;

    void Start()
    {
        m_Vignette = ScriptableObject.CreateInstance<Vignette>();
        m_Vignette.enabled.Override(true);
        m_Vignette.intensity.Override(1f);

        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);
    }

    void Update()
    {
        //m_Vignette.intensity.value = Mathf.Sin(Time.realtimeSinceStartup);
        //Debug.Log("player rigidbody velocity" + PlayerRigidBody.velocity.magnitude);
        if (PlayerRigidBody.velocity.magnitude > 1)
        {
            m_Vignette.intensity.value = PlayerRigidBody.velocity.magnitude/20f;
            if (m_Vignette.intensity.value > 0.8f)
            {
                m_Vignette.intensity.value = 0.8f;
            }
        }
        else
        {
            m_Vignette.intensity.value = 0f;
        }
    }

    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(m_Volume, true, true);
    }
}
