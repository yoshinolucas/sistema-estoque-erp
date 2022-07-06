using FluentMigrator;

namespace Sistema_ERP.Data
{
    [Migration(1)]
    public class FluentMigrator : Migration
    {
        public override void Down()
        {
            Delete.Table("Produtos");
            Delete.Table("Categorias");
        }

        public override void Up()
        {
            Create.Table("Produtos")
                .WithColumn("Id_Produto").AsInt64().PrimaryKey().Identity()
                .WithColumn("Nome").AsString().Unique().NotNullable()
                .WithColumn("Preco").AsDecimal().NotNullable()
                .WithColumn("Estoque").AsInt64().Nullable()
                .WithColumn("Id_Categoria").AsInt64().NotNullable()
                .WithColumn("Data_Criada").AsDateTime().NotNullable()
                .WithColumn("Data_Modificada").AsDateTime().Nullable();

            Create.Table("Categorias")
                .WithColumn("Id_Categoria").AsInt64().PrimaryKey().Identity()
                .WithColumn("Nome").AsString().Unique().NotNullable()
                .WithColumn("Data_Criada").AsDateTime().NotNullable()
                .WithColumn("Data_Modificada").AsDateTime().Nullable();

            Create.ForeignKey()
                .FromTable("Produtos").ForeignColumn("Id_Categoria")
                .ToTable("Categorias").PrimaryColumn("Id_Categoria");


        }
    }
}
