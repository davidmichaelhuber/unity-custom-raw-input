using UnityEngine;
using System.Text;
using System.IO.Pipes;
using System.Threading;

public class PipeReceiver : MonoBehaviour
{
    NamedPipeClientStream rawMouseDataPipe;
    string pipeMessage;
    byte[] buffer;
    Thread readThread;
    int messagesCount;

    // Use this for initialization
    void Start()
    {
        messagesCount = 0;
        rawMouseDataPipe = new NamedPipeClientStream("RawMouseDataPipe");
        pipeMessage = "Not connected";
        buffer = new byte[1024];

        // TO-DO: PipeTransmissionMode.Byte?
        rawMouseDataPipe.ReadMode = PipeTransmissionMode.Message;
        readThread = new Thread(new ThreadStart(ReadData));
    }

    // Update is called once per frame
    void Update()
    {
        if (IsConnected())
        {
            if (!readThread.IsAlive)
            {
                readThread = new Thread(new ThreadStart(ReadData));
                readThread.Start();
            }
        }
        else
        {
            if (!IsInvoking("Connect"))
            {
                Invoke("Connect", 1f);
            }
        }
    }

    private void Connect()
    {
        rawMouseDataPipe.Connect(1);
        if (IsConnected())
        {
            pipeMessage = "Pipe connected";
        }
    }

    public void StopPipe()
    {
        rawMouseDataPipe.Close();
        pipeMessage = "Pipe stopped";
    }

    private void ReadData()
    {
        while (IsConnected())
        {
            int bytesRead = rawMouseDataPipe.Read(buffer, 0, 1024);
            if (bytesRead > 0)
            {
                pipeMessage = Encoding.ASCII.GetString(buffer).Trim();
                messagesCount++;
            }
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), pipeMessage + " " + messagesCount);
    }

    void OnApplicationQuit()
    {
        rawMouseDataPipe.Close();
    }

    public bool IsConnected()
    {
        return rawMouseDataPipe.IsConnected;
    }
}