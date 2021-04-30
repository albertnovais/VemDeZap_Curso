using Microsoft.EntityFrameworkCore;
using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VemDeZap.Domain.Entities;
using VemDeZap.Infra.Repositories.Map;

namespace VemDeZap.Infra.Repositories.Base
{
    public partial class VemDeZapContext:DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Campanha> Campanha { get; set; }
        public DbSet<Contato> Contato { get; set; }
        public DbSet<Envio> Envio { get; set; }
        public DbSet<Grupo> Grupo{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=IATECBR-NB199;Initial Catalog=VemDeZap;Integrated Security=True");
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //ignorar classes
            modelBuilder.Ignore<Notification>();
            //modelBuilder.Ignore<Nome>();
            //modelBuilder.Ignore<Email>();

            //aplicar configurações
            modelBuilder.ApplyConfiguration(new MapUsuario());
            modelBuilder.ApplyConfiguration(new MapGrupo());
            modelBuilder.ApplyConfiguration(new MapContato());
            modelBuilder.ApplyConfiguration(new MapCampanha());
            modelBuilder.ApplyConfiguration(new MapEnvio());


            base.OnModelCreating(modelBuilder);
        }
    }
}
