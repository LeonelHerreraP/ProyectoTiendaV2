import '../css/CheckoutCss.css';
import Navbar from './Navbar';
import Footer from './Footer';
import Login from './Login';
import { useEffect, useState, useRef } from 'react';
import { Link } from 'react-router-dom';
import ProductCardCheckout from './ProductCardCheckout';
import Swal from 'sweetalert2';
import { isExpired, decodeToken } from "react-jwt";

function Checkout() {
    const [Productos, setProductos] = useState([]); //Productos con estado checkout
    const [Monto, setMonto] = useState(0); //Total de a pagar

    var token = sessionStorage.getItem('realToken');
    const myDecodedToken = decodeToken(token);
    const isMyTokenExpired = isExpired(token);

    // Inputs de compra
    const inputNombre = useRef("");
    const inputTarjeta = useRef("");
    const inputFecha = useRef("");
    const inputCVC = useRef("");

    useEffect(() => {
        if (token != null && isMyTokenExpired === false) {
            //Funcion que trae los productos que tiene en checkout
            const fecthProducts = async () => {
                const response = await fetch('https://localhost:7279/api/Productos/ProductosCarrito_Checkout/' + myDecodedToken.id_cliente + '/2',
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

            //Funcion para conseguir el monto a pagar
            const fetchMonto = async () => {
                const response2 = await fetch('https://localhost:7279/api/Productos/ValorAPagar/' + myDecodedToken.id_cliente,
                    {
                        headers: { "Authorization": `Bearer ${token}` }
                    });
                if (!response2.ok) {
                    throw new Error(`HTTP error! status: ${response2.status}`);
                }
                const respuesta2 = await response2.json();
                setMonto(respuesta2);
            };
            fetchMonto();
        }


    }, []);

    async function PagoProductos(e) {
        e.preventDefault();

        if (token != null && isMyTokenExpired === false) {
            if (inputNombre.current.value !== "" && inputTarjeta.current.value !== "" && inputFecha.current.value !== "" && inputCVC.current.value !== "") {
                //Funcion que coloca en estado de pago los productos que estan en el checkout
                const response = await fetch(
                    'https://localhost:7279/api/Solicitudes/PagoProductos/' + myDecodedToken.id_cliente,
                    {
                        headers: { "Authorization": `Bearer ${token}` }
                    });

                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);

                }

                window.location.href = 'http://localhost:3000/success';
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Debe llenar todos los campos para continuar con el pago!'
                });
            }
        }
    }
    if (token != null && isMyTokenExpired === false) {
        if (Productos.length < 1) {
            return (

                <div className="App">
                    <Navbar />
                    <div className='container'>
                        <h2 className='mt-3'>Aún no tiene artículos listos para comprar.</h2>
                        <p>Para completar el proceso de compra, primero debes seleccionar los productos.</p>

                        <Link to='/tienda' className='btn btn-warning'>Volver</Link>
                        <Footer />
                    </div>
                </div>

            );
        } else {
            return (
                <div className="App">
                    <Navbar />
                    <div className='container'>
                        <h2 className='mt-3'>¡Artículos casi suyos!</h2>
                        <ul className='list-group '>
                            {Productos.map((producto, i) => (
                                <ProductCardCheckout key={i} producto={producto} />

                            ))}
                            <li className='list-group-item totalPagar'><b>Total a pagar: ${Monto}</b></li>
                        </ul>


                        <form className="row g-3 mb-3 mt-3">
                            <div className="col-12">
                                <label className="form-label">Nombre</label>
                                <input type="text" ref={inputNombre} className="form-control" id="inputAddress2" placeholder="Peter Parker" />
                            </div>
                            <div className="col-md-6">
                                <label className="form-label">Número de la tarjeta</label>
                                <input type="text" ref={inputTarjeta} className="form-control" id="inputCity" placeholder="789123456" maxLength="16" data-role="input, input-mask" data-mask="**** **** **** ****" />
                                
                            </div>
                            <div className="col-md-4">
                                <label className="form-label">Fecha de expiración</label>
                                <input type="month" ref={inputFecha} className="form-control" name="start" min="2022-08"></input>
                            </div>
                            <div className="col-md-2">
                                <label className="form-label">CVC</label>
                                <input type="text" ref={inputCVC} className="form-control" id="inputZip" placeholder="147" />
                            </div>

                        </form>
                        <Link to='/carrito' className='btn btn-danger me-1'>CANCELAR</Link>
                        <button onClick={PagoProductos} className='btn fondoAzul ms-1'>PAGAR AHORA</button>

                        <Footer />
                    </div>
                </div>
            );
        }
    }
    else {
        return (<Login />);
    }


}

export default Checkout;