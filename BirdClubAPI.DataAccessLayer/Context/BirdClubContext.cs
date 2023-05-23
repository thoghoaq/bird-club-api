using BirdClubAPI.Domain.Commons.Utils;
using BirdClubAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BirdClubAPI.DataAccessLayer.Context
{
    public partial class BirdClubContext : DbContext
    {
        public BirdClubContext()
        {
        }

        public BirdClubContext(DbContextOptions<BirdClubContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activities { get; set; } = null!;
        public virtual DbSet<Attendance> Attendances { get; set; } = null!;
        public virtual DbSet<Bird> Birds { get; set; } = null!;
        public virtual DbSet<Blog> Blogs { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<Like> Likes { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<Newsfeed> Newsfeeds { get; set; } = null!;
        public virtual DbSet<Record> Records { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Activity_pk")
                    .IsClustered(false);

                entity.ToTable("Activity");

                entity.HasIndex(e => e.Id, "Activity_Id_index");

                entity.Property(e => e.ActivityType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Background).IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.Location).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Activity_Member_UserId_fk");
            });

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasKey(e => new { e.MemberId, e.ActivityId })
                    .HasName("Attendance_pk");

                entity.ToTable("Attendance");

                entity.Property(e => e.AttendanceTime).HasColumnType("datetime");

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.ActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Attendance_Activity_Id_fk");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Attendance_Member_UserId_fk");
            });

            modelBuilder.Entity<Bird>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Bird_pk")
                    .IsClustered(false);

                entity.ToTable("Bird");

                entity.HasIndex(e => e.Id, "Bird_Id_index");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Species).HasMaxLength(50);
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.HasKey(e => e.NewsfeedId)
                    .HasName("Blog_pk");

                entity.ToTable("Blog");

                entity.Property(e => e.NewsfeedId).ValueGeneratedNever();

                entity.Property(e => e.Content);

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.HasOne(d => d.Newsfeed)
                    .WithOne(p => p.Blog)
                    .HasForeignKey<Blog>(d => d.NewsfeedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Blog_Newsfeed_Id_fk");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.Content).HasMaxLength(1000);

                entity.Property(e => e.PublicationTime).HasColumnType("datetime");

                entity.Property(e => e.Type)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Comment_Member_UserId_fk");

                entity.HasOne(d => d.Reference)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ReferenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Comment_Activity_Id_fk");

                entity.HasOne(d => d.ReferenceNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ReferenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Comment_Blog_NewsfeedId_fk");

                entity.HasOne(d => d.Reference1)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ReferenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Comment_Record_NewsfeedId_fk");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.Property(e => e.Content).HasMaxLength(1000);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.ActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Feedback_Activity_Id_fk");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Feedback_Member_UserId_fk");
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.ToTable("Like");

                entity.Property(e => e.Type)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Like_Member_UserId_fk");

                entity.HasOne(d => d.Reference)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.ReferenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Like_Activity_Id_fk");

                entity.HasOne(d => d.ReferenceNavigation)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.ReferenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Like_Blog_NewsfeedId_fk");

                entity.HasOne(d => d.Reference1)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.ReferenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Like_Record_NewsfeedId_fk");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("Member_pk");

                entity.ToTable("Member");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.About).HasMaxLength(2000);

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Avatar)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Birthday)
                    .HasConversion<DateOnlyConverter, DateOnlyComparer>();

                entity.Property(e => e.About).HasMaxLength(2000);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Member)
                    .HasForeignKey<Member>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Member_User_Id_fk");
            });

            modelBuilder.Entity<Newsfeed>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Newsfeed_pk")
                    .IsClustered(false);

                entity.ToTable("Newsfeed");

                entity.HasIndex(e => e.Id, "Newsfeed_Id_index");

                entity.Property(e => e.PublicationTime).HasColumnType("datetime");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Newsfeeds)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Newsfeed_Member_UserId_fk");
            });

            modelBuilder.Entity<Record>(entity =>
            {
                entity.HasKey(e => e.NewsfeedId)
                    .HasName("Record_pk");

                entity.ToTable("Record");

                entity.Property(e => e.NewsfeedId).ValueGeneratedNever();

                entity.Property(e => e.Photo)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Bird)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.BirdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Record_Bird_Id_fk");

                entity.HasOne(d => d.Newsfeed)
                    .WithOne(p => p.Record)
                    .HasForeignKey<Record>(d => d.NewsfeedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Record_Newsfeed_Id_fk");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("User_pk")
                    .IsClustered(false);

                entity.ToTable("User");

                entity.HasIndex(e => e.Id, "User_Id_index");

                entity.HasIndex(e => e.Email, "User_pk2")
                    .IsUnique();

                entity.Property(e => e.DisplayName).HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
