import '../App.css';
import '../css/LoginCss.css';
import { React, useRef } from 'react';
import Navbar from './Navbar';
import Swal from 'sweetalert2';
import { useNavigate } from 'react-router-dom';


function Login() {
  //Inputs
  const email = useRef("");
  const contraseña = useRef("");
  //Para cambiar de pagina
  const navigate = useNavigate();

  async function Logearse(e) {
    e.preventDefault();
    if (email.current.value !== "" && contraseña.current.value !== "") {
      const response = await fetch(
        'https://localhost:7279/api/Token/Login',
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify({
            email: email.current.value,
            contraseña: contraseña.current.value,
          })
        }
      );

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }

      const respuesta = await response.json();

      //Si el login no devuelve falso, devuelve el token como un array de caracteres
      if (respuesta !== false) {
        var token = respuesta.join('');// Se une el array en un string
        sessionStorage.setItem('realToken', token);//Guarda el token en el sessionStorage
        navigate('/');//Manda a la pagina principal
      }
      else {
        Swal.fire({
          icon: 'error',
          title: 'Oops...',
          text: 'Datos invalidos!'
        });
      }
    }
    else {
      Swal.fire({
        icon: 'question',
        title: 'Oops...',
        text: 'Campos vacíos!'
      });
    }

  }

  return (
    <div className="App">
      <Navbar />
      <div className="contenedorLogin">
        <div className='divInicio'>
          <form>
            <img className="mb-4" src="/img/pixlr-bg-result.png" alt="" width="auto" height="100" />
            <h1 className="h3 mb-3 fw-normal">Iniciar sesión</h1>

            <div className="form-floating">
              <input ref={email} type="email" className="form-control" id="floatingInput" placeholder="name@example.com" />
              <label htmlFor="floatingInput">Correo electrónico</label>
            </div>
            <div className="form-floating">
              <input ref={contraseña} type="password" className="form-control" id="floatingPassword" placeholder="Password" />
              <label htmlFor="floatingPassword">Contraseña</label>
            </div>

            <button onClick={Logearse} className="w-100 btn btn-lg btn-primary mt-3">Iniciar</button>
            <p className="mt-5 mb-3 text-muted">© 2022</p>
          </form>



        </div>
      </div>
    </div>
  );
}

export default Login;
