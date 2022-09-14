import '../css/TiendaCss.css'
import Swal from 'sweetalert2'
import { isExpired, decodeToken } from "react-jwt";
import { useNavigate } from 'react-router-dom';

function ProductCard({ producto }) {
    const navigate = useNavigate();
    var token = sessionStorage.getItem('realToken');
    const myDecodedToken = decodeToken(token);
    const isMyTokenExpired = isExpired(token);

    async function agregarSolicitud(e) {
        e.preventDefault();
        if (token != null && isMyTokenExpired === false) {
            //Funcion que crea una solicitud al cliente con el id del producto seleccionado
            const response = await fetch(
                'https://localhost:7279/api/Solicitudes/AgregarSolicitud',
                {
                    method: 'POST',
                    headers: {
                        "Authorization": `Bearer ${token}`,
                        'Content-Type': 'application/json'
                    },

                    body: JSON.stringify({
                        id_estado: 1,
                        id_cliente: myDecodedToken.id_cliente,
                        id_producto: producto.id
                    })   
                }
            );

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            if (response.ok) {
                Swal.fire(
                    'Agregado al carrito!',
                    producto.nombre,
                    'success'
                );
                //Luego de 1 segundo de mostrada la alerta se refresca la pagina
                setTimeout(() => {
                    window.location.reload();
                }, 1000);
            }
        }
        else {
            sessionStorage.removeItem('realToken');
            navigate('/login');
        }
    }


    return (
        <li className='productCard'>
            <div className="card m-2" style={{ width: "18rem" }}>
                <img src={producto.imagenUrl} className="card-img-top mt-1" width={200} height={250} alt={producto.nombre} />
                <div className="card-body">

                    <h5 className="card-title">{producto.nombre}</h5>
                    <h4>Precio: <span className='txtAzul'>${producto.precio}</span></h4>
                    <button key={producto.id} onClick={agregarSolicitud} className="btn fondoAzul">Añadir al carro</button>

                </div>
            </div>
        </li>

    );
}

export default ProductCard;