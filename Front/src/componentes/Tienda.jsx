import '../App.css';
import { useEffect, useState } from 'react';
import Footer from './Footer';
import ProductCard from './ProductCard';
import '../css/TiendaCss.css'
import { Link, useNavigate } from 'react-router-dom'
import { isExpired, decodeToken } from "react-jwt";

function Tienda() {
  const [Productos, setProductos] = useState([]); //Los productos de la tienda
  const [CantidadCarrito, setCantidadCarrito] = useState(0); //Cantidad de items que hay en el carrito
  const navigate = useNavigate();
  var token = sessionStorage.getItem('realToken'); //Obtener el token
  const myDecodedToken = decodeToken(token); //Desencriptar el token
  const isMyTokenExpired = isExpired(token); //Comprobar si está caducado

  useEffect(() => {

    const fecthProducts = async () => {
      //Funcion que trae todos los productos de la tienda
      const response = await fetch('https://localhost:7279/api/Productos/Listar',
        {
          headers: { "Authorization": `Bearer ${token}` }
        });

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }

      const respuesta = await response.json();
      if (respuesta) {
        setProductos(respuesta); //Se guardan los productos
      }
    };

    if (token != null && isMyTokenExpired === false) {
      //Funcion que trae los productos que tiene en el carrito
      const fecthProducts2 = async () => {
        const response = await fetch('https://localhost:7279/api/Productos/ProductosCarrito_Checkout/' + myDecodedToken.id_cliente + '/1',
          {
            headers: { "Authorization": `Bearer ${token}` }
          });

        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }

        const respuesta = await response.json();
        if (respuesta) {

          setCantidadCarrito(respuesta.length); //Guarda la cantidad de productos
        }
      };
      fecthProducts2();
    }

    fecthProducts();
  }, []);

  //Funcion para cerrar sesion y que regresa a la pagina de inicio de sesion
  function logout() {
    sessionStorage.removeItem('realToken');
    navigate('/login');

  }

  return (
    <div className="App">
      <nav className="navbar navbar-expand-lg barra">
        <div className="container-fluid">
          <img className="" src="/img/pixlr-bg-result.png" alt="" width="auto" height="42" />
          <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
            <span className="navbar-toggler-icon"></span>
          </button>
          <div className="collapse navbar-collapse" id="navbarSupportedContent">
            <ul className="navbar-nav me-auto mb-2 mb-lg-0">
              <li className="nav-item">
                <Link className="nav-link letrasBarra" to='/'>Inicio</Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link letrasBarra" to='/tienda'>Tienda</Link>
              </li>
              {sessionStorage.getItem('realToken') &&
                <li className="nav-item">
                  <Link className="nav-link letrasBarra position-relative" to='/carrito'>
                    <span className="material-symbols-outlined">
                      shopping_cart
                    </span>
                    {CantidadCarrito !== 0 && <span className="position-absolute top-1 start-100 translate-middle badge rounded-pill badgeCount">
                      {CantidadCarrito}
                    </span>}

                  </Link>
                </li>
              }
            </ul>
            <form className="d-flex" role="search">
              {sessionStorage.getItem('realToken') ?
                <button onClick={logout} className="btn btnLogin letrasBarra"><span className="material-symbols-outlined">
                  logout
                </span></button> :
                <Link to='/login' className="btn btn-outline-warning letrasBarra">Login</Link>
              }
            </form>
          </div>
        </div>
      </nav>
      <div className='container'>
        <ul className='productoGrid'>
          {Productos.map((producto) => (
            <ProductCard key={producto.id} producto={producto} />
          ))}
        </ul>

        <Footer />
      </div>
    </div>

  );
}

export default Tienda;
