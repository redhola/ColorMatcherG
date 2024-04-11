using UnityEngine;

public class Ring : MonoBehaviour
{
    public Material nextMaterial; 
    public GameObject objectToDestroy;

    public Material GetNextMaterial()
    {
        return nextMaterial;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            if (nextMaterial != null)
            {
                Renderer playerRenderer = other.GetComponent<Renderer>();
                if (playerRenderer != null)
                {
                    playerRenderer.material = nextMaterial;
                }

                if (objectToDestroy != null)
                {
                    Destroy(objectToDestroy);
                }
            }
        }
    }
}
