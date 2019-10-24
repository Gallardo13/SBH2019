using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FSO.SDD.DataBaseEfStore.Migrations
{
    public partial class JiraItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JiraEpics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectKey = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FinishDateTime = table.Column<DateTime>(nullable: false),
                    ExpectationDateTime = table.Column<DateTime>(nullable: false),
                    DeadlineDateTime = table.Column<DateTime>(nullable: false),
                    TasksEstimationValue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JiraEpics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JiraEpicTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EpicId = table.Column<int>(nullable: false),
                    TaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JiraEpicTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JiraReleaseStates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JiraReleaseStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JiraReleaseTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReleaseId = table.Column<int>(nullable: false),
                    TaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JiraReleaseTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JiraSprintSates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JiraSprintSates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JiraSprintTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SprintId = table.Column<int>(nullable: false),
                    TaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JiraSprintTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JiraTaskStates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JiraTaskStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SourceSystems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JiraReleaseHistorys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReleaseId = table.Column<int>(nullable: false),
                    StateId = table.Column<int>(nullable: false),
                    TasksEstimationValue = table.Column<int>(nullable: false),
                    TasksRemainderValue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JiraReleaseHistorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JiraReleaseHistorys_JiraReleaseStates_StateId",
                        column: x => x.StateId,
                        principalTable: "JiraReleaseStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JiraReleases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConfigurationItem = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    PlannedFinishDateTime = table.Column<DateTime>(nullable: false),
                    ActualFinishDateTime = table.Column<DateTime>(nullable: false),
                    PlannedIFTDateTime = table.Column<DateTime>(nullable: false),
                    ActualIFTDateTime = table.Column<DateTime>(nullable: false),
                    PlannedATDateTime = table.Column<DateTime>(nullable: false),
                    ActualATDateTime = table.Column<DateTime>(nullable: false),
                    StateId = table.Column<int>(nullable: false),
                    TasksEstimationValue = table.Column<int>(nullable: false),
                    TasksRemainderValue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JiraReleases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JiraReleases_JiraReleaseStates_StateId",
                        column: x => x.StateId,
                        principalTable: "JiraReleaseStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JiraSprintHistorys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SptintId = table.Column<int>(nullable: false),
                    TasksEstimationValue = table.Column<int>(nullable: false),
                    StateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JiraSprintHistorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JiraSprintHistorys_JiraSprintSates_StateId",
                        column: x => x.StateId,
                        principalTable: "JiraSprintSates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JiraSprints",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectKey = table.Column<string>(nullable: true),
                    TeamName = table.Column<string>(nullable: true),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    EndDateTime = table.Column<DateTime>(nullable: false),
                    SprintEstimatedValue = table.Column<int>(nullable: false),
                    TasksEstimationValue = table.Column<int>(nullable: false),
                    StateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JiraSprints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JiraSprints_JiraSprintSates_StateId",
                        column: x => x.StateId,
                        principalTable: "JiraSprintSates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataSources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SourceSystemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataSources_SourceSystems_SourceSystemId",
                        column: x => x.SourceSystemId,
                        principalTable: "SourceSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JiraTaskHistorys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuthorId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: true),
                    DefectSeverity = table.Column<int>(nullable: false),
                    Estimation = table.Column<int>(nullable: false),
                    Remainder = table.Column<int>(nullable: false),
                    OriginalEstimation = table.Column<int>(nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(nullable: false),
                    StateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JiraTaskHistorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JiraTaskHistorys_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JiraTaskHistorys_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JiraTaskHistorys_JiraTaskStates_StateId",
                        column: x => x.StateId,
                        principalTable: "JiraTaskStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JiraTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<string>(nullable: true),
                    AuthorId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: true),
                    ProjectKey = table.Column<string>(nullable: true),
                    ConfigurationItem = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DefectSeverity = table.Column<int>(nullable: false),
                    Estimation = table.Column<int>(nullable: false),
                    Remainder = table.Column<int>(nullable: false),
                    OriginalEstimation = table.Column<int>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(nullable: false),
                    StateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JiraTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JiraTasks_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JiraTasks_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JiraTasks_JiraTaskStates_StateId",
                        column: x => x.StateId,
                        principalTable: "JiraTaskStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataFacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SourceId = table.Column<int>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    PropertyKey = table.Column<string>(nullable: true),
                    PropertyValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataFacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataFacts_DataSources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "DataSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataFacts_SourceId",
                table: "DataFacts",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSources_SourceSystemId",
                table: "DataSources",
                column: "SourceSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_JiraReleaseHistorys_StateId",
                table: "JiraReleaseHistorys",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_JiraReleases_StateId",
                table: "JiraReleases",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_JiraSprintHistorys_StateId",
                table: "JiraSprintHistorys",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_JiraSprints_StateId",
                table: "JiraSprints",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_JiraTaskHistorys_AuthorId",
                table: "JiraTaskHistorys",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_JiraTaskHistorys_OwnerId",
                table: "JiraTaskHistorys",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_JiraTaskHistorys_StateId",
                table: "JiraTaskHistorys",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_JiraTasks_AuthorId",
                table: "JiraTasks",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_JiraTasks_OwnerId",
                table: "JiraTasks",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_JiraTasks_StateId",
                table: "JiraTasks",
                column: "StateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataFacts");

            migrationBuilder.DropTable(
                name: "JiraEpics");

            migrationBuilder.DropTable(
                name: "JiraEpicTasks");

            migrationBuilder.DropTable(
                name: "JiraReleaseHistorys");

            migrationBuilder.DropTable(
                name: "JiraReleases");

            migrationBuilder.DropTable(
                name: "JiraReleaseTasks");

            migrationBuilder.DropTable(
                name: "JiraSprintHistorys");

            migrationBuilder.DropTable(
                name: "JiraSprints");

            migrationBuilder.DropTable(
                name: "JiraSprintTasks");

            migrationBuilder.DropTable(
                name: "JiraTaskHistorys");

            migrationBuilder.DropTable(
                name: "JiraTasks");

            migrationBuilder.DropTable(
                name: "DataSources");

            migrationBuilder.DropTable(
                name: "JiraReleaseStates");

            migrationBuilder.DropTable(
                name: "JiraSprintSates");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "JiraTaskStates");

            migrationBuilder.DropTable(
                name: "SourceSystems");
        }
    }
}
