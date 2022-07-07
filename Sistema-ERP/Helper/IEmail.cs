namespace Sistema_ERP.Helper
{
    public interface IEmail
    {
        bool Enviar(string email, string assunto, string msg);
    }
}
