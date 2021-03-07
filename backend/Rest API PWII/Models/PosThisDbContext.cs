using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Rest_API_PWII.Models
{
    public class PosThisDbContext : DbContext
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

        public PosThisDbContext()
        {

        }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder)
        {
           // optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings);
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(usuario =>
            {

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
                    .HasForeignKey( Post.ForeignKeyUsuario );

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
                    .HasForeignKey( Repost.ForeignKeyPost );

                repost
                    .HasOne( e => e.Usuario )
                    .WithMany( r => r.Reposts )
                    .HasForeignKey( Repost.ForeignKeyUsuario );
            });

            modelBuilder.Entity<Like>(like =>
            {
                like.HasKey(e => e.LikeID);
                like
                    .HasOne(e=> e.Post)
                    .WithMany( p=> p.Likes )
                    .HasForeignKey( Like.ForeignKeyPost );

                like
                    .HasOne( e => e.Usuario )
                    .WithMany( u => u.Likes )
                    .HasForeignKey( Like.ForeignKeyUsuario );
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
                    .WithMany( u => u.Follows )
                    .HasForeignKey( Follow.ForeignKeyUsuarioSeguido) ;

                follow
                    .HasOne(e => e.UsuarioSeguidor)
                    .WithMany(u => u.Follows)
                    .HasForeignKey(Follow.ForeignKeyUsuarioSeguidor);
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
                    .HasKey( e => e.Hashtag );

                hashtagPost
                    .HasKey( e => e.Post );

                hashtagPost
                    .HasOne( e=> e.Hashtag )
                    .WithMany( h => h.HashtagPosts )
                    .HasForeignKey(HashtagPost.ForeignkeyHashtag);

                hashtagPost
                   .HasOne( e=> e.Post )
                   .WithMany( p => p.HashtagPosts )
                   .HasForeignKey(HashtagPost.ForeignkeyPost);
            });

            modelBuilder.Entity<MediaPost>(mediaPost =>
            {
                mediaPost.HasKey(e => e.Media);
                mediaPost.HasKey(e => e.Post);

                mediaPost
                    .HasOne( e => e.Media )
                    .WithMany( m => m.MediaPosts )
                    .HasForeignKey( MediaPost.ForeignkeyMedia );

                mediaPost
                    .HasOne( e => e.Post )
                    .WithMany( p => p.MediaPosts )
                    .HasForeignKey( MediaPost.ForeignkeyPost );
            });
        }
    }
}
