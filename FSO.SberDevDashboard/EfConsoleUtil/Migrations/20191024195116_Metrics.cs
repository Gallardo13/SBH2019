using Microsoft.EntityFrameworkCore.Migrations;

namespace EfConsoleUtil.Migrations
{
    public partial class Metrics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IndicatorDimensions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicatorDimensions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Indicators",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indicators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndicatorSourceAggregates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SourceType = table.Column<string>(nullable: true),
                    IndicatorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicatorSourceAggregates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndicatorValueSourceKeys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SourceType = table.Column<string>(nullable: true),
                    SourceItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicatorValueSourceKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndicatorValueSources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IndicatorValueId = table.Column<int>(nullable: false),
                    IndicatorValueSourceKeyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicatorValueSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndicatorValuesSeries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicatorValuesSeries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Metrics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    IndicatorId = table.Column<int>(nullable: false),
                    MaxValue = table.Column<decimal>(nullable: false),
                    MinValue = table.Column<decimal>(nullable: false),
                    OptimalValueLow = table.Column<decimal>(nullable: false),
                    OptimalValueHigh = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Metrics_Indicators_IndicatorId",
                        column: x => x.IndicatorId,
                        principalTable: "Indicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndicatorValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IndicatorId = table.Column<int>(nullable: false),
                    DimensionId = table.Column<int>(nullable: false),
                    IndicatorSeriesId = table.Column<int>(nullable: false),
                    Value = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicatorValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndicatorValues_IndicatorDimensions_DimensionId",
                        column: x => x.DimensionId,
                        principalTable: "IndicatorDimensions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndicatorValues_Indicators_IndicatorId",
                        column: x => x.IndicatorId,
                        principalTable: "Indicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndicatorValues_IndicatorValuesSeries_IndicatorSeriesId",
                        column: x => x.IndicatorSeriesId,
                        principalTable: "IndicatorValuesSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IndicatorValues_DimensionId",
                table: "IndicatorValues",
                column: "DimensionId");

            migrationBuilder.CreateIndex(
                name: "IX_IndicatorValues_IndicatorId",
                table: "IndicatorValues",
                column: "IndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_IndicatorValues_IndicatorSeriesId",
                table: "IndicatorValues",
                column: "IndicatorSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Metrics_IndicatorId",
                table: "Metrics",
                column: "IndicatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndicatorSourceAggregates");

            migrationBuilder.DropTable(
                name: "IndicatorValues");

            migrationBuilder.DropTable(
                name: "IndicatorValueSourceKeys");

            migrationBuilder.DropTable(
                name: "IndicatorValueSources");

            migrationBuilder.DropTable(
                name: "Metrics");

            migrationBuilder.DropTable(
                name: "IndicatorDimensions");

            migrationBuilder.DropTable(
                name: "IndicatorValuesSeries");

            migrationBuilder.DropTable(
                name: "Indicators");
        }
    }
}
