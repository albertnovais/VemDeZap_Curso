using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Text;
using VemDeZap.Domain.Commands.Usuario.AdicionarUsuario;

namespace VemDeZap.Domain.Commands
{
    public class Response
    {

        public Response(INotifiable notifiable)
        {
            this.Success = notifiable.IsValid();
            this.Notifications = notifiable.Notifications;
        }

        public Response(INotifiable notifiable, object data)
        {
            this.Success = notifiable.IsValid();
            this.Data = data;
            this.Notifications = notifiable.Notifications;
        }

        public IEnumerable<Notification> Notifications { get; set; }

        public bool Success { get; set; }

        public object Data { get; set; }
    }
}
