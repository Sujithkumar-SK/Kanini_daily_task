import React, { useState, useEffect } from 'react';
import { useAuth } from '../context/authContext';
import { productService } from '../Services/productService';
import type { Product, ProductCreateRequest, Category, ProductImage } from '../types/product';

const ProductManagement: React.FC = () => {
  const { user } = useAuth();
  const [products, setProducts] = useState<Product[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(true);
  const [showModal, setShowModal] = useState(false);
  const [showImageModal, setShowImageModal] = useState(false);
  const [selectedProductId, setSelectedProductId] = useState<number | null>(null);
  const [editingProduct, setEditingProduct] = useState<Product | null>(null);
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');
  const [selectedFiles, setSelectedFiles] = useState<File[]>([]);
  const [uploading, setUploading] = useState(false);

  const [formData, setFormData] = useState<ProductCreateRequest>({
    name: '',
    description: '',
    sku: '',
    price: 0,
    discountPrice: 0,
    stockQuantity: 0,
    minStockLevel: 0,
    brand: '',
    weight: '',
    dimensions: '',
    vendorId: user?.userId || 0,
    categoryId: 0
  });

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      setLoading(true);
      const [productsData, categoriesData] = await Promise.all([
        productService.getProductsByVendor(user?.userId || 0),
        productService.getCategories()
      ]);
      setProducts(productsData);
      setCategories(categoriesData);
    } catch (err: any) {
      setError('Failed to load data');
    } finally {
      setLoading(false);
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      if (editingProduct) {
        const updated = await productService.updateProduct(editingProduct.productId, formData);
        setProducts(products.map(p => p.productId === updated.productId ? updated : p));
        setSuccess('Product updated successfully');
      } else {
        const newProduct = await productService.createProduct(formData);
        setProducts([...products, newProduct]);
        setSuccess('Product created successfully');
      }
      handleCloseModal();
    } catch (err: any) {
      setError(err.response?.data?.description || 'Operation failed');
    }
  };

  const handleDelete = async (id: number) => {
    if (window.confirm('Are you sure you want to delete this product?')) {
      try {
        await productService.deleteProduct(id);
        setProducts(products.filter(p => p.productId !== id));
        setSuccess('Product deleted successfully');
      } catch (err: any) {
        setError('Failed to delete product');
      }
    }
  };

  const handleStatusToggle = async (product: Product) => {
    try {
      const newStatus = product.status === 'Active' ? 'Inactive' : 'Active';
      await productService.updateProductStatus(product.productId, newStatus);
      setProducts(products.map(p => 
        p.productId === product.productId ? { ...p, status: newStatus } : p
      ));
      setSuccess(`Product ${newStatus.toLowerCase()} successfully`);
    } catch (err: any) {
      setError('Failed to update product status');
    }
  };

  const handleOpenModal = (product?: Product) => {
    if (product) {
      setEditingProduct(product);
      setFormData({
        name: product.name,
        description: product.description || '',
        sku: product.sku,
        price: product.price,
        discountPrice: product.discountPrice || 0,
        stockQuantity: product.stockQuantity,
        minStockLevel: product.minStockLevel || 0,
        brand: product.brand || '',
        weight: product.weight || '',
        dimensions: product.dimensions || '',
        vendorId: product.vendorId,
        categoryId: product.categoryId
      });
    } else {
      setEditingProduct(null);
      setFormData({
        name: '',
        description: '',
        sku: '',
        price: 0,
        discountPrice: 0,
        stockQuantity: 0,
        minStockLevel: 0,
        brand: '',
        weight: '',
        dimensions: '',
        vendorId: user?.userId || 0,
        categoryId: 0
      });
    }
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
    setEditingProduct(null);
    setError('');
  };

  const handleImageUpload = async () => {
    if (!selectedProductId || selectedFiles.length === 0) return;
    
    try {
      setUploading(true);
      await productService.uploadProductImages(selectedProductId, selectedFiles);
      setSuccess('Images uploaded successfully');
      setSelectedFiles([]);
      setShowImageModal(false);
      loadData(); // Reload to get updated product with images
    } catch (err: any) {
      setError('Failed to upload images');
    } finally {
      setUploading(false);
    }
  };

  const handleFileSelect = (e: React.ChangeEvent<HTMLInputElement>) => {
    const files = Array.from(e.target.files || []);
    const validFiles = files.filter(file => {
      const isValidType = ['image/jpeg', 'image/jpg', 'image/png', 'image/gif'].includes(file.type);
      const isValidSize = file.size <= 5 * 1024 * 1024;
      return isValidType && isValidSize;
    });
    setSelectedFiles(validFiles);
  };

  if (loading) {
    return (
      <div className="container mt-4">
        <div className="d-flex justify-content-center">
          <div className="spinner-border" role="status">
            <span className="visually-hidden">Loading...</span>
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="container mt-4">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h2>Product Management</h2>
        <button className="btn btn-primary" onClick={() => handleOpenModal()}>
          <i className="bi bi-plus-circle me-2"></i>Add Product
        </button>
      </div>

      {error && (
        <div className="alert alert-danger alert-dismissible fade show" role="alert">
          {error}
          <button type="button" className="btn-close" onClick={() => setError('')}></button>
        </div>
      )}

      {success && (
        <div className="alert alert-success alert-dismissible fade show" role="alert">
          {success}
          <button type="button" className="btn-close" onClick={() => setSuccess('')}></button>
        </div>
      )}

      <div className="row">
        {products.map((product) => (
          <div key={product.productId} className="col-md-6 col-lg-4 mb-4">
            <div className="card h-100">
              {/* Image Carousel */}
              {product.imagePaths && product.imagePaths.length > 0 ? (
                <div id={`carousel-${product.productId}`} className="carousel slide" data-bs-ride="carousel">
                  <div className="carousel-inner">
                    {product.imagePaths.map((imagePath, index) => (
                      <div key={index} className={`carousel-item ${index === 0 ? 'active' : ''}`}>
                        <img
                          src={`http://localhost:5108${imagePath}`}
                          className="d-block w-100"
                          alt={product.name}
                          style={{ height: '200px', objectFit: 'cover' }}
                          onError={(e) => {
                            const target = e.target as HTMLImageElement;
                            target.src = 'data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iMzAwIiBoZWlnaHQ9IjIwMCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48cmVjdCB3aWR0aD0iMTAwJSIgaGVpZ2h0PSIxMDAlIiBmaWxsPSIjZGRkIi8+PHRleHQgeD0iNTAlIiB5PSI1MCUiIGZvbnQtc2l6ZT0iMTgiIHRleHQtYW5jaG9yPSJtaWRkbGUiIGR5PSIuM2VtIj5ObyBJbWFnZTwvdGV4dD48L3N2Zz4=';
                          }}
                        />
                      </div>
                    ))}
                  </div>
                  {product.imagePaths.length > 1 && (
                    <>
                      <button className="carousel-control-prev" type="button" data-bs-target={`#carousel-${product.productId}`} data-bs-slide="prev">
                        <span className="carousel-control-prev-icon"></span>
                      </button>
                      <button className="carousel-control-next" type="button" data-bs-target={`#carousel-${product.productId}`} data-bs-slide="next">
                        <span className="carousel-control-next-icon"></span>
                      </button>
                    </>
                  )}
                </div>
              ) : (
                <div className="bg-light d-flex align-items-center justify-content-center" style={{ height: '200px' }}>
                  <i className="bi bi-image fs-1 text-muted"></i>
                </div>
              )}

              <div className="card-body">
                <h5 className="card-title">{product.name}</h5>
                <p className="card-text text-muted">SKU: {product.sku}</p>
                <p className="card-text">Category: {product.categoryName}</p>
                <div className="d-flex justify-content-between align-items-center">
                  <div>
                    <span className="h5 text-primary">₹{product.price}</span>
                    {product.discountPrice && (
                      <span className="text-muted text-decoration-line-through ms-2">₹{product.discountPrice}</span>
                    )}
                  </div>
                  <span className={`badge ${product.status === 'Active' ? 'bg-success' : 'bg-secondary'}`}>
                    {product.status}
                  </span>
                </div>
                <p className="card-text mt-2">Stock: {product.stockQuantity}</p>
              </div>

              <div className="card-footer">
                <div className="btn-group w-100" role="group">
                  <button className="btn btn-outline-primary btn-sm" onClick={() => handleOpenModal(product)}>
                    <i className="bi bi-pencil"></i>
                  </button>
                  <button className="btn btn-outline-info btn-sm" onClick={() => {
                    setSelectedProductId(product.productId);
                    setShowImageModal(true);
                  }}>
                    <i className="bi bi-image"></i>
                  </button>
                  <button className="btn btn-outline-warning btn-sm" onClick={() => handleStatusToggle(product)}>
                    <i className={`bi ${product.status === 'Active' ? 'bi-eye-slash' : 'bi-eye'}`}></i>
                  </button>
                  <button className="btn btn-outline-danger btn-sm" onClick={() => handleDelete(product.productId)}>
                    <i className="bi bi-trash"></i>
                  </button>
                </div>
              </div>
            </div>
          </div>
        ))}
      </div>

      {/* Product Form Modal */}
      <div className={`modal fade ${showModal ? 'show' : ''}`} style={{ display: showModal ? 'block' : 'none' }} tabIndex={-1}>
        <div className="modal-dialog modal-lg">
          <div className="modal-content">
            <div className="modal-header">
              <h5 className="modal-title">{editingProduct ? 'Edit Product' : 'Add Product'}</h5>
              <button type="button" className="btn-close" onClick={handleCloseModal}></button>
            </div>
            <form onSubmit={handleSubmit}>
              <div className="modal-body">
                <div className="row">
                  <div className="col-md-6 mb-3">
                    <label className="form-label">Product Name *</label>
                    <input
                      type="text"
                      className="form-control"
                      value={formData.name}
                      onChange={(e) => setFormData({...formData, name: e.target.value})}
                      required
                    />
                  </div>
                  <div className="col-md-6 mb-3">
                    <label className="form-label">SKU *</label>
                    <input
                      type="text"
                      className="form-control"
                      value={formData.sku}
                      onChange={(e) => setFormData({...formData, sku: e.target.value})}
                      required
                    />
                  </div>
                  <div className="col-12 mb-3">
                    <label className="form-label">Description</label>
                    <textarea
                      className="form-control"
                      rows={3}
                      value={formData.description}
                      onChange={(e) => setFormData({...formData, description: e.target.value})}
                    />
                  </div>
                  <div className="col-md-6 mb-3">
                    <label className="form-label">Category *</label>
                    <select
                      className="form-select"
                      value={formData.categoryId}
                      onChange={(e) => setFormData({...formData, categoryId: parseInt(e.target.value)})}
                      required
                    >
                      <option value={0}>Select Category</option>
                      {categories.map((category) => (
                        <option key={category.categoryId} value={category.categoryId}>
                          {category.name}
                        </option>
                      ))}
                    </select>
                  </div>
                  <div className="col-md-6 mb-3">
                    <label className="form-label">Brand</label>
                    <input
                      type="text"
                      className="form-control"
                      value={formData.brand}
                      onChange={(e) => setFormData({...formData, brand: e.target.value})}
                    />
                  </div>
                  <div className="col-md-4 mb-3">
                    <label className="form-label">Price *</label>
                    <input
                      type="number"
                      className="form-control"
                      value={formData.price}
                      onChange={(e) => setFormData({...formData, price: parseFloat(e.target.value)})}
                      required
                    />
                  </div>
                  <div className="col-md-4 mb-3">
                    <label className="form-label">Discount Price</label>
                    <input
                      type="number"
                      className="form-control"
                      value={formData.discountPrice}
                      onChange={(e) => setFormData({...formData, discountPrice: parseFloat(e.target.value)})}
                    />
                  </div>
                  <div className="col-md-4 mb-3">
                    <label className="form-label">Stock Quantity *</label>
                    <input
                      type="number"
                      className="form-control"
                      value={formData.stockQuantity}
                      onChange={(e) => setFormData({...formData, stockQuantity: parseInt(e.target.value)})}
                      required
                    />
                  </div>
                </div>
              </div>
              <div className="modal-footer">
                <button type="button" className="btn btn-secondary" onClick={handleCloseModal}>Cancel</button>
                <button type="submit" className="btn btn-primary">
                  {editingProduct ? 'Update' : 'Create'}
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>

      {/* Image Upload Modal */}
      <div className={`modal fade ${showImageModal ? 'show' : ''}`} style={{ display: showImageModal ? 'block' : 'none' }} tabIndex={-1}>
        <div className="modal-dialog">
          <div className="modal-content">
            <div className="modal-header">
              <h5 className="modal-title">Upload Product Images</h5>
              <button type="button" className="btn-close" onClick={() => setShowImageModal(false)}></button>
            </div>
            <div className="modal-body">
              <div className="mb-3">
                <label className="form-label">Select Images (JPG, PNG, GIF - Max 5MB each)</label>
                <input
                  type="file"
                  className="form-control"
                  multiple
                  accept="image/*"
                  onChange={handleFileSelect}
                />
              </div>
              {selectedFiles.length > 0 && (
                <div className="row">
                  {selectedFiles.map((file, index) => (
                    <div key={index} className="col-4 mb-2">
                      <img
                        src={URL.createObjectURL(file)}
                        alt={`Preview ${index + 1}`}
                        className="img-thumbnail"
                        style={{ width: '100%', height: '80px', objectFit: 'cover' }}
                      />
                    </div>
                  ))}
                </div>
              )}
            </div>
            <div className="modal-footer">
              <button type="button" className="btn btn-secondary" onClick={() => setShowImageModal(false)}>Cancel</button>
              <button 
                type="button" 
                className="btn btn-primary" 
                onClick={handleImageUpload}
                disabled={uploading || selectedFiles.length === 0}
              >
                {uploading ? (
                  <>
                    <span className="spinner-border spinner-border-sm me-2"></span>
                    Uploading...
                  </>
                ) : (
                  'Upload Images'
                )}
              </button>
            </div>
          </div>
        </div>
      </div>

      {showModal && <div className="modal-backdrop fade show"></div>}
      {showImageModal && <div className="modal-backdrop fade show"></div>}
    </div>
  );
};

export default ProductManagement;