using UnityEngine;

public class AnimatorControllerSwitcher : MonoBehaviour
{
    public GameObject objectToControl;

    public Animator animator;
    public RuntimeAnimatorController ControllerA;
    public RuntimeAnimatorController ControllerB;
    public RuntimeAnimatorController ControllerC;

    public float Key1 = 1f;
    public float Key2 = 2f;
    public float Key3 = 3f;
    public float Key4 = 4f;
    public float Key5 = 5f;

    // Function to switch the Animator Controller
    public void SwitchAnimatorController()
    {
        animator.runtimeAnimatorController = ControllerA;
    }
}
