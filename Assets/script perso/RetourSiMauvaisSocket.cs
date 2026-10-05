using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class RetourSiMauvaisSocket : MonoBehaviour
{
    [SerializeField]
    private XRSocketInteractor[] bonsSockets;

    private XRGrabInteractable grab;

    private Vector3 positionDepart;
    private Quaternion rotationDepart;


    void Start()
    {
        grab = GetComponent<XRGrabInteractable>();

        // Mémorise la position de départ
        positionDepart = transform.position;
        rotationDepart = transform.rotation;

        grab.selectExited.AddListener(QuandRelache);
    }


    void QuandRelache(SelectExitEventArgs args)
    {
        StartCoroutine(VerifierSocket());
    }


    IEnumerator VerifierSocket()
    {
        // Attend que le Socket ait le temps
        // de récupérer le cavalier
        yield return null;

        // Regarde qui sélectionne actuellement le cavalier
        foreach (var interactor in grab.interactorsSelecting)
        {
            // Vérifie tous les bons Sockets
            foreach (XRSocketInteractor socket in bonsSockets)
            {
                if (interactor == socket)
                {
                    // C'est un bon Socket
                    yield break;
                }
            }
        }

        // Aucun bon Socket trouvé :
        // retour à la position de départ
        transform.position = positionDepart;
        transform.rotation = rotationDepart;
    }
}