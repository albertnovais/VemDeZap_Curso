using prmToolkit.NotificationPattern;
using VemDeZap.Domain.Entities.Base;
using VemDeZap.Domain.Enums;

namespace VemDeZap.Domain.Entities
{
    public class Grupo : EntityBase
    {
        public Grupo()
        {            
        }

        public Grupo(Usuario usuario, string nome, EnumNicho nicho)
        {
            Usuario = usuario;
            Nome = nome;
            Nicho = nicho;

            if (usuario == null)
            {
                AddNotification("Usuario", "Informe o usuário");
            }


            new AddNotifications<Grupo>(this)
                .IfNullOrInvalidLength(x => x.Nome, 3, 150)
                .IfEnumInvalid(x => x.Nicho)
                ;
        }

        public Usuario Usuario { get; private set; }

        public string Nome { get; private set; }

        public EnumNicho Nicho { get; private set; }

    }
}
