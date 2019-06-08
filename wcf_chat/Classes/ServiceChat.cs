using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using wcf_chat.Context;
using wcf_chat.Repository;
namespace wcf_chat
{
  
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        List<ServerUser> users = new List<ServerUser>();
        
        
        public void Connect(string name)
        {
           
            ServerUser user1 = users.FirstOrDefault(i => i.LoginName == name);
          
                ServerUser user = new ServerUser()
                {
                    LoginName = name,
                    operationContext = OperationContext.Current
                };
                users.Add(user);
            }
            
            
        

        public void Disconnect(string LoginId)
        {
          
            var user = users.FirstOrDefault(i => i.LoginName == LoginId);
            if (user != null)
            {
                users.Remove(user);
            }
        }
        public ChatRepository<Users> dbUser = new ChatRepository<Users>(new ChatContext());
        public string Registration(string name, string surname, string login, string password,string phone)
        {
            bool match_phone = Regex.IsMatch(phone, "(\\+)[- _]*\\(?[- _]*(\\d{3}[- _]*\\)?([- _]*\\d){9}|\\d\\d[- _]*\\d\\d[- _]*\\)?([- _]*\\d){7})");
            if(string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname)|| string.IsNullOrWhiteSpace(login))
            {
                return "SHORTDATA";
            }
            if(!match_phone)
            {
                return "BADPHONE";
            }
           
                    foreach (var user in dbUser.Get())
                    {
                        if (user.IdLogin.Equals(login))
                            return "TRUE1";
                        if (user.Phone.Equals(phone))
                            return "TRUE2";
                    }

                    Users userAdd = new Users() { Name = name, Surname = surname, IdLogin = login, Password = password, Phone = phone };
                    dbUser.Add(userAdd);
                   
                    return "REGISTER";
                }

        public string Authorization(string login, string password)
        {
            try
            {
                    foreach (var u in dbUser.Get())
                    {
                        if (u.IdLogin.Equals(login))
                        {
                            if (u.Password.Equals(password))
                            {
                                return "AUTHORIZATION";
                            }
                        }

                    }
                return "INCORRECT";
            }
            catch (Exception ex)
            {
                return "CANCEL";
            }
        }

        public Users GetUser(string login)
        {
                return dbUser.FindByLogin(login);
                
        }

        public List<Users> GetUsers(string searchArg)
        {
            return dbUser.FindAllByLogin(searchArg).ToList();
        }
        public ChatRepository<Contacts> dbContacts = new ChatRepository<Contacts>(new ChatContext());
        public List<Contacts> GetContacts(string login)
        {
            
                return dbContacts.FindAllByLoginCont(login).ToList();
        }

        public string ChangeInformationSetting(string LoginUser, string NewName, 
                                                string NewSurname, string OldPassword, 
                                                string NewPassword)
        {
            if (string.IsNullOrWhiteSpace(NewName)|| string.IsNullOrWhiteSpace(NewSurname))
            {
                return "SHORTDATA";
            }
                foreach (var user in dbUser.Get())
                {
                    if (user.IdLogin.Equals(LoginUser))
                    {
                        if (user.Password.Equals(OldPassword))
                        {
                            user.Name = NewName;
                            user.Surname = NewSurname;
                            user.Password = NewPassword;
                        }
                        else
                        {
                            return "BADOLDPASSWORD";
                        }
                    }

                }
                dbUser.Save();
                return "TRUE";
            
        }
        public string AddContacts(string LoginUser, string LoginUserContact)
        {
            try
            {
                using (ChatContext db = new ChatContext())
                {
                    if (Enumerable.Any(db.contacts, c => (c.LoginUser == LoginUser && c.LoginUserContact == LoginUserContact) || 
                    (c.LoginUser == LoginUserContact && c.LoginUserContact == LoginUser)))
                    {
                        return "TRUE1";
                    }
                    dbContacts.Add(new Contacts() { LoginUser = LoginUser, LoginUserContact = LoginUserContact });
                    dbContacts.Add(new Contacts() { LoginUser = LoginUserContact, LoginUserContact = LoginUser });
                    
                    return "TRUE";
                }
            }
            catch (Exception ex)
            {
                return "FALSE";
            }
        }

        public void RemoveContacts(string LoginUser, string LoginUserContact)
        {
            throw new NotImplementedException();
        }


        public ChatRepository<Conversation> dbConversation = new ChatRepository<Conversation>(new ChatContext());
        public void CreateConversation(string loginFirst, string loginSecond)
        {
            dbConversation.AddConversation(loginFirst, loginSecond);
        }
        public ChatRepository<ConversationReply> dbConversationReply = new ChatRepository<ConversationReply>(new ChatContext());
        public void SendMsg(string msg, string LoginUserFirst, string LoginUserSecond)
        {
                var q = dbConversation.GetConv(LoginUserFirst, LoginUserSecond);
                foreach (var query in q)
                {
                    dbConversationReply.Add(new ConversationReply() { Message = msg, UserLoginFk = LoginUserFirst, date = DateTime.Now, ConvId = query.ConvId });
                }
                foreach (var user in users)
                {
                    if (user.LoginName == LoginUserFirst || user.LoginName == LoginUserSecond)
                    {
                        string message = DateTime.Now + ": " + LoginUserFirst + ": " + msg;
                        user.operationContext.GetCallbackChannel<IServerChatCallback>().MsgCallback(message);
                    }
                }
        }
       


        public List<string> GetAllMessage(string LoginUserFirst, string LoginUserSecond)
        {
            List<string> message = new List<string>();
            
                var q = from p in dbConversation.Get()
                        where (p.UserOne == LoginUserFirst && p.UserTwo == LoginUserSecond) || (p.UserOne == LoginUserSecond && p.UserTwo == LoginUserFirst)
                        select p;
                var result = from p in dbConversationReply.Get()
                                where p.ConvId == q.FirstOrDefault().ConvId
                                select p;
                foreach (var query in result)
                {
                    
                    message.Add(query.date + ": " + query.UserLoginFk + ": " + query.Message);
                }
            return message;
        }
    }
}
