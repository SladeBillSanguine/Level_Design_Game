using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRätsel : MonoBehaviour
{
    [SerializeField] GameObject glühBirne1;
    [SerializeField] GameObject glühBirne2;
    [SerializeField] GameObject glühBirne3;
    [SerializeField] Animator heavyDoorOpen;
    
    MeshRenderer MeshRendererBirne1;
    MeshRenderer MeshRendererBirne2;
    MeshRenderer MeshRendererBirne3;
    Color redColor;
    Color greenColor;
    
    void Start()
    {
        MeshRendererBirne1 = glühBirne1.GetComponent<MeshRenderer>();
        MeshRendererBirne2 = glühBirne2.GetComponent<MeshRenderer>();
        MeshRendererBirne3 = glühBirne3.GetComponent<MeshRenderer>();
        redColor = Color.red;
        greenColor = Color.green;
        MeshRendererBirne1.material.color = greenColor;
        MeshRendererBirne2.material.color = greenColor;
        MeshRendererBirne3.material.color = redColor;
    }

    void Update()
    {
        if(MeshRendererBirne1.material.color == Color.green && MeshRendererBirne2.material.color == Color.green && MeshRendererBirne3.material.color == Color.green)
        {
            heavyDoorOpen.Play("HeavyDoorOpen");
            WaitForDoorAnimation();
            heavyDoorOpen.StopPlayback();
        }
    }

    public void ChangeAllColors()
    {
        if(MeshRendererBirne1.material.color == redColor)
        {
            MeshRendererBirne1.material.color = greenColor;
        }
        else
        {
            MeshRendererBirne1.material.color = redColor;
        }
        if(MeshRendererBirne2.material.color == redColor)
        {
            MeshRendererBirne2.material.color = greenColor;
        }
        else
        {
            MeshRendererBirne2.material.color = redColor;
        }
        if(MeshRendererBirne3.material.color == redColor)
        {
            MeshRendererBirne3.material.color = greenColor;
        }
        else
        {
            MeshRendererBirne3.material.color = redColor;
        }
    }

    public void ChangeColorLight2()
    {
        if(MeshRendererBirne2.material.color == redColor)
        {
            MeshRendererBirne2.material.color = greenColor;
        }
        else
        {
            MeshRendererBirne2.material.color = redColor;
        }
    }

    public void ChangeColorLight2And3()
    {
        if(MeshRendererBirne2.material.color == redColor)
        {
            MeshRendererBirne2.material.color = greenColor;
        }
        else
        {
            MeshRendererBirne2.material.color = redColor;
        }
        if(MeshRendererBirne3.material.color == redColor)
        {
            MeshRendererBirne3.material.color = greenColor;
        }
        else
        {
            MeshRendererBirne3.material.color = redColor;
        }
    }

    IEnumerator WaitForDoorAnimation()
    {
        yield return new WaitForSeconds(3);
    }
}
