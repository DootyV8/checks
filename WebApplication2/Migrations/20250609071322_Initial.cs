using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcertAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Concerts",
                columns: table => new
                {
                    ConcertId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvailableTickets = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concerts", x => x.ConcertId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeatNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                });

            migrationBuilder.CreateTable(
                name: "TicketConcerts",
                columns: table => new
                {
                    TicketConcertId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    ConcertId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketConcerts", x => x.TicketConcertId);
                    table.ForeignKey(
                        name: "FK_TicketConcerts_Concerts_ConcertId",
                        column: x => x.ConcertId,
                        principalTable: "Concerts",
                        principalColumn: "ConcertId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketConcerts_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchasedTickets",
                columns: table => new
                {
                    TicketConcertId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasedTickets", x => new { x.TicketConcertId, x.CustomerId });
                    table.ForeignKey(
                        name: "FK_PurchasedTickets_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchasedTickets_TicketConcerts_TicketConcertId",
                        column: x => x.TicketConcertId,
                        principalTable: "TicketConcerts",
                        principalColumn: "TicketConcertId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Concerts",
                columns: new[] { "ConcertId", "AvailableTickets", "Date", "Name" },
                values: new object[,]
                {
                    { 1, 300, new DateTime(2025, 6, 9, 9, 0, 0, 0, DateTimeKind.Unspecified), "Concert 1" },
                    { 2, 500, new DateTime(2025, 6, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), "Concert 14" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "FirstName", "LastName", "PhoneNumber" },
                values: new object[] { 1, "John", "Doe", null });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "SeatNumber", "SerialNumber" },
                values: new object[,]
                {
                    { 1, 124, "TK2034/S4531/12" },
                    { 2, 330, "TK2027/S4831/133" }
                });

            migrationBuilder.InsertData(
                table: "TicketConcerts",
                columns: new[] { "TicketConcertId", "ConcertId", "Price", "TicketId" },
                values: new object[,]
                {
                    { 1, 1, 33.4m, 1 },
                    { 2, 2, 48.4m, 2 }
                });

            migrationBuilder.InsertData(
                table: "PurchasedTickets",
                columns: new[] { "CustomerId", "TicketConcertId", "PurchaseDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 6, 9, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 2, new DateTime(2025, 6, 9, 9, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedTickets_CustomerId",
                table: "PurchasedTickets",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketConcerts_ConcertId",
                table: "TicketConcerts",
                column: "ConcertId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketConcerts_TicketId",
                table: "TicketConcerts",
                column: "TicketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchasedTickets");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "TicketConcerts");

            migrationBuilder.DropTable(
                name: "Concerts");

            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}
