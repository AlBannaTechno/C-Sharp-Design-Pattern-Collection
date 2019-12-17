using System;
using System.Collections.Generic;
using static System.Console;

namespace MainProject.SOLID_PRINCIPLES
{
    public class OpenClosed
    {
        public static void Run()
        {
            WriteLine("[[[START _ [SOLID] OpenClosed ]]]");
            var appleProduct = new Product("Apple", Color.Green , Size.Small);
            var houseProduct = new Product("House", Color.Blue , Size.XLarge);
            var carProduct = new Product("Car", Color.Red , Size.Large);
            
            var products = new Product[] {appleProduct, houseProduct, carProduct};

            var productFilter = new ProductsFilter();
            foreach 
            (
                var product in productFilter.
                Filter(products, new AndSpecification<Product>(
                        new ColorSpecification(Color.Blue),
                        new SizeSpecification(Size.XLarge)
                        )
                )
            )
                
            {
                Console.WriteLine($"{product.Name} : IS {product.Color} And {product.Size}");
            }
        }
    }
    
    /*
     * The idea here
     * we have a online store with [products]
     * every product has a [color] & [size]
     * we need to products by color and size without breaking Open Closed princibles
     */

    #region Core Types

    enum Color
    {
        Red,Green,Blue
    }

    enum Size
    {
        Small,Medium,Large,XLarge
    }
    class Product
    {
        public Product(string name, Color color, Size size)
        {
            Name = name;
            Color = color;
            Size = size;
        }

        public string Name { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }
        
    }

    #endregion

    #region Base Interfaces

    interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }
    
    interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> products, ISpecification<T> spec);
    }

    #endregion

    #region Implementations

    class ColorSpecification: ISpecification<Product>
    {
        public ColorSpecification(Color color)
        {
            Color = color;
        }

        public Color Color { get; set; }
        
        public bool IsSatisfied(Product product)
        {
            return product.Color == Color;
        }
    }

    class SizeSpecification: ISpecification<Product>
    {
        public SizeSpecification(Size size)
        {
            Size = size;
        }

        public Size Size { get; set; }
        
        public bool IsSatisfied(Product product)
        {
            return product.Size == Size;
        }
    }
    
    class ProductsFilter: IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> products, ISpecification<Product> spec)
        {
            foreach (var product in products)
            {
                if (spec.IsSatisfied(product))
                {
                    yield return product;
                }
            }
        }
    }

    class AndSpecification<T>: ISpecification<T>
    {
        private ISpecification<T> first, second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            this.first = first;
            this.second = second;
        }

        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }
    
    
    #endregion
}