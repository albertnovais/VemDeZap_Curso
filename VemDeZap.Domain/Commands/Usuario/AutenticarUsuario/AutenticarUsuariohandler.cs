using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System.Threading;
using System.Threading.Tasks;
using VemDeZap.Domain.Extencions;
using VemDeZap.Domain.Interfaces.Repositories;
using VemDeZap.Domain.Resources;

namespace VemDeZap.Domain.Commands.Usuario.AutenticarUsuario
{
    public class AutenticarUsuariohandler : Notifiable, IRequestHandler<AutenticarUsuarioResquest, AutenticarUsuarioResponse>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryUsuario _repositoryUsuario;

        public AutenticarUsuariohandler(IMediator mediator, IRepositoryUsuario repositoryUsuario)
        {
            _mediator = mediator;
            _repositoryUsuario = repositoryUsuario;
        }

        public async Task<AutenticarUsuarioResponse> Handle(AutenticarUsuarioResquest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));
                return null;
            }

            request.Senha = request.Senha.ConvertToMD5();

            Entities.Usuario usuario = _repositoryUsuario.ObterPor(x => x.Email == request.Email && x.Senha == request.Senha);

            if (usuario == null)
            {
                AddNotification("Usuario", MSG.X0_NAO_INFORMADO.ToFormat("Request"));
                return new AutenticarUsuarioResponse()
                {
                    Autenticado = false
                };
            }

            //Cria objeto de resposta
            var response = (AutenticarUsuarioResponse)usuario;

            ////Retorna o resultado
            return await Task.FromResult(response);
        }
    }
}

