using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class M2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "labelID",
                table: "NoteTable",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LabelsTable",
                columns: table => new
                {
                    labelID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    labelName = table.Column<string>(nullable: true),
                    NoteId = table.Column<long>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabelsTable", x => x.labelID);
                    table.ForeignKey(
                        name: "FK_LabelsTable_UserTable_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NoteTable_labelID",
                table: "NoteTable",
                column: "labelID");

            migrationBuilder.CreateIndex(
                name: "IX_LabelsTable_UserId",
                table: "LabelsTable",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteTable_LabelsTable_labelID",
                table: "NoteTable",
                column: "labelID",
                principalTable: "LabelsTable",
                principalColumn: "labelID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteTable_LabelsTable_labelID",
                table: "NoteTable");

            migrationBuilder.DropTable(
                name: "LabelsTable");

            migrationBuilder.DropIndex(
                name: "IX_NoteTable_labelID",
                table: "NoteTable");

            migrationBuilder.DropColumn(
                name: "labelID",
                table: "NoteTable");
        }
    }
}
