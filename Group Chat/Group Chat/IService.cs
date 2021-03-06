using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Group_Chat
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService" в коде и файле конфигурации.
    [ServiceContract(CallbackContract = typeof(ServerCallBack))]
    public interface IService
    {
        [OperationContract]
        int Connect(string name); // подключение к серверу при нажатии на кнопку 'подключиться'

        [OperationContract]
        void Disconnect(int id); // отключение от сервера при нажатии на крестик или на кнопку 'отключиться' 

        [OperationContract(IsOneWay = true)]
        void SendMessage(string Message, int id); // метод для отправки сообщений на сервер
    }
    //делаем так, чтобы сервер отвечал на подключение и отключение юзера: 
    public interface ServerCallBack {
        
        [OperationContract(IsOneWay = true)]
        void MessageCallBack(string Message); 

    }
}
