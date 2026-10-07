using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballBuddy.Profiles.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProfileSecondaryPositions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PreferredPosition = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileSecondaryPositions",
                columns: table => new
                {
                    Position = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    ProfileId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileSecondaryPositions", x => new { x.ProfileId, x.Position });
                    table.ForeignKey(
                        name: "FK_ProfileSecondaryPositions_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileSecondaryPositions");

            migrationBuilder.DropTable(
                name: "Profiles");
        }
    }
}
