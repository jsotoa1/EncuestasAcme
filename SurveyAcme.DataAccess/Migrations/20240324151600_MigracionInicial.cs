using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyAcme.DataAccess.Migrations
{
    public partial class MigracionInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "survey",
                columns: table => new
                {
                    survey_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    survey_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    survey_description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    survey_link = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    date_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    user_created = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    date_modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    user_modified = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_survey", x => x.survey_id);
                });

            migrationBuilder.CreateTable(
                name: "survey_field",
                columns: table => new
                {
                    survey_field_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    survey_field_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    survey_field_title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    survey_field_required = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    survey_field_Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SurveyId = table.Column<int>(type: "int", nullable: false),
                    date_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    user_created = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    date_modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    user_modified = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_survey_field", x => x.survey_field_id);
                    table.ForeignKey(
                        name: "FK_survey_field_survey_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "survey",
                        principalColumn: "survey_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "survey_field_data",
                columns: table => new
                {
                    survey_field_data_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    survey_field_data_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SurveyFieldId = table.Column<int>(type: "int", nullable: false),
                    date_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    user_created = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    date_modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    user_modified = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_survey_field_data", x => x.survey_field_data_id);
                    table.ForeignKey(
                        name: "FK_survey_field_data_survey_field_SurveyFieldId",
                        column: x => x.SurveyFieldId,
                        principalTable: "survey_field",
                        principalColumn: "survey_field_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_survey_field_SurveyId",
                table: "survey_field",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_survey_field_data_SurveyFieldId",
                table: "survey_field_data",
                column: "SurveyFieldId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "survey_field_data");

            migrationBuilder.DropTable(
                name: "survey_field");

            migrationBuilder.DropTable(
                name: "survey");
        }
    }
}
