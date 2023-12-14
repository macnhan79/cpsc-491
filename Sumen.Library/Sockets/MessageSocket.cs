namespace PhoHa7.Library.Sockets
{
    public class MessageSocket
    {
        int index;

        public int Index
        {
            get { return index; }
            set { index = value; }
        }
        string message;
        string response;
        bool hasError = false;
        public string Response
        {
            get { return response; }
            set { response = value; }
        }
        

        public bool HasError
        {
            get { return hasError; }
            set { hasError = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

    }
}
