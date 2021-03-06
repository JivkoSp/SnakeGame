// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SnakeGame.Data.DBContext;

namespace SnakeGame.Migrations
{
    [DbContext(typeof(SnakeGameDbContext))]
    partial class SnakeGameDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SnakeGame.Data.Entities.NewEntity", b =>
                {
                    b.Property<int>("NewEntityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NewEntityId");

                    b.ToTable("NewEntities");
                });

            modelBuilder.Entity("SnakeGame.Data.Entities.UserScore", b =>
                {
                    b.Property<int>("UserScoreID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Score")
                        .HasColumnType("bigint");

                    b.HasKey("UserScoreID");

                    b.ToTable("UserScores");
                });
#pragma warning restore 612, 618
        }
    }
}
