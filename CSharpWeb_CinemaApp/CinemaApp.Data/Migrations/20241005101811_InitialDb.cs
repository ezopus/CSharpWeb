using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("4dd7c83c-4134-41e3-b9ec-89122eedf2ef"), "Set in Middle-earth, the story tells of the Dark Lord Sauron, who seeks the One Ring, which contains part of his might, to return to power. The Ring has found its way to the young hobbit Frodo Baggins. The fate of Middle-earth hangs in the balance as Frodo and eight companions (who form the Company of the Ring) begin their perilous journey to Mount Doom in the land of Mordor, the only place where the Ring can be destroyed.", "Peter Jackson", 178, "Fantasy", new DateTime(2001, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Lord of the Rings: The Fellowship of the Ring" },
                    { new Guid("a385468f-7d67-43d3-96bd-ee8ec5c2bd2a"), "Harry Potter and the Chamber of Secrets is a 2002 fantasy film directed by Chris Columbus from a screenplay by Steve Kloves. It is based on the 1998 novel Harry Potter and the Chamber of Secrets by J. K. Rowling. The story follows Harry's second year at Hogwarts School of Witchcraft and Wizardry, where the Heir of Salazar Slytherin opens the Chamber of Secrets, unleashing a monster that petrifies the school's students.", "Chris Columbus", 161, "Fantasy", new DateTime(2002, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Chamber of Secrets" },
                    { new Guid("e1a713e0-24cd-4ea1-8562-5eacb7a7fd45"), "Harry Potter, an eleven-year-old orphan, discovers that he is a wizard and is invited to study at Hogwarts. Even as he escapes a dreary life and enters a world of magic, he finds trouble awaiting him.", "Chris Columbus", 157, "Fantasy", new DateTime(2002, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Philosopher's Stone" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
