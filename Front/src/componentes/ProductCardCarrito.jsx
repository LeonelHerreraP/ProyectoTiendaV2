import '../css/TiendaCss.css'
import { useNavigate } from 'react-router-dom';
import { isExpired } from 'react-jwt';

function ProductCardCarrito({ producto }) {
    const navigate = useNavigate();
    var token = sessionStorage.getItem('realToken');
    const isMyTokenExpired = isExpired(token);


    //Funcion que elimina la solicitud del producto
    async function eliminarSolicitud(e) {
        e.preventDefault();
        if (token != null && isMyTokenExpired === false) {
            const response = await fetch(
                'https://localhost:7279/api/Solicitudes/EliminarSolicitud/'+ producto.id_solicitud,
                {
                  headers: {"Authorization" : `Bearer ${token}`}
                });
    
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            window.location.reload();//Refresca la pagina
        }
        else{
            sessionStorage.removeItem('realToken');
            navigate('/login');
        }
        
    }


    return (
        <li className='productCard'>
            <div className="card m-2" style={{ width: "18rem"}}>
                <img src={producto.imagenUrl} className="card-img-top" width={200} height={250} alt={producto.nombre} />
                <div className="card-body">

                    <h5 className="card-title">{producto.nombre}</h5>
                    <h4>Precio: <span className='txtAzul'>${producto.precio}</span></h4>
                    <button onClick={eliminarSolicitud} className="btn btn-danger">Eliminar</button>

                </div>
            </div>
        </li>

    );
}

export default ProductCardCarrito;