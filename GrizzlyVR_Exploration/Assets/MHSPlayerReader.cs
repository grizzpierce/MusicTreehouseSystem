using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MHSPlayerReader : MonoBehaviour {

    List<GameObject> m_InsertedObjects = new List<GameObject>();

    bool m_IsTapeInserted = false;
    GameObject m_CurrentInsertedTape;




    public GameObject GetCurrentTape() { return m_CurrentInsertedTape; }

    // CHECKS IF ANY OBJECTS OUTSIDE THE ONE TAPE HAVE BEEN INSERTED
    public bool GetIfTapeInside() { return m_IsTapeInserted; }
 
    // CHECKS IF ANY OBJECTS OUTSIDE THE ONE TAPE HAVE BEEN INSERTED
    public bool GetIsGarbageInside()
    {
        if (m_InsertedObjects.Count != 0)
        {
            return true;
        }
        else
            return false;
    }



    void Update()
    {
        Debug.Log("Is Garbage Inside? " + GetIsGarbageInside());
    }


    void OnTriggerEnter(Collider col)
    {

        //CHECKS IF THE COLLIDED OBJECT IS AN MHS TAPE
        if (col.gameObject.tag == "MHS_TAPE")
        {
            //CHECKS IF THIS IS THE FIRST MHS TAPE
            if (m_IsTapeInserted == false)
            {
                Debug.Log("ITS A TAPE");
                Debug.Log("Track Name: " + col.gameObject.GetComponent<MHSTapeData>().GetTrackName());
                m_CurrentInsertedTape = col.gameObject;
                m_IsTapeInserted = true;
                Debug.Log("New tape inserted!");
            }

            //CHECKS IF THIS ISNT THE FIRST MHS TAPE
            else
            {
                //CHECKS IF THE MHS TAPE HAS ALREADY BEEN DETECTED
                if (m_InsertedObjects.Contains(col.gameObject) == false)
                {
                    Debug.Log("ITS A TAPE");
                    Debug.Log("There's already one inserted though!");
                    m_InsertedObjects.Add(col.gameObject);
                }
            }
        }

        //CHECKS IF THE COLLIDED OBJECT IS THE MHS PLAYER SHELL
        else if (col.gameObject.tag == "MHS_PLAYER_SHELL")
        {
            Debug.Log("ITS THE SHELL");
            Debug.Log("Item Name: " + col.gameObject.name);
        }

        //CHECKS IF THE COLLIDED OBJECT IS ANYTHING ELSE
        else
        {
            //CHECKS IF THE COLLIDED OBJECT HAS ALREADY BEEN DETECTED
            if (m_InsertedObjects.Contains(col.gameObject) == false)
            {
                Debug.Log("ITS GARBAGE");
                Debug.Log("Item Name: " + col.gameObject.name);
                m_InsertedObjects.Add(col.gameObject);
            }
        }
    }
    
    void OnTriggerExit(Collider col)
    {
        //CHECKS IF THE OBJECT LEAVING IS AN MHS TAPE
        if (col.gameObject.tag == "MHS_TAPE")
        {
            //CHECKS IF THE MHS TAPE IS THE CURRENTLY PLAYING ONE
            if (col.gameObject.name == m_CurrentInsertedTape.name)
            {
                m_CurrentInsertedTape = null;
                m_IsTapeInserted = false;
            }
        }

        //CHECKS IF THE COLLIDED OBJECT IS IN THE DETECTED ITEMS LIST
        if (m_InsertedObjects.Contains(col.gameObject))
        { 
            Debug.Log(col.gameObject.name + " has been removed");
            m_InsertedObjects.Remove(col.gameObject);
        }
    }
}
