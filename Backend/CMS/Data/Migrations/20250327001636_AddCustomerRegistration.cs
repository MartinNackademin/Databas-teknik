using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Ensure the Customers table is created only if it does not already exist
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Customers' AND xtype='U')
                BEGIN
                    CREATE TABLE Customers (
                        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                        CustomerName NVARCHAR(MAX) NOT NULL,
                        Email NVARCHAR(450) NOT NULL UNIQUE
                    )
                END
            ");

            // Ensure the Projects table is created only if it does not already exist
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Projects' AND xtype='U')
                BEGIN
                    CREATE TABLE Projects (
                        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                        ProjectName NVARCHAR(MAX) NOT NULL,
                        ProjectDescription NVARCHAR(MAX) NOT NULL,
                        StartDate DATETIME2 NOT NULL,
                        EndDate DATETIME2 NOT NULL,
                        ProjectStatus NVARCHAR(MAX) NOT NULL,
                        CustomerId INT NOT NULL,
                        CONSTRAINT FK_Projects_Customers_CustomerId FOREIGN KEY (CustomerId) REFERENCES Customers(Id) ON DELETE CASCADE
                    )
                END
            ");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CustomerId",
                table: "Projects",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}