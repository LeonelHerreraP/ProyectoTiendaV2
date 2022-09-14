import '../css/CheckoutCss.css'
import { isExpired } from 'react-jwt';
import { useNavigate } from 'react-router-dom';

function ProductCardCheckout({ producto }) {
    var token = sessionStorage.getItem('realToken');
    const isMyTokenExpired = isExpired(token);
    const navigate = useNavigate();

    //Funcion que elimina la solicitud del producto
    async function eliminarSolicitud(e) {
        e.preventDefault();

        if (token != null && isMyTokenExpired === false) {
            const response = await fetch(
                'https://localhost:7279/api/Solicitudes/EliminarSolicitud/' + producto.id_solicitud,
                {
                    headers: { "Authorization": `Bearer ${token}` }
                });

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            window.location.reload(); //Refresca la pagina
        }
        else {
            sessionStorage.removeItem('realToken');
            navigate('/login');
        }
    }

    return (
        <li className='list-group-item'>
            {producto.nombre} <span className='txtVerde'>${producto.precio}</span>
            <button className='btnQuitar' onClick={eliminarSolicitud}>🗑️</button>
        </li>
    );
}

export default ProductCardCheckout;