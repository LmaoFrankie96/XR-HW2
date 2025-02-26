using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ActionBasedController))]
public class HandController : MonoBehaviour
{
    ActionBasedController controller;
    [SerializeField] private Hand hand;

    /*private void Awake()
    {
        if (hand == null)
            hand = GetComponentInChildren<Hand>();
    }*/

    private void Start()
    {

        controller = GetComponent<ActionBasedController>();
    }

    private void Update()
    {

        if (hand == null)
            hand = GetComponentInChildren<Hand>();
        else
        {
            //Debug.Log("Hand script attached");
            hand.SetGrip(controller.selectActionValue.action.ReadValue<float>());
            hand.SetTrigger(controller.activateActionValue.action.ReadValue<float>());

        }
    }
}
