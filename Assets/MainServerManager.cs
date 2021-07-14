using UnityEngine;
using System.IO;
using MQTTnet.Client;
using MQTTnet;
using MQTTnet.Client.Options;
using System.Threading.Tasks;
using System.Text;
using System.Threading;
using System;
using System.Collections.Generic;
using Assets;

public class MainServerManager : MonoBehaviour
{
  static  IMqttClient mqttClient;
    TextMesh debText;

   static ServerConfig serverConfig ;
  

    public void FuncMQTT()
    {


        var factory = new MqttFactory();
        mqttClient = factory.CreateMqttClient();


        string JSONDeviceConfig = " {\"CommandId\" : 1 , \"Data\" : [\"" + serverConfig.deviceName + "\",\"11\"] } ";

        var options = new MqttClientOptionsBuilder()

             .WithTcpServer(serverConfig.mqttHostName, serverConfig.mqttPort)
             .WithCredentials(serverConfig.mqttUsername, serverConfig.mqttPassword)

       .Build();


        mqttClient.ConnectAsync(options);

        Thread.Sleep(1000);
        mqttClient.SubscribeAsync(serverConfig.placementName + "/" + serverConfig.serverName + "/" + serverConfig.outputTopicName);
        mqttClient.PublishAsync(serverConfig.placementName + "/" + serverConfig.serverName + "/" + serverConfig.inputTopicName, JSONDeviceConfig);
        mqttClient.SubscribeAsync(serverConfig.placementName + "/" + serverConfig.deviceName + "/" + serverConfig.inputTopicName);


        mqttClient.UseApplicationMessageReceivedHandler(MqttMessGet);


  


    }
        


    public void MqttMessGet(MqttApplicationMessageReceivedEventArgs e)
    {

        if (e.ApplicationMessage.Topic == serverConfig.placementName +"/" + serverConfig.deviceName + "/" + serverConfig.inputTopicName)
        {
           
            string json = (Encoding.UTF8.GetString(e.ApplicationMessage.Payload));
        

            ServerCommand s = JsonUtility.FromJson<ServerCommand>(json);

            Text = DateTime.Now + "  :  " + json;



            if (s.CommandId == 1)
            {
                
                SendMQTTMess(1,0);

            }
          
          
            if (s.CommandId == 3)
            {
                try
                {
                    SendMQTTMess(json);
                    Phase =  (TronPhaseStatus)  s.SubcommandId ;
                

                }
                catch (Exception)
                {

                 
                }
            

            }
            if (s.CommandId == 12)
            {

                SendMQTTMess(12, ((int)Phase));

            }
            if (s.CommandId == 5)
            {

                Phase = TronPhaseStatus.DEACTIVATE_DEVICE;

            }

        }
       
     }

    public string Text=":";
    private void Update()
    {
   

        if (Phase != lastPhase)
        {
            try
            {
                Destroy(lastPhaseGo);
            }
            catch (Exception)
            {

                print("Nothing  to  Destory");
            }
            lastPhase = Phase;

            try
            {
                lastPhaseGo = Instantiate(Phases[Phase], Vector3.zero, this.transform.rotation);
                lastPhaseGo.name = Phase.ToString();
            }
            catch (Exception)
            {

                print("Cant make new Prefab");
            }

            if (Phase == TronPhaseStatus.DEACTIVATE_DEVICE)
            {

                System.Diagnostics.Process.Start("cmd", "/c shutdown -s -f -t 01");

            }
        }
        

    }

    
    
    public static void SendMQTTMess(int commandId, int subCommandIdm , string data)
    {
        string m = " {\"CommandId\" : "+commandId+" , \"SubcommandId\": "+subCommandIdm+",  \"Data\" : [\""+ data +"\"] } ";

        Task.Run(() => mqttClient.PublishAsync(serverConfig.placementName +"/" + serverConfig.deviceName +"/"+serverConfig.outputTopicName, m));
    }
    public static void SendMQTTMess(int commandId, int subCommandIdm)
    {
        string m = " {\"CommandId\" : " + commandId + " , \"SubcommandId\": " + subCommandIdm + ",  \"Data\" : [] } ";

        Task.Run(() => mqttClient.PublishAsync(serverConfig.placementName +"/" + serverConfig.deviceName + "/" + serverConfig.outputTopicName, m));
    }
    public static void SendMQTTMess(string json)
    {
       

        Task.Run(() => mqttClient.PublishAsync(serverConfig.placementName + "/" + serverConfig.deviceName + "/" + serverConfig.outputTopicName, json));
    }


    public GameObject PuzzlePhase;
    public GameObject WeaponLoadPhase;
    public GameObject WeaponUnloadPhase;
    public GameObject CodePhase;
    public GameObject VirusPhase;
    public GameObject VictoryPhase;
    public GameObject DefeatPhase;


    public Dictionary<TronPhaseStatus, GameObject> Phases = new Dictionary<TronPhaseStatus, GameObject>();
    void PhaseGen()
    {

        Phases.Add(TronPhaseStatus.TRON_GAME_PUZZLE_PHASE, PuzzlePhase);
        Phases.Add(TronPhaseStatus.TRON_GAME_WEAPON_LOAD_PHASE, WeaponLoadPhase);
        Phases.Add(TronPhaseStatus.TRON_GAME_WEAPON_UNLOAD_PHASE, WeaponUnloadPhase);
        Phases.Add(TronPhaseStatus.TRON_GAME_CODE_PHASE, CodePhase);
        Phases.Add(TronPhaseStatus.TRON_GAME_VIRUS_PHASE, VirusPhase);
        Phases.Add(TronPhaseStatus.TRON_GAME_VICTORY_PHASE, VictoryPhase);
        Phases.Add(TronPhaseStatus.TRON_GAME_DEFEAT_PHASE, DefeatPhase);
    }
    [Serializable]
    public class Command
    {
        public int CommandId;
        public int SubcommandId;
        public string[] Data;
    }
    [Serializable]
    public class ServerCommand
    {
        public int CommandId;
        public int SubcommandId;
        public string[] Data;
    }

    public TronPhaseStatus Phase;
    public TronPhaseStatus lastPhase;
    public GameObject lastPhaseGo;

    void Start()
    {
        PhaseGen();

        serverConfig = JsonUtility.FromJson<ServerConfig>(new StreamReader("ServerConfigFile.txt").ReadToEnd());
 
        debText = this.GetComponentInChildren<TextMesh>();

        Phase = TronPhaseStatus.TRON_GAME_DEFAULT_PHASE;
     
        FuncMQTT();

    }
    [Serializable]
   public  class ServerConfig
    {
        public string deviceName;
         public string mqttHostName;
        public int mqttPort;
        public string mqttUsername;
        public string mqttPassword;

        public string inputTopicName;
        public string outputTopicName;
        public string serverName;
        public string placementName;



    }


}
