import '../css/TiendaCss.css';
import Navbar from './Navbar';
import Footer from './Footer';
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import ProductCardCarrito from './ProductCardCarrito'
import { isExpired, decodeToken } from "react-jwt";
import Login from './Login'

function Carrito() {
    //const [Solicitudes, setSolicitudes] = useState([]);
    const [Productos, setProductos] = useState([]);
    var token = sessionStorage.getItem('realToken');
    const myDecodedToken = decodeToken(token);
    const isMyTokenExpired = isExpired(token);

    const navigate = useNavigate();

    useEffect(() => {
        if (token != null && isMyTokenExpired === false) {
            //Funcion que trae los productos que tiene en el carrito
            const fecthProducts = async () => {
                const response = await fetch('https://localhost:7279/api/Productos/ProductosCarrito_Checkout/' + myDecodedToken.id_cliente + '/1',
                    {
                        headers: { "Authorization": `Bearer ${token}` }
                    });

                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }

                const respuesta = await response.json();
                if (respuesta) {
                    setProductos(respuesta);//Guardar los productos
                }
            };
            fecthProducts();
        }

    }, []);

    async function nextPago(e) {
        e.preventDefault();
        if (token != null && isMyTokenExpired === false) {
            //Funcion que pasa las solicitudes de los productos al estado de checkout
            const response = await fetch(
                'https://localhost:7279/api/Solicitudes/NextPago/' + myDecodedToken.id_cliente,
                {
                    headers: { "Authorization": `Bearer ${token}` }
                });

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            navigate('/checkout');//Manda a la pagina de checkout
        }
        else {
            sessionStorage.removeItem('realToken');
            navigate('/login');
        }

    }
    //Se muestra si hay un token almacenado y no esta caducado
    if (token != null && isMyTokenExpired === false) {
        return (
            <div className="App">
                <Navbar />
                <div className='container pt-3'>

                    {/* Si no hay productos dice que no se han agregado */Productos.length < 1 ? <h2>Aún no has agregado artículos a tu compra.</h2> : <h2>Artículos agregados a tu compra.</h2>}
                    <ul className='productoGrid'>
                        {Productos.map((producto, i) => (
                            <ProductCardCarrito key={i} producto={producto} />
                        ))}
                    </ul>

                    <button onClick={nextPago} className="btn fondoAzul me-1">PASAR ARTICULOS AL PROCESO DE PAGO</button>


                    <Footer />
                </div>
            </div>
        );
    }
    else {//Manda a la pagina de login
       return(<Login />);
    }

}

export default Carrito;