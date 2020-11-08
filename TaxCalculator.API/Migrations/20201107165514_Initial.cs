using Microsoft.EntityFrameworkCore.Migrations;

using System;

namespace TaxCalculator.RESTAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaxBrackets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    From = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    To = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxBrackets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxCalculationResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    PostalCode = table.Column<string>(nullable: true),
                    AnnualIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CalculatedTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxCalculationResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxCalculationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostalCode = table.Column<string>(nullable: true),
                    CalculationType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxCalculationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxFlatRates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxFlatRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxFlatValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlatValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IncomeThreshold = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxFlatValues", x => x.Id);
                });

            migrationBuilder.Sql(@"
               INSERT INTO TaxCalculationTypes (PostalCode, CalculationType) 
               VALUES 
		            ('7441','Progressive'),
		            ('A100','Flat Value'),
		            ('7000', 'Flat rate'),
		            ('1000','Progressive')
            ");

            migrationBuilder.Sql(@"
		       INSERT INTO TaxBrackets (Rate, [From], [To]) 
		       VALUES 
					(10, 0, 8350),
					(15, 8351, 33950),
					(25, 33951, 82250),
					(28, 82251, 171550),
					(33, 171551, 372950),
					(35, 372951, -1)
            ");

            migrationBuilder.Sql(@"
		       INSERT INTO TaxFlatValues (FlatValue, IncomeThreshold, Rate) 
		       VALUES 
					(10000, 200000, 5)
            ");

            migrationBuilder.Sql(@"
		       INSERT INTO TaxFlatRates (Rate) 
		       VALUES 
					(17.5)
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxBrackets");

            migrationBuilder.DropTable(
                name: "TaxCalculationResults");

            migrationBuilder.DropTable(
                name: "TaxCalculationTypes");

            migrationBuilder.DropTable(
                name: "TaxFlatRates");

            migrationBuilder.DropTable(
                name: "TaxFlatValues");
        }
    }
}
