/*
 * This Class has been written by Kumar Vivek Mitra on 16-5-2014
 * for the implementation of the Interface methods as per the console application
 * 
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using System.Threading;

namespace SignalRSelfHost
{
   public class ClassImpl : Hub,IChat
    {

        // -------------------------- Fields
        public static Dictionary<string, string> map = new Dictionary<string, string>();
        // -------------------------- Fields





        // This method handles the initial request from the clients and then assigns them on
        // individual threads to be handled.......... By Kumar Vivek Mitra on 16-5-2014
        public virtual void EntryPoint(string method_name = "", string name_From = "", string message = "", string name_To = "", string group_Name = "")
        {


            if (method_name == "RegisterClient")
            {
                Thread thread = new Thread(new ParameterizedThreadStart(RegisterClient));
                thread.Start(new string[] { name_From, Context.ConnectionId });

            }
            else if (method_name == "SendToClient")
            {
                Thread thread = new Thread(new ParameterizedThreadStart(SendToClient));
                thread.Start(new string[] { name_From, message, name_To });

            }
            else if (method_name == "DisconnectClient")
            {
                Thread thread = new Thread(new ParameterizedThreadStart(DisconnectClient));
                thread.Start(name_From);
            }
            else if (method_name == "FetchUserList")
            {
                Thread thread = new Thread(new ParameterizedThreadStart(UsersOnline));
                thread.Start(new string[] {name_From,Context.ConnectionId});
            }
            else if(method_name == "DownloadFile")
            {
                Thread thread = new Thread(new ParameterizedThreadStart(DownloadFile));
                thread.Start(new string[] { name_From, message, name_To ,group_Name });
            }
            else if(method_name == "JoinGroup")
            {
                Thread thread = new Thread(new ParameterizedThreadStart(JoinGroup));
                thread.Start(new string[] { Context.ConnectionId, group_Name });
            }
            else if (method_name == "DisconnectGroup")
            {

                Thread thread = new Thread(new ParameterizedThreadStart(DisconnectGroup));
                thread.Start(new string[] { Context.ConnectionId, group_Name });
            }
            else if (method_name == "GroupMessage")
            {
                Thread thread = new Thread(new ParameterizedThreadStart(GroupMessage));
                thread.Start(new string[] { name_From, message, group_Name });
            }

        }


        // This method handles registeration of clients .......... By Kumar Vivek Mitra on 16-5-2014
        public void RegisterClient(object client_Obj)
        {
            string[] arr = (string[])client_Obj;

            try
            {
                
                map.Add((string)arr[0], (string)arr[1]);
                System.Console.WriteLine(arr[0] + " is Connected");
            }
            catch (Exception) 
            {
                Clients.Client(arr[1]).userExists(arr[0]);
                System.Console.WriteLine("User " + arr[0] + " already exists");
            }
            
        }


        // This method handles message communication of clients .......... By Kumar Vivek Mitra on 16-5-2014
        public void SendToClient(object client_Obj)
        {
            try
            {
                string[] arr = (string[])client_Obj;
                string name_From = arr[0];
                System.Console.WriteLine("Message from " + Context.ConnectionId + "---" + arr[1]);
                Clients.Client(map[arr[2]]).receiveMessage(name_From, arr[1]);
            }
            catch (Exception) { System.Console.WriteLine("Client don't exist"); }
        }


        // This method handles disconnection of clients .......... By Kumar Vivek Mitra on 16-5-2014
        public void DisconnectClient(object user_Name)
        {
            map.Remove((string)user_Name);
            System.Console.WriteLine(user_Name+" disconnected");
            
        }


        // This method handles sending of users online to the client .......... By Kumar Vivek Mitra on 16-5-2014
        public void UsersOnline(object user_List)
        {
            List<String> userList = new List<string>();
            string[] arr          = (string[])user_List;

            foreach (string key in map.Keys)
            {
               
                userList.Add(key);
            }

              Clients.All.fetchUserList(userList);
            
            
        }


        // This method handles invoking the code in receiver;s end to start downloading of file .......... By Kumar Vivek Mitra on 29-5-2014
        public void DownloadFile(object client_Obj)
        {
            try
            {
                string[] arr = (string[])client_Obj;
                string name_From = arr[0];
                System.Console.WriteLine("Download from " + Context.ConnectionId + "---" + arr[1]);
                if (arr[3] == "")
                {
                    Clients.Client(map[arr[2]]).downloadFile(name_From, arr[1]);
                }
                else
                {
                    Clients.OthersInGroup(arr[3]).downloadFile(name_From, arr[1]);
                }
                
            }
            catch (Exception) { System.Console.WriteLine("Client don't exist"); }
        }



       //This method handles the creation of a group and adding user to it.......... By Kumar Vivek Mitra on 2-6-2014
        public void JoinGroup(object group_Info)
        {
            string[] arr = (string[])group_Info;
            Groups.Add(arr[0], arr[1]);
            System.Console.WriteLine(arr[0] + " joined the group " + arr[1]);
        }



       // This method handles the disconnection of the user from the group.......... By Kumar Vivek Mitra on 2-6-2014
        public void DisconnectGroup(object group_Info)
        {
            string[] arr = (string[])group_Info;
            Groups.Remove(arr[0], arr[1]);
            System.Console.WriteLine(arr[0] + " disconnected from the group " + arr[1]);
        }



       // This method handles message from the user to the group.......... By Kumar Vivek Mitra on 2-6-2014
        public void GroupMessage(object group_Info)
        {
            string[] arr = (string[])group_Info;
            string name_From = arr[0];
            System.Console.WriteLine("Message from " + Context.ConnectionId + "---" + arr[1]);
            Clients.OthersInGroup(arr[2]).receiveMessage(arr[0], arr[1]);
        }
    }
}
