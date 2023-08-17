using IndataProje.Models;

namespace IndataProje.Repository.Abstract
{
    public interface IProductRepository
    {
        public List<Products> Create(Products product);
        public List<Products> Update(Products product);
        public List<Products> Delete(int productId);
        public Products GetById(int productId);
        public List<Products> GetAll();
    }
}
