using FluentMigrator;

namespace Sistema_ERP.Data
{
    [Migration(2)]
    public class FluentMigrator : Migration
    {
        public override void Down()
        {
            Delete.Table("Produtos");
            Delete.Table("Categorias");
        }

        public override void Up()
        {
            Create.Table("Usuarios")
                 .WithColumn("Id_Usuario").AsInt64().PrimaryKey().Identity()
                 .WithColumn("Nome").AsString().NotNullable()
                 .WithColumn("Login").AsString().Unique().NotNullable()
                 .WithColumn("Email").AsString().Unique().NotNullable()
                 .WithColumn("Senha").AsString().NotNullable()
                 .WithColumn("Perfil").AsString().NotNullable()
                 .WithColumn("Data_Criada").AsDateTime().NotNullable()
                 .WithColumn("Data_Modificada").AsDateTime().Nullable();          
        }
    }
}
