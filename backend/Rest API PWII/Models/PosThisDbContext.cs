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
    public class PosThisDbContext : IdentityDbContext<User>
    {
        public DbSet<Post>          Posts       { get; set; }

        public DbSet<Repost>        Reposts     { get; set; }

        public DbSet<Like>          Likes       { get; set; }

        public DbSet<Reply>         Replies     { get; set; }

        public DbSet<Hashtag>       Hashtags    { get; set; }

        public DbSet<Follow>        Follows     { get; set; }

        public DbSet<UserMedia>     UserMedias  { get; set; }
        public DbSet<PostMedia>     PostMedias  { get; set; }
        public DbSet<ReplyMedia>    ReplyMedias { get; set; }

        public DbSet<HashtagPost>   HashtagPosts { get; set; }

        public PosThisDbContext(DbContextOptions<PosThisDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder)
        {
           // optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings);
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder)
        {

            base.OnModelCreating( modelBuilder );

            modelBuilder.Entity<User>( user =>
            {
                user.HasKey( user => user.Id );
                
                user.Property( e => e.UserName )
                    .HasMaxLength( 20 )
                    .IsUnicode( false )
                    .IsRequired();

                user.Property( e => e.Tag )
                    .HasMaxLength( 15 )
                    .IsUnicode( false )
                    .IsRequired();

                user.Property( e => e.Email )
                    .HasMaxLength( 256 )
                    .IsUnicode( false )
                    .IsRequired();

                user.Property( e => e.PasswordHash )
                    .IsRequired();

                user.HasOne ( x => x.ProfilePic )
                    .WithOne( x => x.ProfilePicOwner )
                    .HasForeignKey<User>( x => x.ProfilePicID );

                user.HasOne ( x => x.CoverPic )
                    .WithOne( x => x.CoverPicOwner )
                    .HasForeignKey<User>(x => x.CoverPicID);

                user
                    .HasMany(e => e.Posts)
                    .WithOne(u => u.User)
                    .OnDelete(DeleteBehavior.Cascade);

                user.HasMany(x => x.Replies)
                    .WithOne(x => x.User)
                    .OnDelete(DeleteBehavior.Cascade);

                user.HasMany(x => x.Likes)
                    .WithOne(x => x.User)
                    .OnDelete(DeleteBehavior.Cascade);

                user.HasMany(x => x.Reposts)
                    .WithOne(x => x.User)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Post>(post =>
            {
                post.HasKey(e => e.PostID);

                post.Property( e => e.PostID )
                    .ValueGeneratedOnAdd();

                post.Property(e => e.Content)
                    .HasMaxLength(256)
                    .IsRequired(false);

                post.Property(e => e.PostDate);

                post
                    .HasOne(e => e.User)
                    .WithMany(u => u.Posts)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                post
                    .HasMany(x => x.Medias)
                    .WithOne(x => x.Post)
                    .OnDelete(DeleteBehavior.Cascade);

                post.HasMany(x => x.Replies)
                    .WithOne(x => x.Post)
                    .OnDelete(DeleteBehavior.Cascade);

                post.HasMany(x => x.Likes)
                    .WithOne(x => x.Post)
                    .OnDelete(DeleteBehavior.Cascade);

                post.HasMany(x => x.Reposts)
                    .WithOne(x => x.Post)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Repost>(repost =>
            {
                repost.HasKey( e=>e.RepostID);

                repost.Property(e => e.RepostID)
                    .ValueGeneratedOnAdd();

                repost
                    .HasOne(e => e.Post)
                    .WithMany(p => p.Reposts)
                    .HasForeignKey( r => r.PostID );

                repost
                    .HasOne( e => e.User )
                    .WithMany( r => r.Reposts )
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Like>(like =>
            {
                like.HasKey(e => e.LikeID);

                like.Property(e => e.LikeID)
                    .ValueGeneratedOnAdd();

                like
                    .HasOne(e=> e.Post)
                    .WithMany( p=> p.Likes)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                like
                    .HasOne( e => e.User )
                    .WithMany( u => u.Likes)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Reply>(reply =>
            {
                reply.HasKey( e => e.ReplyID );

                reply.Property( e => e.ReplyID )
                    .ValueGeneratedOnAdd();

                reply
                    .HasOne( e => e.Post )
                    .WithMany( p => p.Replies)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                reply
                    .HasOne( e => e.User )
                    .WithMany( u => u.Replies)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                reply
                    .HasMany(x => x.Medias)
                    .WithOne(x => x.Reply)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Hashtag>(hashtag =>
            {
                hashtag.HasKey(e => e.HashtagID);

                hashtag.Property(e => e.HashtagID)
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Follow>(follow =>
            {
                follow
                    .HasKey(e => e.FollowID);

                follow.Property(e => e.FollowID)
                    .ValueGeneratedOnAdd();

                follow
                    .HasOne( e => e.UserFollower )
                    .WithMany( u => u.Following)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                follow
                    .HasOne( e => e.UserFollow )
                    .WithMany( u => u.Follows)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UserMedia>(media =>
            {
                media
                    .HasKey(e => e.MediaID);

                media.Property(e => e.MediaID)
                    .ValueGeneratedOnAdd();

                media
                    .Property(e => e.MIME)
                    .HasMaxLength(20)
                    .IsRequired();

                media
                    .Property(e => e.Name)
                    .IsRequired();

                media
                    .HasOne(x=>x.ProfilePicOwner)
                    .WithOne(x => x.ProfilePic)
                    .OnDelete(DeleteBehavior.Cascade);

                media
                    .HasOne(x => x.CoverPicOwner)
                    .WithOne(x => x.CoverPic)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PostMedia>(media =>
            {
                media
                    .HasKey(e => e.MediaID);

                media.Property(e => e.MediaID)
                    .ValueGeneratedOnAdd();

                media
                    .Property(e => e.MIME)
                    .HasMaxLength(20)
                    .IsRequired();

                media
                    .Property(e => e.Name)
                    .IsRequired();

                media
                    .HasOne(x => x.Post)
                    .WithMany(x => x.Medias)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ReplyMedia>(media =>
            {
                media
                    .HasKey(e => e.MediaID);

                media.Property(e => e.MediaID)
                    .ValueGeneratedOnAdd();

                media
                    .Property(e => e.MIME)
                    .HasMaxLength(20)
                    .IsRequired();

                media
                    .Property(e => e.Name)
                    .IsRequired();

                media
                    .HasOne(x => x.Reply)
                    .WithMany(x => x.Medias)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<HashtagPost>(hashtagPost =>
            {
                hashtagPost
                    .HasKey(hp => hp.HashtagPostID);

                hashtagPost.Property(e => e.HashtagPostID)
                    .ValueGeneratedOnAdd();

                hashtagPost
                    .HasOne( e=> e.Hashtag )
                    .WithMany( h => h.HashtagPosts )
                    .HasForeignKey( hp=> hp.HashtagID );

                hashtagPost
                   .HasOne( e=> e.Post )
                   .WithMany( p => p.HashtagPosts )
                   .HasForeignKey( hp => hp.PostID );
            });
        }
    }
}