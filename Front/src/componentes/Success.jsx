import Navbar from './Navbar';
import Footer from './Footer';
import Login from './Login';
import '../css/TiendaCss.css';
import { Link } from 'react-router-dom';
import { isExpired } from 'react-jwt';

function Success() {
  var token = sessionStorage.getItem('realToken');
  const isMyTokenExpired = isExpired(token);

  //Si hay un token almacenado y no esta caducado se muestra la confirmacion
  if (token !== null && isMyTokenExpired === false) {
    return (
      <div className="App">
        <Navbar />
        <div className='container'>
          <img src="/img/check3.png" alt="check" className='mt-5' />
          <h3>El pago se ha llevado a cabo correctamente</h3>
          <p>Su pedido llegará a su domicilio en 362 días laborales.</p>
          <Link to='/tienda' className='btn fondoAzul'>Seguir comprando</Link>

          <Footer />
        </div>
      </div>

    );
  }
  else {//En caso de que no, se devuelve a la pagina de inicio de sesion
    return (<Login />);
  }
}

export default Success;
