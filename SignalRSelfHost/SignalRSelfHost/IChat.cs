/*
 * This Interface has been written by Kumar Vivek Mitra on 16-5-2014
 * to provide a skeleton for the Chat functionalities
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRSelfHost
{
    interface IChat
    {
        // Method declaration for EntryPoint of the Client to the Server.......... By Kumar Vivek Mitra on 16-5-2014
        void EntryPoint(string method_name = "", string name_From = "", string message = "", string name_To = "", string group_Name = "");

        // Method declaration for the Client Registeration on to the Server.......... By Kumar Vivek Mitra on 16-5-2014
        void RegisterClient(object client_Obj);

        // Method declaration for Sending message from one Client to another.......... By Kumar Vivek Mitra on 16-5-2014
        void SendToClient(object client_Obj);

        // Method declaration for Disconnecting the Client from the Server.......... By Kumar Vivek Mitra on 16-5-2014
        void DisconnectClient(object user_Name);

        // Method declaration for checking if the username provided by the Client already exists on Server or not.......... By Kumar Vivek Mitra on 16-5-2014
        void UsersOnline(object user_List);

        // Method declaration for informing the receiver that a media file has been uploaded and now its ready for downloading.......... By Kumar Vivek Mitra on 29-5-2014
        void DownloadFile(object client_Obj);
         
        // Method to create and add user to the group.......... By Kumar Vivek Mitra on 2-6-2104
        void JoinGroup(object group_Info);
         
        // Method to disconnect from the group.......... By Kumar Vivek Mitra on 2-6-2014
        void DisconnectGroup(object group_Info);

        // Method to send message to all the group members except the sender him/herself.......... By Kumar Vivek Mitra on 2-6-2014
        void GroupMessage(object group_Object);
    }
}
