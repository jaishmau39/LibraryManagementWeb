using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLibrary.Migrations
{
    public partial class InitialMigrartion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetStatuses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LibraryBranches",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Branch_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Founded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryBranches", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LibraryCards",
                columns: table => new
                {
                    CardID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Overdue_Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryCards", x => x.CardID);
                });

            migrationBuilder.CreateTable(
                name: "BranchTimings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibraryBranchID = table.Column<int>(type: "int", nullable: false),
                    Day_of_Week = table.Column<int>(type: "int", nullable: false),
                    OpeningTime = table.Column<int>(type: "int", nullable: false),
                    ClosingTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchTimings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BranchTimings_LibraryBranches_LibraryBranchID",
                        column: x => x.LibraryBranchID,
                        principalTable: "LibraryBranches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LibraryAssets",
                columns: table => new
                {
                    Asset_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumberofCopies = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    AvailabilityID = table.Column<int>(type: "int", nullable: false),
                    Last_LocationID = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeweyIndex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryAssets", x => x.Asset_ID);
                    table.ForeignKey(
                        name: "FK_LibraryAssets_AssetStatuses_AvailabilityID",
                        column: x => x.AvailabilityID,
                        principalTable: "AssetStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibraryAssets_LibraryBranches_Last_LocationID",
                        column: x => x.Last_LocationID,
                        principalTable: "LibraryBranches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patrons",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateofBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelephoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Library_CardCardID = table.Column<int>(type: "int", nullable: false),
                    Branch_LocationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patrons", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Patrons_LibraryBranches_Branch_LocationID",
                        column: x => x.Branch_LocationID,
                        principalTable: "LibraryBranches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patrons_LibraryCards_Library_CardCardID",
                        column: x => x.Library_CardCardID,
                        principalTable: "LibraryCards",
                        principalColumn: "CardID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckOutHistories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibraryAssetAsset_ID = table.Column<int>(type: "int", nullable: false),
                    LibraryCardCardID = table.Column<int>(type: "int", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckOutHistories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CheckOutHistories_LibraryAssets_LibraryAssetAsset_ID",
                        column: x => x.LibraryAssetAsset_ID,
                        principalTable: "LibraryAssets",
                        principalColumn: "Asset_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckOutHistories_LibraryCards_LibraryCardCardID",
                        column: x => x.LibraryCardCardID,
                        principalTable: "LibraryCards",
                        principalColumn: "CardID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Holds",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibraryCardCardID = table.Column<int>(type: "int", nullable: false),
                    LibraryAssetAsset_ID = table.Column<int>(type: "int", nullable: false),
                    Hold_Placed = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holds", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Holds_LibraryAssets_LibraryAssetAsset_ID",
                        column: x => x.LibraryAssetAsset_ID,
                        principalTable: "LibraryAssets",
                        principalColumn: "Asset_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Holds_LibraryCards_LibraryCardCardID",
                        column: x => x.LibraryCardCardID,
                        principalTable: "LibraryCards",
                        principalColumn: "CardID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanedAssets",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibraryAssetAsset_ID = table.Column<int>(type: "int", nullable: false),
                    LibraryCardCardID = table.Column<int>(type: "int", nullable: false),
                    CheckoutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanedAssets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LoanedAssets_LibraryAssets_LibraryAssetAsset_ID",
                        column: x => x.LibraryAssetAsset_ID,
                        principalTable: "LibraryAssets",
                        principalColumn: "Asset_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanedAssets_LibraryCards_LibraryCardCardID",
                        column: x => x.LibraryCardCardID,
                        principalTable: "LibraryCards",
                        principalColumn: "CardID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BranchTimings_LibraryBranchID",
                table: "BranchTimings",
                column: "LibraryBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_CheckOutHistories_LibraryAssetAsset_ID",
                table: "CheckOutHistories",
                column: "LibraryAssetAsset_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CheckOutHistories_LibraryCardCardID",
                table: "CheckOutHistories",
                column: "LibraryCardCardID");

            migrationBuilder.CreateIndex(
                name: "IX_Holds_LibraryAssetAsset_ID",
                table: "Holds",
                column: "LibraryAssetAsset_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Holds_LibraryCardCardID",
                table: "Holds",
                column: "LibraryCardCardID");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryAssets_AvailabilityID",
                table: "LibraryAssets",
                column: "AvailabilityID");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryAssets_Last_LocationID",
                table: "LibraryAssets",
                column: "Last_LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanedAssets_LibraryAssetAsset_ID",
                table: "LoanedAssets",
                column: "LibraryAssetAsset_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanedAssets_LibraryCardCardID",
                table: "LoanedAssets",
                column: "LibraryCardCardID");

            migrationBuilder.CreateIndex(
                name: "IX_Patrons_Branch_LocationID",
                table: "Patrons",
                column: "Branch_LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Patrons_Library_CardCardID",
                table: "Patrons",
                column: "Library_CardCardID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BranchTimings");

            migrationBuilder.DropTable(
                name: "CheckOutHistories");

            migrationBuilder.DropTable(
                name: "Holds");

            migrationBuilder.DropTable(
                name: "LoanedAssets");

            migrationBuilder.DropTable(
                name: "Patrons");

            migrationBuilder.DropTable(
                name: "LibraryAssets");

            migrationBuilder.DropTable(
                name: "LibraryCards");

            migrationBuilder.DropTable(
                name: "AssetStatuses");

            migrationBuilder.DropTable(
                name: "LibraryBranches");
        }
    }
}
