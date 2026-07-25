using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace inventoryApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_locations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    barcode = table.Column<string>(type: "text", nullable: false),
                    brand = table.Column<string>(type: "text", nullable: false),
                    family = table.Column<string>(type: "text", nullable: false),
                    category = table.Column<string>(type: "text", nullable: false),
                    item_code = table.Column<string>(type: "text", nullable: false),
                    cost_price = table.Column<decimal>(type: "numeric", nullable: false),
                    sell_price = table.Column<decimal>(type: "numeric", nullable: false),
                    has_tva = table.Column<bool>(type: "boolean", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "stock_entries",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    location_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    row_pos = table.Column<int>(type: "integer", nullable: true),
                    col_pos = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stock_entries", x => x.id);
                    table.CheckConstraint("quantity_not_negative", "quantity >= 0");
                    table.ForeignKey(
                        name: "fk_stock_entries_locations_location_id",
                        column: x => x.location_id,
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stock_entries_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "stock_movements",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    from_id = table.Column<Guid>(type: "uuid", nullable: true),
                    to_id = table.Column<Guid>(type: "uuid", nullable: true),
                    type = table.Column<string>(type: "text", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stock_movements", x => x.id);
                    table.CheckConstraint("different_source_destination", "from_id IS DISTINCT FROM to_id");
                    table.CheckConstraint("movement_quantity_positive", "quantity > 0");
                    table.ForeignKey(
                        name: "fk_stock_movements_locations_from_id",
                        column: x => x.from_id,
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stock_movements_locations_to_id",
                        column: x => x.to_id,
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stock_movements_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_stock_entries_location_id",
                table: "stock_entries",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "ix_stock_entries_product_id_location_id",
                table: "stock_entries",
                columns: new[] { "product_id", "location_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_stock_movements_from_id",
                table: "stock_movements",
                column: "from_id");

            migrationBuilder.CreateIndex(
                name: "ix_stock_movements_product_id",
                table: "stock_movements",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_stock_movements_to_id",
                table: "stock_movements",
                column: "to_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "stock_entries");

            migrationBuilder.DropTable(
                name: "stock_movements");

            migrationBuilder.DropTable(
                name: "locations");

            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
