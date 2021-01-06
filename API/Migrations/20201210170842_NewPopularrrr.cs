using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class NewPopularrrr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    Documento = table.Column<string>(nullable: true),
                    DataCadastro = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Cnpj = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    CodigoProduto = table.Column<string>(nullable: true),
                    Valor = table.Column<decimal>(nullable: false),
                    Promocao = table.Column<bool>(nullable: false),
                    ValorPromo = table.Column<decimal>(nullable: false),
                    Categoria = table.Column<string>(nullable: true),
                    Imagem = table.Column<string>(nullable: true),
                    Quantidade = table.Column<long>(nullable: false),
                    FornecedorId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vendas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClienteId = table.Column<long>(nullable: false),
                    FornecedorId = table.Column<long>(nullable: false),
                    TotalCompra = table.Column<decimal>(nullable: false),
                    DataCompra = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vendas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vendas_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendaProdutos",
                columns: table => new
                {
                    VendaId = table.Column<long>(nullable: false),
                    produtoId = table.Column<long>(nullable: false),
                    TotalProduto = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendaProdutos", x => new { x.produtoId, x.VendaId });
                    table.ForeignKey(
                        name: "FK_VendaProdutos_Vendas_VendaId",
                        column: x => x.VendaId,
                        principalTable: "Vendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendaProdutos_Produtos_produtoId",
                        column: x => x.produtoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "DataCadastro", "Documento", "Email", "Nome", "Senha" },
                values: new object[,]
                {
                    { 1L, new DateTime(2020, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "88.888.888-44", "joao@joao.com", "João Paulo", "FC791BD5779D9D076129FD56584B3392" },
                    { 2L, new DateTime(2020, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "77.858.458-96", "camila@camila.com", "Camila Santos", "9403ADBD654AE140BB27A8036FC64196" }
                });

            migrationBuilder.InsertData(
                table: "Fornecedores",
                columns: new[] { "Id", "Cnpj", "Nome" },
                values: new object[,]
                {
                    { 1L, "81.191.124/0001-01", "João do Gás" },
                    { 2L, "88.857.444/0001-01", "Amazon" }
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Categoria", "CodigoProduto", "FornecedorId", "Imagem", "Nome", "Promocao", "Quantidade", "Valor", "ValorPromo" },
                values: new object[,]
                {
                    { 2L, "Gás", "4487", 1L, "gas.jpg", "Gás", false, 150L, 70m, 20m },
                    { 4L, "Gás", "045", 1L, "gasp45.jpg", "GásP45", false, 50L, 150m, 20m },
                    { 1L, "Eletronicos", "548", 2L, "echodot.jpg", "Echo dot", true, 100L, 199m, 150m },
                    { 3L, "Celulares", "874", 2L, "sansung.jpg", "Smartphone", true, 30L, 1699m, 99m }
                });

            migrationBuilder.InsertData(
                table: "Vendas",
                columns: new[] { "Id", "ClienteId", "DataCompra", "FornecedorId", "TotalCompra" },
                values: new object[] { 1L, 1L, new DateTime(2020, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, 16m });

            migrationBuilder.InsertData(
                table: "VendaProdutos",
                columns: new[] { "produtoId", "VendaId", "TotalProduto" },
                values: new object[] { 1L, 1L, 12m });

            migrationBuilder.InsertData(
                table: "VendaProdutos",
                columns: new[] { "produtoId", "VendaId", "TotalProduto" },
                values: new object[] { 3L, 1L, 4m });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_FornecedorId",
                table: "Produtos",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_VendaProdutos_VendaId",
                table: "VendaProdutos",
                column: "VendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_ClienteId",
                table: "Vendas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_FornecedorId",
                table: "Vendas",
                column: "FornecedorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendaProdutos");

            migrationBuilder.DropTable(
                name: "Vendas");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Fornecedores");
        }
    }
}
