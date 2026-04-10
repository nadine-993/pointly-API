using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pointlyAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalPointsPool = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chargers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    AvailablePoints = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    AssignedByAdminId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chargers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chargers_Admins_AssignedByAdminId",
                        column: x => x.AssignedByAdminId,
                        principalTable: "Admins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Chargers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    PointsBalance = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Merchants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    BusinessName = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    PointsBalance = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    ChargedByChargetId = table.Column<int>(type: "INTEGER", nullable: true),
                    ChrargedByChargerId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Merchants_Chargers_ChrargedByChargerId",
                        column: x => x.ChrargedByChargerId,
                        principalTable: "Chargers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Merchants_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MerchantId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    PointsCost = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ValidUntil = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    QuantityAvailable = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_Merchants_MerchantId",
                        column: x => x.MerchantId,
                        principalTable: "Merchants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PointAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    FromAdminId = table.Column<int>(type: "INTEGER", nullable: true),
                    FromChargerId = table.Column<int>(type: "INTEGER", nullable: true),
                    ToChargerId = table.Column<int>(type: "INTEGER", nullable: true),
                    ToMerchantId = table.Column<int>(type: "INTEGER", nullable: true),
                    PointsAmount = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointAssignments_Admins_FromAdminId",
                        column: x => x.FromAdminId,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PointAssignments_Chargers_FromChargerId",
                        column: x => x.FromChargerId,
                        principalTable: "Chargers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PointAssignments_Chargers_ToChargerId",
                        column: x => x.ToChargerId,
                        principalTable: "Chargers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PointAssignments_Merchants_ToMerchantId",
                        column: x => x.ToMerchantId,
                        principalTable: "Merchants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MerchantId = table.Column<int>(type: "INTEGER", nullable: false),
                    StoreName = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Latitude = table.Column<double>(type: "REAL", nullable: false),
                    Longitude = table.Column<double>(type: "REAL", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stores_Merchants_MerchantId",
                        column: x => x.MerchantId,
                        principalTable: "Merchants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    MerchantId = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    PointsAmount = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    PurchaseAmount = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    OfferId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Merchants_MerchantId",
                        column: x => x.MerchantId,
                        principalTable: "Merchants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserId",
                table: "Admins",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chargers_AssignedByAdminId",
                table: "Chargers",
                column: "AssignedByAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Chargers_UserId",
                table: "Chargers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Merchants_ChrargedByChargerId",
                table: "Merchants",
                column: "ChrargedByChargerId");

            migrationBuilder.CreateIndex(
                name: "IX_Merchants_UserId",
                table: "Merchants",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_MerchantId",
                table: "Offers",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_PointAssignments_FromAdminId",
                table: "PointAssignments",
                column: "FromAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_PointAssignments_FromChargerId",
                table: "PointAssignments",
                column: "FromChargerId");

            migrationBuilder.CreateIndex(
                name: "IX_PointAssignments_ToChargerId",
                table: "PointAssignments",
                column: "ToChargerId");

            migrationBuilder.CreateIndex(
                name: "IX_PointAssignments_ToMerchantId",
                table: "PointAssignments",
                column: "ToMerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_MerchantId",
                table: "Stores",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CustomerId",
                table: "Transactions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_MerchantId",
                table: "Transactions",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_OfferId",
                table: "Transactions",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_CustomerId",
                table: "Wallets",
                column: "CustomerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PointAssignments");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Merchants");

            migrationBuilder.DropTable(
                name: "Chargers");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
