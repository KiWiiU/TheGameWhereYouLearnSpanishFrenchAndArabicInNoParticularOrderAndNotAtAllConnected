using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Diagnostics;


public class PlayerCosmetics : MonoBehaviour
{
   
    private Animator animator = null;
    private string pantsA;
    private string shoesA;
   // private GameObject goshoe;
    Vector3 currentPos;
    
   
    void Start()
    {
        SetAllPlayerCosmetics();
        SetPlayerColors();
        animator = GetComponent<Animator>();

        //makes sure that when the scene starts that the animator can immedietly put 
        //the animations in the idle stage of the correct peice of clothing
        initialPants();
        initialShoes();
    }



    void Update(){
        
        DirectionCosmetics();
    }

    public void SetAllPlayerCosmetics() {
        for(int i = 0; i < Enum.GetNames(typeof(CosmeticItem.CosmeticType)).Length;i++) {
            if(Holder.currentCosmetics[i] != null) {
              
                transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = Holder.currentCosmetics[i].Front;
                // print(transform.GetChild(i).GetComponent<SpriteRenderer>().sprite);
                
                
            }
        }
    }

    public void SetPlayerColors() {
        // Indexes of children need to be correct (head, face, accessory, pants, shirt, shoes)
        transform.GetComponent<SpriteRenderer>().color = Holder.skinColor;
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = Holder.currentCosmeticColors[0];
        transform.GetChild(1).GetComponent<SpriteRenderer>().color = Holder.currentCosmeticColors[1];
        transform.GetChild(2).GetComponent<SpriteRenderer>().color = Holder.currentCosmeticColors[2];
        transform.GetChild(3).GetComponent<SpriteRenderer>().color = Holder.currentCosmeticColors[3];
        transform.GetChild(4).GetComponent<SpriteRenderer>().color = Holder.currentCosmeticColors[4];
        transform.GetChild(5).GetComponent<SpriteRenderer>().color = Holder.currentCosmeticColors[5];
    }

    public void SetPlayerCosmetic(int type) {
        if(Holder.currentCosmetics[type] != null) {
                transform.GetChild(type).GetComponent<SpriteRenderer>().sprite = Holder.currentCosmetics[type].Front;
        }
    }

    public void SetAllPlayerBackCosmetics() {
        for(int i = 0; i < Enum.GetNames(typeof(CosmeticItem.CosmeticType)).Length;i++) {
            if(Holder.currentCosmetics[i] != null) {
                transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = Holder.currentCosmetics[i].Back;
            }
        }
    }

    public void SetAllPlayerAnimationCosmetics() {
      AnimationClip names;
        for(int i = 0; i < Enum.GetNames(typeof(CosmeticItem.CosmeticType)).Length;i++) {
            if(Holder.currentCosmetics[i] != null) {
                //checking to see if there is animation 
                if(Holder.currentCosmetics[i].Animation != null){
                    names = Holder.currentCosmetics[i].Animation;
                    // print(names.name);

                       //checking the shoes and setting the animation into motion
                    if (names.name == "sneakers"){
                        animator.SetFloat("sneakers", 1f);

                    } else if(names.name == "boots"){
                        animator.SetFloat("boots", 1f);
                        
                    } else if(names.name == "heels"){
                        animator.SetFloat("heels", 1f);

                    } else if(names.name == "sandals"){
                        animator.SetFloat("sandals", 1f);

                    }

                    //checking the pants 
                    //setting the pants animation into moving
                    if (names.name == "jeans"){
                        animator.SetFloat("jeans", 1f);

                    }else if(names.name == "sweatpants"){
                        // print("Sweat working");
                        animator.SetFloat("sweatpants", 1f);
                        // print(animator.GetFloat("sweatpants"));

                    }else if(names.name == "backSkirt"){
                        animator.SetFloat("skirt", 1f);  

                    }else if(names.name == "Shorts"){
                        animator.SetFloat("shorts", 1f);
                    }
                   


                }
               
            }
        }
    }

    //stuff
    public void initialPants(){
        AnimationClip names;
        for(int i = 0; i < Enum.GetNames(typeof(CosmeticItem.CosmeticType)).Length;i++) {
            if(Holder.currentCosmetics[i] != null) {
                if(Holder.currentCosmetics[i].Animation != null){
                    names = Holder.currentCosmetics[i].Animation;
                    if (names.name == "jeans"){
                        //Dum is a variable that chooses which kind of pants they are wearing
                        //so that the animator can play the right animation
                        animator.SetFloat("Dum", 0f);
                        pantsA = names.name;

                    }else if(names.name == "sweatpants"){
                        
                        // print("Working?????");
                        animator.SetFloat("Dum", 1f);
                        pantsA = names.name;
                        // print("working working ??" + pantsA);

                    }else if(names.name == "backSkirt"){
                        animator.SetFloat("Dum", 2f);                       
                        pantsA = names.name;

                    }else if(names.name == "Shorts"){
                        animator.SetFloat("Dum", 3f);
                        pantsA = names.name;
                    }
                }
            }
        }
    }

    public void initialShoes(){
        AnimationClip names;
        for(int i = 0; i < Enum.GetNames(typeof(CosmeticItem.CosmeticType)).Length;i++) {
            if(Holder.currentCosmetics[i] != null) {
                if(Holder.currentCosmetics[i].Animation != null){
                    names = Holder.currentCosmetics[i].Animation;
                    // print(names.name);
                
                     if (names.name == "sneakers"){
                        //um is the variable for shoes that set the kind of shoes
                        //so that the animator can play the right shoe animation
                        animator.SetFloat("UM", 0f);
                        shoesA = names.name;

                    } else if(names.name == "boots"){
                        animator.SetFloat("UM", 1f);
                        shoesA = names.name;
                        
                    } else if(names.name == "heels"){
                        animator.SetFloat("UM", 2f);
                        shoesA = names.name;
                        
                    } else if(names.name == "sandals"){
                        animator.SetFloat("UM", 3f);
                        shoesA = names.name;
                    }


                }
            }
        }
    }

    //setting the animation for either movement or idle if up arrow is pressed or let go
    public void DirectionCosmetics()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.speed = 1;
            SetAllPlayerBackCosmetics();
            //xVelocity is for the base animation. might actually do somethiing.
            animator.SetFloat("xVelocity", 1f);
            //makes the animations go
            SetAllPlayerAnimationCosmetics();

        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            //idle base person
            animator.SetFloat("xVelocity", 0f);

            if (shoesA != null)
            {
                //setting the animation to the idle stages
                if (shoesA == "sneakers")
                {
                    animator.SetFloat("sneakers", 0f);

                }
                else if (shoesA == "boots")
                {
                    animator.SetFloat("boots", 0f);

                }
                else if (shoesA == "heels")
                {
                    animator.SetFloat("heels", 0f);

                }
                else if (shoesA == "sandals")
                {
                    animator.SetFloat("sandals", 0f);

                }
            }
            if (pantsA != null)
            {
                if (pantsA == "jeans")
                {
                    //print("CHOERENT WORDS");
                    animator.SetFloat("jeans", 0f);

                }
                else if (pantsA == "sweatpants")
                {
                    animator.SetFloat("sweatpants", 0f);

                }
                else if (pantsA == "backSkirt")
                {
                    animator.SetFloat("skirt", 0f);

                }
                else if (pantsA == "Shorts")
                {
                    animator.SetFloat("shorts", 0f);
                }
            }
            //prob does something, don't feel like thinking about it rn
            SetAllPlayerCosmetics();
        }

    }
}
