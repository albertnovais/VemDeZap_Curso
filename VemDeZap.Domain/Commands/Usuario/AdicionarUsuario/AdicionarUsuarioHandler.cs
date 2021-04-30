using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;
using VemDeZap.Domain.Interfaces.Repositories;
using VemDeZap.Domain.Resources;

namespace VemDeZap.Domain.Commands.Usuario.AdicionarUsuario
{
    public class AdicionarUsuarioHandler : Notifiable, IRequestHandler<AdicionarUsuarioRequest, Response>
    {
        private readonly IRepositoryUsuario _repositoryUsuario;
        private readonly IMediator _mediator;

        public AdicionarUsuarioHandler(IRepositoryUsuario repositoryUsuario, IMediator mediator)
        {
            _repositoryUsuario = repositoryUsuario;
            _mediator = mediator;
        }

        public async Task<Response> Handle(AdicionarUsuarioRequest request, CancellationToken cancellationToken)
        {
            if(request == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));
                return new Response(this);
            }

            if(_repositoryUsuario.Existe(x=> x.Email == request.Email))
            {
                AddNotification("Email", MSG.ESTE_X0_JA_EXISTE.ToFormat("Email"));
                return new Response(this);
            }

            Entities.Usuario usuario = new Entities.Usuario(request.PrimeiroName, request.UltimoNome, request.Email,request.Senha);

            AddNotifications(usuario.Notifications);

            if (IsInvalid())
            {
                return new Response(this);
            }

            usuario = _repositoryUsuario.Adicionar(usuario);

            var response = new Response(this, usuario);

            AdicionarUsuarioNotification adicionarUsuarioNotification = new AdicionarUsuarioNotification(usuario);

            await _mediator.Publish(adicionarUsuarioNotification);

            return await Task.FromResult(response);
        }
    }
}
