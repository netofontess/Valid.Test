using FluentMigrator;

namespace Valid.Test.Repository.Migrations
{
    [Migration(202409180001)]
    public class Migration202409180001CriarTabelaProtocolo : Migration
    {
        public override void Up()
        {
            Create.Table("Protocolo")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Nome").AsString(255).NotNullable()
                .WithColumn("Cpf").AsString(11).NotNullable()
                .WithColumn("Rg").AsString(20).NotNullable()
                .WithColumn("NumeroProtocolo").AsString(50).NotNullable()
                .WithColumn("NumeroVia").AsInt32().NotNullable()
                .WithColumn("NomeMae").AsString(255).Nullable()
                .WithColumn("NomePai").AsString(255).Nullable()
                .WithColumn("Foto").AsBinary(int.MaxValue).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Protocolo");
        }
    }
}
