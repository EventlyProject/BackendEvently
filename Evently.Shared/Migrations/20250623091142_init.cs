using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Evently.Shared.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoryes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoryes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Emailaddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxParticipants = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AccessRequirements = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Categoryes_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categoryes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventParticipants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    RegisteretAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventParticipants_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventParticipants_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categoryes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "workshop" },
                    { 2, "Foredrag" },
                    { 3, "Sport" },
                    { 4, "Musik" },
                    { 5, "Kunst" },
                    { 6, "Netwærk" },
                    { 7, "Teknologi" },
                    { 8, "Sundhed" },
                    { 9, "Mad & Drikke" },
                    { 10, "Turnering" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Emailaddress", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "admin@evently.com", "admin1234", "admin", "admin" },
                    { 2, "alice@example.com", "password1", "user", "alice" },
                    { 3, "bob@example.com", "password2", "user", "bob" },
                    { 4, "carol@example.com", "password3", "user", "carol" },
                    { 5, "dave@example.com", "password4", "user", "dave" },
                    { 6, "eve@example.com", "password5", "user", "eve" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "AccessRequirements", "CategoryId", "Details", "Location", "LogoUrl", "MaxParticipants", "Name", "Price", "StartTime", "UserId" },
                values: new object[,]
                {
                    { 1, "", 1, "Lær C#", "Room 101", "", 30, "C# Workshop", 0m, new DateTime(2025, 7, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "", 2, "Sommerfest for alle", "Café", "", 100, "Sommerfest", 50m, new DateTime(2025, 7, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, "", 7, "Bliv klogere på AI", "Auditorium", "", 80, "Foredrag om AI", 0m, new DateTime(2025, 7, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, "", 8, "Morgenyoga", "Byparken", "", 20, "Yoga i parken", 0m, new DateTime(2025, 7, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 5, "", 6, "Mød nye mennesker", "Lounge", "", 40, "Netværksaften", 0m, new DateTime(2025, 7, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 6, "", 9, "Lær at lave mad", "Køkkenet", "", 15, "Madlavningskursus", 100m, new DateTime(2025, 7, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 7, "", 5, "Se moderne kunst", "Galleri", "", 60, "Kunstudstilling", 25m, new DateTime(2025, 7, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 8, "", 3, "Turnering for alle", "Stadion", "", 22, "Fodboldturnering", 0m, new DateTime(2025, 7, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 9, "", 4, "Live musik", "Pladsen", "", 200, "Musikfestival", 150m, new DateTime(2025, 7, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 10, "", 10, "Diverse aktiviteter", "Fælleshuset", "", 50, "Andet Event", 0m, new DateTime(2025, 7, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventParticipants_EventId",
                table: "EventParticipants",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventParticipants_UserId",
                table: "EventParticipants",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CategoryId",
                table: "Events",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_UserId",
                table: "Events",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventParticipants");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Categoryes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
