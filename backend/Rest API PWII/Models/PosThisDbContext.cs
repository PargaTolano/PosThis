using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Rest_API_PWII.Models
{
    public class PosThisDbContext : IdentityDbContext<Usuario>
    {
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Repost> Reposts { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Reply> Replies { get; set; }

        public DbSet<Hashtag> Hashtags { get; set; }

        public DbSet<Follow> Follows { get; set; }

        public DbSet<Media> Medias { get; set; }

        public DbSet<HashtagPost> HashtagPosts { get; set; }

        public DbSet<MediaPost> MediaPosts { get; set; }

        public PosThisDbContext(DbContextOptions<PosThisDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder)
        {
           // optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings);
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(usuario =>
            {
                usuario.HasKey(u => u.Id);
                usuario.HasKey(usuario => usuario.UsuarioID);
                
                usuario.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsRequired();

                usuario.Property(e => e.Tag)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsRequired();

                usuario.Property(e => e.Correo)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .IsRequired();

                usuario.Property(e => e.Contrasena)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsRequired();

                usuario.HasKey(e => e.FechaNacimiento);

            });

            modelBuilder.Entity<Post>(post =>
            {
                post.HasKey(e => e.PostID);

                post.Property(e => e.Texto)
                    .HasMaxLength(256)
                    .IsRequired(false);

                post
                    .HasOne( e => e.Usuario )
                    .WithMany( u => u.Posts )
                    .HasForeignKey( p => p.UsuarioID );

                post.HasKey(e => e.FechaPublicacion);
            });

            modelBuilder.Entity<Repost>(repost =>
            {
                repost.HasKey( e=>e.RepostID);

                repost
                    .Property(e => e.Texto)
                    .HasMaxLength(256);

                repost
                    .HasOne(e => e.Post)
                    .WithMany(p => p.Reposts)
                    .HasForeignKey( r => r.PostID );

                repost
                    .HasOne( e => e.Usuario )
                    .WithMany( r => r.Reposts )
                    .HasForeignKey( r => r.UsuarioID );
            });

            modelBuilder.Entity<Like>(like =>
            {
                like.HasKey(e => e.LikeID);

                like
                    .HasOne(e=> e.Post)
                    .WithMany( p=> p.Likes )
                    .HasForeignKey( l => l.PostID );

                like
                    .HasOne( e => e.Usuario )
                    .WithMany( u => u.Likes )
                    .HasForeignKey( l => l.UsuarioID );
            });

            modelBuilder.Entity<Reply>(reply =>
            {
                reply.HasKey( e => e.ReplyID );

                reply
                    .HasOne( e => e.Post )
                    .WithMany( p => p.Replies )
                    .HasForeignKey( r => r.PostID );

                reply
                    .HasOne( e => e.Usuario )
                    .WithMany( u => u.Replies )
                    .HasForeignKey( r => r.UsuarioID );
            });

            modelBuilder.Entity<Hashtag>(hashtag =>
            {
                hashtag.HasKey(e => e.HastagID);

                hashtag.HasKey(e => e.Texto);
            });

            modelBuilder.Entity<Follow>(follow =>
            {
                follow
                    .HasKey(e => e.FollowID);

                follow
                    .HasOne( e=> e.UsuarioSeguido )
                    .WithMany( u => u.Following )
                    .HasForeignKey( f => f.UsuarioSeguidoID )
                    .HasPrincipalKey( f => f.UsuarioID );

                follow
                    .HasOne( e => e.UsuarioSeguidor )
                    .WithMany( u => u.Follows )
                    .HasForeignKey( f => f.UsuarioSeguidorID)
                    .HasPrincipalKey(f => f.UsuarioID);
            });

            modelBuilder.Entity<Media>(media =>
            {
                media
                    .HasKey(e => e.MediaID);

                media
                    .Property(e => e.MIME)
                    .HasMaxLength(20)
                    .IsRequired();

                media
                    .Property(e => e.Contenido)
                    .IsRequired();
            });

            modelBuilder.Entity<HashtagPost>(hashtagPost =>
            {
                hashtagPost
                .HasKey(hp => hp.HashtagPostID);

                hashtagPost
                    .HasOne( e=> e.Hashtag )
                    .WithMany( h => h.HashtagPosts )
                    .HasForeignKey( hp=> hp.HashtagID );

                hashtagPost
                   .HasOne( e=> e.Post )
                   .WithMany( p => p.HashtagPosts )
                   .HasForeignKey( hp => hp.PostID );
            });

            modelBuilder.Entity<MediaPost>(mediaPost =>
            {
                mediaPost
                .HasKey(mp => mp.MediaPostID);

                mediaPost
                    .HasOne( e => e.Media )
                    .WithMany( m => m.MediaPosts )
                    .HasForeignKey( mp=> mp.MediaID );

                mediaPost
                    .HasOne( e => e.Post )
                    .WithMany( p => p.MediaPosts )
                    .HasForeignKey( mp => mp.PostID );
            });
        }
    }
}