using System.ServiceModel;
namespace wcf_chat
{
    public class ServerUser
    {
        public string LoginName { get; set; }
        public OperationContext operationContext { get; set; }
    }
}
