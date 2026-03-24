
using MauiAppMinhasCompras.Models;
using SQLite;
namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn; 
        public SQLiteDatabaseHelper(string path)  //construtor da classe
        {
            _conn = new SQLiteAsyncConnection(path);

            _conn.CreateTableAsync<Produto>().Wait();
        }
        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }
        public Task<List<Produto>> Update(Produto p) //atualiza um produto existente
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?,Categoria=?, Preco=? WHERE Id=?";
            return _conn.QueryAsync<Produto>(
            sql, p.Descricao, p.Quantidade, p.Categoria ,p.Preco, p.Id
            );
        }
        public Task<int> Delete(int id) //deleta produto
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }
        public Task<List<Produto>> GetAll() //busca todos os produtos da lista
        {
            return _conn.Table<Produto>().ToListAsync();
        }
        public Task<List<Produto>> Search(string q) //pesquisar um produto
        {
            string sql = "SELECT * FROM  Produto WHERE descricao LIKE '%" + q + "%'";
            return _conn.QueryAsync<Produto>(sql);
        }
        public Task<List<Produto>> SearchByCategoria(string categoria)
        {
            string sql = "SELECT * FROM Produto WHERE Categoria = ?";
            return _conn.QueryAsync<Produto>(sql, categoria);
        }
    }
}
