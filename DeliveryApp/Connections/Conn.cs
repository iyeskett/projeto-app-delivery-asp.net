
namespace DeliveryApp.Connections
{
    public class Conn
    {

        static private string servidor = "localhost";
        static private string bancoDeDados = "deliveryapp";
        static private string usuario = "root";
        static private string senha = "root";

        static public string strConn = $"SERVER={servidor};PORT=3307;USER={usuario};PASSWORD={senha};DATABASE={bancoDeDados};";
    }
}
