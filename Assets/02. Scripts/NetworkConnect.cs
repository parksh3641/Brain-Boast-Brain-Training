using UnityEngine;

public class NetworkConnect : MonoBehaviour
{
    public static NetworkConnect instance;

    // public static bool isConnect = false;
    public bool isConnect = false;

    void Awake()
    {
        instance = this;
    }

    public bool CheckConnectInternet()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            // ���ͳ� ������ �ȵǾ�����
            isConnect = false;
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            // �����ͷ� ���ͳ� ������ �Ǿ�����
            isConnect = true;
        }
        else
        {
            // �������̷� ������ �Ǿ�����
            isConnect = true;
        }
        return isConnect;
    }
}