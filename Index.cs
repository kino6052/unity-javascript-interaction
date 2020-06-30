using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
public class Index : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void OnMessage(string str);
    private string ID;
    public Dictionary<string, GameObject> map = new Dictionary<string, GameObject>();       
    // Start is called before the first frame update
    void Start()
    {
        ID = GenerateID();
        ReceivePositions(GenerateID(), new float[3]{-1.0f, -2.0f, 0.0f});
        ReceivePositions(GenerateID(), new float[3]{1.0f, -2.0f, 0.0f});
        ReceivePositions(GenerateID(), new float[3]{-1.0f, 2.0f, 0.0f});
    }

    public string GenerateID() {
        string result = Random.Range(1,255) + "" + Random.Range(1,255) + "" + Random.Range(1,255);
        return result;
    }

    // Update is called once per frame
    void Update()
    {
        //get the Input from Horizontal axis
        float horizontalInput = Input.GetAxis("Horizontal");
        //get the Input from Vertical axis
        float verticalInput = Input.GetAxis("Vertical");

        //update the position
        transform.position = transform.position + new Vector3(horizontalInput * 5 * Time.deltaTime, verticalInput * 5 * Time.deltaTime, 0);
    }    
    void Log() {
        // Application.ExternalCall( "SayHello", "The game says hello!" );
        Debug.Log("It Worked!");
    }
    string GetCurrentPosition() {
        string data = "" +
            ID + "," + 
            transform.position.x + "," +
            transform.position.y + "," +
            transform.position.z + ",";
        OnMessage(data);
        return data;
    }
    void _ReceivePositions(string args) {
        string[] argsArray = args.Split(',');
        string id = argsArray[0];
        float x = (float) System.Convert.ToSingle(argsArray[1]);
        float y = (float) System.Convert.ToSingle(argsArray[2]);
        float z = (float) System.Convert.ToSingle(argsArray[3]);
        ReceivePositions(id, new float[3]{x, y, z});
    }
    void ReceivePositions(string id, float[] position) {
        Debug.Log("Here, " + id + ", " + position);
        try {
            if (id == ID) { // That's us, but we only need other's data
                return;
            }
            if (!map.ContainsKey(id)) { // Couldn't find the player
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                map.Add(id, obj);
            }
            GameObject result = map[id];
            Vector3 vPosition = new Vector3(position[0], position[1], position[2]);
            result.transform.position = vPosition;
        } catch (System.Exception e) {
            Debug.Log("Exception in ReceivePositions: " + e.ToString());
        }     
    }
}
