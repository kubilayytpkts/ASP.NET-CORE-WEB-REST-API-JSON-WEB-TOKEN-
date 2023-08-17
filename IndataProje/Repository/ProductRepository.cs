using IndataProje.Models;
using IndataProje.Repository.Abstract;

namespace IndataProje.Repository
{
    public class ProductRepository:IProductRepository
    {
        private List<Products> _products = new List<Products>()
        {
            new Products{ID=1, Name="cardigan",Price=100},
            new Products{ID=2,Name="scarf",Price=88},
            new Products{ID=3,Name="trousers",Price=110,}
        };


        public List<Products> Update(Products product)
        {
            var productfound = _products.FirstOrDefault(m => m.ID == product.ID);
            productfound.Name = product.Name;
            productfound.Price = product.Price;
            return _products;

        }

        public List<Products> Delete(int productId)
        {
            var productfound = _products.FirstOrDefault(m => m.ID == productId);
            _products.Remove(productfound);
            return _products;
        }

        public Products GetById(int productId)
        {
            var productFound = _products.FirstOrDefault(m => m.ID == productId);
            return productFound;
        }

        public List<Products> GetAll()
        {
            return _products.ToList();
        }

        public List<Products> Create(Products product)
        {
            _products.Add(product);
            return _products;
        }
    }

}
