using System;

namespace SalesWebMVC.Services.Exceptions
{
    public class IntegrityException : ApplicationException
    {
        public IntegrityException(string message) : base(message) // construtor da classe IntegrityException que recebe uma string message como parâmetro. O construtor chama o construtor da classe base ApplicationException, passando a mensagem recebida como argumento. Isso permite que a mensagem de erro personalizada seja associada à exceção lançada, facilitando a identificação e o tratamento de erros relacionados à integridade dos dados em operações de banco de dados ou outras situações onde a integridade dos dados é importante.
        {
        }
    }
}
