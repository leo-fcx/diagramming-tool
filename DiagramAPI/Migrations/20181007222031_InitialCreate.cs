using Microsoft.EntityFrameworkCore.Migrations;

namespace DiagramAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diagrams",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagrams", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Classifiers",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    DiagramID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classifiers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Classifiers_Diagrams_DiagramID",
                        column: x => x.DiagramID,
                        principalTable: "Diagrams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Relationships",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(nullable: false),
                    ClassifierID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relationships", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Relationships_Classifiers_ClassifierID",
                        column: x => x.ClassifierID,
                        principalTable: "Classifiers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classifiers_DiagramID",
                table: "Classifiers",
                column: "DiagramID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationships_ClassifierID",
                table: "Relationships",
                column: "ClassifierID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relationships");

            migrationBuilder.DropTable(
                name: "Classifiers");

            migrationBuilder.DropTable(
                name: "Diagrams");
        }
    }
}
