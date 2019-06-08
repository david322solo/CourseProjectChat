using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace wcf_chat
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IServiceChat" в коде и файле конфигурации.
    [ServiceContract(CallbackContract = typeof(IServerChatCallback))]
    public interface IServiceChat
    {
        [OperationContract]
        void Connect(string name);
        [OperationContract]
        void Disconnect(string LoginId);
        [OperationContract]
        string Registration(string name, string surname, string login, string password, string phone);
        [OperationContract]
        string Authorization(string login, string password);
        [OperationContract]
        Users GetUser(string login);
        [OperationContract]
        List<Users> GetUsers(string searchArg);
        [OperationContract]
        List<Contacts> GetContacts(string login);
        [OperationContract]
        string ChangeInformationSetting(string LoginUser, string NewName, string NewSurname, string OldPassword, string NewPassword);
        [OperationContract]
        string AddContacts(string LoginUser, string LoginUserContact);
        [OperationContract]
        void RemoveContacts(string LoginUser, string LoginUserContact);
        [OperationContract]
        List<string> GetAllMessage(string LoginUserFirst, string LoginUserSecond);
        [OperationContract]
        void CreateConversation(string loginFirst, string loginSecond);
        [OperationContract(IsOneWay = true)]
        void SendMsg(string msg, string LoginUserFirst, string LoginUserSecond);
        
    }

    public interface IServerChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void MsgCallback(string msg);
    }
}
