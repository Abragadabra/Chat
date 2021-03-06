using System.ServiceModel;

namespace Group_Chat
{
    class ServerUser
    {
        public int ID { get; set; } //поле для ID

        public string Name { get; set; } // поле для имени

        public OperationContext OperationContext { get; set; }
    }
}
