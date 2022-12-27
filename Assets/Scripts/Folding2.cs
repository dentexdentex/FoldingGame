using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Folding2 : MonoBehaviour
{
    private bool fingerDown;


    public bool canRot;
    public GameObject mainPivots;
    public GameObject allPivots;
    GameObject lastChild;
    void Start()
    {
        canRot = true;
        checkPivots(gameObject);

        getMainPivots();
    }
    void Update()
    {
        if (Input.GetKeyDown("a") && canRot)
        {
            rot();

            transform.SetParent(mainPivots.transform.GetChild(2));
            transform.parent.DORotate(new Vector3(0, 180, 0), 1).SetEase(Ease.Linear).OnComplete(() => Complited(lastChild));


        }
        if (Input.GetKeyDown("d") && canRot)
        {
            rot();

            transform.SetParent(mainPivots.transform.GetChild(3));
            transform.parent.DORotate(new Vector3(0, -180, 0), 1).SetEase(Ease.Linear).OnComplete(() => Complited(lastChild));


        }
        if (Input.GetKeyDown("s") && canRot)
        {
            rot();

            transform.SetParent(mainPivots.transform.GetChild(1));
            transform.parent.DORotate(new Vector3(-180, 0, 0), 1).SetEase(Ease.Linear).OnComplete(() => Complited(lastChild));


        }
        
    }

    public void MoveUp()
    {
        if (canRot)
        {
            //GameObject newChild = Instantiate(gameObject, transform.position, Quaternion.identity);
            //Instantiate(newGammeObject, parent);
            //transform.rotation = Quaternion.Lerp(transform.rotation,
            //    Quaternion.Euler(0, -90, 0), .1f);
            rot();

            transform.SetParent(mainPivots.transform.GetChild(0));
            transform.parent.DORotate(new Vector3(180, 0, 0), 1).SetEase(Ease.Linear).OnComplete(() => Complited(lastChild));

        }
    }

    void Complited(GameObject child)
    {
        transform.parent = null;
        child.transform.parent = transform;
        canRot = true;
        for (int i = 0; i < allPivots.transform.childCount; i++)
        {
            Destroy(allPivots.transform.GetChild(i).gameObject);
            
        }
        checkPivots(gameObject);

        getMainPivots();

    }






    void rot()
    {
        canRot = false;


        lastChild = Instantiate(gameObject, transform.position, transform.rotation);
        lastChild.GetComponent<Folding2>().enabled = false;


       

        for (int i = 0; i < mainPivots.transform.childCount; i++)
        {
            mainPivots.transform.GetChild(i).transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }







    void checkPivots(GameObject checkObj)
    {
        for (int i = 0; i < checkObj.transform.childCount; i++)
        {
            if(checkObj.transform.GetChild(i).gameObject.tag == "pivot")
            {
                GameObject newPivot = checkObj.transform.GetChild(i).gameObject;
                Instantiate(newPivot.gameObject, newPivot.transform.position, newPivot.transform.rotation, allPivots.transform);
            }
            else
            {
                checkPivots(checkObj.transform.GetChild(i).gameObject);
            }
        }
    }




    void getMainPivots()
    {
        for (int i = 0; i < allPivots.transform.childCount; i++)
        {
            allPivots.transform.GetChild(i).transform.rotation = Quaternion.Euler(Vector3.zero);


            GameObject mainUp = mainPivots.transform.GetChild(0).gameObject;

            GameObject lastUp = allPivots.transform.GetChild(i).GetChild(0).gameObject;

            GameObject mainDown = mainPivots.transform.GetChild(1).gameObject;

            GameObject lastDown = allPivots.transform.GetChild(i).GetChild(1).gameObject;



            if (mainUp.transform.position.y < lastUp.transform.position.y)
            {
                mainUp.transform.position = lastUp.transform.position;
            }


            if (mainDown.transform.position.y > lastDown.transform.position.y)
            {
                mainDown.transform.position = lastDown.transform.position;
            }




            GameObject mainLeft = mainPivots.transform.GetChild(2).gameObject;

            GameObject lastLeft = allPivots.transform.GetChild(i).GetChild(2).gameObject;

            GameObject mainRight = mainPivots.transform.GetChild(3).gameObject;

            GameObject lastRight = allPivots.transform.GetChild(i).GetChild(3).gameObject;


            if (mainLeft.transform.position.x > lastLeft.transform.position.x)
            {
                mainLeft.transform.position = lastLeft.transform.position;
            }

            if (mainRight.transform.position.x < lastRight.transform.position.x)
            {
                mainRight.transform.position = lastRight.transform.position;
            }
        }
    }
}
