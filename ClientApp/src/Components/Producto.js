import React from 'react'
import "bootstrap/dist/css/bootstrap.min.css"; //recurso bootstrap
import { useEffect, useState } from "react";

const Producto = () => {


    const [products, setProducts] = useState([]);
    const [nameProduct, setNameProduct] = useState("");
    const [description, setDescription] = useState("");
    const [stocks, setStocks] = useState("");
    const [image, setImage] = useState(null);
  
    const mostrarProductos = async () => {
      const response = await fetch("api/product/Lista");
  
      if (response.ok) {
        const data = await response.json();
        setProducts(data);
      } else {
        console.log("status code " + response.status);
      }
    };
  
    //metodo para convertir el formato de fecha
    const formatDate = (string) => {
      let options = { year: "numeric", month: "long", day: "numeric" };
      let fecha = new Date(string).toLocaleDateString("es-PE", options);
      let hora = new Date(string).toLocaleTimeString();
      return fecha + " | " + hora;
    };
  
    useEffect(() => {
      mostrarProductos();
    }, []);
  
    const guardarEvento = async (e) => {
      e.preventDefault();
      const formData = new FormData();
      formData.append("nameProduct", nameProduct);
      formData.append("description", description);
      formData.append("stocks", stocks);
      formData.append("image", image);
  
      try {
        const response = await fetch("api/product/Guardar", {
          method: "POST",
          body: formData,
        });
        console.log(response);
        if (response.ok) {
          setNameProduct("");
          setDescription("");
          setStocks("");
          setImage("");
          await mostrarProductos();
        }
      } catch (error) {
        console.log(error);
      }
    };
  
    const BorrarProducto = async (id) => {
      try {
        const response = await fetch("api/product/Borrar/" + id, {
          method: "DELETE",
        });
        console.log(response);
        if (response.ok) {
          await mostrarProductos();
        }
      } catch (error) {
        console.log(error);
      }
    };
  
  return (
    
      <div>
        <div className="container bg-dark p-4 vh-auto">
          <h2 className="text-white">PRODUCTOS </h2>
          <div className="container">
            <div className="row">
              <form onSubmit={guardarEvento} className="form-inline">
                <div className="col form-group">
                  <label className="text-white">
                    Name:
                    <input
                      readonly
                      className="form-control"
                      type="text"
                      value={nameProduct}
                      onChange={(e) => setNameProduct(e.target.value)}
                    />
                  </label>
                </div>
                <div className="col form-group">
                  <label className="text-white">
                    Descripcion:
                    <input
                      className="form-control"
                      type="text"
                      value={description}
                      onChange={(e) => setDescription(e.target.value)}
                    />
                  </label>
                </div>
                <div className="col form-group">
                  <label className="text-white">
                    unidades:
                    <input
                      className="form-control"
                      type="number"
                      value={stocks}
                      onChange={(e) => setStocks(e.target.value)}
                    />
                  </label>
                </div>
                <div className="col form-group">
                  <label className="text-white">
                    Image:
                    <input
                      className="form-control"
                      type="file"
                      accept="image/*"
                      onChange={(e) => setImage(e.target.files[0])}
                    />
                  </label>
                </div>
                <br></br>

                <div className="col form-groupl">
                  <button className="btn btn-success" type="submit">
                    Registrar
                  </button>
                </div>
              </form>
            </div>
          </div>

          <div>
            <div className="row mt-4">
              <div className="col-sm-12">
                <div className="list-group">
                  {products.map((item) => (
                    <div
                      key={item.idProduct}
                      className="list-group-item list-group-item-action"
                    >
                      <center>
                        <h5 className="text-primary">{item.nameProduct}</h5>
                      </center>
                      <h5 className="text-secundary">{item.description}</h5>
                      <h5 className="text-info">{item.stocks} </h5>
                      <h5 className="text-dark">{item.imagePath} </h5>

                      <div className="d-flex justify-content-between">
                        <small className="text-dark">
                          {formatDate(item.dateAdmission)}{" "}
                        </small>
                        <button
                          onClick={() => BorrarProducto(item.idProduct)}
                          className="btn btn-sm btn-outline-danger"
                        >
                          cerrar
                        </button>
                      </div>
                    </div>
                  ))}
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    
  );
};
  
export default Producto
