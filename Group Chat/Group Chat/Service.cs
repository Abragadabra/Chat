using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Group_Chat
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)] // чтобы все подключались к одному сервису
    public class Service : IService

    {
        List<ServerUser> users = new List<ServerUser>(); // список юзеров
        int NextID = 1;

        public int Connect(string name)
        {
            ServerUser user = new ServerUser() {
                ID = NextID,                                //в общем здесь мы генериуем ID юзера придаем ему имя
                Name = name,
                OperationContext = OperationContext.Current
            };
            NextID++;

            SendMessage(" | " + user.Name + " присоединился к чату", 0);

            users.Add(user); // добавляем юзера в список
            return user.ID;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id); // ищем в списке нужного юзера

            if (user != null)
            {
                users.Remove(user); // удаляем юзера если он найден
                SendMessage(" | " + user.Name + " покинул чат", 0); //ноль нужен, для того, чтобы нам не приходило сообщение о том что я подключился или отключился
            }
        }

        public void DoWork()
        {
        }

        public void SendMessage(string Message, int id)
        {
            foreach (var item in users)
            {
                string Answer = DateTime.Now.ToLongTimeString();

                var user = users.FirstOrDefault(i => i.ID == id); // ищем в списке нужного юзера

                if (user != null)
                {
                    Answer += ": " + user.Name + " - "; //ответ
                }

                Answer += Message; 

                item.OperationContext.GetCallbackChannel<ServerCallBack>().MessageCallBack(Answer); // отправляем сообщение
            }
        }
    }
}
