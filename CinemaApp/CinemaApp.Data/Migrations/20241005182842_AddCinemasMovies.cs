using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCinemasMovies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("4dd7c83c-4134-41e3-b9ec-89122eedf2ef"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("a385468f-7d67-43d3-96bd-ee8ec5c2bd2a"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("e1a713e0-24cd-4ea1-8562-5eacb7a7fd45"));

            migrationBuilder.CreateTable(
                name: "Cinemas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(86)", maxLength: 86, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cinemas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CinemasMovies",
                columns: table => new
                {
                    CinemaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinemasMovies", x => new { x.MovieId, x.CinemaId });
                    table.ForeignKey(
                        name: "FK_CinemasMovies_Cinemas_CinemaId",
                        column: x => x.CinemaId,
                        principalTable: "Cinemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CinemasMovies_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("286bd205-8ab4-4d7e-8683-7d3b4550faa0"), "Plovdiv", "Cinema City" },
                    { new Guid("ba8c3d48-f05b-4600-9852-ba5ee98a9fc2"), "Plovdiv", "Eccoplex" },
                    { new Guid("e720fef3-1656-437c-a40c-a073a73bde67"), "Plovdiv", "Arena" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("32d81c09-3ef9-4c43-a068-23d1317b294b"), "Harry Potter and the Chamber of Secrets is a 2002 fantasy film directed by Chris Columbus from a screenplay by Steve Kloves. It is based on the 1998 novel Harry Potter and the Chamber of Secrets by J. K. Rowling. The story follows Harry's second year at Hogwarts School of Witchcraft and Wizardry, where the Heir of Salazar Slytherin opens the Chamber of Secrets, unleashing a monster that petrifies the school's students.", "Chris Columbus", 161, "Fantasy", new DateTime(2002, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Chamber of Secrets" },
                    { new Guid("4716b4cc-8738-4c95-842c-4021b354691e"), "Harry Potter, an eleven-year-old orphan, discovers that he is a wizard and is invited to study at Hogwarts. Even as he escapes a dreary life and enters a world of magic, he finds trouble awaiting him.", "Chris Columbus", 157, "Fantasy", new DateTime(2002, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Philosopher's Stone" },
                    { new Guid("a9545ef4-0e63-4e0e-aa61-40e6890b82f7"), "Set in Middle-earth, the story tells of the Dark Lord Sauron, who seeks the One Ring, which contains part of his might, to return to power. The Ring has found its way to the young hobbit Frodo Baggins. The fate of Middle-earth hangs in the balance as Frodo and eight companions (who form the Company of the Ring) begin their perilous journey to Mount Doom in the land of Mordor, the only place where the Ring can be destroyed.", "Peter Jackson", 178, "Fantasy", new DateTime(2001, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Lord of the Rings: The Fellowship of the Ring" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CinemasMovies_CinemaId",
                table: "CinemasMovies",
                column: "CinemaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CinemasMovies");

            migrationBuilder.DropTable(
                name: "Cinemas");

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("32d81c09-3ef9-4c43-a068-23d1317b294b"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("4716b4cc-8738-4c95-842c-4021b354691e"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("a9545ef4-0e63-4e0e-aa61-40e6890b82f7"));

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
    }
}
